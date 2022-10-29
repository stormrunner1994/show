using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Phase6_Software.Properties;

namespace Phase6_Software
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        StreamWriter sw;
        ClassProfil profil;

        private const string strverbotenezeichen = "\n|\r|;";

        #region TabHome

        private void listBoxvorhandeneprofile_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxvorhandeneprofile.SelectedIndex != -1)
                buttonanmelden_Click(sender, e);
        }

        private void listBoxvorhandeneprofile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxvorhandeneprofile.SelectedIndex != -1)
            {
                buttonlöschen.Enabled = buttonanmelden.Enabled = buttonumbennen.Enabled = true;                
            }
            else
            {
                buttonlöschen.Enabled = buttonanmelden.Enabled = buttonumbennen.Enabled = false;
            }
        }


        private void listBoxvorhandeneprofile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (listBoxvorhandeneprofile.SelectedIndex != -1)
                    buttonanmelden_Click(sender, e);
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (listBoxvorhandeneprofile.SelectedIndex != -1)
                    buttonlöschen_Click(sender, e);
            }
        }

        private void textBoxprofilname_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && Program.MLeerstellenEntfernen(textBoxprofilname.Text) != "")
                buttonbenutzeranlegen_Click(sender, e);
        }

        private void buttonbenutzeranlegen_Click(object sender, EventArgs e)
        {
            textBoxprofilname.Text = Program.MLeerstellenEntfernen(textBoxprofilname.Text);
            // ist Eingabe gültig?
            try
            {
                if (textBoxprofilname.Text.Contains("\n") || textBoxprofilname.Text.Contains("\r"))
                {
                    MessageBox.Show("Ihre Eingabe darf keinen Zeilenumbruch enthalten!");
                    return;
                }
                else if (textBoxprofilname.Text.Contains(";"))
                {
                     MessageBox.Show("Ihre Eingabe darf kein Semikolon enthalten!");
                     return;
                }
                else if (textBoxprofilname.Text.Contains("."))
                {
                    MessageBox.Show("Ihre Eingabe darf keinen Punkt enthalten!");
                    return;
                }

                // ist Profil neu?
                if (!listBoxvorhandeneprofile.Items.Contains(textBoxprofilname.Text))
                {
                    File.Create("C:\\Phase6\\" + textBoxprofilname.Text + ".csv").Close();

                    // Erste Zeile, die Spalten der Datei nennt, schreiben
                    sw = new StreamWriter("C:\\Phase6\\" + textBoxprofilname.Text + ".csv");
                    sw.WriteLine("Kategorie;Frage;Antwort;Phase;Richtige;Falsche;Datum");
                    sw.Close();

                    MHomeListboxfüllen();
                }
                else
                    MessageBox.Show("Das Profil '" + textBoxprofilname.Text + "' ist bereits vorhanden.");

                listBoxvorhandeneprofile.SelectedItem = textBoxprofilname.Text;
                buttonanmelden.Enabled = buttonlöschen.Enabled = true;
                textBoxprofilname.Text = "";
            }
            catch // ungültige Eingabe
            {
                MessageBox.Show("Ihre Eingabe ist nicht erlaubt!");
            }
        }

        private void MHomeListboxfüllen()
        {
            listBoxvorhandeneprofile.Items.Clear();
            listBoxsicherungen.Items.Clear();

            // Ordner nach Profilen durchsuchen
            foreach (string datei in Directory.GetFiles("C:\\Phase6"))
            {
                string strtemp = "";
                bool bolcontinue = false;

                // Profilname aus Pfad filtern
                for (int a = datei.Length - 1; a > 0; a--)
                {
                    if (datei[a] == '.')
                    {
                        bolcontinue = true;
                        continue;
                    }
                    else if (datei[a] == '\\')
                        break;

                    if (bolcontinue == true)
                        strtemp = datei[a] + strtemp;
                }

                if (strtemp.Length > 4)
                {
                    if (!strtemp.Contains("Einstellungen") && (strtemp[0].ToString() +strtemp[1].ToString() +strtemp[2].ToString() +strtemp[3].ToString()) != "safe")
                    {
                        listBoxvorhandeneprofile.Items.Add(strtemp);
                    }
                    else if (strtemp[0].ToString() + strtemp[1].ToString() + strtemp[2].ToString() + strtemp[3].ToString() == "safe")
                    {
                        listBoxsicherungen.Items.Add(strtemp);
                    }
                }
                else if (Program.MLeerstellenEntfernen(strtemp) != "")
                    listBoxvorhandeneprofile.Items.Add(strtemp);
            }
        }

        private void MHomeVoreinstellungen()
        {
            // Speicherordner erstellen, falls nicht vorhanden
            if (!Directory.Exists("C:\\Phase6"))
                Directory.CreateDirectory("C:\\Phase6");


            // Alle Profile im Ordner zur Verfügung stellen
            MHomeListboxfüllen();

            toolStrip1.Visible = false;
            if (listBoxvorhandeneprofile.Items.Count < 1)
                textBoxprofilname.Focus();
            else
            {
                listBoxvorhandeneprofile.SelectedIndex = 0;
                listBoxvorhandeneprofile.Focus();
            }

            buttonsicherunglöschen.Enabled = buttonhomesicherung.Enabled = false;
        }

        private void buttonlöschen_Click(object sender, EventArgs e)
        {
            string löschen = listBoxvorhandeneprofile.SelectedItem.ToString();

            // Profil aus Ordner löschen
            if (File.Exists("C:\\Phase6\\" + löschen + ".csv"))
                File.Delete("C:\\Phase6\\" + löschen + ".csv");
            if (File.Exists("C:\\Phase6\\" + löschen + "_Einstellungen.csv"))
                File.Delete("C:\\Phase6\\" + löschen + "_Einstellungen.csv");

            listBoxvorhandeneprofile.Items.Remove(löschen);

            if (profil != null && profil.name == löschen)
            {
                profil = null;
                this.Text = "Phase-6";
            }

            if (listBoxvorhandeneprofile.Items.Count > 0)
                listBoxvorhandeneprofile.SelectedIndex = 0;
            else
                buttonlöschen.Enabled = buttonanmelden.Enabled = false;
        }

        private void buttonanmelden_Click(object sender, EventArgs e)
        {
            profil = new ClassProfil(listBoxvorhandeneprofile.SelectedItem.ToString());
            MHintergrundfarbenAnpassen();
            profil.MHoleKarteikartenAusDatei();

            if (profil.fehler != "")
            {
                string fehler = profil.fehler;
                profil = null;
                MessageBox.Show(fehler);
            }
            else
            {
                this.Text = "Phase6 [" + profil.name + ']';
                tabControl1.SelectedTab = tabPageEingabe;
            }
        }

        private void textBoxprofilname_TextChanged(object sender, EventArgs e)
        {
            if (Program.MLeerstellenEntfernen(textBoxprofilname.Text) == "")
                buttonbenutzeranlegen.Enabled = false;
            else
                buttonbenutzeranlegen.Enabled = true;
        }

        #endregion

        #region TabEingabe

        private void MEingabeVoreinstellungen()
        {
            comboBoxeingabealleKategorien.Items.Clear();
            // Combobox füllen
            foreach (KeyValuePair<int, ClassKarteikarte> pair in profil.karteikarten)
            {
                // Aufbau der Datei: Kategorie;Frage;Antwort;Phase;Richtige;Falsche;Datum
                if (!comboBoxeingabealleKategorien.Items.Contains(pair.Value.Kategorie))
                    comboBoxeingabealleKategorien.Items.Add(pair.Value.Kategorie);
            }


            if (comboBoxeingabealleKategorien.Items.Count > 0)
            {
                comboBoxeingabealleKategorien.Enabled = true;
                comboBoxeingabealleKategorien.SelectedIndex = 0;
            }
            else
                comboBoxeingabealleKategorien.Enabled = false;

            richTextBoxeingabefrage.Text = "";
            richTextBoxeingabeantwort.Text = "";
            textBoxneuekategorie.Text = "";

            richTextBoxeingabefrage.Focus();
        }

        private void tabPageEingabe_Enter(object sender, EventArgs e)
        {
            // ist ein Profil vorhanden?
            if (profil != null)
                MEingabeVoreinstellungen();
            else
            {
                MessageBox.Show("Bitte melden Sie sich zuerst an.");
                tabControl1.SelectedTab = tabPageHome;
            }
        }

        private void comboBoxeingabealleKategorien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                buttonkarteikartehinzufügen_Click(sender, e);
        }

        private bool MKarteikarteErstellen()
        {
            // Karteikarte bereits vorhnaden?

            bool vorhanden = false;
            string kategorie = textBoxneuekategorie.Text;

            if (comboBoxeingabealleKategorien.SelectedIndex != -1)
                kategorie = comboBoxeingabealleKategorien.SelectedItem.ToString();

            // Zeilenweises Überprüfen
            foreach (KeyValuePair<int, ClassKarteikarte> pair in profil.karteikarten)
            {
                if (pair.Value.Kategorie == kategorie && pair.Value.Frage == richTextBoxeingabefrage.Text && pair.Value.Antwort == richTextBoxeingabeantwort.Text)
                    vorhanden = true;
            }

            if (vorhanden)
            {
                // Dennoch erstellen?
                FormVorhanden fv = new FormVorhanden();
                fv.ShowDialog();
                return fv.GetErstellen();
            }
            return true;

        }

        private void richTextBoxeingabefrage_Leave(object sender, EventArgs e)
        {
            richTextBoxeingabefrage.Font = new Font(new FontFamily("Microsoft Sans Serif"), 9, FontStyle.Regular);
        }

        private void richTextBoxeingabeantwort_Leave(object sender, EventArgs e)
        {
            richTextBoxeingabeantwort.Font = new Font(new FontFamily("Microsoft Sans Serif"), 9, FontStyle.Regular);
        }

        private bool MEingabeStimmt()
        {
            richTextBoxeingabeantwort.Text = Program.MLeerstellenEntfernen(richTextBoxeingabeantwort.Text);
            richTextBoxeingabefrage.Text = Program.MLeerstellenEntfernen(richTextBoxeingabefrage.Text);
            textBoxneuekategorie.Text = Program.MLeerstellenEntfernen(textBoxneuekategorie.Text);
            string[] split = strverbotenezeichen.Split('|');

            if (richTextBoxeingabefrage.Text.Contains(split[0]) || richTextBoxeingabefrage.Text.Contains(split[1]))
            {
                richTextBoxeingabefrage.BackColor = Color.Red;
                MessageBox.Show("Ihre Eingabe darf keinen Zeilenumbruch enthalten!");
                return false;
            }
            else if (richTextBoxeingabefrage.Text.Contains(split[2]))
            {
                richTextBoxeingabefrage.BackColor = Color.Red;
                MessageBox.Show("Ihre Eingabe darf kein Semikolon enthalten!");
                return false;
            }

            if (richTextBoxeingabeantwort.Text.Contains(split[0]) || richTextBoxeingabeantwort.Text.Contains(split[1]))
            {
                richTextBoxeingabeantwort.BackColor = Color.Red;
                MessageBox.Show("Ihre Eingabe darf keinen Zeilenumbruch enthalten!");
                return false;
            }
            else if (richTextBoxeingabeantwort.Text.Contains(split[2]))
            {
                richTextBoxeingabeantwort.BackColor = Color.Red;
                MessageBox.Show("Ihre Eingabe darf kein Semikolon enthalten!");
                return false;
            }

            if (textBoxneuekategorie.Text.Contains(split[0]) || textBoxneuekategorie.Text.Contains(split[1]))
            {
                textBoxneuekategorie.BackColor = Color.Red;
                MessageBox.Show("Ihre Eingabe darf keinen Zeilenumbruch enthalten!");
                return false;
            }
            else if (textBoxneuekategorie.Text.Contains(split[2]))
            {
                textBoxneuekategorie.BackColor = Color.Red;
                MessageBox.Show("Ihre Eingabe darf kein Semikolon enthalten!");
                return false;
            }

            return true;
        }

        private void buttonkarteikartehinzufügen_Click(object sender, EventArgs e)
        {
            textBoxneuekategorie.BackColor = comboBoxeingabealleKategorien.BackColor;
            // Enthält Eingabe kein verbotenes Zeichen?
            if (MEingabeStimmt())
            {
                if (MKarteikarteErstellen())
                {
                    ClassKarteikarte karteikarte = new ClassKarteikarte();
                    if (textBoxneuekategorie.Text == "")
                        karteikarte.Kategorie = comboBoxeingabealleKategorien.SelectedItem.ToString();
                    else
                        karteikarte.Kategorie = textBoxneuekategorie.Text;
                    karteikarte.Frage = richTextBoxeingabefrage.Text;
                    karteikarte.Antwort = richTextBoxeingabeantwort.Text;
                    karteikarte.Phase = 1;
                    karteikarte.Richtige = 0;
                    karteikarte.Falsche = 0;
                    karteikarte.ID = profil.karteikarten.Count;
                    karteikarte.Datum = DateTime.Now;

                    profil.karteikarten.Add(profil.karteikarten.Count, karteikarte);

                    richTextBoxeingabefrage.Text = "";
                    richTextBoxeingabeantwort.Text = "";

                    // Combobox anpassen
                    if (textBoxneuekategorie.Text != "")
                    {
                        comboBoxeingabealleKategorien.Enabled = true;
                        comboBoxeingabealleKategorien.Items.Add(textBoxneuekategorie.Text);
                        comboBoxeingabealleKategorien.SelectedItem = textBoxneuekategorie.Text;
                        textBoxneuekategorie.Text = "";
                    }
                }
                else
                {
                    richTextBoxeingabefrage.Text = "";
                    richTextBoxeingabeantwort.Text = "";
                    checkBoxgeltenlassen.Checked = false;
                }
            }
            

            richTextBoxeingabefrage.Focus();
        }

        private void richTextBoxeingabefrage_TextChanged(object sender, EventArgs e)
        {
            // Hintergrundfarbe nach Fehleingabe zurücksetzen
            if (richTextBoxeingabefrage.BackColor != Color.White)
                richTextBoxeingabefrage.BackColor = Color.White;
            MEingabeButtonStatus();
        }

        private void MEingabeButtonStatus()
        {
            if (richTextBoxeingabefrage.Text != "" && richTextBoxeingabeantwort.Text != "" && (comboBoxeingabealleKategorien.SelectedItem != null || textBoxneuekategorie.Text != ""))
                buttonkarteikartehinzufügen.Enabled = true;
            else
                buttonkarteikartehinzufügen.Enabled = false;
        }

        private void richTextBoxeingabeantwort_TextChanged(object sender, EventArgs e)
        {
            // Hintergrundfarbe nach Fehleingabe zurücksetzen            
            if (richTextBoxeingabeantwort.BackColor != Color.White)
                richTextBoxeingabeantwort.BackColor = Color.White;

            MEingabeButtonStatus();
        }

        private void textBoxneuekategorie_TextChanged(object sender, EventArgs e)
        {
            // Hintergrundfarbe nach Fehleingabe zurücksetzen            
            if (textBoxneuekategorie.BackColor != Color.White)
                textBoxneuekategorie.BackColor = Color.White;

            if (comboBoxeingabealleKategorien.SelectedIndex != -1 && textBoxneuekategorie.Text != "")
                comboBoxeingabealleKategorien.SelectedIndex = -1;

            MEingabeButtonStatus();
        }

        private void comboBoxeingabealleKategorien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxeingabealleKategorien.SelectedIndex != -1)
                textBoxneuekategorie.Text = "";
            MEingabeButtonStatus();
        }

        #endregion

        #region TabAbfrage


        private void tabPageAbfrage_Leave(object sender, EventArgs e)
        {
            if (profil != null)
            {
                toolStrip1.Visible = false;
            }
        }

        private void checkBoxinaktive_CheckedChanged(object sender, EventArgs e)
        {
            buttonnächsteFrage_Click(sender, e);
        }

        private void richTextBoxabfrageantwort_TextChanged(object sender, EventArgs e)
        {
            MAbfrageButtonPrüfenStatus();
        }

        private void tabPageAbfrage_Enter(object sender, EventArgs e)
        {
            // ist ein Profil vorhanden?
            if (profil != null)
            {
                MAbfrageVoreinstellungen();
                richTextBoxabfrageantwort.Focus(); // funktioniert nicht?
            }
            else
            {
                MessageBox.Show("Bitte melden Sie sich zuerst an.");
                tabControl1.SelectedTab = tabPageHome;
            }
        }

        private void MAbfrageButtonPrüfenStatus()
        {
            if (richTextBoxabfragefrage.Text != "" && richTextBoxabfrageantwort.Text != "")
                buttonprüfen.Enabled = true;
            else
                buttonprüfen.Enabled = false;
        }

        private void MAbfrageVoreinstellungen()
        {
            comboBoxabfrageallekategorien.Items.Clear();
            comboBoxabfrageallekategorien.Items.Add("Alle");

            foreach (KeyValuePair<int, ClassKarteikarte> pair in profil.karteikarten)
            {
                // alle vorhandenen Kategorien zur Combobox hinzufügen
                if (!comboBoxabfrageallekategorien.Items.Contains(pair.Value.Kategorie))
                    comboBoxabfrageallekategorien.Items.Add(pair.Value.Kategorie);
            }

            comboBoxabfrageallekategorien.SelectedItem = comboBoxabfrageallekategorien.Items[0];
            comboBoxabfragephase.SelectedItem = comboBoxabfragephase.Items[0];


            if (profil.karteikarten.Count > 0)
                checkBoxinaktive.Enabled = true;
            else
                checkBoxinaktive.Enabled = false;

            toolStrip1.Visible = true;
            toolStripButtonDefault.Visible = false;
            richTextBoxabfrageantwort.Text = "";
            checkBoxinaktive.Checked = false;
            checkBoxgeltenlassen.Enabled = buttonprüfen.Enabled = false;
        }

        private void buttonprüfen_Click(object sender, EventArgs e)
        {
            richTextBoxabfrageantwort.Enabled = false;
            string richtigeAntwort = "";
            bool richtig = false;

            if (checkBoxtauschen.Checked)
            {
                richtigeAntwort = profil.momentaneKarteikarte.Frage;
                if (profil.momentaneKarteikarte.Frage == richTextBoxabfrageantwort.Text)
                    richtig = true;
            }
            else
            {
                richtigeAntwort = profil.momentaneKarteikarte.Antwort;
                if (profil.momentaneKarteikarte.Antwort == richTextBoxabfrageantwort.Text)
                    richtig = true;
            }

            // ist vermutete Antwort richtig?
            if (richtig)
            {
                richTextBoxabfrageantwort.Text += "\n\nDie Antwort ist richtig.";
                richTextBoxabfrageantwort.ForeColor = Color.Green;
                profil.versuch = "richtig";

                //Abändern
                profil.MBearbeiteKarteikarte(profil.momentaneKarteikarte.ID);
            }
            else
            {
                richTextBoxabfrageantwort.Text += "\n\nDie Antwort ist falsch.\n\nKorrekte Antwort: " + richtigeAntwort;
                richTextBoxabfrageantwort.ForeColor = Color.Red;
                checkBoxgeltenlassen.Enabled = true;
                profil.versuch = "falsch";         
            }

            buttonprüfen.Enabled = false;
            buttonnächsteFrage.Select();
        }

        private void buttonnächsteFrage_Click(object sender, EventArgs e)
        {
            if (profil.versuch != null && profil.versuch != "kein")
            {
                if (profil.versuch == "falsch")
                {
                    // Vorher ändern, dass es als richtig abgespeichert werden soll
                    if (checkBoxgeltenlassen.Checked)
                        profil.versuch = "richtig";
                    else
                        profil.versuch = "falsch";

                    profil.MBearbeiteKarteikarte(profil.momentaneKarteikarte.ID);
                }
            }

            profil.versuch = "kein";
            richTextBoxabfrageantwort.Text = "";
            richTextBoxabfragefrage.Text = "";
            checkBoxgeltenlassen.Enabled = false;
            checkBoxgeltenlassen.Checked = false;

            MAbfrageFragestellen();

            if (richTextBoxabfrageantwort.ForeColor != Color.Black)
                richTextBoxabfrageantwort.ForeColor = Color.Black;

        }

        private void checkBoxtauschen_CheckedChanged(object sender, EventArgs e)
        {
            buttonnächsteFrage_Click(sender, e);
        }


        private void MAbfrageFragestellen()
        {
            richTextBoxabfrageantwort.Text = richTextBoxabfragefrage.Text = "";

            // Comboboxen deaktiveren, solange eingelesen wird
            comboBoxabfrageallekategorien.Enabled = comboBoxabfragephase.Enabled = false;

            // Alle Karteikarten, die zu den Comboboxauswahlen passen, zur 'Liste' hinzufügen
            profil.MFiltereKarteikarten(comboBoxabfragephase.SelectedItem.ToString(), comboBoxabfrageallekategorien.SelectedItem.ToString(), checkBoxinaktive.Checked);

            // stehen Karteikärtchen bevor?
            if (profil.gefiltert.Count > 0)
            {
                if (profil.gefiltert.Count > 1)
                    toolStripLabelnotiz.Text = profil.gefiltert.Count + " ausstehende Karteikarten";
                else
                    toolStripLabelnotiz.Text = profil.gefiltert.Count + " ausstehende Karteikarte";

                Random rand = new Random();
                int index = rand.Next(0, profil.gefiltert.Count);
                profil.momentaneKarteikarte = profil.gefiltert.ElementAt(index).Value;
                profil.momentaneKarteikarte.ID = profil.gefiltert.ElementAt(index).Value.ID;

                checkBoxtauschen.Enabled = richTextBoxabfrageantwort.Enabled = true;
                textBoxabfragekategorie.Text = profil.momentaneKarteikarte.Kategorie;

                if (checkBoxtauschen.Checked)
                    richTextBoxabfragefrage.Text = profil.momentaneKarteikarte.Antwort;
                else
                    richTextBoxabfragefrage.Text = profil.momentaneKarteikarte.Frage;

                if (buttonnächsteFrage.Enabled == false)
                    buttonnächsteFrage.Enabled = true;
            }
            else
            {
                toolStripLabelnotiz.Text = "0 ausstehende Karteikarten";
                checkBoxtauschen.Enabled = buttonnächsteFrage.Enabled = false;
                richTextBoxabfrageantwort.Enabled = false;
            }

            // Comboboxen nach Wartezeit wieder freigeben
            comboBoxabfrageallekategorien.Enabled = comboBoxabfragephase.Enabled = true;
        }

        private void comboBoxabfragephase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxabfrageallekategorien.SelectedItem != null)
            {
                MAbfrageFragestellen();
                richTextBoxabfrageantwort.Focus();
            }
        }

        private void comboBoxabfrageallekategorien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxabfragephase.SelectedItem != null)
            {
                MAbfrageFragestellen();
                richTextBoxabfrageantwort.Focus();
            }
        }

        #endregion

        #region Allgemein



        private void Form1_Load(object sender, EventArgs e)
        {
            timerspeichern.Start();
            toolStripTextBoxsuchfunktion.Text = "Suchen nach";
            toolStripTextBoxsuchfunktion.Visible = false;
            toolStripButtonDefault.Text = "Einstellungen auf Default setzen";
            
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Resources.Phase6_Logo_svg;


            // immer in Bildschirmmitte laden
          int  iBildschirmhöhe = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
           int iBildschirmbreite = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.Location = new Point((iBildschirmbreite / 2) - this.Width / 2, (iBildschirmhöhe / 2) - this.Height / 2);
            toolStrip1.Items.Add(toolStripTextBoxsuchfunktion);
            toolStrip1.Items.Add(toolStripLabelnotiz);
            toolStrip1.Items.Add(toolStripButtonDefault);
            //this.MaximumSize = System.Drawing.Size(iBildschirmhöhe,iBildschirmbreite);

            MHomeVoreinstellungen();
        }

        #endregion

        #region Bibliothek

        private void listViewbibliothek_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                listViewbibliothek_DoubleClick(sender, e);
            }
            else if (e.KeyData == Keys.Delete)
            {
                for (int a = 0; a < listViewbibliothek.SelectedIndices.Count; a++)
                {
                    int index = listViewbibliothek.SelectedIndices[a];

                    // Karteikarte löschen
                    if (index != -1)
                    {
                        if (profil.suchbegriff == "" && profil.suchbegriff == "Suchen nach")
                        {
                            profil.karteikarten.Remove(profil.gesucht[index].ID);
                            profil.gesucht.Remove(index);
                            profil.MGesuchtAktualisieren();
                            profil.MAlleAktualiseren();
                        }
                        else
                        {
                            profil.karteikarten.Remove(index);
                            profil.MAlleAktualiseren();
                        }
                    }
                }

                // Liste aktualisieren
                if (profil.suchbegriff == "" || profil.suchbegriff == "Suchen nach")
                    MBibliothekVoreinstellungen(profil.karteikarten);
                else
                    MBibliothekVoreinstellungen(profil.gesucht);
            }
        }

        private void listViewbibliothek_DoubleClick(object sender, EventArgs e)
        {
            if (listViewbibliothek.SelectedItems.Count > 1)
            {
                listViewbibliothek.FocusedItem.Text = listViewbibliothek.SelectedItems[0].Text;
            }

            if (listViewbibliothek.FocusedItem.Text != "")
            {
                int idbearbeiten = -1;
                foreach (KeyValuePair<int, ClassKarteikarte> pair in profil.karteikarten)
                {
                    if (pair.Value.Kategorie == listViewbibliothek.FocusedItem.Text && pair.Value.Frage == listViewbibliothek.FocusedItem.SubItems[1].Text && pair.Value.Antwort == listViewbibliothek.FocusedItem.SubItems[2].Text)
                    {
                        idbearbeiten = pair.Key;
                        break;
                    }
                }

                FormBearbeiten fbopen = new FormBearbeiten(profil.karteikarten[idbearbeiten].Kategorie, profil.karteikarten[idbearbeiten].Frage, profil.karteikarten[idbearbeiten].Antwort);
                fbopen.ShowDialog();

                // Bearbeiten erfolgt jetzt
                if (fbopen.status == "löschen")
                {
                    profil.karteikarten.Remove(idbearbeiten);
                    profil.MAlleAktualiseren();
                }
                else
                {
                    profil.karteikarten[idbearbeiten].Kategorie = fbopen.kategorie;
                    profil.karteikarten[idbearbeiten].Frage = fbopen.frage;
                    profil.karteikarten[idbearbeiten].Antwort = fbopen.antwort;

                    if (fbopen.status == "zurücksetzen")
                    {
                        profil.karteikarten[idbearbeiten].Phase = 1;
                        profil.karteikarten[idbearbeiten].Richtige = 0;
                        profil.karteikarten[idbearbeiten].Falsche = 0;
                        profil.karteikarten[idbearbeiten].Datum = DateTime.Now;
                    }
                }

                if (profil.suchbegriff == "" || profil.suchbegriff == "Suchen nach")
                    MBibliothekVoreinstellungen(profil.karteikarten);
                else
                    MBibliothekVoreinstellungen(profil.gesucht);
            }
        }

        private void tabPageBibliothek_Leave(object sender, EventArgs e)
        {
            if (toolStripTextBoxsuchfunktion.Focused == false)
                toolStrip1.Visible = toolStripTextBoxsuchfunktion.Visible = false;
        }

        private void tabPageBibliothek_Enter(object sender, EventArgs e)
        {
            // ist ein Profil vorhanden?
            if (profil != null)
            {
                if (profil.suchbegriff == "" || profil.suchbegriff == "Suchen nach")
                    MBibliothekVoreinstellungen(profil.karteikarten);
                else
                    MBibliothekVoreinstellungen(profil.gesucht);
            }
            else
            {
                MessageBox.Show("Bitte melden Sie sich zuerst an.");
                tabControl1.SelectedTab = tabPageHome;
            }
        }

        private void MBibliothekVoreinstellungen(Dictionary<int, ClassKarteikarte> karteikarten)
        {

            listViewbibliothek.Items.Clear();

            foreach (KeyValuePair<int, ClassKarteikarte> pair in karteikarten)
            {
                ListViewItem item = new ListViewItem(pair.Value.Kategorie);
                item.SubItems.Add(pair.Value.Frage);
                item.SubItems.Add(pair.Value.Antwort);
                item.SubItems.Add(pair.Value.Phase.ToString());
                item.SubItems.Add(pair.Value.Richtige.ToString());
                item.SubItems.Add(pair.Value.Falsche.ToString());
                item.SubItems.Add(pair.Value.Datum.ToShortDateString());

                listViewbibliothek.Items.Add(item);
            }

            string notiz = "Es liegen " + karteikarten.Count + " Karteikarten vor. Zum Bearbeiten doppelklicken Sie auf ein Item.";

            if (karteikarten.Count == 1)
                notiz = "Es liegt 1 Karteikarte vor. Zum Bearbeiten doppelklicken Sie auf das Item.";
            else if (karteikarten.Count == 0)
                notiz = "Es liegen keine Karteikarten vor.";

            toolStripLabelnotiz.Text = notiz;

            toolStrip1.Visible  = true;
            toolStripLabelnotiz.Visible = true;
            toolStripTextBoxsuchfunktion.Visible = true;
            toolStripButtonDefault.Visible = false;
        }
        

        private void toolStripTextBoxsuchfunktion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                MSuchfunktion();
        }
        
        private void MSuchfunktion()
        {
            if (profil != null)
            {
                listViewbibliothek.Items.Clear();
                profil.suchbegriff = toolStripTextBoxsuchfunktion.Text = Program.MLeerstellenEntfernen(toolStripTextBoxsuchfunktion.Text);
                // listview neu füllen
                profil.MSucheKarteikarten();
                MBibliothekVoreinstellungen(profil.gesucht);
            }
        }

        #endregion

        #region Einstellungen

        private void MEinstellungenVoreinstellungen()
        {
            try
            {
                textBoxeinstellungenphase1.Text = profil.einstellungen.abstände[1].ToString();
                textBoxeinstellungenphase2.Text = profil.einstellungen.abstände[2].ToString();
                textBoxeinstellungenphase3.Text = profil.einstellungen.abstände[3].ToString();
                textBoxeinstellungenphase4.Text = profil.einstellungen.abstände[4].ToString();
                textBoxeinstellungenphase5.Text = profil.einstellungen.abstände[5].ToString();
                textBoxeinstellungenphase6.Text = profil.einstellungen.abstände[6].ToString();



                buttoneinstellungenübernehmen.Enabled = false;
                profil.MFiltereKarteikarten("6. Phase", "", true);

                label26.Text ="Es befinden sich " + profil.gefiltert.Count + " Karteikarten in der 6. Phase!";
                if (profil.gefiltert.Count > 0)
                    buttonphase6entfernen.Enabled = true;
                else
                    buttonphase6entfernen.Enabled = false;

                label27.Visible = label28.Visible=false;
                textBoxhintergrundfarbeaußen.Text = profil.einstellungen.Hintergrundfarbeaußen;
                textBoxhintergrundfarbeinnen.Text = profil.einstellungen.Hintergrundfarbeinnen;

                if (!File.Exists(@"C:\Phase6\safe_" + profil.name + ".csv"))
                {
                    labelsicherungstatus.Text = "Es ist keine Sicherung vorhanden.";
                    buttonsicherunganlegen.Enabled = true;
                    buttonsicherungeinlesen.Enabled = false;
                    buttonsicherunganlegen.Text = "Sicherung anlegen";
                }
                else
                {
                    labelsicherungstatus.Text = "Letzte Sicherung am " + File.GetLastWriteTime(@"C:\Phase6\safe_" + profil.name + ".csv") + ".";
                    buttonsicherunganlegen.Enabled =  buttonsicherungeinlesen.Enabled =true;
                    buttonsicherunganlegen.Text = "Aktualisieren";
                }

                profil.MFindeMehrfache();
                if (profil.mehrfache.Count > 0)
                {
                    label29.Text = "Es sind Karteikarten mehrfach vorhanden.";
                    buttonmehrfache.Enabled = true;
                }
                else
                {
                    buttonmehrfache.Enabled = false;
                    label29.Text = "Es sind keine mehrfachen Karteikarten vorhanden.";
                }

                toolStrip1.Visible = true;
                toolStripLabelnotiz.Visible = false;
                toolStripButtonDefault.Visible = true;
                toolStripButtonDefault.ForeColor = Color.FromArgb(Convert.ToInt32(profil.einstellungen.Hintergrundfarbeinnen));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void tabPageEinstellungen_Enter(object sender, EventArgs e)
        {
            if (profil != null)
                MEinstellungenVoreinstellungen();
            else
            {
                MessageBox.Show("Bitte melden Sie sich zuerst an.");
                tabControl1.SelectedTab = tabPageHome;
            }
        }
        
        private void buttoneinstellungenübernehmen_Click(object sender, EventArgs e)
        {
            int iout;
            if (Int32.TryParse(textBoxeinstellungenphase1.Text, out iout) && Int32.TryParse(textBoxeinstellungenphase2.Text, out iout)
                && Int32.TryParse(textBoxeinstellungenphase3.Text, out iout) && Int32.TryParse(textBoxeinstellungenphase4.Text, out iout)
                && Int32.TryParse(textBoxeinstellungenphase5.Text, out iout) && Int32.TryParse(textBoxeinstellungenphase6.Text, out iout))
            {
                if (Convert.ToInt32(textBoxeinstellungenphase1.Text) < Convert.ToInt32(textBoxeinstellungenphase2.Text) &&
                    Convert.ToInt32(textBoxeinstellungenphase2.Text) < Convert.ToInt32(textBoxeinstellungenphase3.Text) &&
                    Convert.ToInt32(textBoxeinstellungenphase3.Text) < Convert.ToInt32(textBoxeinstellungenphase4.Text) &&
                    Convert.ToInt32(textBoxeinstellungenphase4.Text) < Convert.ToInt32(textBoxeinstellungenphase5.Text) &&
                    Convert.ToInt32(textBoxeinstellungenphase5.Text) < Convert.ToInt32(textBoxeinstellungenphase6.Text)
                    )
                {
                    // Einstellungen anpassen
                    // Änderungen übernehmen und Datei neuschreiben         
                    profil.einstellungen.abstände[1] = Convert.ToInt32(textBoxeinstellungenphase1.Text);
                    profil.einstellungen.abstände[2] = Convert.ToInt32(textBoxeinstellungenphase2.Text);
                    profil.einstellungen.abstände[3] = Convert.ToInt32(textBoxeinstellungenphase3.Text);
                    profil.einstellungen.abstände[4] = Convert.ToInt32(textBoxeinstellungenphase4.Text);
                    profil.einstellungen.abstände[5] = Convert.ToInt32(textBoxeinstellungenphase5.Text);
                    profil.einstellungen.abstände[6] = Convert.ToInt32(textBoxeinstellungenphase6.Text);

                    profil.einstellungen.MDateiAktualisieren();

                    buttoneinstellungenübernehmen.Enabled = false;
                    textBoxeinstellungenphase1.Focus();
                }
                else
                    MessageBox.Show("Der Wert einer Phase muss kleiner sein als der Wert der nachfolgenden Phasen!");
            }
            else
                MessageBox.Show("Die Textboxen müssen gültige Zahlwerte enthalten!");
        }

        #endregion

        #region Statistik

        private void tabPageStatistik_Enter(object sender, EventArgs e)
        {
            // ist ein Profil vorhanden?
            if (profil != null)
            {
                if (comboBoxstatistikanzeige.SelectedIndex == -1)
                    comboBoxstatistikanzeige.SelectedIndex = 0;
                else
                    MAnzeigeAktualisieren();
            }
            else
            {
                MessageBox.Show("Bitte melden Sie sich zuerst an.");
                tabControl1.SelectedTab = tabPageHome;
            }
        }

        private void MAnzeigeAktualisieren()
        {
            // wurde gültiges Item ausgewählt?
            if (comboBoxstatistikanzeige.SelectedItem != null)
            {
                switch (comboBoxstatistikanzeige.SelectedItem.ToString())
                {
                    case "Phasenweise":
                        // Listview anpassen
                        columnHeaderoption.Text = "Phase";

                        Dictionary<int, int> dictphase = new Dictionary<int, int>();

                        for (int a = 1; a < 7; a++)
                            dictphase.Add(a, 0);

                        foreach (KeyValuePair<int, ClassKarteikarte> pair in profil.karteikarten)
                            dictphase[pair.Value.Phase]++;
                       
                        // Füllen des Charts
                        chart1.Series.Clear();
                        chart1.Series.Add("Phasen");
                        chart1.Series["Phasen"].Points.AddXY("1. Phase", dictphase[1]);
                        chart1.Series["Phasen"].Points.AddXY("2. Phase", dictphase[2]);
                        chart1.Series["Phasen"].Points.AddXY("3. Phase", dictphase[3]);
                        chart1.Series["Phasen"].Points.AddXY("4. Phase", dictphase[4]);
                        chart1.Series["Phasen"].Points.AddXY("5. Phase", dictphase[5]);
                        chart1.Series["Phasen"].Points.AddXY("6. Phase", dictphase[6]);

                        chart1.ChartAreas[0].RecalculateAxesScale();


                        // Liste itemweise füllen
                        listViewstatistik.Items.Clear();
                        foreach (KeyValuePair<int, int> pair in dictphase)
                        {
                            ListViewItem item = new ListViewItem(pair.Key.ToString() + ". Phase");
                            item.SubItems.Add(pair.Value.ToString());

                            listViewstatistik.Items.Add(item);
                        }
                        toolStrip1.Visible = false;
                        break;

                    case "Kategorienweise":

                        columnHeaderoption.Text = "Kategorie";

                        Dictionary<string, int> dictkategorie = new Dictionary<string, int>();

                        foreach (KeyValuePair<int, ClassKarteikarte> pair in profil.karteikarten)
                        {
                            if (dictkategorie.ContainsKey(pair.Value.Kategorie))
                                dictkategorie[pair.Value.Kategorie]++;
                            else
                                dictkategorie.Add(pair.Value.Kategorie, 1);
                        }


                        // Füllen des Charts
                        chart1.Series.Clear();

                        chart1.Series.Add("Kategorie");
                        foreach (KeyValuePair<string, int> list in dictkategorie)
                            chart1.Series["Kategorie"].Points.AddXY(list.Key, list.Value);

                        chart1.ChartAreas[0].RecalculateAxesScale();

                        // Liste itemweise füllen
                        listViewstatistik.Items.Clear();
                        foreach (KeyValuePair<string, int> pair in dictkategorie)
                        {
                            ListViewItem item = new ListViewItem(pair.Key);
                            item.SubItems.Add(pair.Value.ToString());

                            listViewstatistik.Items.Add(item);
                        }

                        toolStrip1.Visible = true;
                        if (dictkategorie.Count > 1)
                            toolStripLabelnotiz.Text = "Es sind " + dictkategorie.Count.ToString() + " Kategorien vorhanden.";
                        else if (dictkategorie.Count == 1)
                            toolStripLabelnotiz.Text = "Es ist 1 Kategorie vorhanden.";
                        else
                            toolStripLabelnotiz.Text = "Es sind keine Kategorien vorhanden.";

                        break;
                }
                chart1.Focus();
            }
        }

        private void comboBoxstatistikanzeige_SelectedIndexChanged(object sender, EventArgs e)
        {
            MAnzeigeAktualisieren();
        }

        #endregion
               

        

        private void buttondateieinlesen_Click(object sender, EventArgs e)
        {
            timerspeichern.Stop();
            if (comboBoxeingabealleKategorien.SelectedIndex == -1 && Program.MLeerstellenEntfernen(textBoxneuekategorie.Text) == "")
            {
                MessageBox.Show("Bitte wählen Sie eine Kategorie für die Inhalte der Datei aus!");
                return;
            }

            // Enthält einzulesende Datei Semikolon?
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            string pfad = openFileDialog1.FileName;

            if (!File.Exists(pfad))
            {
                MessageBox.Show("Bitte wählen Sie einen gültigen Pfad aus!");
                return;
            }

            StreamReader sr = new StreamReader(pfad, Program.GetEncoding(pfad));
            int izeile = 1;
            while (!sr.EndOfStream)
            {
                string zeile = sr.ReadLine();
                if (zeile.Split(';').Length > 2)
                {
                    MessageBox.Show("Die einzulesende Datei enthält zu viele Semikolon in der Zeile: " + izeile);
                    return;
                }
                izeile++;
            }
            sr.Close();

            string kategorie = comboBoxeingabealleKategorien.Text;

            if (textBoxneuekategorie.Text != "")
                kategorie = textBoxneuekategorie.Text;

            FormVorhanden fv = new FormVorhanden("[" + kategorie + "] als Kategorie wählen?");
            fv.ShowDialog();

            
            if (fv.GetErstellen())
            {
                fv = new FormVorhanden("Karteikarten mehrfach erlauben?");
                fv.ShowDialog();
                
                try
                {
                    // Vorbeugen, dieselbe Karteikarte erneut zu übernehmen
                    List<string> vorhanden = new List<string>();
                    foreach (KeyValuePair<int, ClassKarteikarte> pair in profil.karteikarten)
                    {
                        if (!vorhanden.Contains(pair.Value.Frage))
                            vorhanden.Add(pair.Value.Frage);

                        if (!vorhanden.Contains(pair.Value.Antwort))
                            vorhanden.Add(pair.Value.Antwort);
                    }

                    sr = new StreamReader(pfad, Encoding.Default);
                    // Frage;Antwort
                    while (!sr.EndOfStream)
                    {
                        string zeile = sr.ReadLine();

                        if (Program.MLeerstellenEntfernen(zeile) != "")
                        {
                            string[] split = zeile.Split(';');

                            if (split.Length == 2)
                            {
                                if (fv.GetErstellen() || !fv.GetErstellen() && !vorhanden.Contains(split[0]) && !vorhanden.Contains(split[1]))
                                {
                                    ClassKarteikarte karteikarte = new ClassKarteikarte();
                                    karteikarte.Kategorie = kategorie;
                                    karteikarte.ID = profil.karteikarten.Count;
                                    karteikarte.Frage = split[0];
                                    karteikarte.Antwort = split[1];
                                    karteikarte.Phase = 1;
                                    karteikarte.Richtige = 0;
                                    karteikarte.Falsche = 0;
                                    karteikarte.Datum = DateTime.Now;
                                    profil.karteikarten.Add(profil.karteikarten.Count, karteikarte);
                                }
                            }
                        }
                    }
                    sr.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                timerspeichern.Start();
            }
        }

        private void timerspeichern_Tick(object sender, EventArgs e)
        {
            if (profil != null)
            {
                // Schreibe in Datei
                string file = @"C:\Phase6\" + profil.name + ".csv";
                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine("Kategorie;Frage;Antwort;Phase;Richtige;Falsche;Datum");

                foreach (KeyValuePair<int, ClassKarteikarte> pair in profil.karteikarten)
                    sw.WriteLine(pair.Value.Kategorie + ";" + pair.Value.Frage + ";" + pair.Value.Antwort + ";" + pair.Value.Phase + ";" + pair.Value.Richtige + ";" + pair.Value.Falsche + ";" + pair.Value.Datum.ToShortDateString());

                sw.Close();
            }
        }

        private void toolStripTextBoxsuchfunktion_Enter(object sender, EventArgs e)
        {
            if (((ToolStripTextBox)sender).Text == "Suchen nach")
                ((ToolStripTextBox)sender).Text = "";
        }

        private void textBoxeinstellungenphase1_TextChanged(object sender, EventArgs e)
        {
            MEinstellungenPrüfeButton();
        }

        private void MEinstellungenPrüfeButton()
        {
            if (textBoxeinstellungenphase1.Text != "" && textBoxeinstellungenphase2.Text != ""
                && textBoxeinstellungenphase3.Text != "" && textBoxeinstellungenphase4.Text != ""
                && textBoxeinstellungenphase5.Text != "" && textBoxeinstellungenphase6.Text != "")            
                buttoneinstellungenübernehmen.Enabled = true;            
            else
                buttoneinstellungenübernehmen.Enabled = false;
        }

        private void textBoxeinstellungenphase2_TextChanged(object sender, EventArgs e)
        {
            MEinstellungenPrüfeButton();
        }

        private void textBoxeinstellungenphase3_TextChanged(object sender, EventArgs e)
        {
            MEinstellungenPrüfeButton();
        }

        private void textBoxeinstellungenphase4_TextChanged(object sender, EventArgs e)
        {
            MEinstellungenPrüfeButton();
        }

        private void textBoxeinstellungenphase5_TextChanged(object sender, EventArgs e)
        {
            MEinstellungenPrüfeButton();
        }

        private void textBoxeinstellungenphase6_TextChanged(object sender, EventArgs e)
        {
            MEinstellungenPrüfeButton();
        }

        private void buttonphase6entfernen_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<int,ClassKarteikarte> pair in profil.gefiltert)
                profil.karteikarten.Remove(pair.Key);

            profil.MAlleAktualiseren();
            MessageBox.Show("Alle Karteikarten aus der 6. Phase wurden entfernt!");
        }

        private void buttonsicherunganlegen_Click(object sender, EventArgs e)
        {
            if (profil != null)
            {
                try
                {
                    // Schreibe in Datei
                    string file = @"C:\Phase6\safe_" + profil.name + ".csv";
                    StreamWriter sw = new StreamWriter(file);
                    sw.WriteLine("Kategorie;Frage;Antwort;Phase;Richtige;Falsche;Datum");

                    foreach (KeyValuePair<int, ClassKarteikarte> pair in profil.karteikarten)
                        sw.WriteLine(pair.Value.Kategorie + ";" + pair.Value.Frage + ";" + pair.Value.Antwort + ";" + pair.Value.Phase + ";" + pair.Value.Richtige + ";" + pair.Value.Falsche + ";" + pair.Value.Datum.ToShortDateString());

                    sw.Close();
                    label27.Visible = true;

                    if (buttonsicherunganlegen.Text == "Aktualisieren")
                        label27.Text = "Sicherung wurde aktualisiert.";
                    else
                    {
                        label27.Text = "Sicherung wurde angelegt.";
                        buttonsicherunganlegen.Text = "Aktualisieren";
                    }

                    buttonsicherungeinlesen.Enabled = true;
                    labelsicherungstatus.Text = "Letzte Sicherung am " + File.GetLastWriteTime(@"C:\Phase6\safe_" + profil.name + ".csv");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Datei konnte nicht eingelesen werden wegen:\n"+ex.Message);
                }
            }
            else
                MessageBox.Show("Bitte anmelden!");
        }

        private void buttonsicherungeinlesen_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\Phase6\safe_" + profil.name + ".csv"))
            {
                profil.MHoleKarteikartenAusDatei(@"C:\Phase6\safe_" + profil.name + ".csv");
                label28.Visible = true;
            }          
        }

        private void listBoxsicherungen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxsicherungen.SelectedIndex != -1)
                buttonhomesicherung.Enabled = buttonsicherunglöschen.Enabled = true;
            else
                buttonhomesicherung.Enabled = buttonsicherunglöschen.Enabled = false;
        }

        private void buttonhomesicherung_Click(object sender, EventArgs e)
        {
            if (listBoxsicherungen.SelectedIndex != -1)
            {
                string datei = listBoxsicherungen.SelectedItem.ToString().Split('_')[1];
                File.Copy(@"C:\Phase6\safe_" + datei + ".csv", @"C:\Phase6\" + datei + ".csv",true);
            }

            MHomeVoreinstellungen();
        }

        private void buttonsicherunglöschen_Click(object sender, EventArgs e)
        {
            if (listBoxsicherungen.SelectedIndex != -1)
            {
                string datei = listBoxsicherungen.SelectedItem.ToString().Split('_')[1];
                File.Delete(@"C:\Phase6\safe_" + datei + ".csv");
            }

            MHomeVoreinstellungen();
        }

        private void tabPageHome_Enter(object sender, EventArgs e)
        {
            MHomeVoreinstellungen();
        }

        private void toolStripTextBoxsuchfunktion_TextChanged(object sender, EventArgs e)
        {
            MSuchfunktion();
        }

        private void listBoxsicherungen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (listBoxsicherungen.SelectedIndex != -1)
                    buttonhomesicherung_Click(sender, e);
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (listBoxsicherungen.SelectedIndex != -1)
                    buttonsicherunglöschen_Click(sender, e);
            }
        }

        private void listBoxsicherungen_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxsicherungen.SelectedIndex != -1)
                buttonhomesicherung_Click(sender, e);
        }
        

        private void MRekursiveFormGestalten(Control control)
        {
            foreach (Control c in control.Controls.OfType<TextBox>())
                c.Font = new Font(profil.einstellungen.Schriftart, profil.einstellungen.Schriftgröße);
            foreach (Control c in control.Controls.OfType<Label>())
                c.Font = new Font(profil.einstellungen.Schriftart, profil.einstellungen.Schriftgröße);
            foreach (Control c in control.Controls.OfType<RichTextBox>())
                c.Font = new Font(profil.einstellungen.Schriftart, profil.einstellungen.Schriftgröße);
            foreach (Control c in control.Controls.OfType<ListBox>())
                c.Font = new Font(profil.einstellungen.Schriftart, profil.einstellungen.Schriftgröße);
            foreach (Control c in control.Controls.OfType<ComboBox>())
                c.Font = new Font(profil.einstellungen.Schriftart, profil.einstellungen.Schriftgröße);
            foreach (Control c in control.Controls.OfType<CheckBox>())
                c.Font = new Font(profil.einstellungen.Schriftart, profil.einstellungen.Schriftgröße);
            foreach (Control c in control.Controls.OfType<Button>())
                c.Font = new Font(profil.einstellungen.Schriftart, profil.einstellungen.Schriftgröße);
            foreach (Control c in control.Controls.OfType<ListView>())
                c.Font = new Font(profil.einstellungen.Schriftart, profil.einstellungen.Schriftgröße);

            foreach (Control c in control.Controls.OfType<GroupBox>())
            {
                MRekursiveFormGestalten(c);
                c.Font = new Font(profil.einstellungen.Schriftart, profil.einstellungen.Schriftgröße);
            }            
        }
        

        private void buttonmehrfache_Click(object sender, EventArgs e)
        {
            FormMehrfach mf = new FormMehrfach(profil.mehrfache, Color.FromArgb(Convert.ToInt32( profil.einstellungen.Hintergrundfarbeaußen)), Color.FromArgb(Convert.ToInt32(profil.einstellungen.Hintergrundfarbeinnen)));
            mf.ShowDialog();
            foreach (ClassKarteikarte karte in mf.löschen)
            {
                profil.karteikarten.Remove(karte.ID);
            }
            profil.MAlleAktualiseren();

            if (mf.löschen.Count == 0)
                MessageBox.Show("Es wurden keine Karteikarten gelöscht.");
            else if (mf.löschen.Count == 1)
                MessageBox.Show("Es wurde eine Karteikarte gelöscht.");
            else
                MessageBox.Show("Es wurden " + mf.löschen.Count + " Karteikarten gelöscht.");

            MEinstellungenVoreinstellungen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            profil.einstellungen.Hintergrundfarbeaußen = textBoxhintergrundfarbeaußen.Text = colorDialog1.Color.ToArgb().ToString();
            MHintergrundfarbenAnpassen();
            profil.einstellungen.MDateiAktualisieren();
        }

        private void MHintergrundfarbenAnpassen()
        {
            try
            {
                Color außen;

                
                int iout;

                if (!Int32.TryParse(profil.einstellungen.Hintergrundfarbeaußen, out iout))
                    außen = Color.FromName(profil.einstellungen.Hintergrundfarbeaußen);
                else
                    außen = Color.FromArgb(Convert.ToInt32(profil.einstellungen.Hintergrundfarbeaußen));
                
                if (außen != null)
                {
                    this.BackColor = außen;

                    try
                    {

                        Color innen;

                        if (!Int32.TryParse(profil.einstellungen.Hintergrundfarbeinnen, out iout))
                            innen = Color.FromName(profil.einstellungen.Hintergrundfarbeinnen);
                        else
                            innen = Color.FromArgb(Convert.ToInt32(profil.einstellungen.Hintergrundfarbeinnen));


                        for (int a = 0; a < tabControl1.TabPages.Count; a++)
                            tabControl1.TabPages[a].BackColor = innen;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonhintergrundfarbeinnen_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            profil.einstellungen.Hintergrundfarbeinnen = textBoxhintergrundfarbeinnen.Text = colorDialog1.Color.ToArgb().ToString();
            MHintergrundfarbenAnpassen();
            profil.einstellungen.MDateiAktualisieren();
        }

        private void toolStripButtonDefault_Click(object sender, EventArgs e)
        {
            profil.einstellungen.Hintergrundfarbeinnen = profil.einstellungen.DefaultHintergrundfarbeinnen;
            profil.einstellungen.Hintergrundfarbeaußen = profil.einstellungen.DefaultHintergrundfarbeaußen;

            textBoxeinstellungenphase1.Text = profil.einstellungen.Defaultabstände[1].ToString();
            textBoxeinstellungenphase2.Text = profil.einstellungen.Defaultabstände[2].ToString();
            textBoxeinstellungenphase3.Text = profil.einstellungen.Defaultabstände[3].ToString();
            textBoxeinstellungenphase4.Text = profil.einstellungen.Defaultabstände[4].ToString();
            textBoxeinstellungenphase5.Text = profil.einstellungen.Defaultabstände[5].ToString();
            textBoxeinstellungenphase6.Text = profil.einstellungen.Defaultabstände[6].ToString();


            for (int index = 1; index < 7; index++)
            {
                profil.einstellungen.abstände[index] = profil.einstellungen.Defaultabstände[index];
                index++;
            }

            MHintergrundfarbenAnpassen();
            profil.einstellungen.MDateiAktualisieren();
        }

        private void buttonumbennen_Click(object sender, EventArgs e)
        {
            List<string> listverboteneprofile = new List<string>();
            List<string> listvorhandeneprofile = new List<string>();

            foreach (string item in listBoxvorhandeneprofile.Items)
            {
                listverboteneprofile.Add(item);
                listvorhandeneprofile.Add(item);
            }
            
            foreach (string item in listBoxsicherungen.Items)
                listverboteneprofile.Add(item.Split('_')[1]);

            string profilvorher = listBoxvorhandeneprofile.SelectedItem.ToString();
            FormUmbennen fu = new FormUmbennen(profilvorher, listverboteneprofile);
            fu.ShowDialog();

            string profilnachher = fu.MGetProfilname();
            if (profilnachher != profilvorher)
            {
                // Datei ändern
                File.Copy("C:\\Phase6\\" + profilvorher + ".csv","C:\\Phase6\\" + profilnachher + ".csv");

                if (File.Exists("C:\\Phase6\\" + profilvorher + ".csv"))
                    File.Delete("C:\\Phase6\\" + profilvorher + ".csv");

                // Listbox aktualisieren
                listBoxvorhandeneprofile.Items.Clear();
                foreach (string item in listvorhandeneprofile)
                {                  
                        if (item != profilvorher)
                            listBoxvorhandeneprofile.Items.Add(item);
                        else
                            listBoxvorhandeneprofile.Items.Add(profilnachher);
                    
                }
            }

            listBoxvorhandeneprofile.SelectedItem = profilnachher;

        }

        //eventueller Vollbildmodus
        //private void MFensteränderung()
        //{
        //    // Home
        //    tabControl1.Height = this.Height - (10 * tabControl1.Margin.Top) -toolStrip1.Height;
        //    tabControl1.Width = this.Width - (4 * tabControl1.Margin.Left);
        //    tabControl1.Location = new Point(tabControl1.Margin.Left, tabControl1.Margin.Top);
        //    groupBox2.Location = new Point(groupBox1.Location.X, groupBox1.Location.Y + groupBox1.Height + groupBox2.Margin.Top);
        //    groupBox2.Width = groupBox1.Width;
        //    groupBox2.Height = tabPageHome.Height - groupBox1.Height - (groupBox2.Margin.Top * 4);
        //    listBoxvorhandeneprofile.Location = new Point(listBoxvorhandeneprofile.Margin.Left, listBoxvorhandeneprofile.Margin.Top + Font.Height);
        //    listBoxvorhandeneprofile.Height = groupBox2.Height - Font.Height - listBoxvorhandeneprofile.Margin.Top - listBoxvorhandeneprofile.Margin.Bottom;
        //    listBoxvorhandeneprofile.Width = groupBox3.Width- buttonanmelden.Width - (buttonanmelden.Margin.Left * 2) - (listBoxvorhandeneprofile.Margin.Left * 2);

        //    // Eingabe
        //    buttonkarteikartehinzufügen.Location = new Point(tabPageEingabe.Width - buttonkarteikartehinzufügen.Margin.Right - buttonkarteikartehinzufügen.Width,tabPageEingabe.Height - buttonkarteikartehinzufügen.Height - buttonkarteikartehinzufügen.Margin.Bottom);
        //    label7.Location = new Point(25, buttonkarteikartehinzufügen.Location.Y);
        //    label6.Location = new Point(25, tabPageEingabe.Height - (label7.Height * 2) - (label7.Margin.Bottom * 2));
        //    comboBoxeingabealleKategorien.Location = new Point(25 + label6.Width + (2*label6.Margin.Right) + comboBoxeingabealleKategorien.Margin.Left,label6.Location.Y);
        //    textBoxneuekategorie.Location = new Point(25 + label7.Width + (2 * label7.Margin.Right) + textBoxneuekategorie.Margin.Left, label7.Location.Y);
        //    richTextBoxeingabefrage.Size = new Size(269 * Convert.ToInt32(this.Font.Size) / 9, 194 * Convert.ToInt32(this.Font.Size) / 9);
        //    richTextBoxeingabefrage.Location = new Point(25,label2.Location.Y + label2.Margin.Bottom + 20);
        //    richTextBoxeingabeantwort.Location = new Point(50 + richTextBoxeingabefrage.Width, richTextBoxeingabefrage.Location.Y);

        //    // Bibliothek
        //    listViewbibliothek.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Left);



        //}

        //private void Form1_SizeChanged(object sender, EventArgs e)
        //{
        //    // Vollbildmodus ausgewählt?
        //    if (this.Height == System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height - 24 && this.Width == System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width + 6)
        //        this.Font = new Font(new FontFamily("Microsoft Sans Serif"), 10, FontStyle.Regular);
        //    else
        //    {
        //        this.Size = MinimumSize;
        //        this.Font = new Font(new FontFamily("Microsoft Sans Serif"),9, FontStyle.Regular);
        //    }


        //    //MFensteränderung();            
        //}
    }
}