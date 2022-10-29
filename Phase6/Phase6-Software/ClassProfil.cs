using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Phase6_Software
{
    class ClassProfil
    {
        public ClassProfil(string name)
        {
            einstellungen = new ClassEinstellungen(name);
            this.name = name;
            versuch = "kein";
            suchbegriff = "";
            fehler = "";
        }

        public string name { get; set; }
        public string versuch { get; set; }
        public string suchbegriff { get; set; }
        public Dictionary<int, ClassKarteikarte> karteikarten { get; set; }
        public Dictionary<int, ClassKarteikarte> gefiltert { get; set; }
        public Dictionary<int, ClassKarteikarte> gesucht { get; set; }
        public Dictionary<string, List<ClassKarteikarte>> mehrfache { get; set; }
        public ClassKarteikarte momentaneKarteikarte { get; set; }
        public ClassEinstellungen einstellungen;
        public string fehler { get; set; }

        public void MSucheKarteikarten()
        {
            gesucht = new Dictionary<int, ClassKarteikarte>();

            foreach (KeyValuePair<int, ClassKarteikarte> pair in karteikarten)
            {
                if (pair.Value.Kategorie.Contains(suchbegriff) || pair.Value.Frage.Contains(suchbegriff)
                    || pair.Value.Antwort.Contains(suchbegriff) || pair.Value.Phase.ToString().Contains(suchbegriff)
                    || pair.Value.Richtige.ToString().Contains(suchbegriff) || pair.Value.Falsche.ToString().Contains(suchbegriff)
                    || pair.Value.Datum.ToShortDateString().Contains(suchbegriff))
                {
                    gesucht.Add(gesucht.Count, pair.Value);
                }
            }
        }

        public void MFindeMehrfache()
        {
            // Sind mehrfache Karteikarten vorhanden?
            Dictionary<string, List<ClassKarteikarte>> temp = new Dictionary<string, List<ClassKarteikarte>>();

            foreach (KeyValuePair<int, ClassKarteikarte> pair in karteikarten)
            {
                if (temp.ContainsKey(pair.Value.Frage + "," + pair.Value.Antwort))
                    temp[pair.Value.Frage + "," + pair.Value.Antwort].Add(pair.Value);
                else if (temp.ContainsKey(pair.Value.Antwort + "," + pair.Value.Frage))
                    temp[pair.Value.Antwort + "," + pair.Value.Frage].Add(pair.Value);
                else
                    temp.Add(pair.Value.Frage + "," + pair.Value.Antwort, new List<ClassKarteikarte>() { pair.Value });
            }

            mehrfache = new Dictionary<string, List<ClassKarteikarte>>();
            foreach (KeyValuePair<string, List<ClassKarteikarte>> pair in temp)
            {
                if (pair.Value.Count > 1)
                    mehrfache.Add(pair.Key, pair.Value);
            }
        }

        public void MHoleKarteikartenAusDatei()
        {
            karteikarten = new Dictionary<int, ClassKarteikarte>();
            string file = "C:\\Phase6\\" + name + ".csv";

            if (!File.Exists(file))
            {
                MessageBox.Show("Profildatei existiert noch nicht!");
                return;
            }

            MDateiLesen(file);
        }

        public void MDateiLesen(string file)
        {
            StreamReader sr = new StreamReader(file);
            karteikarten = new Dictionary<int, ClassKarteikarte>();
            int izeile = 2;

            // Erste Zeile ignorieren: Kategorie;Frage;Antwort;Phase;Richtige;Falsche;Datum
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                try
                {
                    string zeile = sr.ReadLine();
                    string[] split = zeile.Split(';');

                    if (split.Length != 7)
                        MFehlerHinzufügen(izeile, "Zeile hat keine 7 Spalten");
                    else {
                        ClassKarteikarte karteikarte = new ClassKarteikarte();
                        karteikarte.ID = karteikarten.Count;
                        karteikarte.Kategorie = split[0];
                        karteikarte.Frage = split[1];
                        karteikarte.Antwort = split[2];
                        karteikarte.Phase = Convert.ToInt32(split[3]);
                        karteikarte.Richtige = Convert.ToInt32(split[4]);
                        karteikarte.Falsche = Convert.ToInt32(split[5]);
                        karteikarte.Datum = Convert.ToDateTime(split[6]);
                        karteikarten.Add(karteikarten.Count, karteikarte);
                    }
                }
                catch (Exception ex)
                {
                    MFehlerHinzufügen(izeile,ex.Message);
                }

                izeile++;
            }

            sr.Close();
        }

        private void MFehlerHinzufügen(int izeile, string fehler)
        {
            if (this.fehler != "")
                this.fehler += "\nIn Zeile " + izeile + " trat folgender Fehler auf: " + fehler;
            else
                this.fehler += "In Zeile " + izeile + " trat folgender Fehler auf: " + fehler;
        }

        public void MHoleKarteikartenAusDatei(string sicherungspfad)
        {
            string file = sicherungspfad;

            if (!File.Exists(file))
            {
                MessageBox.Show("Sicherungsdatei existiert noch nicht!");
                return;
            }

            MDateiLesen(file);
        }

        public void MBearbeiteKarteikarte(int id)
        {
            if (versuch == "richtig")
            {
                if (karteikarten[id].Datum <= DateTime.Now)
                {
                    if (karteikarten[id].Phase < 6)
                        karteikarten[id].Phase++;
                    karteikarten[id].Datum = karteikarten[id].Datum.AddDays(einstellungen.abstände[karteikarten[id].Phase]);
                }
                karteikarten[id].Richtige++;
            }
            else if (versuch == "falsch")
            {
                karteikarten[id].Falsche++;
                if (karteikarten[id].Phase > 1)
                    karteikarten[id].Phase--;
                karteikarten[id].Datum = DateTime.Now.AddDays(einstellungen.abstände[karteikarten[id].Phase]);
            }
        }

        public void MGesuchtAktualisieren()
        {
            Dictionary<int, ClassKarteikarte> temp = new Dictionary<int, ClassKarteikarte>();
            foreach (KeyValuePair<int, ClassKarteikarte> pair in gesucht)
                temp.Add(temp.Count, pair.Value);

            gesucht = new Dictionary<int, ClassKarteikarte>();
            foreach (KeyValuePair<int, ClassKarteikarte> pair in temp)
                gesucht.Add(pair.Key, pair.Value);
        }

        public void MGefiltertAktualisieren()
        {
            Dictionary<int, ClassKarteikarte> temp = new Dictionary<int, ClassKarteikarte>();
            foreach (KeyValuePair<int, ClassKarteikarte> pair in gefiltert)
                temp.Add(temp.Count, pair.Value);

            gefiltert = new Dictionary<int, ClassKarteikarte>();
            foreach (KeyValuePair<int, ClassKarteikarte> pair in temp)
                gefiltert.Add(pair.Key, pair.Value);
        }

        public void MAlleAktualiseren()
        {
            Dictionary<int, ClassKarteikarte> temp = new Dictionary<int, ClassKarteikarte>(); ;
            foreach (KeyValuePair<int, ClassKarteikarte> pair in karteikarten)
                temp.Add(temp.Count, pair.Value);

            karteikarten = new Dictionary<int, ClassKarteikarte>();
            foreach (KeyValuePair<int, ClassKarteikarte> pair in temp)
                karteikarten.Add(pair.Key, pair.Value);
        }

        public void MFiltereKarteikarten(string phase, string kategorie, bool inaktive)
        {
            gefiltert = new Dictionary<int, ClassKarteikarte>();

            foreach (KeyValuePair<int, ClassKarteikarte> pair in karteikarten)
            {
                if (inaktive)
                {
                    if (phase != "Alle")
                    {
                        if (pair.Value.Phase == Convert.ToInt32(phase.Split('.')[0].ToString()))
                        {
                            if (kategorie != "Alle")
                            {
                                if (pair.Value.Kategorie == kategorie)
                                {
                                    ClassKarteikarte karteikarte = new ClassKarteikarte();
                                    karteikarte.Kategorie = pair.Value.Kategorie;
                                    karteikarte.Frage = pair.Value.Frage;
                                    karteikarte.Antwort = pair.Value.Antwort;
                                    karteikarte.Phase = pair.Value.Phase;
                                    karteikarte.Richtige = pair.Value.Richtige;
                                    karteikarte.Falsche = pair.Value.Falsche;
                                    karteikarte.Datum = pair.Value.Datum;
                                    karteikarte.ID = pair.Value.ID;
                                    // Zum besseren Suchen IDTemp
                                    gefiltert.Add(gefiltert.Count, karteikarte);
                                }
                            }
                            else
                            {
                                ClassKarteikarte karteikarte = new ClassKarteikarte();
                                karteikarte.Kategorie = pair.Value.Kategorie;
                                karteikarte.Frage = pair.Value.Frage;
                                karteikarte.Antwort = pair.Value.Antwort;
                                karteikarte.Phase = pair.Value.Phase;
                                karteikarte.Richtige = pair.Value.Richtige;
                                karteikarte.Falsche = pair.Value.Falsche;
                                karteikarte.Datum = pair.Value.Datum;
                                karteikarte.ID = pair.Value.ID;
                                // Zum besseren Suchen IDTemp
                                gefiltert.Add(gefiltert.Count, karteikarte);
                            }
                        }
                    }
                    else
                    {
                        if (kategorie != "Alle")
                        {
                            if (pair.Value.Kategorie == kategorie)
                            {
                                ClassKarteikarte karteikarte = new ClassKarteikarte();
                                karteikarte.Kategorie = pair.Value.Kategorie;
                                karteikarte.Frage = pair.Value.Frage;
                                karteikarte.Antwort = pair.Value.Antwort;
                                karteikarte.Phase = pair.Value.Phase;
                                karteikarte.Richtige = pair.Value.Richtige;
                                karteikarte.Falsche = pair.Value.Falsche;
                                karteikarte.Datum = pair.Value.Datum;
                                karteikarte.ID = pair.Value.ID;
                                // Zum besseren Suchen IDTemp
                                gefiltert.Add(gefiltert.Count, karteikarte);
                            }
                        }
                        else
                        {
                            ClassKarteikarte karteikarte = new ClassKarteikarte();
                            karteikarte.Kategorie = pair.Value.Kategorie;
                            karteikarte.Frage = pair.Value.Frage;
                            karteikarte.Antwort = pair.Value.Antwort;
                            karteikarte.Phase = pair.Value.Phase;
                            karteikarte.Richtige = pair.Value.Richtige;
                            karteikarte.Falsche = pair.Value.Falsche;
                            karteikarte.Datum = pair.Value.Datum;
                            karteikarte.ID = pair.Value.ID;
                            // Zum besseren Suchen IDTemp
                            gefiltert.Add(gefiltert.Count, karteikarte);
                        }
                    }
                }
                else if (pair.Value.Datum <= DateTime.Now)
                {
                    if (phase != "Alle")
                    {
                        if (pair.Value.Phase == Convert.ToInt32(phase.Split('.')[0].ToString()))
                        {
                            if (kategorie != "Alle")
                            {
                                if (pair.Value.Kategorie == kategorie)
                                {
                                    ClassKarteikarte karteikarte = new ClassKarteikarte();
                                    karteikarte.Kategorie = pair.Value.Kategorie;
                                    karteikarte.Frage = pair.Value.Frage;
                                    karteikarte.Antwort = pair.Value.Antwort;
                                    karteikarte.Phase = pair.Value.Phase;
                                    karteikarte.Richtige = pair.Value.Richtige;
                                    karteikarte.Falsche = pair.Value.Falsche;
                                    karteikarte.Datum = pair.Value.Datum;
                                    karteikarte.ID = pair.Value.ID;
                                    // Zum besseren Suchen IDTemp
                                    gefiltert.Add(gefiltert.Count, karteikarte);
                                }
                            }
                            else
                            {
                                ClassKarteikarte karteikarte = new ClassKarteikarte();
                                karteikarte.Kategorie = pair.Value.Kategorie;
                                karteikarte.Frage = pair.Value.Frage;
                                karteikarte.Antwort = pair.Value.Antwort;
                                karteikarte.Phase = pair.Value.Phase;
                                karteikarte.Richtige = pair.Value.Richtige;
                                karteikarte.Falsche = pair.Value.Falsche;
                                karteikarte.Datum = pair.Value.Datum;
                                karteikarte.ID = pair.Value.ID;
                                // Zum besseren Suchen IDTemp
                                gefiltert.Add(gefiltert.Count, karteikarte);
                            }
                        }
                    }
                    else
                    {
                        if (kategorie != "Alle")
                        {
                            if (pair.Value.Kategorie == kategorie)
                            {
                                ClassKarteikarte karteikarte = new ClassKarteikarte();
                                karteikarte.Kategorie = pair.Value.Kategorie;
                                karteikarte.Frage = pair.Value.Frage;
                                karteikarte.Antwort = pair.Value.Antwort;
                                karteikarte.Phase = pair.Value.Phase;
                                karteikarte.Richtige = pair.Value.Richtige;
                                karteikarte.Falsche = pair.Value.Falsche;
                                karteikarte.Datum = pair.Value.Datum;
                                karteikarte.ID = pair.Value.ID;
                                // Zum besseren Suchen IDTemp
                                gefiltert.Add(gefiltert.Count, karteikarte);
                            }
                        }
                        else
                        {
                            ClassKarteikarte karteikarte = new ClassKarteikarte();
                            karteikarte.Kategorie = pair.Value.Kategorie;
                            karteikarte.Frage = pair.Value.Frage;
                            karteikarte.Antwort = pair.Value.Antwort;
                            karteikarte.Phase = pair.Value.Phase;
                            karteikarte.Richtige = pair.Value.Richtige;
                            karteikarte.Falsche = pair.Value.Falsche;
                            karteikarte.Datum = pair.Value.Datum;
                            karteikarte.ID = pair.Value.ID;
                            // Zum besseren Suchen IDTemp
                            gefiltert.Add(gefiltert.Count, karteikarte);
                        }
                    }
                }

            }

        }
    }
}
