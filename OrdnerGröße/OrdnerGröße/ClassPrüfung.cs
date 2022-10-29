using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Ordner_;
using System.IO;

namespace OrdnerGröße
{
    public class ClassPrüfung
    {
        public enum Liste { Sorted = 0, Duplikate = 1, SelbeGröße = 2 };
        public enum Status { Läuft = 0, Fertig = 1 };
        //Status status = Status.Läuft;
        public string error = "";
        public string prüfungsPfad = "";
        public bool unterordnerPrüfen = false;
        private List<int> listSelectedAufgaben = new List<int>();
        public ClassOrdner ordner = null;
        public string verlauf = "";
        public List<ClassDatei> listsorted = new List<ClassDatei>();
        private Dictionary<double, List<ClassMD5>> dictVielleichtDuplikate = new Dictionary<double, List<ClassMD5>>();
        public Dictionary<double, ClassMD5> dictduplikate = new Dictionary<double, ClassMD5>();
        public List<ClassMD5> listboxduplikate = new List<ClassMD5>();
        public List<List<ClassDatei>> listboxselbeGröße = new List<List<ClassDatei>>();
        public Dictionary<double, List<ClassDatei>> dictselbegröße = new Dictionary<double, List<ClassDatei>>();
        public int geprüfteDateientop100 = 0;
        public int geprüfteDateienduplikate = 0;
        public List<Thread> threads; // 0: für Ordnerstruktur 1: für Top100, 2 für Duplikate
        public Stopwatch sw;
        public Stopwatch swDuplikate;
        public string straktuelleDateiGröße = "0";

        public ClassPrüfung()
        {

        }

        // Kontruktor für Laden einer Datei
        public ClassPrüfung(string prüfungsPfad, bool unterordnerPrüfen)
        {
            this.prüfungsPfad = prüfungsPfad;
            this.unterordnerPrüfen = unterordnerPrüfen;
        }

        public ClassPrüfung(string prüfungsPfad, List<int> listSelectedAufgaben, bool unterordnerPrüfen, Stopwatch sw)
        {
            this.sw = sw;
            this.unterordnerPrüfen = unterordnerPrüfen;
            this.listSelectedAufgaben = new List<int>();
            this.prüfungsPfad = prüfungsPfad;

            foreach (int i in listSelectedAufgaben)
                this.listSelectedAufgaben.Add(i);
            threads = new List<Thread>();

            if (listSelectedAufgaben.Contains(0))
                threads.Add(null); // erstelle erstmal leeren thread
            if (this.listSelectedAufgaben.Contains(1))
                threads.Add(null); // Top100 
            if (this.listSelectedAufgaben.Contains(2))
                threads.Add(null); // Duplikate
        }

        /*Summary
        * Versuche, die Prüfung zu starten
        * 
        * */
        public bool Starten()
        {
            try
            {
                // ES gibt keine Aufgabe abzuarbeiten
                if (threads.Count == 0)
                {
                    error = "Keine Aufgaben ausgewählt";
                    return false;
                }

                int mainid = Thread.CurrentThread.ManagedThreadId;
                //status = Status.Läuft;
                threads[0] = new Thread(delegate () { OrdnerstrukturBerechnen(); });
                int id = threads[0].ManagedThreadId;
                threads[0].Start();
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
            return true;
        }

        /*Summary
        * Versuche, die Prüfung zu stoppen
        * 
        * */
        public bool Stoppen()
        {
            if (threads == null) return true;

            try
            {
                foreach (Thread t in threads)
                    if (t != null)
                        t.Abort();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public List<ClassDatei> GetNichtIgnorierteDateien(List<string> ignorierteOrdner, List<ClassDatei> duplikate, bool ignoreAlbumArt)
        {
            List<ClassDatei> gültigeDateien = new List<ClassDatei>();
            foreach (ClassDatei cd in duplikate)
            {
                bool bIgnoriert = false;
                foreach (string ignoriert in ignorierteOrdner)
                    if (cd.pfad.Contains(ignoriert))
                    {
                        bIgnoriert = true;
                        break;
                    }

                if (bIgnoriert || (ignoreAlbumArt && cd.pfad.Contains("AlbumArt"))) continue;


                if (File.Exists(cd.pfad))
                    gültigeDateien.Add(cd);
            }
            return gültigeDateien;
        }

        public ClassDatei FindeDatei(string dateipfad, Liste l)
        {
            if (l == Liste.Duplikate)
            {
                foreach (ClassMD5 md5 in dictduplikate.Values)
                {
                    foreach (ClassDatei cd in md5.duplikate)
                        if (cd.pfad == dateipfad)
                            return cd;
                }
            }
            else if (l == Liste.SelbeGröße)
            {
                foreach (KeyValuePair<double, List<ClassDatei>> pair in dictselbegröße)
                {
                    foreach (ClassDatei cd in pair.Value)
                    {
                        if (cd.pfad == dateipfad)
                            return cd;
                    }
                }
            }

            else if (l == Liste.Sorted)
            {
                foreach (ClassDatei cd in listsorted)
                    if (cd.pfad == dateipfad)
                        return cd;
            }

            return null;
        }

        public KeyValuePair<double, ClassMD5> GetDuplikatElement(string eindateipfad)
        {
            foreach (KeyValuePair<double, ClassMD5> pair in dictduplikate)
                foreach (ClassDatei cd in pair.Value.duplikate)
                    if (cd.pfad == eindateipfad)
                        return pair;

            return new KeyValuePair<double, ClassMD5>();
        }


        public void SortDescending()
        {
            SortDescending(ordner);
        }

        private void SortDescending(ClassOrdner o)
        {
            List<ClassDatei> dateien = o.GetAllFiles().OrderByDescending(i => i.größe).ToList();
            listsorted = new List<ClassDatei>();
            foreach (ClassDatei cd in dateien)
                listsorted.Add(cd);
        }

        public int EntferneDateiAusTop100(string dateipfad)
        {
            for (int a = 0; a < listsorted.Count; a++)
            {
                if (listsorted[a].pfad == dateipfad)
                {
                    listsorted.RemoveAt(a);
                    return a;
                }
            }
            return -1;
        }

        private void ErstelleVielleichtDuplikate(ClassOrdner o, int ebene)
        {
            foreach (ClassOrdner subo in o.ordner)
                ErstelleVielleichtDuplikate(subo, ebene + 1);
            foreach (ClassDatei datei in o.dateien)
            {
                straktuelleDateiGröße = datei.GetGröße();
                geprüfteDateienduplikate++;
                // Duplikate suchen
                if (!dictVielleichtDuplikate.ContainsKey(datei.größe))
                {
                    ClassMD5 m = new ClassMD5(datei);
                    dictVielleichtDuplikate.Add(datei.größe, new List<ClassMD5>() { m });
                }
                else // selbe Größe
                    try
                    {
                        // Test
                        var md5 = MD5.Create();
                        FileStream file = new FileStream(datei.pfad, FileMode.Open, FileAccess.Read);
                        byte[] data = md5.ComputeHash(file);
                        file.Close();

                        for (int a = 0; a < dictVielleichtDuplikate[datei.größe].Count; a++)
                        {
                            if (dictVielleichtDuplikate[datei.größe][a].md5 != null)
                            {
                                // MD5 Prüfsumme schon vorhanden, adde andere Datei
                                if (sindIdentisch(dictVielleichtDuplikate[datei.größe][a].md5, data))
                                {
                                    dictVielleichtDuplikate[datei.größe][a].duplikate.Add(datei);
                                    break;
                                }
                            }
                            else
                            {
                                md5 = MD5.Create();
                                file = new FileStream(dictVielleichtDuplikate[datei.größe][a].duplikate.First().pfad, FileMode.Open, FileAccess.Read);
                                byte[] datatest = md5.ComputeHash(file);
                                file.Close();
                                if (sindIdentisch(data, datatest))
                                {
                                    dictVielleichtDuplikate[datei.größe][a].md5 = data;
                                    dictVielleichtDuplikate[datei.größe][a].duplikate.Add(datei);
                                }
                                else
                                {
                                    ClassMD5 m = new ClassMD5(datei, datatest);
                                    dictVielleichtDuplikate[datei.größe].Add(m);
                                }
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ordner.fehlermeldung != "")
                            ordner.fehlermeldung += "\n" + datei.name + ": " + ex.Message;
                        else
                            ordner.fehlermeldung += datei.name + ": " + ex.Message;
                    }
            }

            if (ebene == 0)
            {
                MDuplikateListeUpdaten();
            }
        }
        public void ListenAktualisieren()
        {
            // Ordnerstruktur aktualisieren
            ordner.AktualisierenPhysisch();

            // Sortierte Liste aktualisieren
            for (int a = 0; a < listsorted.Count; a++)
            {
                if (!File.Exists(listsorted[a].pfad))
                    listsorted.RemoveAt(a--);
            }

            // Duplikate aktualisieren
            for (int a = 0; a < dictduplikate.Count; a++)
            {
                for (int b = 0; b < dictduplikate.ElementAt(a).Value.duplikate.Count; b++)
                {
                    if (!File.Exists(dictduplikate.ElementAt(a).Value.duplikate[b].pfad))
                        dictduplikate.ElementAt(a).Value.duplikate.RemoveAt(b--);
                }

                if (dictduplikate.ElementAt(a).Value.duplikate.Count <= 1)
                    dictduplikate.Remove(dictduplikate.ElementAt(a).Key);
            }

            for (int a = 0; a < dictselbegröße.Count; a++)
            {
                for (int b = 0; b < dictselbegröße.ElementAt(a).Value.Count; b++)
                {
                    if (!File.Exists(dictselbegröße.ElementAt(a).Value[b].pfad))
                        dictselbegröße.ElementAt(a).Value.RemoveAt(b--);
                }

                if (dictselbegröße.ElementAt(a).Value.Count <= 1)
                    dictselbegröße.Remove(dictselbegröße.ElementAt(a).Key);
            }
        }

        public bool EntferneDateiAusSelbeGröße(ClassDatei cd, ref List<int> listboxIndices)
        {
            bool gefunden = false;
            for (int a = 0; a < dictselbegröße.Count; a++)
            {
                for (int b = 0; b < dictselbegröße.ElementAt(a).Value.Count; b++)
                {
                    if (cd != dictselbegröße.ElementAt(a).Value[b]) continue;

                    gefunden = true;
                    dictselbegröße.ElementAt(a).Value.RemoveAt(b);
                    b--;
                }

                if (dictselbegröße.ElementAt(a).Value.Count <= 1)
                    dictselbegröße.Remove(dictselbegröße.ElementAt(a).Key);
            }

            if (!gefunden) return false;

            // aus Listbox entfernen
            for (int a = 0; a < listboxselbeGröße.Count; a++)
            {
                for (int b = 0; b < listboxselbeGröße[a].Count; b++)
                {
                    if (listboxselbeGröße[a][b] != cd) continue;

                    listboxselbeGröße[a].RemoveAt(b);
                    b--;
                }

                if (listboxselbeGröße[a].Count <= 1)
                {
                    listboxselbeGröße.RemoveAt(a);
                    listboxIndices.Add(a);
                    a--;
                }
            }

            return true;
        }

        public bool EntferneDateiAusDuplikate(ClassDatei cd, ref List<int> listboxIndices)
        {
            bool gefunden = false;
            // Aus dict
            for (int a = 0; a < dictduplikate.Count; a++)
            {
                for (int b = 0; b < dictduplikate.ElementAt(a).Value.duplikate.Count; b++)
                {
                    if (cd != dictduplikate.ElementAt(a).Value.duplikate[b]) continue;

                    gefunden = true;
                    dictduplikate.ElementAt(a).Value.duplikate.RemoveAt(b);
                    b--;
                }

                if (dictduplikate.ElementAt(a).Value.duplikate.Count <= 1)
                    dictduplikate.Remove(dictduplikate.ElementAt(a).Key);
            }

            if (!gefunden) return false;

            gefunden = false;
            // aus liste
            for (int a = 0; a < listboxduplikate.Count; a++)
            {
                for (int b = 0; b < listboxduplikate[a].duplikate.Count; b++)
                {
                    if (listboxduplikate[a].duplikate[b] != cd) continue;

                    gefunden = true;
                    listboxduplikate[a].duplikate.RemoveAt(b);
                    b--;
                }

                if (listboxduplikate[a].duplikate.Count <= 1)
                {
                    listboxIndices.Add(a);
                    listboxduplikate.RemoveAt(a);
                    a--;
                }
            }

            return gefunden;
        }

        private bool sindIdentisch(byte[] array1, byte[] array2)
        {
            for (int a = 0; a < array1.Length; a++)
            {
                if (array1[a] != array2[a])
                    return false;
            }
            return true;
        }

        public string GetTop100Größe()
        {
            long größe = 0;
            int index = 0;
            foreach (ClassDatei cd in listsorted)
            {
                if (++index == 101) break;
                größe += cd.größe;
            }

            return ClassDatei.GetGröße(größe);
        }


        private void PrüfeDateien(ClassOrdner o, int ebene)
        {
            // Top100 prüfen?
            if (!listSelectedAufgaben.Contains(1)) return;
            else
            {
                threads[1] = new Thread(delegate () { SortDescending(ordner); });
                threads[1].Start();
            }

            // Duplikate prüfen?
            if (!listSelectedAufgaben.Contains(2)) return;
            else
            {
                // Erst jetzt kommt dritter Thread zum Einsatz
                threads[2] = new Thread(delegate () { ErstelleVielleichtDuplikate(ordner, 0); });

                // wie viele Dateien müssen überprüft werden?
                swDuplikate = new Stopwatch();
                swDuplikate.Start();
                threads[2].Start();
            }
        }

        public string GetDuplikatenInfo()
        {
            long msleft = (sw.ElapsedMilliseconds / geprüfteDateienduplikate) * (ordner.anzahlDateien - geprüfteDateienduplikate);
            string zeit = LongToTime(msleft);
            string info = "noch " + zeit + "(" + geprüfteDateienduplikate + "/" + ordner.anzahlDateien + ")    " + straktuelleDateiGröße;
            return info;
        }

        public string LongToTime(long l)
        {
            l /= 1000;
            int sek = Convert.ToInt32(l);
            int h = sek / 3600;
            sek -= 3600 * h;
            int min = sek / 60;
            sek -= 60 * min;

            string smin = "";
            if (min < 10)
                smin = "0" + min;
            else
                smin = "" + min;

            string ssek = "";
            if (sek < 10)
                ssek = "0" + sek;
            else
                ssek = "" + sek;

            string sh = "";
            if (h < 10)
                sh = "0" + h;
            else
                sh = "" + h;

            if (h > 0)
                return sh + ":" + smin + ":" + ssek + " h";

            return smin + ":" + ssek + " min";
        }


        private void MDuplikateListeUpdaten()
        {
            dictduplikate = new Dictionary<double, ClassMD5>();
            dictselbegröße = new Dictionary<double, List<ClassDatei>>();

            // Duplikate
            verlauf += "Für DuplikateListeupdaten:";

            for (int a = 0; a < dictVielleichtDuplikate.Count; a++)
            {
                double key = dictVielleichtDuplikate.ElementAt(a).Key;
                List<ClassMD5> value = dictVielleichtDuplikate.ElementAt(a).Value;

                // Für alle verschiedenen MD5 Werte
                for (int b = 0; b < value.Count; b++)
                {
                    // Gibt für diese Datei kein Duplikat => selbe Größe
                    if (value[b].duplikate.Count == 1)
                    {
                        if (dictselbegröße.ContainsKey(key))
                            dictselbegröße[key].Add(value[b].duplikate.First());
                        else
                            dictselbegröße.Add(key, new List<ClassDatei>() { value[b].duplikate.First() });
                    }
                    else // Gibt für diese Datei Duplikate mit selber MD5
                    {
                        // Füge neuen Eintrag hinzu
                        double tempkey = key;
                        while (dictduplikate.ContainsKey(tempkey))
                            tempkey -= (0.00001);

                        dictduplikate.Add(tempkey, value[b]);

                    }
                }

                // Entferne Dateien, die doch nicht derselben Größe angehören
                if (dictselbegröße.ContainsKey(key))
                {
                    if (dictselbegröße[key].Count == 1)
                        dictselbegröße.Remove(key);
                    else
                    {
                        // Umsortieren, dass (1) und kopie nicht im ersten item stehen
                        string erste = dictselbegröße[key].First().pfad;
                        if (erste.Contains("(1)") || erste.ToLower().Contains("kopie"))
                        {
                            for (int c = 1; c < dictselbegröße[key].Count; c++)
                            {
                                string aktuelle = dictselbegröße[key][c].pfad;
                                if (!aktuelle.Contains("(1)") && !aktuelle.ToLower().Contains("kopie"))
                                {
                                    dictselbegröße[key][0].pfad = aktuelle;
                                    dictselbegröße[key][c].pfad = erste;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // Sortiere nach Größe absteigend
            SortiereDuplikatDict(ref dictduplikate);
            SortiereSelbeGrößeDict(ref dictselbegröße);
        }

        public long GetDuplikateSpeicher()
        {
            long speicher = 0;

            foreach (var d in dictduplikate)
                speicher += d.Value.duplikate.First().größe * (d.Value.duplikate.Count - 1);
            return speicher;
        }

        public long GetListBoxDuplikateSpeicher()
        {
            long speicher = 0;

            foreach (var d in listboxduplikate)
                speicher += (d.duplikate.Count - 1) * d.duplikate.First().größe;
            return speicher;
        }


        public long GetSelbeGrößeeSpeicher()
        {
            long speicher = 0;

            foreach (var d in dictselbegröße)
                speicher += d.Value.First().größe * (d.Value.Count - 1);
            return speicher;
        }

        public long GetListBoxSelbeGrößeSpeicher()
        {
            long speicher = 0;

            foreach (var d in listboxselbeGröße)
                speicher += (d.Count - 1) * d.First().größe;
            return speicher;
        }


        public Dictionary<double, List<ClassDatei>> GetSelbeGröße()
        {
            return dictselbegröße;
        }

        public Dictionary<double, ClassMD5> GetDuplikatenListe()
        {
            return dictduplikate;
        }


        public void SortiereSelbeGrößeDict(ref Dictionary<double, List<ClassDatei>> dict)
        {
            Dictionary<double, List<ClassDatei>> temp = new Dictionary<double, List<ClassDatei>>();
            foreach (KeyValuePair<double, List<ClassDatei>> pair in dict.OrderByDescending(i => i.Key))
                temp.Add(pair.Key, pair.Value);
            dict.Clear();

            foreach (KeyValuePair<double, List<ClassDatei>> pair in temp)
                dict.Add(pair.Key, pair.Value);
        }


        public void SortiereDuplikatDict(ref Dictionary<double, ClassMD5> dict)
        {
            Dictionary<double, ClassMD5> temp = new Dictionary<double, ClassMD5>();
            foreach (KeyValuePair<double, ClassMD5> pair in dict.OrderByDescending(i => i.Key))
                temp.Add(pair.Key, pair.Value);
            dict.Clear();

            foreach (KeyValuePair<double, ClassMD5> pair in temp)
                dict.Add(pair.Key, pair.Value);
        }

        public void OrdnerstrukturBerechnen()
        {
            ordner = new ClassOrdner(prüfungsPfad, unterordnerPrüfen);

            // Hier stoppen?
            if (!listSelectedAufgaben.Contains(1))
            {
                //status = Status.Fertig;
                int id = threads[0].ManagedThreadId; // hier Beenden des Threads          
                threads[0].Abort();
                return;
            }

            // noch selber Thread
            PrüfeDateien(ordner, 0);
        }


        public string OrdnerstrukturInDatei(ClassOrdner o)
        {
            string datei = "";

            foreach (ClassOrdner subo in o.ordner)
                datei += "\n" + OrdnerstrukturInDatei(subo);
            foreach (ClassDatei subd in o.dateien)
                datei += "\n" + subd.größe + ";" + subd.name;

            return datei;
        }

    }
}
