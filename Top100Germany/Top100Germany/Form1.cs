using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmLadder;

namespace Top100Germany
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            nachDifferenzToolStripMenuItem.Enabled = false;
            comboBox2.SelectedIndex = 0;
        }

        private Dictionary<string, Vergleich> dictvergleiche;
        private static string ORDNERNAME = "history";
        private bool ende = false;
        private List<Spieler> spieler;
        private Dictionary<string, string> historie;
        private FormName fn = new FormName(ORDNERNAME);
        private List<Spieler> nichtzugewiesen = new List<Spieler>();
        private FormAusgestiegene fa = new FormAusgestiegene(ORDNERNAME);
        private string sort = "nach Differenz";

        private void Form1_Load(object sender, EventArgs e)
        {
            fa = new FormAusgestiegene(ORDNERNAME);
            timer1.Interval = 60 * 1000 * 60; // stündlich
            timer1.Start();

            spieler = new List<Spieler>();
            ende = true;
            // http://de.tm-ladder.com/3_401_100_solo.php
            textBox1.Text = "http://de.tm-ladder.com/3__100_solo.php";
            webBrowser1.Navigate(textBox1.Text);
        }

        private string GetDateiname(string dateipfad)
        {
            string dateiname = dateipfad.Split('\\').Last();
            string[] split = dateiname.Split('.');
            dateiname = "";
            for (int a = 0; a < split.Length - 1; a++)
            {
                if (dateiname == "") dateiname += split[a];
                else dateiname += "." + split[a];
            }

            return dateiname;
        }

        private string[] SortNachDatum(string[] dateien)
        {
            Dictionary<string, DateTime> daten = new Dictionary<string, DateTime>();

            foreach (string s in dateien)
            {
                DateTime dout;
                string dateiname = GetDateiname(s);

                if (DateTime.TryParse(dateiname, out dout))
                    daten.Add(s,Convert.ToDateTime(dateiname));
            }

            string sorted = "";
            foreach (KeyValuePair<string, DateTime> pair in daten.OrderBy(i => i.Value))
            {
                if (sorted != "") sorted += ";" + pair.Key;
                else sorted += pair.Key;
            }            

            return sorted.Split(';');
        }

        private int findnextmonth(string[] dateien, int a)
        {
            int index = 1;
            string tag = GetDateiname(dateien[a]).Split('.').First();

            while (dateien.Length > a + index)
            {
                string testtag = GetDateiname(dateien[a + index]).Split('.').First();
                if (tag == testtag) return index-1;

                index++;
            }

            return 32;
        }

        private void DateienEinlesen()
        {
            historie = new Dictionary<string, string>();
            if (!Directory.Exists(@ORDNERNAME))
                Directory.CreateDirectory(@ORDNERNAME);

            comboBox1.Items.Clear();
            string[] dateien = Directory.GetFiles(@ORDNERNAME);
            dateien = SortNachDatum(dateien);

            if (dateien.Length > 0)
            {
                int beginn = 0;
                // letzten zwei Tage
                if (comboBox2.SelectedIndex == 0  && dateien.Length > 3)
                {
                    beginn = dateien.Length - 2;
                }                

                for (int a = beginn; a < dateien.Length; a++)
                {
                    string s = dateien[a];
                    string dateiname = GetDateiname(s);

                    DateTime dout;
                    // ignoriere files, die kein datum als namen haben
                    if (!DateTime.TryParse(dateiname, out dout)) continue;

                    historie.Add(s, dateiname);
                    comboBox1.Items.Add(dateiname);

                    // wöchentlich
                    if (comboBox2.SelectedIndex == 1)
                        a += 6;
                    else if (comboBox2.SelectedIndex == 2)
                        a += findnextmonth(dateien, a);
                }

                if (!historie.ContainsKey(dateien.Last()))
                {
                    string dateiname = GetDateiname(dateien.Last());
                    historie.Add(dateien.Last(), dateiname);
                    comboBox1.Items.Add(dateiname);
                }

                VergleicheLetzte();
                LetztenEintragAuswählen();
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            HTMLParser parser = new HTMLParser(webBrowser1.DocumentText, true);
            List<Tag> tags = parser.getTags();
            List<Spieler> hundertspieler = GetSpieler(tags);
            spieler.AddRange(hundertspieler);

            if (ende)
            {
                SpeicherInDatei();
                DateienEinlesen();
                LetztenEintragAuswählen();
            }

        }

        private void Vergleiche(string dateipfad)
        {
            nichtzugewiesen = new List<Spieler>();
            List<string> zugewiesen = new List<string>();
            List<Spieler> alt = ReadDatei(@dateipfad);
            dictvergleiche = new Dictionary<string, Vergleich>();
            foreach (Spieler s in spieler)
            {
                bool gefunden = false;
                foreach (Spieler l in alt)
                {
                    if (s.name == l.name)
                    {
                        zugewiesen.Add(l.name);
                        int index = 0;
                        if (dictvergleiche.ContainsKey(s.name))
                        {
                            while (dictvergleiche.ContainsKey(s.name + index))
                                index++;
                            dictvergleiche.Add(s.name + index, new Vergleich(s.name, l.punkte, s.punkte));
                        }
                        else
                            dictvergleiche.Add(s.name, new Vergleich(s.name, l.punkte, s.punkte));

                        gefunden = true;
                        break;
                    }
                }

                if (!gefunden)
                    dictvergleiche.Add(s.name, new Vergleich(s.name, 0, s.punkte));
            }

            // nichtzugewiesen zugewiesene
            foreach (Spieler l in alt)
            {
                bool gefunden = false;
                foreach (string zu in zugewiesen)
                {
                    if (l.name == zu)
                    {
                        gefunden = true;
                        break;
                    }
                }

                if (!gefunden && !fa.EnthältSpieler(l))
                    nichtzugewiesen.Add(l);
            }

            if (nichtzugewiesen.Count > 0)
                nichtZugewieseneToolStripMenuItem.Enabled = true;
            else
                nichtZugewieseneToolStripMenuItem.Enabled = false;

            FillDataGrid();
        }


        private void VergleicheLetzte()
        {
            string letztedateipfad = GetLetzteDateipfad();
            if (letztedateipfad != "")
                Vergleiche(letztedateipfad);
        }

        private void FillDataGrid()
        {
            if (dictvergleiche == null || dictvergleiche.Count == 0) return;

            dataGridView1.Rows.Clear();
            // Fill Datagrid
            List<string> spalten = new List<string>();
            spalten.Add("Name");
            spalten.Add("Vorher");
            spalten.Add("Neu");
            spalten.Add("Differenz");
            dataGridView1.ColumnCount = spalten.Count;

            for (int a = 0; a < spalten.Count; a++)
                dataGridView1.Columns[a].Name = spalten[a];

            int index = -1;
            int merkeindex = index;

            if (sort == "nach Differenz")
            {
                foreach (KeyValuePair<string, Vergleich> pair in dictvergleiche.OrderByDescending(i => i.Value.differenz))
                {
                    index++;
                    string[] zeile = pair.Value.GetZeile();
                    //Color def = dataGridView1.Rows[0].DefaultCellStyle.BackColor;

                    dataGridView1.Rows.Add(zeile);
                    if (zeile.First().Contains("unner"))
                        merkeindex = index;
                }
            }
            else if (sort == "nach neuen Punkten")
            {
                foreach (KeyValuePair<string, Vergleich> pair in dictvergleiche.OrderByDescending(i => i.Value.neu))
                {
                    index++;
                    string[] zeile = pair.Value.GetZeile();
                    //Color def = dataGridView1.Rows[0].DefaultCellStyle.BackColor;

                    dataGridView1.Rows.Add(zeile);
                    if (zeile.First().Contains("unner"))
                        merkeindex = index;
                }
            }

            
            if (merkeindex != -1)
            dataGridView1.Rows[merkeindex].DefaultCellStyle.BackColor = Color.Red;
        }

        private List<Spieler> ReadDatei(string dateipfad)
        {
            List<Spieler> spieler = new List<Spieler>();
            StreamReader sr = new StreamReader(@dateipfad);
            while (!sr.EndOfStream)
            {
                string zeile = sr.ReadLine();
                if (zeile == "") continue;

                string[] splits = zeile.Split(';');
                int rang = Convert.ToInt32(splits[0]);
                string name = splits[1];
                int punkte = Convert.ToInt32(splits[2]);

                spieler.Add(new Spieler(rang, name, punkte));
            }
            sr.Close();
            return spieler;

        }

        private string GetLetzteDateipfad()
        {
            List<Spieler> spieler = new List<Spieler>();
            if (Directory.GetFiles(@ORDNERNAME).Length < 2) return "";

            string dateipfad = "";

            DateTime t = DateTime.Now.AddDays(-1);
            while (t > DateTime.MinValue)
            {
                dateipfad = ORDNERNAME + "\\" + t.ToShortDateString() + ".csv";
                if (File.Exists(@dateipfad)) break;
                t = t.AddDays(-1);
            }
            // Gefunden
            if (dateipfad != "") return dateipfad;
            return "";
        }

        private void SpeicherInDatei()
        {
            try
            {
                string dateipfad = ORDNERNAME + "\\" + DateTime.Now.ToShortDateString() + ".csv";
                StreamWriter sw = new StreamWriter(@dateipfad);
                foreach (Spieler s in spieler)
                    sw.WriteLine(s.getZeile());
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetStart(List<Tag> tags)
        {
            string stop = "TM Forever Solo-Rangliste";
            int start = -1;

            for (int a = 0; a < tags.Count; a++)
            {
                Tag t = tags[a];
                // skippe bis zur relevanten tabelle
                if (!t.tag.Contains(stop) &&
                    !t.attribute.Contains(stop) &&
                    !t.tagname.Contains(stop) &&
                    !t.inhalt.Contains(stop)) continue;

                start = a;
                break;
            }
            return start;
        }

        private List<Spieler> GetSpieler(List<Tag> tags)
        {
            List<Spieler> spieler = new List<Spieler>();
            string zeile = "";
            int start = GetStart(tags);            

            // Tabelle gefunden
            for (int a = start; a < tags.Count; a++)
            {
                Tag t = tags[a];

                if (t.parent != null && (t.parent.tagname.Contains("tr") || t.parent.tagname.Contains("td")))
                {
                    if (t.inhalt != "")
                        Append(ref zeile, t.inhalt, ";");
                }
                else if (zeile != "")
                {
                    Spieler s = GetSpieler(zeile);

                    if (s != null)
                        spieler.Add(s);
                    zeile = "";
                }

            }

            return spieler;
        }

        private Spieler GetSpieler(string zeile)
        {
            try
            {
                // splits.first() = rang
                // name
                // splits.last() = punkte
                string[] splits = zeile.Split(';');

                int rang = 0;
                string name = "";
                int punkte = 0;
                int iout;

                // Teste Rang
                if (Int32.TryParse(splits.First(), out iout))
                    rang = Convert.ToInt32(splits.First());
                else
                    return null;

                // Name
                for (int a = 1; a < splits.Length - 1; a++)
                    name += splits[a];

                // Teste Punkte
                string spunkte = splits.Last();
                while (!Int32.TryParse(spunkte.Last().ToString(), out iout))
                    spunkte = spunkte.Remove(spunkte.Length - 1, 1);
                while (spunkte.Contains(' '))
                    spunkte = spunkte.Replace(" ", "");

                if (Int32.TryParse(spunkte, out iout))
                    punkte = Convert.ToInt32(spunkte);
                else
                    return null;

                Spieler s = new Spieler(rang, name, punkte);
                return s;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void Append(ref string text1, string add, string trenner)
        {
            if (text1 == "") text1 += add;
            else text1 += trenner + add;
            Säubern(ref text1);
        }
        private void Säubern(ref string text)
        {
            while (text.Contains("&nbsp;"))
                text = text.Replace("&nbsp;", "");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1) return;

            string dateiname = comboBox1.SelectedItem.ToString();
            string dateipfad = "";
            foreach (KeyValuePair<string,string> pair in historie)
            {
                if (pair.Value == dateiname)
                    dateipfad = pair.Key;
            }

            if (dateipfad != "")
            {   
                Vergleiche(@dateipfad);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }


        private void LetztenEintragAuswählen()
        {
            comboBox1.SelectedIndex = comboBox1.Items.Count - 2;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Form1_Load(sender, e);
        }

        private void nameErsetzenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fn.offen)
                {
                    fn.ShowDialog();
                    Form1_Load(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void nichtZugewieseneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(nichtzugewiesen);
            f.Show();
        }

   

        private void ausgestiegeneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fa.ShowDialog();
        }

        private void nachDifferenzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sort = "nach Differenz";
            nachDifferenzToolStripMenuItem.Enabled = false;
            nachNeuenPunktenToolStripMenuItem.Enabled = true;
            FillDataGrid();
        }

        private void nachNeuenPunktenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sort = "nach neuen Punkten";
            nachDifferenzToolStripMenuItem.Enabled = true ;
            nachNeuenPunktenToolStripMenuItem.Enabled = false;
            FillDataGrid();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (spieler == null || spieler.Count == 0) return;
            DateienEinlesen();
        }
    }
}
