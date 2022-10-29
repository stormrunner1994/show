using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Background
{
    public partial class FormMusik : Form
    {
        public FormMusik(bool musik, Dictionary<int, string> dictmusik, Dictionary<string, string> dictspeicherpfade)
        {
            this.musik = musik;
            this.dictmusik = dictmusik;
            this.dictspeicherpfade =dictspeicherpfade;
            InitializeComponent();
        }

        private Dictionary<string, string> dictspeicherpfade;
        private bool musik;
        private Dictionary<int, string> dictmusik;

        private void FormMusik_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            checkBox1.Checked = musik;
            for (int a = 2; a < dictmusik.Count; a++)
            {
                if (richTextBox1.Text != "")
                    richTextBox1.Text += "\n" + dictmusik[a];
                else
                    richTextBox1.Text += dictmusik[a];
            }           
        }

        private void buttonabbrechen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonspeichern_Click(object sender, EventArgs e)
        {
            string[] zeilen = richTextBox1.Text.Split('\n');
            for (int a = 0; a < zeilen.Length; a++)
            {
                string zeile = zeilen[a];

                if (zeile.First() == '!')
                   zeile = zeile.Remove(0, 1);

                if (!Directory.Exists(zeile) && !File.Exists(zeile))
                {
                    MessageBox.Show("Der Pfad " + zeile+ " existiert nicht!");
                    return;
                }
            }

            // Datei
            StreamWriter sw = new StreamWriter(dictspeicherpfade["Musik"]);
            sw.WriteLine(dictmusik[0]);
            sw.WriteLine(checkBox1.Checked);
            
            for (int a = 0; a < zeilen.Length; a++)
                sw.WriteLine(zeilen[a]);
            sw.Close();
            this.Close();
        }
    }
}
