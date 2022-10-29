using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmLadder;

namespace Top100Germany
{
    public partial class FormAusgestiegene : Form
    {
        public FormAusgestiegene(string pfad)
        {
            InitializeComponent();
            this.pfad = pfad += "\\ausgestiegene.csv";

            // Einlesen
            if (!File.Exists(pfad))
            {
                StreamWriter sw = new StreamWriter(pfad);
                sw.Close();
            }
            else
            {
                StreamReader sr = new StreamReader(pfad);
                while (!sr.EndOfStream)
                {
                    string zeile = sr.ReadLine();

                    if (zeile == "") continue;

                    string[] split = zeile.Split(';');
                    int rang = Convert.ToInt32(split[0].ToString());
                    string name = split[1].ToString();
                    int punkte = Convert.ToInt32(split[2].ToString());

                    Spieler s = new Spieler(rang, name, punkte);
                    ausgestiegene.Add(s);
                }
                sr.Close();
            }
        }

        private string pfad;
        public List<Spieler> ausgestiegene = new List<Spieler>();

        private void FormAusgestiegene_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = GetSpielerListe();            
        }

        private string GetSpielerListe()
        {
            string text = "";
            foreach (Spieler s in ausgestiegene)
            {
                if (text== "") text += s.getZeile();
                else text += "\n" + s.getZeile();
            }
            return text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ausgestiegene = new List<Spieler>();
            string[] zeilen = richTextBox1.Text.Split('\n');
            foreach (string z in zeilen)
            {
                if (z == "" || z.Split(';').Length != 3) continue;

                string[] split = z.Split(';');

                int rang = Convert.ToInt32(split[0].ToString());
                string name = split[1].ToString();
                int punkte = Convert.ToInt32(split[2].ToString());

                Spieler s = new Spieler(rang, name, punkte);
                ausgestiegene.Add(s);
            }

            StreamWriter sw = new StreamWriter(pfad);
            sw.WriteLine(GetSpielerListe());
            sw.Close();             
        }
   
        public bool EnthältSpieler(Spieler s)
        {
            foreach (Spieler a in ausgestiegene)
            {
                if (a.name == s.name) return true;
            }
            return false;
        }
    
    }
}
