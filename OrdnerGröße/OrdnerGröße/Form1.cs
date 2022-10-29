using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ordner_;
using Invoker_;

namespace OrdnerGröße
{
    public partial class Form1 : Form
    {
        /* Ordnersuche schafft pro Sekunde etwa 6000 Dateien
         * treeview schafft pro Sekunde etwa 21000 Dateien
         * Topliste schafft pro Sekunde etwa 16645 Dateien
         * Duplikatsuche schafft pro Sekunde etwa 695 Dateien
         * 
         * 
         * 
         * */
        public Form1()
        {
            InitializeComponent();
        }

        private enum Quelle { treeview = 0, top100 = 1, duplikate = 2, selbegröße = 3};
        private const bool ignoriereAlbumArt = true;
        private Thread prüfungsthread;
        private const bool TESTMODUS = false;
        private List<TabPage> tabpages = new List<TabPage>();
        private ClassPrüfung prüfung = new ClassPrüfung();
        private Stopwatch sw = new Stopwatch(); // Es gibt nur eine Stopwatch für alles, wird an ClassPrüfung übergeben
        private bool duplikateWerdenGetestet = false;
        private List<string> ignorierteOrdner = new List<string>();
        private Thread laden;

        private void buttonpfad_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            if (Directory.Exists(folderBrowserDialog1.SelectedPath))
            {
                textBoxpfad.Text = folderBrowserDialog1.SelectedPath;
                buttonstarten.Enabled = true;
            }
        }

        private void Top100InListbox()
        {
            // Top100
            listBoxtop100.Items.Clear();
            SuspendLayout();

            long größe = 0;
            for (int a = 0; a < prüfung.listsorted.Count; a++)
            {
                if (listBoxtop100.Items.Count >= 100) break;

                ClassDatei cd = prüfung.listsorted[a];

                größe += cd.größe;
                AddeListBoxTop100Item(cd);
            }

            ResumeLayout();
            labeltop100größe.Text = ClassDatei.GetGröße(größe);
            labelstatus.Text = "Top100 geprüft nach " + GetStopwatchZeit();
        }

        private void AddeListBoxTop100Item(ClassDatei datei)
        {
            listBoxtop100.Items.Add(datei.pfad + "\t" + ClassDatei.GetGröße(datei.größe));
        }

        private List<ClassDatei> GetNichtIgnorierte(List<ClassDatei> dateien)
        {
            List<ClassDatei> list = new List<ClassDatei>();
            foreach (ClassDatei cd in dateien)
            {
                bool bIgnoriert = false;
                foreach (string ignoriert in ignorierteOrdner)
                {
                    if (cd.pfad.ToLower().Contains(ignoriert.ToLower()))
                    {
                        bIgnoriert = true;
                        break;
                    }
                }

                if (bIgnoriert || (ignoriereAlbumArt && cd.pfad.Contains("AlbumArt"))) continue;
                list.Add(cd);
            }

            return list;
        }

        private List<ClassDatei> MehrfacheDateitypen(List<ClassDatei> dateien)
        {
            List<ClassDatei> mehrfach = new List<ClassDatei>();

            foreach (ClassDatei cd in dateien)
            {
                int count = 0;
                foreach (ClassDatei cd2 in dateien)
                {
                    if (cd2.dateityp == cd.dateityp) count++;
                }
                if (count > 1)
                    mehrfach.Add(cd);
            }

            return mehrfach;
        }

        private void SelbeGrößeInListBox()
        {
            listBoxselbegröße.Items.Clear();
            prüfung.listboxselbeGröße = new List<List<ClassDatei>>();

            SuspendLayout();
            // selbe Größe in Listbox
            foreach (KeyValuePair<double, List<ClassDatei>> pair in prüfung.GetSelbeGröße())
            {
                List<ClassDatei> nicht = GetNichtIgnorierte(pair.Value);
                if (nicht.Count < 2) continue;

                // von selbem Dateityp?
                if (checkBoxselbeDateityp.Checked)
                    nicht = MehrfacheDateitypen(nicht);

                if (nicht.Count < 2) continue;

                listBoxselbegröße.Items.Add(pair.Value.First().pfad);
                prüfung.listboxselbeGröße.Add(nicht);
            }
            ResumeLayout();
            AktualisiereListenLabels();
        }

        private void DuplikateInListbox()
        {
            listBoxduplikate.Items.Clear();
            prüfung.listboxduplikate = new List<ClassMD5>();

            // Duplikate in Listebox
            SuspendLayout();
            foreach (KeyValuePair<double, ClassMD5> pair in prüfung.GetDuplikatenListe())
            {
                List<ClassDatei> nicht = GetNichtIgnorierte(pair.Value.duplikate);

                if (nicht.Count <2) continue;

                listBoxduplikate.Items.Add(pair.Value.duplikate.First().pfad);
                prüfung.listboxduplikate.Add(new ClassMD5(pair.Value.md5, nicht));
            }

            buttongemeinsam.Enabled = (listBoxduplikate.Items.Count != 0);

            ResumeLayout();

            labelstatus.Visible = label2.Visible = true;
            buttonfehler.Visible = (prüfung.ordner.fehlermeldung != "");
            buttongemeinsam.Enabled = (listBoxduplikate.Items.Count > 0);

            AktualisiereListenLabels();
            Invoker.invokeTextSet(labelstatus, "Duplikate geprüft nach " + GetStopwatchZeit());
        }

        private void AktualisiereListenLabels()
        {
            long duplikatespeicher = prüfung.GetListBoxDuplikateSpeicher();
            long selbegrößespeicher = prüfung.GetListBoxSelbeGrößeSpeicher();

            Invoker.invokeTextSet(label3, "Alle " + prüfung.listboxduplikate.Count + " Duplikate nehmen " + ClassDatei.GetGröße(duplikatespeicher) + " ein.");
            Invoker.invokeTextSet(label5, "Alle " + prüfung.listboxselbeGröße.Count + " selber Größe nehmen " + ClassDatei.GetGröße(selbegrößespeicher) + " ein.");
        }

        private string GetStopwatchZeit()
        {
            if (sw.ElapsedMilliseconds < 5000)
                return sw.ElapsedMilliseconds + " ms";
            return sw.ElapsedMilliseconds / 1000 + " sek";
        }

        private bool OrdnerstrukturZeichnen()
        {
            // Zeichne Strukturbaum
            try
            {
                labelstatus.Text = "Strukturbaum wird gezeichnet.";
                treeView1.SuspendLayout();
                treeView1.BeginUpdate();
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(new TreeNode(prüfung.ordner.pfad + " " + prüfung.ordner.GetGröße()));
                AddNodes(prüfung.ordner, treeView1.Nodes[0]);
                treeView1.EndUpdate();
                treeView1.ResumeLayout();
                labelstatus.Text = "Strukturbaum erstellt nach " + GetStopwatchZeit();


                label2.Text = prüfung.ordner.anzahlOrdner + " Ordner und " + prüfung.ordner.anzahlDateien + " Dateien";
                label2.Visible = true;
                labelstatus.Visible = true;
                buttonstarten.Enabled = true;
                checkBoxunterordner.Enabled = true;

                pictureBox1.Enabled = false;
                pictureBox1.Visible = false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if (!duplikateWerdenGetestet) return;

            progressBarselbergröße.Value = progressBarduplikate.Value = prüfung.geprüfteDateienduplikate;
            labelselbegröße.Text = labelduplikate.Text = prüfung.GetDuplikatenInfo();
        }

        private List<int> GetSelectedAufgaben()
        {
            List<int> listSelectedAufgaben = new List<int>();
            for (int a = 0; a < checkedListBoxaufgaben.Items.Count; a++)
            {
                if (checkedListBoxaufgaben.GetItemChecked(a))
                    listSelectedAufgaben.Add(a);
            }
            return listSelectedAufgaben;
        }

        private void Ausgangsdarstellung()
        {
            label2.Visible = tabControl1.Enabled = checkBoxunterordner.Enabled = false;
            pictureBox1.Enabled = pictureBox1.Visible = true;
            progressBartop100.Value = progressBarduplikate.Value = progressBarselbergröße.Value = 0;
            progressBartop100.Visible = progressBarduplikate.Visible = progressBarselbergröße.Visible = true;
            labelduplikate.Visible = labelselbegröße.Visible = labeltop100.Visible = true;
            labelduplikate.Text = labelselbegröße.Text = labeltop100.Text = "00:00 min";
            buttongemeinsam.Enabled = false;
            listBoxduplikate.Items.Clear();
            listBoxselbegröße.Items.Clear();
            listBoxtop100.Items.Clear();
            leereOrdnerLöschenToolStripMenuItem.Enabled = false;
        }

        /*Summary
      * Methode (eigener Thread), der sich darum kümmert, dass die Form aktuell gehalten wird ohne Timer
      * 
      * */
        private void FormControl()
        {
            try
            {
                // Versuche, die Prüfung zu starten
                if (prüfung.Starten())
                {
                    pictureBox1.Invoke((MethodInvoker)(() => pictureBox1.Visible = true));
                    // Warte, bis der erste Thread begonnen hat
                    while (prüfung.threads[0] == null || prüfung.threads[0].ThreadState == System.Threading.ThreadState.Unstarted)
                    {
                        Thread.Sleep(1);
                        labelstatus.Text = "Warten, dass Prüfung startet";
                    }
                }
                else
                {
                    MessageBox.Show("Fehler beim Starten der Prüfung aufgetreten!\n" + prüfung.error);
                    return;
                }

                labelstatus.Invoke((MethodInvoker)(() => labelstatus.Text = "Ordnerstruktur wird geprüft"));

                prüfung.threads[0].Join();

                if (prüfung.ordner == null) return;

                // Jetzt müsste Ordnerstruktur fertig sein
                pictureBox1.Invoke((MethodInvoker)(() => pictureBox1.Visible = false));
                this.Invoke((MethodInvoker)delegate
                {
                    OrdnerstrukturZeichnen();
                });

                // Keine Top100 prüfen
                if (prüfung.threads.Count < 2)
                {
                    labelstatus.Invoke((MethodInvoker)(() => labelstatus.Text = "Fertig nach " + GetStopwatchZeit()));
                    buttonstarten.Invoke((MethodInvoker)(() => buttonstarten.Text = "Starten"));
                    sw.Stop();
                    return;
                }

                prüfung.threads[1].Join();
                // Jetzt müsste Top100 fertig sein
                Invoker.invokeVisible(labeltop100, false);
                Invoker.invokeVisible(progressBartop100, false);
                labeltop100größe.Invoke((MethodInvoker)(() => labeltop100größe.Text = prüfung.GetTop100Größe()));

                this.Invoke((MethodInvoker)delegate
                {
                    Top100InListbox();
                });

                // Keine Duplikate prüfen
                if (prüfung.threads.Count < 3)
                {
                    labelstatus.Invoke((MethodInvoker)(() => labelstatus.Text = "Fertig nach " + GetStopwatchZeit()));
                    buttonstarten.Invoke((MethodInvoker)(() => buttonstarten.Text = "Starten"));
                    sw.Stop();
                    return;
                }

                Invoker.invokeProgressBar(progressBarduplikate, 0, 0, prüfung.ordner.anzahlDateien);
                Invoker.invokeProgressBar(progressBarselbergröße, 0, 0, prüfung.ordner.anzahlDateien);

                duplikateWerdenGetestet = true;
                prüfung.threads[2].Join();

                duplikateWerdenGetestet = false;

                // Jetzt müssten Duplikate fertig sein
                Invoker.invokeVisible(labelselbegröße, false);
                Invoker.invokeVisible(labelduplikate, false);
                Invoker.invokeVisible(progressBarselbergröße, false);
                Invoker.invokeVisible(progressBarduplikate, false);
                this.Invoke((MethodInvoker)delegate
                {
                    DuplikateInListbox();
                    SelbeGrößeInListBox();
                });

                buttonstarten.Invoke((MethodInvoker)(() => buttonstarten.Enabled = true));
                buttonstarten.Invoke((MethodInvoker)(() => buttongemeinsam.Enabled = true));

                labelstatus.Invoke((MethodInvoker)(() => labelstatus.Text = "Fertig nach " + GetStopwatchZeit()));
                Invoker.invokeEnable(leereOrdnerLöschenToolStripMenuItem, true); 
                buttonstarten.Invoke((MethodInvoker)(() => buttonstarten.Text = "Starten"));
                sw.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonstarten_Click(object sender, EventArgs e)
        {
            if (buttonstarten.Text == "Starten")
            {
                buttonstarten.Text = "Abbrechen";
                if (textBoxpfad.Text.Last() != '\\')
                    textBoxpfad.Text += @"\";

                // Ausgangssituation, quasi reset der Form
                Ausgangsdarstellung();

                prüfung = new ClassPrüfung(textBoxpfad.Text, GetSelectedAufgaben(), checkBoxunterordner.Checked, sw);
                sw.Restart();
                prüfungsthread = new Thread(delegate () { FormControl(); });
                prüfungsthread.Start();

                labelstatus.Visible = tabControl1.Enabled = true;
            }
            else
            {
                buttonstarten.Text = "Starten";

                if (!prüfung.Stoppen())
                    MessageBox.Show("Stoppen der Prüfung hat nicht fehlerfrei funktioniert!");
                sw.Stop();

                if (prüfung.listsorted != null && prüfung.listsorted.Count > 0)
                    Top100InListbox();
                if (prüfung.dictduplikate != null && prüfung.dictduplikate.Count > 0)
                {
                    DuplikateInListbox();
                    SelbeGrößeInListBox();
                }

                progressBarduplikate.Visible = progressBarselbergröße.Visible = progressBartop100.Visible = false;
                labelduplikate.Visible = labelselbegröße.Visible = labeltop100.Visible = false;
            }
        }


        private void AddNodes(ClassOrdner o, TreeNode tn)
        {
            int index = 0;
            foreach (ClassOrdner subo in o.ordner.OrderByDescending(i => i.größe))
            {
                tn.Nodes.Add(new TreeNode(subo.name + " " + subo.GetGröße()));
                AddNodes(subo, tn.Nodes[index]);
                index++;
            }
            foreach (ClassDatei datei in o.dateien.OrderByDescending(i => i.größe))
            {
                tn.Nodes.Add(datei.name + " " + datei.GetGröße());
            }
        }


        private void textBoxpfad_TextChanged(object sender, EventArgs e)
        {
            buttonstarten.Enabled = Directory.Exists(textBoxpfad.Text);
            tabControl1.Enabled = false;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                string tempname = ((TreeView)sender).SelectedNode.Text;
                Doppelklick(tempname);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBoxtop100_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxtop100.SelectedIndex != -1)
            {
                string tempname = listBoxtop100.Text;
                Doppelklick(tempname);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelstatus.Visible = label2.Visible = false;
            foreach (TabPage t in tabControl1.TabPages)
                tabpages.Add(t);

            // Aktiviere alle CheckboxItems per Default
            checkBoxunterordner.Checked = true;
            for (int a = 0; a < checkedListBoxaufgaben.Items.Count; a++)
                checkedListBoxaufgaben.SetItemChecked(a, true);

            //---Testen
            // textBoxpfad.Text = @"D:\Studium\WS21\Computational Intelligence";
            // checkBoxunterordner.Checked = true;
            // checkedListBoxaufgaben.SetItemCheckState(0, CheckState.Checked);
            // checkedListBoxaufgaben.SetItemCheckState(1, CheckState.Checked);
            // buttonstarten_Click(sender, e);
            //ladenToolStripMenuItem_Click(sender, e);
            //----


            pictureBox1.Enabled = pictureBox1.Visible = false;

            timer.Start();
            this.ActiveControl = textBoxpfad;
        }

        private void listBoxduplikate_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxduplikate.SelectedIndex == -1) return;

            try
            {
                // Finde MD5 Element
                KeyValuePair<double, ClassMD5> element = prüfung.GetDuplikatElement(listBoxduplikate.SelectedItem.ToString());

                if (element.Equals(new KeyValuePair<double, ClassMD5>()))
                {
                    MessageBox.Show("Element nicht gefunden!");
                    return;
                }

                List<ClassDatei> gültigeDateien = prüfung.GetNichtIgnorierteDateien(ignorierteOrdner, element.Value.duplikate, ignoriereAlbumArt);

                if (gültigeDateien.Count == 0)
                {
                    MessageBox.Show("Bitte auf Aktualisieren klicken, da fälschlicherweise keine Pfade gefunden wurden!");
                    return;
                }

                FormDuplikate fd = new FormDuplikate(listBoxduplikate.Text, gültigeDateien);
                fd.ShowDialog();

                List<ClassDatei> nichtgelöscht = new List<ClassDatei>();

                // nichts zu löschen
                if (fd.löschenliste.Count == 0) return;

                foreach (ClassDatei cd in fd.löschenliste)
                {
                    if (!LöscheDatei(cd, Quelle.duplikate))
                        nichtgelöscht.Add(cd);
                }

                AktualisiereListenLabels();
                // Löschen lief erfolgreich
                if (nichtgelöscht.Count == 0)
                    return;

                string ausgabe = "";
                foreach (ClassDatei nicht in nichtgelöscht)
                {
                    if (ausgabe != "") ausgabe += "\n" + nicht.pfad;
                    else ausgabe = nicht.pfad;
                }
                ausgabe += "\nkonnten nicht gelöscht werden";
                MessageBox.Show(ausgabe);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Doppelklick(string tempname)
        {
            try
            {
                string name = "";
                foreach (char c in tempname)
                {
                    if (c != '\t') name += c;
                    else break;
                }


                if (Directory.Exists(name))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.InitialDirectory = name;
                    ofd.ShowDialog();
                }
                else if (File.Exists(name))
                {
                    Process.Start(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonfehler_Click(object sender, EventArgs e)
        {
            FormFehler ff = new FormFehler(prüfung.ordner.fehlermeldung);
            ff.ShowDialog();
        }

        private string DateinameOhneGröße(string[] split)
        {
            string dateiname = "";
            for (int a = 0; a < split.Length - 2; a++)
            {
                if (dateiname == "")
                    dateiname += split[a];
                else
                    dateiname += " " + split[a];
            }
            return dateiname;
        }

        private bool EntferneDateiAusTop100Listbox(int index)
        {
            try
            {
                if (index >= listBoxtop100.Items.Count) return true;
                listBoxtop100.Items.RemoveAt(index);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool EntferneDateiAusDuplikatenListbox(List<int> listboxIndices)
        {
            return EntferneDateiAusListbox(ref listBoxduplikate, listboxIndices);
        }
        private bool EntferneDateiAusSelbeGrößeListbox(List<int> listboxIndices)
        {
            return EntferneDateiAusListbox(ref listBoxselbegröße, listboxIndices);
        }

        private bool EntferneDateiAusListbox(ref ListBox lb, List<int> listboxIndices)
        {
            try
            {
                for (int a = 0; a < listboxIndices.Count; a++)
                {
                    int index = listboxIndices[a];
                    lb.Items.RemoveAt(index);

                    for (int b = 0; b < listboxIndices.Count; b++)
                    {
                        if (listboxIndices[b] > index) listboxIndices[b] = listboxIndices[b] - 1;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool EntferneDateiAusTree(string dateipfad)
        {
            if (prüfung.ordner.EntferneDatei(dateipfad))
                return OrdnerstrukturZeichnen();
            return false;
        }

        /// <summary> Lösche Datei aus Tree, Topliste und Duplikatenlisten und deren Listboxen
        ///</summary>
        private bool LöscheDatei(ClassDatei cd, Quelle quelle, bool vonLaufwerk = true)
        {
            try
            {
                // Datei aus Tree entfernen
                if (!EntferneDateiAusTree(cd.pfad))
                {
                    MessageBox.Show("Datei konnte nicht aus Ordnerbaum entfernt werden!");
                    return false;
                }

                int listBoxIndex = prüfung.EntferneDateiAusTop100(cd.pfad);

                // Datei aus top100 liste entfernen
                if ((quelle == Quelle.top100 && listBoxIndex == -1) || !EntferneDateiAusTop100Listbox(listBoxIndex))
                {
                    MessageBox.Show("Datei konnte nicht aus sortierter Liste entfernt werden!");
                    return false;
                }
                List<int> listboxIndices = new List<int>();

                bool gefunden = prüfung.EntferneDateiAusDuplikate(cd, ref listboxIndices);

                if ((quelle == Quelle.duplikate && (!gefunden || listboxIndices.Count == 0)) ||
                    !EntferneDateiAusDuplikatenListbox(listboxIndices))
                {
                    MessageBox.Show("Datei konnte nicht aus Duplikaten Liste entfernt werden!");
                    return false;
                }
                listboxIndices.Clear();

                gefunden = prüfung.EntferneDateiAusSelbeGröße(cd, ref listboxIndices);

                if ((quelle == Quelle.selbegröße && (!gefunden || listboxIndices.Count == 0)) ||
                    !EntferneDateiAusSelbeGrößeListbox(listboxIndices))
                {
                    MessageBox.Show("Datei konnte nicht aus SelberGröße Liste entfernt werden!");
                    return false;
                }

                if (vonLaufwerk && File.Exists(cd.pfad))
                    File.Delete(cd.pfad);

                // Labels aktualisieren
            }
            catch (Exception ex)
            {
                MessageBox.Show("Löschen der Datei: \n" + cd.pfad + "\nnicht möglich!\n" + ex.Message);
                return false;
            }
            return true;
        }

        private void listBoxtop100_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listBoxtop100.SelectedIndex != -1)
            {
                LöscheTop100Item();
            }
        }


        private bool LöscheTop100Item()
        {
            var selected = listBoxtop100.SelectedItems;

            try
            {
                string dateipfad = listBoxtop100.Text.Split('\t').First();
                ClassDatei cd = prüfung.FindeDatei(dateipfad, ClassPrüfung.Liste.Sorted);

                if (cd == null)
                {
                    MessageBox.Show("Item konnte nicht gefunde werden!\n" + dateipfad);
                    return false;
                }

                // Datei von Laufwerk löschen
                if (File.Exists(dateipfad))
                {
                    if (!LöscheDatei(cd, Quelle.top100))
                    {
                        MessageBox.Show("Datei konnte nicht gelöscht werden!");
                        return false;
                    }

                    // Damit immer 100 Items angezeigt werden
                    if (prüfung.listsorted.Count > 99)
                        AddeListBoxTop100Item(prüfung.listsorted[99]);
                    labeltop100größe.Text = prüfung.GetTop100Größe();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }


        private bool RemoveNode(TreeNode node, string löschpfad, int a)
        {
            string nodetext = node.Text.Split(' ').First();
            if (a == 0)
            {
                while (nodetext.Split('\\')[a] == löschpfad.Split('\\')[a]) a++;
            }

            string löschabschnitt = löschpfad.Split('\\')[a]; // entweder Ordner bzw am ende datei

            TreeNodeCollection tnc = node.Nodes;
            foreach (TreeNode tn in tnc)
            {
                // Ordner?
                if (a < löschpfad.Split('\\').Length - 1)
                {
                    nodetext = tn.Text.Split(' ').First();
                    if (nodetext == löschabschnitt)
                    {
                        if (RemoveNode(tn, löschpfad, a + 1))
                        {
                            // wurden alle unterordner entfernt?
                            if (tn.Nodes.Count == 0)
                                tn.Nodes.Remove(tn);
                            return true;
                        }
                    }
                }
                else // Datei?
                {
                    löschabschnitt = löschabschnitt.Split('.').First();
                    nodetext = DateinameOhneGröße(tn.Text.Split(' '));

                    if (löschabschnitt == nodetext)
                    {
                        treeView1.Nodes.Remove(tn);
                        return true;
                    }
                }
            }

            return false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!prüfung.Stoppen())
                MessageBox.Show("Stoppen der Threads hat nicht fehlerfrei funktioniert!");
        }

        private void textBoxpfad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                buttonstarten_Click(sender, e);
        }


        private void listBoxselbegröße_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxselbegröße.SelectedIndex == -1) return;

            List<ClassDatei> gleichegröße = prüfung.listboxselbeGröße.ElementAt(listBoxselbegröße.SelectedIndex);

            FormDuplikate fd = new FormDuplikate(listBoxselbegröße.Text, gleichegröße);
            fd.ShowDialog();

            // Löschen
            if (fd.MGetAnzahlPfade() < 2)
            {
                foreach (ClassDatei cd in fd.löschenliste)
                {
                    if (LöscheDatei(cd, Quelle.selbegröße)) continue;

                    MessageBox.Show(cd.name + " konnte nicht entfernt werden");
                    break;
                }
            }
            AktualisiereListenLabels();
        }

        private void buttonordneranzeigen_Click(object sender, EventArgs e)
        {
            string datei = listBoxtop100.SelectedItem.ToString();
            string ordner = "";
            string[] split = datei.Split('\\');

            for (int a = 0; a < split.Length - 1; a++)
            {
                if (ordner != "")
                    ordner += "\\" + split[a];
                else
                    ordner = split[a];
            }

            // Öffne Pfad im Explorer
            string dateipfad = datei.Split('\t').First();

            if (File.Exists(dateipfad))
            {
                string argument = "/select, \"" + dateipfad + "\"";
                Process.Start("explorer.exe", argument);
            }
            else
                MessageBox.Show("Explorer konnte Datei nicht selektieren!");
        }

        private void listBoxtop100_SelectedIndexChanged(object sender, EventArgs e)
        {
            toolStripMenuItem1.Enabled = (listBoxtop100.SelectedItems.Count == 1);
        }

        private void buttongemeinsam_Click(object sender, EventArgs e)
        {
            // beeinflusst durch ignorieren liste
            FormGemeinsameOrdner fgo = new FormGemeinsameOrdner(prüfung.dictduplikate, ignorierteOrdner);
            fgo.ShowDialog();

            foreach (ClassDatei cd in fgo.gelöschteDateien)
            {
                if (!LöscheDatei(cd, Quelle.duplikate))
                {
                    MessageBox.Show("Datei [" + cd.nameMitTyp + "] konnte nicht gelöscht werden!");
                    break;
                }
            }
            AktualisiereListenLabels();
        }


        private void checkedListBoxaufgaben_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            List<int> pageIndices = new List<int>();
            foreach (TabPage t in tabControl1.TabPages)
                pageIndices.Add(tabControl1.TabPages.IndexOf(t));
            tabControl1.TabPages.Clear();

            if (pageIndices.Contains(e.Index) && e.NewValue == CheckState.Unchecked)
            {
                // entferne bei duplikate tab auch selbe größe tab
                if (e.Index == 2) pageIndices.Remove(e.Index + 1);
                pageIndices.Remove(e.Index);
            }
            else if (!pageIndices.Contains(e.Index) && e.NewValue == CheckState.Checked)
            {

                // adde bei duplikate tab auch selbe größe tab
                if (e.Index == 2) pageIndices.Add(e.Index + 1);
                pageIndices.Add(e.Index);
            }

            // Ordnerstruktur
            // Top100
            // Duplikate
            foreach (int index in pageIndices)
                tabControl1.TabPages.Add(tabpages[index]);

        }

        private void letzteAktionRückgängigToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void listBoxtop100_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || ((ListBox)sender).SelectedIndex == -1) return;

            contextMenuStrip1.Show(MousePosition);
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            buttonordneranzeigen_Click(sender, e);
        }


        private void tabPage3_Leave(object sender, EventArgs e)
        {
            //DuplikateInListbox();
        }

        private void tabPage2_Leave(object sender, EventArgs e)
        {
            //Top100InListbox();
        }

        private void SpeichereSicherung()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            StreamWriter sw = new StreamWriter("sicherung.csv");
            sw.WriteLine("[PFAD]");
            sw.WriteLine(prüfung.prüfungsPfad + ";" + prüfung.unterordnerPrüfen);
            sw.WriteLine("[DUPLIKATE]");
            foreach (KeyValuePair<double, ClassMD5> duplikat in prüfung.dictduplikate)
            {
                sw.Write(duplikat.Key);
                foreach (ClassDatei cd in duplikat.Value.duplikate)
                    sw.Write(";" + cd.pfad);
                sw.WriteLine();
            }
            sw.WriteLine("[SELBE GRÖßE]");
            foreach (KeyValuePair<double, List<ClassDatei>> duplikat in prüfung.dictselbegröße)
            {
                sw.Write(duplikat.Key);
                foreach (ClassDatei cd in duplikat.Value)
                    sw.Write(";" + cd.pfad);
                sw.WriteLine();
            }
            sw.Close();
            watch.Stop();
            string zeit = watch.ElapsedMilliseconds.ToString() + "ms";
            if (watch.ElapsedMilliseconds > 3000)
                zeit = watch.ElapsedMilliseconds / 1000 + "sek";

            Invoker.invokeTextSet(labelstatus, "Zwischenstand gespeichert nach " + zeit);
            Invoker.invokeVisible(pictureBox1, false);
        }

        private void speichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread sichern = new Thread(delegate () { SpeichereSicherung(); });
            pictureBox1.Visible = true;
            labelstatus.Text = "Aktueller Stand wird gesichert";
            sichern.Start();
        }

        private void SicherungLaden()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            if (!File.Exists("sicherung.csv"))
            {
                Invoker.invokeTextSet(labelstatus, "Keine Sicherungsdatei zum Laden vorhanden!");
                return;
            }

            StreamReader sr = new StreamReader("sicherung.csv");
            string zeile = sr.ReadLine();

            if (zeile != "[PFAD]")
            {
                MessageBox.Show("Pfad Header erwartet!");
                sr.Close();
                return;
            }

            string[] splits = sr.ReadLine().Split(';');
            if (splits.Length != 2)
            {
                sr.Close();
                MessageBox.Show("Ordnerpfad und Unterordner prüfen erwartet!");
                return;
            }

            string ordnerpfad = splits[0];
            Invoker.invokeTextSet(textBoxpfad, ordnerpfad);
            bool unterordnerPrüfen = Convert.ToBoolean(splits[1]);
            Invoker.invokeChecked(checkBoxunterordner, unterordnerPrüfen);

            if (!Directory.Exists(ordnerpfad))
            {
                MessageBox.Show("Ordner aus Sicherungsdatei nicht vorhanden!");

                FormEnterOrdnerpfad feo = new FormEnterOrdnerpfad();
                while (!feo.abbrechen)
                {
                    feo.ShowDialog();
                    if (Directory.Exists(feo.pfad))
                    {
                        ordnerpfad = feo.pfad;
                        unterordnerPrüfen = feo.unterordner;
                        break;
                    }
                }

                if (!Directory.Exists(feo.pfad))
                {
                    sr.Close();
                    return;
                }
            }

            Invoker.invokeEnable(pictureBox1, true);
            Invoker.invokeVisible(pictureBox1, true);
            Invoker.invokeTextSet(labelstatus, "Letzter Stand wird geladen. Bitte warten!");

            prüfung = new ClassPrüfung(ordnerpfad, unterordnerPrüfen);

            // Ordnerstruktur
            prüfung.ordner = new ClassOrdner(ordnerpfad, unterordnerPrüfen);
            // SortDescending
            prüfung.SortDescending();

            zeile = sr.ReadLine();
            if (zeile != "[DUPLIKATE]")
            {
                sr.Close();
                MessageBox.Show("Duplikate Header erwartet!");
                return;
            }

            prüfung.dictduplikate = new Dictionary<double, ClassMD5>();
            zeile = sr.ReadLine();
            while (!sr.EndOfStream && zeile != "[SELBE GRÖßE]")
            {
                splits = zeile.Split(';');
                double dateigröße = Convert.ToDouble(splits[0].ToString());
                List<string> pfade = new List<string>();
                while (prüfung.dictduplikate.ContainsKey(dateigröße))
                    dateigröße -= 0.00001;

                List<ClassDatei> dupPfade = new List<ClassDatei>();
                for (int a = 1; a < splits.Length; a++)
                {
                    if (File.Exists(splits[a]))
                        dupPfade.Add(new ClassDatei(splits[a]));
                }

                if (dupPfade.Count > 1)
                {
                    prüfung.dictduplikate.Add(dateigröße, new ClassMD5(dupPfade));
                }

                zeile = sr.ReadLine();
            }

            if (zeile == "[SELBE GRÖßE]")
            {
                zeile = sr.ReadLine();
                while (true)
                {
                    splits = zeile.Split(';');
                    double dateigröße = Convert.ToDouble(splits[0].ToString());
                    List<string> pfade = new List<string>();
                    while (prüfung.dictselbegröße.ContainsKey(dateigröße))
                        dateigröße -= 0.00001;

                    List<ClassDatei> dupPfade = new List<ClassDatei>();
                    for (int a = 1; a < splits.Length; a++)
                    {
                        if (File.Exists(splits[a]))
                            dupPfade.Add(new ClassDatei(splits[a]));
                    }

                    if (dupPfade.Count > 1)
                    {
                        prüfung.dictselbegröße.Add(dateigröße, dupPfade);
                    }

                    if (!sr.EndOfStream)
                        zeile = sr.ReadLine();
                    else
                        break;
                }
            }

            sr.Close();

            this.Invoke((MethodInvoker)delegate
            {
                OrdnerstrukturZeichnen();
                Top100InListbox();
                DuplikateInListbox();
                SelbeGrößeInListBox();
            });

            sw.Stop();
            string zeit = sw.ElapsedMilliseconds.ToString() + "ms";
            if (sw.ElapsedMilliseconds > 3000)
                zeit = sw.ElapsedMilliseconds / 1000 + "sek";
            Invoker.invokeTextSet(labelstatus, "Dateien erfolgreich geladen nach " + zeit);
            Invoker.invokeEnable(tabControl1, true);
            Invoker.invokeEnable(pictureBox1, false);
            Invoker.invokeVisible(pictureBox1, false);
        }

        private void ladenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (laden != null && laden.IsAlive)
                laden.Abort();

            laden = new Thread(delegate () { SicherungLaden(); });
            labelstatus.Text = "Sicherung wird geladen";
            laden.Start();
        }

        private void checkBoxduplikateselberOrdner_CheckedChanged(object sender, EventArgs e)
        {
            DuplikateInListbox();
            SelbeGrößeInListBox();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            SelbeGrößeInListBox();
        }

        private void ignoriertePfadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormIgnoriertePfade fip = new FormIgnoriertePfade(ignorierteOrdner);
            fip.ShowDialog();
            ignorierteOrdner = fip.GetIgnoriertePfade();

            if (prüfung == null || prüfung.ordner == null) return;

            DuplikateInListbox();
            SelbeGrößeInListBox();
        }

        private void leereOrdnerLöschenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (prüfung.ordner == null)
            {
                MessageBox.Show("Kein Ordner gesetzt!");
                return;
            }

            List<string> leere = prüfung.ordner.GetLeereOrdner(true, false);
           ClassOrdner.LöscheOrdnerPhsyisch(ref leere);
            if (ClassOrdner.staticErrors.Count == 0) return;

            string fehler = "";
            foreach (string f in ClassOrdner.staticErrors)
            {
                if (fehler != "") fehler += "\n" + f;
                else fehler = f;
            }    

            MessageBox.Show(fehler);
        }
    }
}
