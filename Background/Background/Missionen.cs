using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Background
{
    // pro Tag
    class Mission
    {
        public string titel { get; set; }
        public int anzahl { get; set; }
        public int maxAnzahl { get; set; }
        public int erfahrungspunkte { get; set; }

        public Mission(string titel, int anzahl, int maxAnzahl,int erfahrungspunkte)
        {
            this.titel = titel;
            this.anzahl = anzahl;
            this.maxAnzahl = maxAnzahl;
            this.erfahrungspunkte = erfahrungspunkte;
        }

        public string toString()
        {
            return titel + "\n" + anzahl + "\n" + maxAnzahl + "\n" + erfahrungspunkte;
        }
    }

    class Missionen
    {
        private List<Mission> missionen;
        private const string DATEINAME = "missionen.csv";
        private string fehlermeldung = "";
        private int erfahrungspunkte = 0;

        public Missionen ()
        {            missionen = new List<Mission>();
            leseDatei(ref missionen);
        }

        public List<Mission> getMissionen()
        {
            return missionen;
        }

        private void leseDatei(ref List<Mission> missionen)
        {
            // Dateistruktur:
            // erste Zeile: erfahrungspunkte
            // titel;anzahl;maxanzahl;erfahrungspunkte
            if (!File.Exists(DATEINAME))
            {
                StreamWriter sw = new StreamWriter(DATEINAME);
                sw.WriteLine("0");
                sw.Close();
                return;
            }

            StreamReader sr = new StreamReader(DATEINAME);
            erfahrungspunkte = Convert.ToInt32(Convert.ToInt32(sr.ReadLine()));

            while (!sr.EndOfStream)
            {
                string zeile = sr.ReadLine();
                string[] split = zeile.Split(';');
                if (zeile != "")
                    missionen.Add(new Mission(split[0], Convert.ToInt32(split[1]), Convert.ToInt32(split[2]), Convert.ToInt32(split[3])));
            }
            sr.Close();

            // Abschluss
        }

        private void schreibeInDatei()
        {
            if (missionen == null)
                return;

            string dateiinhalt = erfahrungspunkte.ToString();
            foreach (Mission mis in missionen)
                    dateiinhalt += "\n" + mis.toString();

            try
            {
                StreamWriter sw = new StreamWriter(DATEINAME);
                sw.WriteLine(dateiinhalt);
                sw.Close();
            }
            catch (Exception ex)
            {
                fehlermeldung = ex.Message;
            }
        }

    }
}
