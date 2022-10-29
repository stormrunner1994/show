using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Phase6_Software
{
    class ClassEinstellungen
    {
        public ClassEinstellungen(string profilname)
        {
            Schriftart = "Times New Roman";
            Schriftgröße = 10;
            Hintergrundfarbeaußen = "Green";
            Hintergrundfarbeinnen = "Orange";
            DefaultHintergrundfarbeaußen = Color.FromName(Hintergrundfarbeaußen).ToArgb().ToString();
            DefaultHintergrundfarbeinnen = Color.FromName(Hintergrundfarbeinnen).ToArgb().ToString();
            Defaultabstände = new Dictionary<int, double>();
            Defaultabstände.Add(Defaultabstände.Count + 1, 0);
            Defaultabstände.Add(Defaultabstände.Count + 1, 2);
            Defaultabstände.Add(Defaultabstände.Count + 1, 4);
            Defaultabstände.Add(Defaultabstände.Count + 1, 8);
            Defaultabstände.Add(Defaultabstände.Count + 1, 16);
            Defaultabstände.Add(Defaultabstände.Count + 1, 32);
            this.profilname = profilname;
            MEinstellungsDateiErstellen();
    
            MDateiEinlesen();
        }

        public string DefaultHintergrundfarbeaußen { get; set; }
        public string DefaultHintergrundfarbeinnen { get; set; }
        public Dictionary<int, double> Defaultabstände { get; set; }

        public string profilname;
        public Dictionary<int, double> abstände { get; set; }
        public string Schriftart { get; set; }
        public int Schriftgröße { get; set; }
        public string Hintergrundfarbeaußen { get; set; }
        public string Hintergrundfarbeinnen { get; set; }

        public void MDateiEinlesen()
        {
            abstände = new Dictionary<int, double>();
            string file = "C:\\Phase6\\" + profilname + "_Einstellungen.csv";

            if (!File.Exists(file))
            {
                MessageBox.Show("Einstellungsdatei existiert noch nicht!");
                return;
            }

            StreamReader sr = new StreamReader(file);
            sr.ReadLine(); // Überspringe 1. Zeile: Dauer in Tagen
            string zeile = sr.ReadLine();
            string[] split = zeile.Split(';');

            for (int a = 0; a < split.Length; a++)
                abstände.Add(a + 1, Convert.ToDouble(split[a].ToString()));

            sr.ReadLine(); // Überspringe Hintergrundfarbe außen;Hintergrundfarbe innen
            zeile = sr.ReadLine();
            split = zeile.Split(';');
            this.Hintergrundfarbeaußen = split[0];
            this.Hintergrundfarbeinnen = split[1];
            sr.Close();
        }

        private void MEinstellungsDateiErstellen()
        {// Einstellungsdatei erstellen, falls nicht vorhanden
            if (!File.Exists("C:\\Phase6\\" + profilname+"_Einstellungen.csv"))
            {
                // Datei füllen
                StreamWriter sw = new StreamWriter("C:\\Phase6\\" + profilname + "_Einstellungen.csv");
                sw.WriteLine("Dauer in Tagen\n0;2;4;8;16;32\nHintergrundfarbe außen;Hintergrundfarbe innen\n" + Color.Green.ToArgb() + ";" + Color.Orange.ToArgb());
                sw.Close();
            }
        }

    public void MDateiAktualisieren()
        {
            string datei = "Dauer in Tagen\n";

            for (int a = 1; a <= abstände.Count; a++)
            {
                datei += abstände[a];
                if (a < abstände.Count)
                    datei+=";";
            }

            datei += "\nHintergrundfarbe Außen;Hintergrundfarbe Innen\n";
            datei += Hintergrundfarbeaußen + ";" + Hintergrundfarbeinnen;
            
            StreamWriter sw = new StreamWriter("C:\\Phase6\\" + profilname + "_Einstellungen.csv");   
            sw.WriteLine(datei);
            sw.Close();
        }
    }
}
