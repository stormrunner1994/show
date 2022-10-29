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

namespace Background
{
    public partial class FormDateien : Form
    {
        public FormDateien(Dictionary<int, string> pfadliste, Dictionary<string, string> dictspeicherpfade)
        {
            InitializeComponent();
            dictpfade = pfadliste;
            this.dictspeicherpfade = dictspeicherpfade;
    }
        private Dictionary<string, string> dictspeicherpfade;
        private Dictionary<int, string> dictpfade = new Dictionary<int, string>();
        private StreamReader sr;
        private StreamWriter sw;

        private void toolStripButtonneu_Click(object sender, EventArgs e)
        {
            while (true)
            {
                folderBrowserDialog1.SelectedPath = "";
                folderBrowserDialog1.ShowDialog();
                string pfad = folderBrowserDialog1.SelectedPath;
                if (pfad != "")
                {
                    try
                    {
                        if (!dictpfade.ContainsValue(pfad))
                        {
                            // Test, ob auf Pfad geschrieben werden kann
                            Directory.CreateDirectory(pfad + "\\Temp");
                            Directory.Delete(pfad + "\\Temp");

                            dictpfade.Add(dictpfade.Count, pfad);
                            MDictInDatei();
                            MFüllePfadeFeld();
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Bitte einen geeigneten Unterordner auswählen!\n"+ex.Message);
                    }

                    toolStripButtonentfernen.Enabled = toolStripButtonbearbeiten.Enabled = false;
                }
                else
                    break;
            }
        }

        private void MDictInDatei()
        {
            string datei = "Pfade";

            foreach (KeyValuePair<int, string> pair in dictpfade)
            {
                if (pair.Key != -1)
                {
                    datei += '\n' + pair.Value;
                    if (datei[datei.Length - 1] == '\r')
                        datei = datei.Remove(datei.Length - 1, 1);
                }
            }

            sw = new StreamWriter(dictspeicherpfade["Pfade"]);
            sw.WriteLine(datei);
            sw.Close();
        }

        private void FormPfade_Load(object sender, EventArgs e)
        {
            MVoreinstellungen();
        }

        private void MLegeDictAn()
        {
            sr = new StreamReader(dictspeicherpfade["Pfade"]);

            // erste Zeile überspringen
            sr.ReadLine();
            string datei = sr.ReadToEnd();
            sr.Close();

            string[] split = datei.Split('\n');
            for (int a = 0; a < split.Length-1; a++)
                dictpfade.Add(a, split[a]);
        }

        private void MVoreinstellungen()
        {
            MFülleDateienFeld();
            MFüllePfadeFeld();
        }
        private void MFüllePfadeFeld()
        {
            listBoxpfade.Items.Clear();

            foreach (KeyValuePair<int, string> pair in dictpfade)
            {
                if (pair.Key != -1)
                {
                    if (Directory.Exists(pair.Value))
                    {
                        if (!Directory.Exists(pair.Value + "/Background"))
                            Directory.CreateDirectory(pair.Value + "/Background");
                        if (!Directory.Exists(pair.Value + "/Background/Dateien"))
                            Directory.CreateDirectory(pair.Value + "/Background/Dateien");

                        listBoxpfade.Items.Add(pair.Value);
                    }
                }
            }
        }

        private void MFülleDateienFeld()
        {
            listBoxdateien.Items.Clear();
            // Dateien hinzufügen
            if (Directory.GetFiles("C:\\BackGround\\Dateien").Length>0)
            {
            string[] split = Directory.GetFiles("C:\\BackGround\\Dateien").Where(str => !Path.GetFileName(str).StartsWith("~")).ToArray();

            for (int a = 0; a < split.Length; a++)
                listBoxdateien.Items.Add(split[a].Split('\\')[split[a].Split('\\').Length - 1]);
            }
        }

        private void listBoxdateien_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxdateien.SelectedIndex != -1)
            {
                Process proc = new Process();
                proc.StartInfo.FileName = "C:\\BackGround\\Dateien\\" + listBoxdateien.SelectedItem;
                proc.Start();
            }
        }

        private void listBoxpfade_MouseDown(object sender, MouseEventArgs e)
        {
            int i = listBoxpfade.IndexFromPoint(e.X, e.Y);

            // wurde Item ausgewählt?
            if (i != -1)
                toolStripButtonentfernen.Enabled = toolStripButtonbearbeiten.Enabled = true;
            else
                toolStripButtonentfernen.Enabled = toolStripButtonbearbeiten.Enabled = false;
        }

        private void toolStripButtonbearbeiten_Click(object sender, EventArgs e)
        {
            // Pfad bearbeiten

            while (true)
            {
                folderBrowserDialog1.ShowDialog();
                string pfad = folderBrowserDialog1.SelectedPath;

                if (pfad != "")
                {
                    if (pfad.Split('\\')[1] != "" && pfad.Split('\\').Length>1)
                    {
                        dictpfade[listBoxpfade.SelectedIndex] = pfad;
                        MDictInDatei();
                        MFüllePfadeFeld();
                        break;
                    }
                    else
                        MessageBox.Show("Bitte einen geeigneten Unterordner auswählen!");

                    toolStripButtonentfernen.Enabled = toolStripButtonbearbeiten.Enabled = false;
                }
                else
                    break;
            }
        }

        private void toolStripButtonentfernen_Click(object sender, EventArgs e)
        {
            // Pfad löschen
            dictpfade.Remove(listBoxpfade.SelectedIndex);
            MDictInDatei();
            MFüllePfadeFeld();
        }
    }
}
