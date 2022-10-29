using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Ordner_;

namespace OrdnerGröße
{
    public partial class FormDuplikate : Form
    {
        public FormDuplikate(string listboxName, List<ClassDatei> dateien)
        {
            InitializeComponent();
            this.dateien = dateien;
            this.listboxName = listboxName;
            dateigröße = new FileInfo(listboxName).Length;
            löschenliste = new List<ClassDatei>();
        }

        private List<ClassDatei> dateien;
        public string listboxName;

        public List<ClassDatei> löschenliste { get; }
        public long dateigröße { get; }


        public int MGetAnzahlPfade()
        {
            int anzahl = 0;

            foreach (ClassDatei cd in dateien)
            {
                if (File.Exists(cd.pfad))
                    anzahl++;
            }
            return anzahl;
        }

        private void FormDoppelt_Load(object sender, EventArgs e)
        {
            button1.Visible = false;
            textBoxname.Text = dateien[0].pfad;

            for (int a = 0; a < dateien.Count; a++)
            {
                if (File.Exists(dateien[a].pfad))
                    listBoxpfade.Items.Add(dateien[a].pfad);
            }

            if (listBoxpfade.Items.Count > 1)
            {
                listBoxpfade.SelectedIndex = 0;
                //listBoxpfade_SelectedIndexChanged(sender, e);
            }
        }
        private void listBoxpfade_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxpfade.SelectedIndex != -1)
            {
                Process.Start(listBoxpfade.Text);
            }
        }

        private void listBoxpfade_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Delete && listBoxpfade.Text != "")
                {
                    foreach (string item in listBoxpfade.SelectedItems)
                    {
                        if (!File.Exists(item))
                        {
                            MessageBox.Show("Datei existiert nicht!");
                            return;
                        }

                        ClassDatei cd = FindDatei(item);

                        if (cd == null)
                        {
                            MessageBox.Show("Datei " + item + "konnte nicht gelöscht werden!");
                        }

                        löschenliste.Add(cd);
                        dateien.Remove(cd);

                        if (item == listboxName && dateien.Count > 0)
                            listboxName = dateien.First().pfad;
                    }

                    foreach (ClassDatei löschen in löschenliste)
                        listBoxpfade.Items.Remove(löschen.pfad);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private ClassDatei FindDatei(string pfad)
        {
            foreach (ClassDatei cd in dateien)
                if (cd.pfad == pfad)
                    return cd;

            return null;
        }

        private void listBoxpfade_SelectedIndexChanged(object sender, EventArgs e)
        {
            label4.Text = ClassDatei.GetGröße(listBoxpfade.SelectedIndices.Count * dateigröße) + "/" + ClassDatei.GetGröße(listBoxpfade.Items.Count * dateigröße);
            if (listBoxpfade.SelectedItems.Count == 1)
            {
                button1.Visible = true;
            }
            else
            {
                button1.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBoxpfade.SelectedIndex == -1) return;

            string dateipfad = listBoxpfade.SelectedItem.ToString();
            // Öffne Pfad im Explorer

            if (File.Exists(dateipfad))
            {
                string argument = "/select, \"" + dateipfad + "\"";
                Process.Start("explorer.exe", argument);
            }
            else
                MessageBox.Show("Explorer konnte Datei nicht selektieren!");

            listBoxpfade.Items.Clear();
            for (int a = 0; a < dateien.Count; a++)
            {
                if (File.Exists(dateien[a].pfad))
                    listBoxpfade.Items.Add(dateien[a].pfad);
                else
                {
                    dateien.RemoveAt(a);
                    a--;
                }
            }
        }
    }
}
