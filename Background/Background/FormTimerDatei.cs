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
    public partial class FormTimerDatei : Form
    {
        public FormTimerDatei(Dictionary<string, string> dictspeicherpfade)
        {
            InitializeComponent();
            this.dictspeicherpfade = dictspeicherpfade;
        }

        private Dictionary<string, string> dictspeicherpfade;

        private void FormTimerDatei_Load(object sender, EventArgs e)
        {
            if (File.Exists(dictspeicherpfade["Timer"]))
            {
                StreamReader sr = new StreamReader(dictspeicherpfade["Timer"]);
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string zeile = sr.ReadLine();
                    if (zeile != "")
                    {
                        if (richTextBox1.Text == "")
                            richTextBox1.Text = zeile;
                        else
                            richTextBox1.Text += "\n" + zeile;
                    }
                }
                sr.Close();
            }
            else
            {
                MessageBox.Show("Timerdatei existiert nicht!");
                this.Close();
            }
        }

        private void buttonabbrechen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonspeichern_Click(object sender, EventArgs e)
        {
            if (MInhaltKorrekt())
            {
                StreamWriter sw = new StreamWriter(dictspeicherpfade["Timer"]);
                sw.WriteLine("Timer\n" + richTextBox1.Text);
                sw.Close();
                this.Close();
            }
        }

        private bool MInhaltKorrekt()
        {
            string[] zeilen = richTextBox1.Text.Split('\n');

            if (zeilen.Length == 1 && zeilen[0] == "")
                return true;

            foreach (string zeile in zeilen)
            {
                if (ClassÜbergreifend.MKürzen(zeile) == "")
                {
                    MessageBox.Show("Es dürfen keine leeren Zeilen vorhanden sein!");
                    return false;
                }

                if (zeile.Split(';').Length != 2)
                {
                    MessageBox.Show("Es darf nur ein Semikolon in einer Zeile stehen!");
                    return false;
                }

                bool zahlfertig = false;
                string zeit = zeile.Split(';')[1];
                string zahl = "";
                string wort = "";
                int iout;

                // Folgt auf eine Zahl eine Wort wie: h,min,sek,sec?
                for (int a = 0; a < zeit.Length; a++)
                {
                    if (!zahlfertig && Int32.TryParse(zeit[a].ToString(), out iout) == true)
                        zahl += zeit[a];
                    else
                    {
                        zahlfertig = true;
                        wort += zeit[a];
                    }
                }

                if (Int32.TryParse(zahl, out iout) == false)
                {
                    MessageBox.Show("Es fehlt eine Zahl!");
                    return false;
                }

                wort = ClassÜbergreifend.MKürzen(wort);
                if (wort != "h" && wort != "min" && wort != "sek" && wort != "sec")
                {
                    MessageBox.Show("Es steht das falsche Wort hinter einer Zahl! Erlaubt sind:\nh,min,sec und sek");
                    return false;
                }

            }

            return true;
        }
    }
}
