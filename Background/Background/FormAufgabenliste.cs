using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Background
{
    public partial class FormAufgabenliste : Form
    {
        public FormAufgabenliste(DateTime datumfüraufgabe, string excelpfad)
        {
            InitializeComponent();

            dictaufgaben = new Dictionary<string, bool>();
            this.excelpfad = excelpfad;
            this.datumfüraufgabe = datumfüraufgabe;
            // neu einlesen?                        

            if (!File.Exists(dateiname) || !IstDateiAktuell())
                NeuEinlesen();
            else
                HoleAusDatei();
        }

        private Dictionary<string, bool> dictaufgaben;
        private string dateiname = "Aufgabenliste.csv";
        private string excelpfad = @"D:\Studium\SS2019\Plan September 2019.xlsx";
        private DateTime datumfüraufgabe;


        private void HoleAusDatei()
        {
            StreamReader sr = new StreamReader("Aufgabenliste.csv");
            sr.ReadLine();

            while (!sr.EndOfStream)
            {
                string zeile = sr.ReadLine();
                string[] split = zeile.Split(';');
                if (!dictaufgaben.ContainsKey(split[0]))
                    dictaufgaben.Add(split[0], Convert.ToBoolean(split[1]));
            }
            sr.Close();
        }

        private void NeuEinlesen()
        {
            // Hole Aufgaben aus akutellem Tag aus Excel-Datei
            string reihe = "aufgabe1;aufgabe2;...";
            DateTime date = datumfüraufgabe;

            ExcelReader er = new ExcelReader(excelpfad, date);
                reihe = er.getAufgaben();

            AufgabenInDict(reihe);
            SichereInDatei(date);
        }

        private bool IstDateiAktuell()
        {
            StreamReader sr = new StreamReader(dateiname);
            string datum = sr.ReadLine();
            sr.Close();

            DateTime dout;
            if (DateTime.TryParse(datum, out dout))
            {
                DateTime test = Convert.ToDateTime(datum);

                if (test.Year == datumfüraufgabe.Year
                && test.Month == datumfüraufgabe.Month
                && test.Day == datumfüraufgabe.Day)
                {
                    return true;
                }
            }

            return false;
        }

        public int GetAnzahlAufgaben()
        {
            int count = 0;

            foreach (bool b in dictaufgaben.Values)
            {
                if (!b)
                    count++;
            }

            return count;
        }

        private void SichereInDatei(DateTime date)
        {
            StreamWriter sw = new StreamWriter(dateiname);
            sw.WriteLine(date.ToShortDateString());
            foreach (KeyValuePair<string, bool> pair in dictaufgaben)
                sw.WriteLine(pair.Key + ';' + pair.Value);
            sw.Close();
        }

        private void AufgabenInDict(string reihe)
        {
            dictaufgaben = new Dictionary<string, bool>();
            int iout;
            string[] split = reihe.Split(';');
            foreach (string iteraufgabe in split)
            {
                string aufgabe = iteraufgabe;
                while (aufgabe.Length > 0 && aufgabe.First() == ' ')
                    aufgabe = aufgabe.Remove(0, 1);


                // Strukur 10x?
                string smulti = aufgabe.Split('x')[0];
                if (Int32.TryParse(smulti, out iout) && aufgabe.Split('x').Length > 1)
                {
                    Multiaufgabe ma = new Multiaufgabe();
                    ma.multi = Convert.ToInt32(smulti);
                    ma.text = getMultiaufgabe(aufgabe);

                    for (int a = 0; a < ma.multi; a++)
                    {
                        if (!dictaufgaben.ContainsKey((a + 1) + ". " + ma.text))
                            dictaufgaben.Add((a + 1) + ". " + ma.text, false);
                    }
                }
                else
                {
                    if (!dictaufgaben.ContainsKey(aufgabe))
                        dictaufgaben.Add(aufgabe, false);
                }
            }
        }

        private string getMultiaufgabe(string aufgabe)
        {
            string[] split = aufgabe.Split('x');
            string text = "";
            for (int a = 1; a < split.Length; a++)
            {
                if (text != "")
                    text += "x" + split[a];
                else
                    text += split[a];
            }
            return text;
        }

        private void FormAufgabenliste_Load(object sender, EventArgs e)
        {
            while (panel1.Controls.Count > 0)
                panel1.Controls.RemoveAt(0);
            int abstand = 30;
            int x = 10;
            int y = 0;
            int index = 0;
            foreach (KeyValuePair<string, bool> pair in dictaufgaben)
            {
                ErstelleCheckBox(index.ToString(), pair.Key, pair.Value, x, y);
                y += abstand;
                index++;
            }
        }

        private void ErstelleCheckBox(string name, string text, bool bearbeitet, int x, int y)
        {
            CheckBox cb = new CheckBox();
            cb.Text = text;
            cb.Name = name;
            cb.Size = new Size(text.Length * 30, 43);
            cb.Checked = bearbeitet;
            cb.Location = new Point(x, y);
            cb.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            panel1.Controls.Add(cb);
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            string name = ((CheckBox)sender).Name;
            string key = dictaufgaben.ElementAt(Convert.ToInt32(name)).Key;
            dictaufgaben[key] = ((CheckBox)sender).Checked;
            SichereInDatei(datumfüraufgabe);
        }

        private void neuEinlesenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NeuEinlesen();
            FormAufgabenliste_Load(sender, e);
        }
    }

    class Multiaufgabe
    {
        public int multi { get; set; }
        public string text { get; set; }
    }
}
