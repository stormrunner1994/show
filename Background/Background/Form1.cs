using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using WMPLib;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Background
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            stdicon = this.Icon;
            showContextMenu = false;
            string rootpfad = Application.StartupPath;
            rootpfad += "\\BackGround";
            dictspeicherpfade = new Dictionary<string, string>();
            dictspeicherpfade.Add("Root", rootpfad);
            dictspeicherpfade.Add("Termine", rootpfad + "\\Termine.csv");
            dictspeicherpfade.Add("Aufgaben", rootpfad + "\\Aufgaben.csv");
            dictspeicherpfade.Add("Notizen", rootpfad + "\\Notizen.csv");
            dictspeicherpfade.Add("Einstellungen", rootpfad + "\\Einstellungen.csv");
            dictspeicherpfade.Add("Musik", rootpfad + "\\Musik.csv");
            dictspeicherpfade.Add("Timer", rootpfad + "\\Timer.csv");
            dictspeicherpfade.Add("Pfade", rootpfad + "\\Pfade.csv");
            ClassÜbergreifend.dictspeicherpfade = dictspeicherpfade;
        }
        
        private Icon stdicon;
        private StreamWriter sw;
        private StreamReader sr;
        private List<Termin> termine;
        private Dictionary<int, string> dictaufgaben;
        private Dictionary<int, string> dictmusik;
        private DateTime aktuell = DateTime.Now;
        private Dictionary<int, string> dictfehlerhaftepfade;
        private int terminindex = -1;
        private string[] abspielbare;
        private bool showContextMenu;
        private bool bmusikspielt;
        private Windows window;
        private bool vlcmeldung = true;
        private FormPCModusÄndern fmä = new FormPCModusÄndern();
        private Stopwatch swmusik = new Stopwatch();
        private Dictionary<string, string> dictspeicherpfade;
        private FormAufgabenliste fal;
        private string excelpfad;

        private void buttonhelp_Click(object sender, EventArgs e)
        {
            string text = "Termine: Auflistung aller Termine\nRechtsklick: Hinzufügen neuer Termine oder Bearbeiten und Entfernen bestehender Termine\n";
            text += "Doppellinksklick: Beschreibung aufrufen\n\nAufgaben: Auflistung aller Aufgaben\n";
            text += "";
            MessageBox.Show(text);
        }

        private void MVoreinstellung()
        {
            // Ordner und Dateien unter C
            if (!Directory.Exists(dictspeicherpfade["Root"]))
                Directory.CreateDirectory(dictspeicherpfade["Root"]);
            // Termine
            if (!File.Exists(dictspeicherpfade["Termine"]))
            {
                sw = new StreamWriter(dictspeicherpfade["Termine"]);
                sw.WriteLine("Index;GruppenIndex;Datum;Uhrzeit;Grund;Beschreibung;Erinnert");
                sw.Close();
            }
            // Aufgaben            
            // Notizen
            if (!File.Exists(dictspeicherpfade["Notizen"]))
            {
                sw = new StreamWriter(dictspeicherpfade["Notizen"]);
                sw.Close();
            }
            // Musik
            if (!File.Exists(dictspeicherpfade["Musik"]))
            {
                sw = new StreamWriter(dictspeicherpfade["Musik"]);
                sw.WriteLine("F1;F2;F3\nfalse");
                sw.Close();
            }
            // Timer
            if (!File.Exists(dictspeicherpfade["Timer"]))
            {
                sw = new StreamWriter(dictspeicherpfade["Timer"]);
                sw.Close();
            }// Pfade
            if (!File.Exists(dictspeicherpfade["Pfade"]))
            {
                sw = new StreamWriter(dictspeicherpfade["Pfade"]);
                sw.Close();
            }

            // Einstellungen
            if (!File.Exists(dictspeicherpfade["Einstellungen"]))
            {
                sw = new StreamWriter(dictspeicherpfade["Einstellungen"]);
                RegistryKey wallPaper = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false);
                string WallpaperPath = wallPaper.GetValue("WallPaper").ToString();
                wallPaper.Close();
                sw.WriteLine(WallpaperPath);
                sw.WriteLine("");
                sw.Close();
            }
            else 
            {
                StreamReader sr = new StreamReader(dictspeicherpfade["Einstellungen"]);
                sr.ReadLine();
                sr.ReadLine();
                excelpfad = sr.ReadLine();
                sr.Close();
            }


            window = new Windows(dictspeicherpfade);
            // Dictionary mit aktuellen Dateien füllen
            //dictpfade = MPfadeInDict();

            MTermineEinlesen();
            dictaufgaben = MAufgabenInDict();
            MMusikInDict();
            bmusikspielt = Convert.ToBoolean(dictmusik[1]);


            // Timer für Hauptfenster
            timeraktuell.Start();

            // Timer für Keys
            timerkeys.Start();

            // Soll Timer erinnern?
            sr = new StreamReader(dictspeicherpfade["Timer"]);
            sr.ReadLine();
            List<string> listtimer = new List<string>();

            while (!sr.EndOfStream)
            {
                string zeile = sr.ReadLine();
                if (zeile != "")
                    listtimer.Add(zeile);
            }
            sr.Close();

            if (listtimer.Count > 0)
                new FormTimer(listtimer, dictspeicherpfade).Show();

            // Felder füllen
            MFülleNotizenFeld();
            MFülleTermineFeld();
            MFülleAufgabenFeld();

            // ContextMenuItems
            contextMenuStriptermin.Items.Add("Neuer Termin");
            contextMenuStriptermin.Items.Add("Termin bearbeiten");
            contextMenuStriptermin.Items.Add("Termin löschen");
            contextMenuStriptermin.ItemClicked += new ToolStripItemClickedEventHandler(contextMenuStriptermin_ItemClicked);

            contextMenuStriptaskleiste.Items.Add("Musik anhalten");
            contextMenuStriptaskleiste.Items.Add("nächster Titel");
            contextMenuStriptaskleiste.Items.Add("vorheriger Titel");
            contextMenuStriptaskleiste.Items.Add("Titan Quest schließen");
            contextMenuStriptaskleiste.Items.Add("TM schließen");


            /*
            // TMX
            FormTmx ft = new FormTmx(true);
            ft.Show();
            if (!ft.GetNurErster())
                MessageBox.Show("Es existiert eine TMX Map, auf der nicht mehr der 1. Rekord belegt ist.");
        */
        }

        private void MMusikInDict()
        {
            dictmusik = new Dictionary<int, string>();
            sr = new StreamReader(dictspeicherpfade["Musik"]);
            while (!sr.EndOfStream)
            {
                string song = sr.ReadLine();
                if (song != "")
                    dictmusik.Add(dictmusik.Count, song);
            }
            sr.Close();

            if (Convert.ToBoolean(dictmusik[1]))
                abspielenToolStripMenuItem.Enabled = true;
            else
                abspielenToolStripMenuItem.Enabled = false;
        }

        private void MFülleTermineFeld()
        {
            listViewtermine.Items.Clear();
            DateTime now = DateTime.Now;

            // Liste itemweise füllen
            foreach (Termin t in termine)
            {
                ListViewItem item = new ListViewItem(t.datum.ToShortDateString());
                item.SubItems.Add(t.uhrzeit);
                item.SubItems.Add(t.grund);
                item.SubItems.Add(t.beschreibung);

                // Hintergrundfarbe abhängig der Dringlichkeit
                DateTime termin = t.datum;
                string[] split = t.uhrzeit.Split(':');
                termin = termin.AddHours(Convert.ToInt32(split[0]));
                termin = termin.AddMinutes(Convert.ToInt32(split[1]));
                termin = termin.AddSeconds(Convert.ToInt32(split[2]));
                double seconds = (termin - now).TotalSeconds;

                if (seconds < 0)
                    item.BackColor = Color.DarkRed;
                if (seconds >= 0 && seconds < 24 * 3600)
                    item.BackColor = Color.Red;
                else if (seconds > 23 * 3600 && seconds < 72 * 3600)
                    item.BackColor = Color.Yellow;

                listViewtermine.Items.Add(item);
            }
        }

        private void MFülleAufgabenFeld()
        {
            listBoxaufgaben.Items.Clear();

            // Items itemweise füllen

            foreach (KeyValuePair<int, string> pair in dictaufgaben)
                listBoxaufgaben.Items.Add(pair.Value);
        }

        private void MFülleNotizenFeld()
        {
            string datei;
            sr = new StreamReader(dictspeicherpfade["Notizen"]);

            // erste Zeile überspringen
            sr.ReadLine();
            datei = sr.ReadToEnd();
            sr.Close();

            if (datei.Length > 2)
            {
                while (true)
                {
                    if (datei[datei.Length - 1] == '\n' || datei[datei.Length - 1] == '\r')
                    {
                        datei = datei.Remove(datei.Length - 1, 1);
                        if (datei == "")
                            break;

                    }
                    else
                        break;
                }
            }

            richTextBoxnotizen.Text = datei;
        }

        #region Aus Datei in Dictionary

        private Dictionary<int, string> MPfadeInDict()
        {
            dictfehlerhaftepfade = new Dictionary<int, string>();
            Dictionary<int, string> ausgabe = new Dictionary<int, string>();
            string datei;

            // Standardordner hinzufügen
            ausgabe.Add(-1, dictspeicherpfade["Root"]);

            try
            {
                sr = new StreamReader(dictspeicherpfade["Pfade"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // erste Zeile überspringen
            sr.ReadLine();
            datei = sr.ReadToEnd();
            sr.Close();

            if (datei.Length > 0)
            {
                while (datei[datei.Length - 1] == '\r' || datei[datei.Length - 1] == '\n')
                {
                    datei = datei.Remove(datei.Length - 1, 1);
                    if (datei.Length == 0)
                        break;
                }
            }

            string[] zeile = datei.Split('\n');
            if (zeile[0] != "")
            {
                for (int a = 0; a < zeile.Length; a++)
                {
                    if (Directory.Exists(zeile[a]))
                    {
                        try
                        {
                            // Unterordner sind zuerstellen, falls nicht vorhanden
                            if (!Directory.Exists(zeile[a] + "\\BackGround"))
                                Directory.CreateDirectory(zeile[a] + "\\BackGround");
                            if (!Directory.Exists(zeile[a] + "\\BackGround\\Dateien"))
                                Directory.CreateDirectory(zeile[a] + "\\BackGround\\Dateien");

                            ausgabe.Add(a, zeile[a]);
                        }
                        catch
                        {
                            dictfehlerhaftepfade.Add(dictfehlerhaftepfade.Count, zeile[a]);
                        }
                    }
                }
            }

            return ausgabe;
        }

        private void MTermineEinlesen()
        {
            termine = new List<Termin>();

            sr = new StreamReader(dictspeicherpfade["Termine"]);
            // erste Zeile überspringen
            sr.ReadLine();
            string datei = sr.ReadToEnd();
            sr.Close();

            if (datei.Length > 0)
            {
                while (datei[datei.Length - 1] == '\r' || datei[datei.Length - 1] == '\n')
                {
                    datei = datei.Remove(datei.Length - 1, 1);
                    if (datei.Length == 0)
                        break;
                }
            }

            string[] zeilen = datei.Split('\n');
            if (zeilen[0] != "")
            {
                for (int a = 0; a < zeilen.Length; a++)
                {
                    string zeile = zeilen[a];
                    string[] elemente = zeile.Split(';');
                    Termin t = new Termin(Convert.ToInt32(elemente[0]), Convert.ToInt32(elemente[1]), Convert.ToDateTime(elemente[2]), elemente[3], elemente[4], elemente[5], Convert.ToBoolean(elemente[6]));
                    termine.Add(t);
                }
            }

            // Termine sortieren
            /*if (false)
            {
                sortTermine();
                ClassÜbergreifend.SpeichereTermineInDatei(termine);
            }
            */
        }

        private void sortTermine()
        {
            Dictionary<DateTime, List<Termin>> sorted = new Dictionary<DateTime, List<Termin>>();
            DateTime date = new DateTime();
            List<Termin> uhrzeiten = new List<Termin>();
            foreach (Termin t in termine.OrderBy(i => i.datum))
            {
                // neues Datum
                if (date != t.datum)
                {
                    uhrzeiten.Add(t);
                    date = t.datum;
                    sorted.Add(date, uhrzeiten);
                    uhrzeiten.Clear();
                }
                else
                {
                    uhrzeiten.Add(t);
                }
            }

            termine.Clear();

            foreach (KeyValuePair<DateTime,List<Termin>> pair in sorted)
            {
                foreach (Termin t in pair.Value)
                    termine.Add(t);
            }
        }

        private Dictionary<int, string> MAufgabenInDict()
        {
            Dictionary<int, string> ausgabe = new Dictionary<int, string>();
            string datei;

            sr = new StreamReader(dictspeicherpfade["Aufgaben"]);
            // erste Zeile überspringen
            sr.ReadLine();
            datei = sr.ReadToEnd();
            sr.Close();

            if (datei.Length > 0)
            {
                while (datei[datei.Length - 1] == '\r' || datei[datei.Length - 1] == '\n')
                {
                    datei = datei.Remove(datei.Length - 1, 1);
                    if (datei.Length == 0)
                        break;
                }
            }

            string[] zeile = datei.Split('\n');
            if (zeile[0] != "")
            {
                for (int a = 0; a < zeile.Length; a++)
                    ausgabe.Add(a, zeile[a]);
            }

            return ausgabe;
        }

        #endregion

        void contextMenuStriptermin_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Pressed)
            {
                int i = contextMenuStriptermin.Items.IndexOf(e.ClickedItem);
                FormTermine ft;
                switch (i)
                {
                    case 0: // Neuer Termin
                        ft = new FormTermine(-1, termine);
                        ft.ShowDialog();
                        termine = ft.termine;
                        break;
                    case 1: // Termin bearbeiten
                        ft = new FormTermine(terminindex, termine);
                         ft.ShowDialog();
                        termine = ft.termine;
                        break;
                    case 2: // Termin entfernen
                        MEntferneTermin(listViewtermine.SelectedIndices[0]);
                        break;
                }
                MFülleTermineFeld();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Starte Tempform
                notifyIcon1.Icon = this.Icon;
                notifyIcon1.Visible = true;
                MVoreinstellung();
                timeraktuell_Tick(sender, e);

                bool temporär = false;

                if (richTextBoxnotizen.Text.ToLower().Contains("motivation"))
                    temporär = true;


                // Temporär motivation
                if (temporär)
                {
                    if (File.Exists(excelpfad))
                        Process.Start(excelpfad);

                    string[] files = Directory.GetFiles(@"D:\Sport\Motivation");
                    Random rand = new Random();
                    int irand = rand.Next(0, files.Length); // maxvalue ist filelength -1
                    Process.Start(files[irand]);

                    /*
                    MessageBox.Show("Spürst du nicht:\n1) Waden  --> Fußstrecker\n2)Beine  --> Hüftstrecker\n" +
                    "3) Bauch  --> Käfer\n4) Rücken  --> Paket/AYIX\n5) Arme  --> Liegestütz");
                    */
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool MIstWiederholungstermin(int gruppenindex)
        {
            int zähler = 0;
            foreach (Termin t in termine)
            {
                if (t.gruppenindex == gruppenindex)
                    zähler++;
                if (zähler > 1)
                    return true;
            }
            return false;
        }
        
        private void MEntferneTermin(int löschindex)
        {
            List<int> zulöschendeindices = new List<int>();

            // Ist Wiederholungstermin?
            bool wiederholungstermin = MIstWiederholungstermin(termine[löschindex].gruppenindex);

            // modus 0 = nichts unternehmen 
            // modus 1 = nur dieses löschen
            // modus 2 = alle Wiederholungstermine löschen

            int modus = 1;
            if (wiederholungstermin)
            {
                FormWiederholung fw = new FormWiederholung();
                fw.ShowDialog();
                modus = fw.MGetModus();
            }
            
            if (modus != 0)
            {
               for (int a = 0; a < termine.Count; a++)
               {
                    if (modus == 2)
                    {
                        if (termine[a].gruppenindex == termine[löschindex].gruppenindex)
                            zulöschendeindices.Add(a);
                    }
                    else if (a == löschindex)
                    {
                        zulöschendeindices.Add(a);
                        break;
                    }
                }

                // Nur einen löschen
                if (modus == 1)
                    termine.RemoveAt(zulöschendeindices.First());
                else // mehrere löschen
                {
                    int diff = 0;
                    foreach (int a in zulöschendeindices)
                    {
                        termine.RemoveAt(a-diff);
                        diff++;
                    }
                }            
            }
            ClassÜbergreifend.SpeichereTermineInDatei(termine);
        }

        private void buttonaktualisieren_Click(object sender, EventArgs e)
        {
            // Terminfeld aktualisieren
            MFülleTermineFeld();

            // Aufgaben speichern


            // Notizen aktualisieren
            MNotizenSpeichern();
            
        }

        private string MNamenvonPfadlösen(string nurpfad, string alles)
        {
            // Pfad wegschneiden
            int a = 0;

            while (true)
            {
                if (a < nurpfad.Length)
                {
                    if (alles[0] == nurpfad[a])
                    {
                        alles = alles.Remove(0, 1);
                        a++;
                    }
                    else
                        break;
                }
                else
                    break;
            }

            return alles;
        }
        
        private void MLöscheTempOrdner()
        {
            if (Directory.Exists(dictspeicherpfade["Root"] + @"\TempBackGround"))
            {
                try
                {
                    Directory.Delete(dictspeicherpfade["Root"] + @"\TempBackGround", true);
                }
                catch (UnauthorizedAccessException) // falls Dateien schreibgeschützt sind
                {
                    try
                    {
                        foreach (string file in Directory.GetFiles(dictspeicherpfade["Root"] + @"\TempBackGround"))
                        {
                            File.SetAttributes(file, FileAttributes.Normal);
                            File.Delete(file);
                        }

                        Directory.Delete(dictspeicherpfade["Root"] + @"\TempBackGround", true);
                    }
                    catch
                    {
                        MessageBox.Show("Verdammt");
                    }
                }
            }
        }

        private void MRekursivesLöschen(string pfad)
        {
            string[] directories = Directory.GetDirectories(pfad);
            if (directories.Length > 0)
            {
                foreach (String directory in directories)
                    MRekursivesLöschen(directory);
            }

            string[] files = Directory.GetFiles(pfad);
            if (files.Length > 0)
            {
                foreach (String file in files)
                    File.Delete(file);
            }

            Directory.Delete(pfad, false);
        }

        private void MOrdnerNeu(string pfad)
        {
            // Alten Ordner löschen, neu erstellen und neu füllen
            try
            {
                MRekursivesLöschen(pfad + "\\BackGround");
            }
            catch
            {
            }
            Directory.CreateDirectory(pfad + "\\BackGround");
            Directory.CreateDirectory(pfad + "\\BackGround\\Dateien");

            foreach (string str in Directory.GetFiles(dictspeicherpfade["Root"] + @"\TempBackGround\BackGround"))
            {
                File.Copy(str, pfad + "\\BackGround\\" + str.Split('\\').Last());
            }
            foreach (string str in Directory.GetFiles(dictspeicherpfade["Root"] + @"\TempBackGround\BackGround\Dateien"))
            {
                File.Copy(str, pfad + "\\BackGround\\Dateien\\" + str.Split('\\').Last());
            }
        }

        private Dictionary<string, string> MDictionaryFüllen(Dictionary<string, string> dict, string pfad)
        {
            foreach (string str in Directory.GetFiles(pfad + "\\BackGround"))
            {
                // Es handelt sich um keine Backgrounddatei und Kopien offener Dateien
                if (!str.Split('\\').Contains("Background.suo") && !str.Contains('~'))
                {
                    string dateiname = MNurDateiname(str);
                    // Datei bereits in Dictionary?
                    if (dict.ContainsKey(dateiname))
                    {
                        // ist neue Datei aktueller?
                        if (File.GetLastWriteTime(str) > Convert.ToDateTime(dict[dateiname].Split(',')[1]))
                        {
                            // Neuere Datei ist nicht leer
                            sr = new StreamReader(str);
                            int izeilen = 0;
                            while (!sr.EndOfStream)
                            {
                                sr.ReadLine();
                                izeilen++;
                            }
                            sr.Close();

                            if (izeilen <= 2)
                            {
                                FormAktuelleDateien fad = new FormAktuelleDateien(str, dict[dateiname].Split(',')[0]);
                                if (!fad.MGetBehalten())
                                    dict[dateiname] = pfad + ',' + File.GetLastWriteTime(str);
                            }
                            else if (File.GetLastWriteTime(str) > Convert.ToDateTime(dict[dateiname].Split(',')[1]))
                            {
                                dict[dateiname] = pfad + ',' + File.GetLastWriteTime(str);
                            }
                        }
                    }
                    else
                        dict.Add(dateiname, pfad + ',' + File.GetLastWriteTime(str));
                }
            }

            foreach (string str in Directory.GetFiles(pfad + "\\BackGround\\Dateien"))
            {
                if (!str.Split('\\').Contains("Background.suo") && !str.Contains('~'))
                {
                    string dateiname = MNurDateiname(str);
                    // Datei bereits in Dicionary?
                    if (dict.ContainsKey(dateiname))
                    {
                        // ist neue Datei aktueller?
                        if (File.GetLastWriteTime(str) > Convert.ToDateTime(dict[dateiname].Split(',')[1]))
                            dict[dateiname] = pfad + ',' + File.GetLastWriteTime(str);
                    }
                    else
                        dict.Add(dateiname, pfad + ',' + File.GetLastWriteTime(str));
                }
            }

            return dict;
        }

        private string MNurDateiname(string pfad)
        {
            string name = "";

            string[] teilpfad = pfad.Split('\\');

            // BackGround zum Namen hinzufügen?
            if (teilpfad[teilpfad.Length - 2] == "BackGround")
                name = teilpfad[teilpfad.Length - 2] + '\\' + teilpfad.Last();
            else
                name = teilpfad.Last();

            return name;
        }

        private void MLabel()
        {
            if (aktuell.Year > 1)
            {
                int h = (DateTime.Now - aktuell).Hours;
                int min = (DateTime.Now - aktuell).Minutes;

                if (h.ToString().Length > 1 && min.ToString().Length > 1)
                    label1.Text = "Letzte Aktualisierung vor " + h.ToString() + "h:" + min.ToString() + "min";
                else if (h.ToString().Length == 1 && min.ToString().Length > 1)
                    label1.Text = "Letzte Aktualisierung vor 0" + h.ToString() + "h:" + min.ToString() + "min";
                else if (h.ToString().Length > 1 && min.ToString().Length == 1)
                    label1.Text = "Letzte Aktualisierung vor " + h.ToString() + "h:0" + min.ToString() + "min";
                else
                    label1.Text = "Letzte Aktualisierung vor 0" + h.ToString() + "h:0" + min.ToString() + "min";
            }
        }//   00:00h

        private Dictionary<string, string> MAktuelleDateienUnterC(bool dateien)
        {
            Dictionary<string, string> ausgabe = new Dictionary<string, string>();

            // Sollen andere Dateien aktualisiert werden?
            if (dateien)
            {
                string[] splitC = Directory.GetFiles(dictspeicherpfade["Root"] + @"\BackGround\Dateien").Where(str => !Path.GetFileName(str).StartsWith("~")).ToArray(); ;

                string dateiaufzählung = "";
                for (int a = 0; a < splitC.Length; a++)
                {
                    if (dateiaufzählung != "")
                        dateiaufzählung += '|' + splitC[a].Split('\\')[splitC[a].Split('\\').Length - 1];
                    else
                        dateiaufzählung += splitC[a].Split('\\')[splitC[a].Split('\\').Length - 1];
                }
                ausgabe.Add(dictspeicherpfade["Root"] + @"\BackGround\Dateien", dateiaufzählung);
            }
            else // sollen die BackGrounddateien aktualisiert werden?
            {
                string[] splitC = Directory.GetFiles(dictspeicherpfade["Root"] + @"\BackGround\").Where(str => !Path.GetFileName(str).StartsWith("~")).ToArray(); ;

                string dateiaufzählung = "";
                for (int a = 0; a < splitC.Length; a++)
                {
                    if (dateiaufzählung != "")
                        dateiaufzählung += '|' + splitC[a].Split('\\')[splitC[a].Split('\\').Length - 1];
                    else
                        dateiaufzählung += splitC[a].Split('\\')[splitC[a].Split('\\').Length - 1];
                }
                ausgabe.Add(dictspeicherpfade["Root"] + @"\BackGround\", dateiaufzählung);
            }

            return ausgabe;
        }

        private void listViewtermine_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem list = listViewtermine.GetItemAt(e.X, e.Y);
                terminindex = listViewtermine.Items.IndexOf(list);

                // wurde Item ausgewählt?
                if (list != null)
                {
                    contextMenuStriptermin.Items[0].Enabled = false;
                    contextMenuStriptermin.Items[1].Enabled = true;
                    contextMenuStriptermin.Items[2].Enabled = true;
                }
                else
                {
                    contextMenuStriptermin.Items[0].Enabled = true;
                    contextMenuStriptermin.Items[1].Enabled = false;
                    contextMenuStriptermin.Items[2].Enabled = false;
                }

                contextMenuStriptermin.Show(Cursor.Position);
            }
        }

        private void listBoxaufgaben_MouseDown(object sender, MouseEventArgs e)
        {
            int i = listBoxaufgaben.IndexFromPoint(e.X, e.Y);

            // wurde Item ausgewählt?
            if (i != -1)
            {
                listBoxaufgaben.SelectedIndex = i;
                toolStripButtonentfernen.Enabled = toolStripButtonbearbeiten.Enabled = true;
            }
            else
            {
                listBoxaufgaben.SelectedIndex = -1;
                toolStripButtonentfernen.Enabled = toolStripButtonbearbeiten.Enabled = false;
            }
        }

        private void toolStripButtonneu_Click(object sender, EventArgs e)
        {
            // Neue Aufgabe
           FormAufgaben a =  new FormAufgaben();
           a.ShowDialog();
           dictaufgaben.Add(dictaufgaben.Count, a.MGetAufgabe());
           MAufgabenSpeichern();
            MFülleAufgabenFeld();
        }

        private void toolStripButtonbearbeiten_Click(object sender, EventArgs e)
        {
            // Aufgabe bearbeiten
            FormAufgaben a = new FormAufgaben(listBoxaufgaben.SelectedItem.ToString());
            a.ShowDialog();
            dictaufgaben[listBoxaufgaben.SelectedIndex] = a.MGetAufgabe();
            MAufgabenSpeichern();
            MFülleAufgabenFeld();
            toolStripButtonentfernen.Enabled = toolStripButtonbearbeiten.Enabled = false;
        }

        private void toolStripButtonentfernen_Click(object sender, EventArgs e)
        {
            // Aufgabe entfernen
            dictaufgaben.Remove(listBoxaufgaben.SelectedIndex);
            MAufgabenSpeichern();
            MFülleAufgabenFeld();
            toolStripButtonentfernen.Enabled = toolStripButtonbearbeiten.Enabled = false;
        }

        private void listBoxaufgaben_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBoxaufgaben.SelectedItems.Count > 0)
            {
                if (e.KeyData == Keys.Enter)
                    toolStripButtonbearbeiten_Click(sender, e);
                else if (e.KeyData == Keys.Delete)
                    toolStripButtonentfernen_Click(sender, e);
            }
        }
        

        private void timeraktuell_Tick(object sender, EventArgs e)
        {
            MLabel();            

            List<int> erinnert = new List<int>();
            List<string> ausgaben = new List<string>();

            // Termine prüfen
           for (int a = 0; a < termine.Count; a++)
            {
                Termin t = termine[a];
                DateTime tag = t.datum;
                DateTime uhrzeit = Convert.ToDateTime(t.uhrzeit);

                if (!t.erinnert)
                {
                    double diff = DateTime.Now.TimeOfDay.TotalMinutes - uhrzeit.TimeOfDay.TotalMinutes;
                    if (tag.DayOfYear < DateTime.Now.Date.DayOfYear && tag.Year <= DateTime.Now.Year || tag == DateTime.Now.Date && diff >= 0)
                    {
                        if (erinnert.Count == 0)
                        {
                            for (int b = 0; b < 5; b++)
                                Console.Beep(750, 500);
                        }

                        if (diff <= 2)
                            ausgaben.Add("Ein Termin steht an: \n\n" + t.grund);
                        else if (DateTime.Now.Date.DayOfYear == tag.DayOfYear)
                            ausgaben.Add("Ein Termin stand um " + t.uhrzeit + " Uhr an: \n\n" + t.grund);
                        else if (DateTime.Now.Date.DayOfYear - tag.DayOfYear <= 1)
                            ausgaben.Add("Ein Termin stand gestern um " + t.uhrzeit + " Uhr an: \n\n" + t.grund);
                        else
                            ausgaben.Add("Ein Termin stand am " + t.datum + " um " + t.uhrzeit + " Uhr an: \n\n" + t.grund);

                        // Es wurde erinnert
                        erinnert.Add(a);
                    }
                }
            }

            // Ausgabenfenster anzeigen
            if (ausgaben.Count > 0)
            {
                window.AusgabenAnzeigen(ausgaben);
                timerterminfenster.Start();
            }

            // Vlc Player
            System.Diagnostics.Process[] procs = null;
            try
            {
                procs = Process.GetProcessesByName("vlc");

                if (procs.Length > 0 && vlcmeldung)
                {
                    // Wird nur einmal pro vlc start gemeldet
                    vlcmeldung = false; 
                    Process vlc = procs[0];

                    // VLC player ist geöffnet
                    if (!vlc.HasExited)
                    {
                       // MessageBox.Show("Dehnübungen");
                    }
                }
                else if (procs.Length == 0)
                    vlcmeldung = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // Erinnerungen eintragen in Liste
            foreach (int index in erinnert)
                termine[index].erinnert = true;

            if (erinnert.Count > 0)
                ClassÜbergreifend.SpeichereTermineInDatei(termine);


            MFülleTermineFeld();
        }        

        private void listViewtermine_DoubleClick(object sender, EventArgs e)
        {
            int index = listViewtermine.FocusedItem.Index;
            if (index != -1)
                MessageBox.Show("Grund: " + termine[index].grund +  "\n\nBeschreibung: \n\n" + termine[index].beschreibung);

        }

        private void listViewtermine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete && listViewtermine.SelectedIndices.Count > 0)
            {
                MEntferneTermin(listViewtermine.SelectedIndices[0]);
                MFülleTermineFeld();
            }
        }

        #region DictInDatei

        private void MNotizenSpeichern()
        {
            // Notizendatei anpassen
            string datei;

            // sind Notizen vorhanden?
            if (richTextBoxnotizen.Text != "")
                datei = "Notizen\n";
            else
                datei = "Notizen";
            datei += richTextBoxnotizen.Text;
            sw = new StreamWriter(dictspeicherpfade["Notizen"]);
            sw.WriteLine(datei);
            sw.Close();
        }

        private void MAufgabenSpeichern()
        {
            // Notizendatei anpassen
            string datei = "Aufgaben";

            foreach (KeyValuePair<int, string> pair in dictaufgaben)
                datei += "\n" + pair.Value;

            sw = new StreamWriter(dictspeicherpfade["Aufgaben"]);
            sw.WriteLine(datei);
            sw.Close();
        }

        
        #endregion

        private void buttontimer_Click(object sender, EventArgs e)
        {
            new FormTimer(new List<string>(),dictspeicherpfade).Show();
        }

      

        private bool MIstAbspielbar(string pfad)
        {
            List<string> musikendungen = new List<string>();
            musikendungen.Add("mp3");
            musikendungen.Add("mp4");
            musikendungen.Add("m4a");
            musikendungen.Add("avi");

            if (musikendungen.Contains(pfad.Split('.').Last()))
                return true;

            return false;
        }



        private void voreinstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormTimerDatei(dictspeicherpfade).ShowDialog();
        }

        private void dateiÄndernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormMusik(Convert.ToBoolean(dictmusik[1]), dictmusik,dictspeicherpfade).ShowDialog();
            MMusikInDict();
        }

        
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!showContextMenu)
                {
                    contextMenuStriptaskleiste.Show(Cursor.Position);
                    showContextMenu = true;
                }
                else
                {
                    contextMenuStriptaskleiste.Hide();
                    showContextMenu = false;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!showContextMenu)
                {
                    contextMenuStriptaskleiste.Show(new Point(Cursor.Position.X,Cursor.Position.Y - 30));
                    showContextMenu = true;
                }
                else
                {
                    contextMenuStriptaskleiste.Hide();
                    showContextMenu = false;
                }
            }
        }

        string pressed = "";
        int zahl = 0;

        private void shortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShortcuts fs = new FormShortcuts(dictmusik,dictspeicherpfade);
            fs.ShowDialog();
            dictmusik[0] = fs.MGetKeys();
        }

        private void buttongoogledrive_Click(object sender, EventArgs e)
        {
            //new FormGoogleDrive().ShowDialog();
        }

        private void pCModusÄndernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!fmä.FormWirdangezeigt)
            {
                fmä = new FormPCModusÄndern();
                fmä.FormWirdangezeigt = true;
                fmä.Show();
            }
        }

        private string GetZeit(long ms)
        {
           
            int sek = Convert.ToInt32(ms /= 1000);
            int h = sek / 3600;
            sek -= (h * 3600);
            int min = sek / 60;
            sek -= (min * 60);

            string strsek,strmin,strh;

            if (h < 10)
                strh = "0" + h;
            else
                strh =  h.ToString();
            if (min < 10)
                strmin = "0" + min;
            else
                strmin = min.ToString();
            if (sek < 10)
                strsek = "0" + sek;
            else
                strsek = sek.ToString();


            string output = "";

            if (h > 0)
                output = strh + ":" + strmin + ":" + strsek + " h";
            else
                output = strmin + ":" + strsek + " min";

            return output;
        }

        private void timerterminfenster_Tick(object sender, EventArgs e)
        {
            if (window != null)
            {
                if (!window.FensterOffen())
                {
                    window.HintergrundbildZurücksetzen();
                    window.work = false;
                    timerterminfenster.Stop();
                }
                else if (!window.TerminBildWirdAngezeigt()) // Hintergrundbild wurde wegen Timer zurückgesetzt, obwohl noch Termin offen ist
                {
                    window.TerminbildAlsHintergrund();
                }
            }
            else
            {
                MessageBox.Show("Terminfenster können nicht angezeigt werden");
                timerterminfenster.Stop();
            }
             
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormEinstellungen fe = new FormEinstellungen(dictspeicherpfade);
            fe.ShowDialog();
            excelpfad = fe.getEinstellungen()[1];
        }

        private void tMXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTmx tmx = new FormTmx();
            tmx.ShowDialog();
        }

        private void timermusik_Tick(object sender, EventArgs e)
        {
            toolStripMenuItemusik.Text = GetZeit(swmusik.ElapsedMilliseconds);
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.Text = "";
            toolStripTextBox1.ForeColor = Color.Black;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            buttonaktualisieren_Click(sender, e);
        }


        private void missionenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMissionen fm = new FormMissionen();
            fm.ShowDialog();
        }

        private void excelDateiÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(excelpfad))
                Process.Start(excelpfad);
            else
                MessageBox.Show("Keine Datei mit dem Pfad\n'" + excelpfad + "'\n gefunden");
        }

        private void aufgabenlisteZeigenToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

    }
}
