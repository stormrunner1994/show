using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace Ordner_
{
    public class ClassSynchronisierung
    {
        public enum SynchroModus { RechtsNachLinks = 1, LinksNachRechts = 2, Beide = 3 }
        public enum Status { Aktuell = 1, NichtAktuell = 2 }
        private SynchroModus modus;
        public int count = 0;
        public int max = 0;
        public List<Error> errors = new List<Error>();
        public List<string> nichtGelöschteDateien = new List<string>();
        public List<string> nichtGelöschteOrdner = new List<string>();
        public List<string> nichtKopierteDateien = new List<string>();
        public string synchrostatus = "kein Status";
        public bool fertig = false;
        public bool istAktuell = false;
        public ClassOrdner schnittmenge;
        public ClassOrdner nurpfad1;
        public ClassOrdner nurpfad2;
        public Thread thread;
        public string pfad1 = "";
        public string pfad2 = "";
        public DateTime letzteSynchro = DateTime.MinValue;
        private List<string> überschreibenTypen = new List<string>() { "doc", "docx", "zip", "txt", "xlsx", "xls" };
        private bool leereOrdnerLöschen;
        private bool alleDateienErsetzen = false;
        private bool abbrechen = false;


        public ClassSynchronisierung(string pfad1, string pfad2)
        {
            this.pfad1 = pfad1;
            this.pfad2 = pfad2;
        }

        public ClassSynchronisierung(string pfad1, string pfad2, SynchroModus modus, bool leereOrdnerLöschen, bool alleDateienErsetzen)
        {
            this.pfad1 = pfad1;
            this.pfad2 = pfad2;
            this.modus = modus;
            this.leereOrdnerLöschen = leereOrdnerLöschen;
            this.alleDateienErsetzen = alleDateienErsetzen;
        }

        /*Summary
       * Welche Dateitypen sollen immer aktualisiert/überschrieben werden?
       * */
        public void DateiTypenAnpassen(List<string> überschreibenTypen)
        {
            überschreibenTypen.Clear();
            this.überschreibenTypen.AddRange(überschreibenTypen);
        }


        public Status GetStatus(ref string statustext)
        {
            string sicherungspfad = this.pfad1 + "\\" + this.pfad2.Split('\\').Last();
            if (nurpfad1.anzahlDateien + nurpfad1.anzahlOrdner
                + nurpfad2.anzahlDateien + nurpfad2.anzahlOrdner == 0)
            {
                statustext = sicherungspfad + " ist aktuell!";
                return Status.Aktuell;
            }
            if (nurpfad2.anzahlDateien + nurpfad2.anzahlOrdner > 0)
                statustext = nurpfad2.pfad + " hat eigene Dateien!";
            if (nurpfad1.anzahlDateien + nurpfad1.anzahlOrdner > 0)
            {
                if (statustext == "") statustext = sicherungspfad + " hat eigene Dateien!";
                else statustext += "\n" + sicherungspfad + " hat eigene Dateien!";
            }
            return Status.NichtAktuell;
        }

        public string GetProzent()
        {
            if (max == 0) return "0%";

            return count * 100 / max + "%";
        }

        public bool SynchronisierungLäuft()
        {
            if (thread != null && thread.IsAlive)
                return true;
            return false;
        }


        public bool PrüfeAufAktualität()
        {
            string sicherungspfad = pfad1 + "\\" + pfad2.Split('\\').Last();
            List<ClassOrdner> list = ClassOrdner.GetSchnittmengeDifferenzen(new ClassOrdner(sicherungspfad, true), new ClassOrdner(pfad2, true));
            schnittmenge = list[0];
            nurpfad1 = list[1];
            nurpfad2 = list[2];

            if (nurpfad1.anzahlDateien + nurpfad1.anzahlOrdner == 0
                && nurpfad2.anzahlDateien + nurpfad2.anzahlOrdner == 0)
            {
                istAktuell = true;
                return true;
            }

            return false;
        }

        private bool SchreibeZuDatei(List<string> list, string dateinameOhneTyp)
        {
            try
            {
                StreamWriter sw = new StreamWriter(@dateinameOhneTyp + ".csv");
                foreach (string el in list)
                    sw.WriteLine(el);
                sw.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public bool SichereNichtGetaneAufgaben()
        {
            if (SichereNichtGelöschteDateien()
                && SichereNichtGelöschteOrdner()
                && SichereNichtKopierteDateien()) return true;

            return false;
        }
        private bool SichereNichtGelöschteDateien()
        {
            return SchreibeZuDatei(nichtGelöschteDateien, "nichtGelöschteDateien");
        }
        private bool SichereNichtGelöschteOrdner()
        {
            return SchreibeZuDatei(nichtGelöschteOrdner, "nichtGelöschteOrdner");
        }
        private bool SichereNichtKopierteDateien()
        {
            return SchreibeZuDatei(nichtKopierteDateien, "nichtKopierteDateien");
        }

        public bool SynchronisierenThread(SynchroModus modus, bool leereOrdnerLöschen, bool alleDateienErsetzen)
        {
            this.modus = modus;
            this.leereOrdnerLöschen = leereOrdnerLöschen;
            this.alleDateienErsetzen = alleDateienErsetzen;
            return SynchronisierenThread();
        }

        public bool SynchroAbbrechen()
        {
            abbrechen = true;
            return true;
        }
        public bool SynchronisierenThread()
        {
            try
            {
                abbrechen = false;
                thread = new Thread(delegate () { Synchronisieren(); });
                thread.Start();
            }
            catch (Exception ex)
            {
                errors.Add(new Error(ex, "Synchronierung schlug fehl!", "", Error.TYP.sonstiges));
                return false;
            }
            return true;
        }

        public bool Synchronisieren()
        {
            if (abbrechen) { synchrostatus = "Abgebrochen"; return true; }

            errors = new List<Error>();
            nichtGelöschteDateien = new List<string>();
            nichtGelöschteOrdner = new List<string>();
            nichtKopierteDateien = new List<string>();

            synchrostatus = "Vergleiche beide Pfade";
            string sicherungspfad = pfad1 + "\\" + pfad2.Split('\\').Last();

            if (schnittmenge == null || nurpfad1 == null || nurpfad2 == null)
            {
                List<ClassOrdner> list = ClassOrdner.GetSchnittmengeDifferenzen(new ClassOrdner(pfad1, true), new ClassOrdner(pfad2, true));
                schnittmenge = list[0];
                nurpfad1 = list[1];
                nurpfad2 = list[2];
            }

            if (abbrechen) { synchrostatus = "Abgebrochen"; return true; }

            if (modus == SynchroModus.RechtsNachLinks)
            {
                return SynchronisierenPhysischNach(nurpfad2, nurpfad1, schnittmenge, leereOrdnerLöschen);
            }
            else if (modus == SynchroModus.LinksNachRechts)
            {
                return SynchronisierenPhysischNach(nurpfad1, nurpfad2, schnittmenge, leereOrdnerLöschen);
            }
            else if (modus == SynchroModus.Beide)
            {
                return SynchronisierenPhysischBeide(nurpfad1, nurpfad2, schnittmenge);
            }
            errors.Add(new Error("Der SynchroModus wurde nicht richtig gewählt!", "", Error.TYP.sonstiges));
            return false;
        }



        private bool SynchronisierenPhysischBeide(ClassOrdner nurpfad1, ClassOrdner nurpfad2, ClassOrdner schnittmenge)
        {
            List<ClassDatei> nurvon = nurpfad1.GetAllFiles();
            List<ClassDatei> nurnach = nurpfad2.GetAllFiles();
            List<ClassDatei> schnittmengeDateien = schnittmenge.GetAllFiles();
            max = nurnach.Count + nurvon.Count + schnittmengeDateien.Count;
            count = 0;

            synchrostatus = "Kopieren von " + nurpfad1.pfad + " nach " + nurpfad2.pfad;
            if (!KopiereNach(nurvon, nurpfad1.pfad, nurpfad2.pfad)) return false;

            synchrostatus = "Kopieren von " + nurpfad2.pfad + " nach " + nurpfad1.pfad;
            if (!KopiereNach(nurnach, nurpfad2.pfad, nurpfad1.pfad)) return false;

            synchrostatus = "Aktualisiere Schnittmenge";
            if (!AktualisiereSchnittmenge(schnittmengeDateien, nurpfad1.pfad, nurpfad2.pfad)) return false;

            synchrostatus = "Fertig synchronisiert";
            fertig = true;
            return true;
        }


        private bool KopiereNach(List<ClassDatei> nurvon, string vonordner, string nachordner)
        {
            max = nurvon.Count;
            count = 0;
            foreach (ClassDatei cd in nurvon)
            {
                if (abbrechen) { synchrostatus = "Abgebrochen"; return true; }
                try
                {
                    count++;
                    string nachpfad = cd.pfad.Replace(vonordner, nachordner);
                    if (!ClassOrdner.GarantiereOrdnerPhysisch(nachpfad))
                    {
                        errors.Add(new Error("Pfad " + cd.ordnerpfad + " konnte nicht garantiert werden.", cd.ordnerpfad, Error.TYP.sonstiges));
                        nichtKopierteDateien.Add(cd.pfad);
                        continue;
                    }

                    File.Copy(cd.pfad, nachpfad);
                }
                catch (Exception ex)
                {
                    nichtKopierteDateien.Add(cd.pfad);
                    errors.Add(new Error(ex, "Datei " + cd.pfad + " konnte nicht kopiert werden.", cd.pfad, Error.TYP.nichtkopiert));
                }
            }
            return true;
        }

        private bool AktualisiereSchnittmenge(List<ClassDatei> schnittmengeDateien, string vonpfad, string nachpfad)
        {
            max = schnittmengeDateien.Count;
            foreach (ClassDatei cd in schnittmengeDateien)
            {
                if (abbrechen) { synchrostatus = "Abgebrochen"; return true; }
                count++;
                if (!alleDateienErsetzen && !überschreibenTypen.Contains(cd.dateityp)) continue;

                string vondateipfad = "";
                string nachdateipfad = "";

                // ist Teil des "Von-Pfades"
                if (cd.pfad.Contains(vonpfad))
                {
                    vondateipfad = cd.pfad;
                    nachdateipfad = vondateipfad.Replace(vonpfad, nachpfad);
                }
                // ist Teil des "Nach-Pfades"
                else if (cd.pfad.Contains(nachpfad))
                {
                    nachdateipfad = cd.pfad;
                    vondateipfad = nachdateipfad.Replace(nachpfad, vonpfad);
                }
                else
                {
                    errors.Add(new Error("Datei " + cd.pfad + " konnte keinem Pfad zugeordnet werden.", cd.pfad, Error.TYP.sonstiges));
                    nichtKopierteDateien.Add(cd.pfad);
                }

                try
                {
                    File.Copy(vondateipfad, nachdateipfad, true);
                }
                catch (Exception ex)
                {
                    errors.Add(new Error(ex, "Datei " + nachdateipfad + " konnte nicht überschrieben werden.", cd.pfad, Error.TYP.nichtüberschrieben));
                    nichtKopierteDateien.Add(cd.pfad);
                }
            }
            return true;
        }


        private bool SynchronisierenPhysischNach(ClassOrdner von, ClassOrdner nach, ClassOrdner schnittmenge, bool leereOrdnerLöschen)
        {
            // kopiere alle dateien von "von" nach "nach"

            synchrostatus = "Sammle Dateien nur im jeweiligen Pfad";
            List<ClassDatei> nurvon = von.GetAllFiles();
            List<ClassDatei> nurnach = nach.GetAllFiles();
            List<ClassDatei> schnittmengeDateien = schnittmenge.GetAllFiles();

            synchrostatus = "Kopiere Dateien";
            if (!KopiereNach(nurvon, von.pfad, nach.pfad)) return false;

            // entferne alle Dateien aus "nach"
            max = nurnach.Count;
            count = 0;
            synchrostatus = "entferne Dateien";
            foreach (ClassDatei cd in nurnach)
            {
                if (abbrechen) { synchrostatus = "Abgebrochen"; return true; }
                count++;
                if (!cd.EntfernePhysisch())
                {
                    errors.Add(new Error("Datei " + cd.pfad + " konnte nicht gelöscht werden.", cd.pfad, Error.TYP.nichtgelöscht));
                    nichtGelöschteDateien.Add(cd.pfad);
                }
            }

            synchrostatus = "Aktualisiere Schnittmenge";
            if (!AktualisiereSchnittmenge(schnittmengeDateien, von.pfad, nach.pfad)) return false;

            if (leereOrdnerLöschen)
            {
                synchrostatus = "lösche leere Ordner";
                List<string> leereOrdner = new ClassOrdner(nach.pfad, nach.unterordner).GetLeereOrdner(true, false);
                max = leereOrdner.Count;
                count = 0;
                foreach (string leer in leereOrdner)
                {
                    if (abbrechen) { synchrostatus = "Abgebrochen"; return true; }
                    count++;
                    try
                    {
                        Directory.Delete(leer, true);
                    }
                    catch (Exception ex)
                    {
                        errors.Add(new Error(ex, "Ordner " + leer + " konnte nicht gelöscht werden!", leer, Error.TYP.nichtgelöscht));
                        nichtGelöschteOrdner.Add(leer);
                    }
                }
            }

            synchrostatus = "Fertig synchronisiert";
            fertig = true;
            return true;
        }

    }

    public class ClassOrdner
    {
        public static List<string> staticErrors = new List<string>();
        public List<ClassOrdner> ordner;
        public List<ClassDatei> dateien;
        public string pfad;
        public long größe;
        public string name;
        public bool unterordner = false;
        public string fehlermeldung = "";
        public int anzahlOrdner = 0;
        public int anzahlDateien = 0;


        /*Summary
        * Copy Contructor
        * */
        public ClassOrdner(ClassOrdner co)
        {
            this.ordner = new List<ClassOrdner>();
            this.dateien = new List<ClassDatei>();
            this.pfad = co.pfad;
            this.größe = co.größe;
            this.name = co.name;
            this.unterordner = co.unterordner;
            this.fehlermeldung = co.fehlermeldung;
            this.anzahlOrdner = co.anzahlOrdner;
            this.anzahlDateien = co.anzahlDateien;
            foreach (ClassOrdner sub in co.ordner)
                ordner.Add(new ClassOrdner(sub));
            foreach (ClassDatei sub in co.dateien)
                dateien.Add(new ClassDatei(sub));
        }

        /*Summary
        * Leeres Ordnerobjekt
        * */
        public ClassOrdner(string pfad)
        {
            ordner = new List<ClassOrdner>();
            dateien = new List<ClassDatei>();
            this.pfad = pfad;
            größe = 0;
            this.name = pfad.Split('\\').Last();
        }

        /*Summary
        * Erhalte Ordnerstruktur für einen Ordnerpfad
        * */
        public ClassOrdner(string pfad, bool unterordner, int ebene = 0)
        {
            this.unterordner = unterordner;
            größe = 0;
            this.pfad = pfad;
            this.name = pfad.Split('\\').Last();
            ordner = new List<ClassOrdner>();
            dateien = new List<ClassDatei>();
            string[] files;
            string[] directories;
            try
            {
                files = Directory.GetFiles(pfad);

                if (unterordner)
                {
                    directories = Directory.GetDirectories(pfad);
                    anzahlOrdner += directories.Length;

                    for (int a = 0; a < directories.Length; a++)
                    {
                        ClassOrdner o = new ClassOrdner(directories[a], unterordner, ebene + 1);
                        anzahlDateien += o.anzahlDateien;
                        anzahlOrdner += o.anzahlOrdner;
                        if (o.größe >= 0)
                        {
                            ordner.Add(o);

                            if (o.fehlermeldung != "")
                            {
                                if (fehlermeldung != "")
                                    fehlermeldung += "\n" + o.fehlermeldung;
                                else
                                    fehlermeldung += fehlermeldung;
                            }
                            größe += o.größe;
                        }
                    }
                }

                anzahlDateien += files.Length;
                for (int a = 0; a < files.Length; a++)
                {
                    ClassDatei c = new ClassDatei(files[a]);
                    dateien.Add(c);
                    größe += c.größe;
                }
            }
            catch (Exception ex)
            {
                if (fehlermeldung != "")
                    fehlermeldung += "\n" + ex.Message;
                else
                    fehlermeldung += ex.Message;
            }

            // Ganz am ende
            if (ebene == 0)
                SetDateiIDs();
        }

        private void SetDateiIDs()
        {
            long id = 0;
            foreach (ClassDatei cd in GetAllFiles())
                cd.id = id++;
        }


        /*Summary
       * Untersuche zwei Pfade, hole 
       * 1) Schnittmenge
       * 2) Nur in Pfad1
       * 3) Nur in Pfad2
       * */
        public static List<ClassOrdner> GetSchnittmengeDifferenzen(string pfad1, string pfad2, bool unterordner)
        {
            ClassOrdner links = new ClassOrdner(pfad1, unterordner);
            ClassOrdner rechts = new ClassOrdner(pfad2, unterordner);
            return GetSchnittmengeDifferenzen(links, rechts);
        }


        /*Summary
       * Untersuche zwei ClassOrdner Objekten, hole 
       * 1) Schnittmenge
       * 2) Nur in linkem Ordnerobjekt
       * 3) Nur in rechtem Ordnerobjekt
       * */
        public static List<ClassOrdner> GetSchnittmengeDifferenzen(ClassOrdner links, ClassOrdner rechts)
        {
            ClassOrdner schnittmenge = new ClassOrdner(links);
            ClassOrdner nurlinks = EntferneIdente(new ClassOrdner(links), new ClassOrdner(rechts), schnittmenge);
            nurlinks.Aktualisieren();
            ClassOrdner nurrechts = EntferneIdente(new ClassOrdner(rechts), new ClassOrdner(links));
            nurrechts.Aktualisieren();
            schnittmenge.Aktualisieren();

            return new List<ClassOrdner>() { schnittmenge, nurlinks, nurrechts };
        }

        public static bool ÖffneImExplorer(string ordnerpfad)
        {
            if (Directory.Exists(ordnerpfad))
                Process.Start(ordnerpfad);
            else return false;

            return true;
        }        

        public static List<ClassOrdner> GetSchnittmengeDifferenzenMD5(ClassOrdner links, ClassOrdner rechts)
        {
            return new List<ClassOrdner>() { null, null, null };
        }

        public static void LöscheOrdnerPhsyisch(ref List<string> pfade)
        {
            staticErrors.Clear();
            for (int a = 0; a < pfade.Count; a++)
            {
                try
                {
                    Directory.Delete(pfade[a]);
                    pfade.RemoveAt(a);
                    a--;
                }
                catch (Exception)
                {
                    staticErrors.Add(pfade[a] + " konnte nicht gelöscht werden");
                }
            }
        }

        public static bool LöscheOrdnerPhsyisch(string pfad)
        {
            try
            {
                Directory.Delete(pfad);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        public List<string> GetLeereOrdner(bool nurObererOrdner, bool entferneRoot)
        {
            List<string> leereordner = new List<string>();
            int ileere = 0;
            foreach (ClassOrdner co in ordner)
            {
                bool istLeer = false;
                List<string> leeresubordner = co.GetLeereOrdner(nurObererOrdner, ref istLeer);
                if (istLeer)
                {
                    ileere++;
                    if (nurObererOrdner) leereordner.Add(co.pfad);
                    else
                    {
                        leereordner.AddRange(leeresubordner);
                        leereordner.Add(co.pfad);
                    }
                }
                else leereordner.AddRange(leeresubordner);
            }

            if (ileere == ordner.Count && dateien.Count == 0 && entferneRoot)
                return new List<string>() { this.pfad };

            return leereordner;
        }

        private List<string> GetLeereOrdner(bool nurObererOrdner, ref bool istLeer)
        {
            List<string> leereordner = new List<string>();
            int ileere = 0;
            foreach (ClassOrdner co in ordner)
            {
                List<string> leeresubordner = co.GetLeereOrdner(nurObererOrdner, ref istLeer);

                if (istLeer)
                {
                    ileere++;
                    if (nurObererOrdner) leereordner.Add(co.pfad);
                    else
                    {
                        leereordner.AddRange(leeresubordner);
                        leereordner.Add(co.pfad);
                    }
                }
                else leereordner.AddRange(leeresubordner);
            }

            if (dateien.Count > 0) istLeer = false;

            if (ileere == ordner.Count && dateien.Count == 0)
            {
                istLeer = true;
                if (nurObererOrdner)
                    return new List<string>() { this.pfad };
            }

            return leereordner;
        }


        /*Summary
       * Entferne alle Dateien und Ordner, die in beiden Ordnerstrukturen enthalten sind
       * */
        public static ClassOrdner EntferneIdente(ClassOrdner von, ClassOrdner löschmenge)
        {
            // Ordnerweise
            for (int a = 0; a < von.ordner.Count; a++)
            {
                string ordnername = von.ordner[a].name;

                int index = -1;
                // finde entsprechenden Ordner bei löschmenge
                for (int b = 0; b < löschmenge.ordner.Count; b++)
                {
                    if (ordnername == löschmenge.ordner[b].name)
                    {
                        index = b;
                        break;
                    }
                }

                // Ordner nicht gefunden
                if (index == -1) continue;

                // Gehe bei beiden Ordner eine Ebene tiefer
                ClassOrdner ret = EntferneIdente(von.ordner[a], löschmenge.ordner[index]);

                // Wurden jeweilige Ordner geleert?
                if (ret.ordner.Count + ret.dateien.Count == 0)
                    von.ordner.RemoveAt(a--);
                if (löschmenge.ordner[index].ordner.Count + löschmenge.ordner[index].dateien.Count == 0)
                    löschmenge.ordner.RemoveAt(index);
            }

            // Dateienweise
            for (int a = 0; a < von.dateien.Count; a++)
            {
                string dateiname = von.dateien[a].nameMitTyp;
                int index = -1;

                // finde entsprechende Datei bei löschmenge
                for (int b = 0; b < löschmenge.dateien.Count; b++)
                {
                    if (dateiname == löschmenge.dateien[b].nameMitTyp)
                    {
                        index = b;
                        break;
                    }
                }

                // Datei nicht gefunden
                if (index == -1) continue;

                // Entferne aus Listen
                von.dateien.RemoveAt(a--);
                löschmenge.dateien.RemoveAt(index);
            }

            return von;
        }

        /*Summary
       * Entferne alle Dateien und Ordner, die in beiden Ordnerstrukturen enthalten sind und merke die Schnittmenge
       * */
        public static ClassOrdner EntferneIdente(ClassOrdner von, ClassOrdner löschmenge, ClassOrdner schnittmenge)
        {
            // Ordnerweise
            for (int a = 0; a < von.ordner.Count; a++)
            {
                string ordnername = von.ordner[a].name;

                int index = -1;
                // finde entsprechenden Ordner bei löschmenge
                for (int b = 0; b < löschmenge.ordner.Count; b++)
                {
                    if (ordnername == löschmenge.ordner[b].name)
                    {
                        index = b;
                        break;
                    }
                }

                // Ordner nicht gefunden
                if (index == -1) continue;

                // Gehe bei beiden Ordner eine Ebene tiefer
                ClassOrdner ret = EntferneIdente(von.ordner[a], löschmenge.ordner[index], schnittmenge);

                // Wurden jeweilige Ordner geleert?
                if (ret.ordner.Count + ret.dateien.Count == 0)
                    von.ordner.RemoveAt(a--);
                if (löschmenge.ordner[index].ordner.Count + löschmenge.ordner[index].dateien.Count == 0)
                    löschmenge.ordner.RemoveAt(index);
            }

            // Dateienweise
            for (int a = 0; a < von.dateien.Count; a++)
            {
                string dateiname = von.dateien[a].nameMitTyp;
                int index = -1;

                // finde entsprechende Datei bei löschmenge
                for (int b = 0; b < löschmenge.dateien.Count; b++)
                {
                    if (dateiname == löschmenge.dateien[b].nameMitTyp)
                    {
                        index = b;
                        break;
                    }
                }

                // Datei nicht gefunden
                if (index == -1) continue;
                else
                {
                    schnittmenge.AddDatei(von.dateien[a]);
                }

                // Entferne aus Listen
                von.dateien.RemoveAt(a--);
                löschmenge.dateien.RemoveAt(index);
            }

            return von;
        }


        private void AddNodes(ClassOrdner o, TreeNode tn)
        {
            int index = 0;
            foreach (ClassOrdner subo in o.ordner.OrderByDescending(i => i.größe))
            {
                tn.Nodes.Add(new TreeNode(subo.name + " " + subo.GetGröße()));
                AddNodes(subo, tn.Nodes[index]);
                index++;
            }
            foreach (ClassDatei datei in o.dateien.OrderByDescending(i => i.größe))
            {
                tn.Nodes.Add(datei.nameMitTyp + " " + datei.GetGröße());
            }
        }


        /* Summary:
         * entfernt alle Trees und addet einen neuen Tree
         * 
         */
        public bool ZeichneTree(GroupBox gb, int platzunten)
        {
            // Bishere Treeviews in der Groupbox entfernen
            for (int a = 0; a < gb.Controls.Count; a++)
            {
                if (gb.Controls[a] is TreeView)
                    gb.Controls.RemoveAt(a--);
            }


            if (!ZeichneTree(gb)) return false;

            // Finde den einzigen Tree
            TreeView t = gb.Controls.OfType<TreeView>().First();
            t.Size = new System.Drawing.Size(gb.Size.Width - 12, gb.Size.Height - 25 - platzunten);

            return true;
        }

        public bool ZeichneTree(GroupBox gb)
        {
            TreeView treeView1 = new TreeView();
            // Zeichne Strukturbaum
            try
            {
                treeView1.SuspendLayout();
                treeView1.BeginUpdate();
                treeView1.Nodes.Clear();
                treeView1.Nodes.Add(new TreeNode(this.pfad + " " + this.GetGröße()));
                AddNodes(this, treeView1.Nodes[0]);
                treeView1.EndUpdate();
                treeView1.ResumeLayout();

                treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
  | System.Windows.Forms.AnchorStyles.Left)
  | System.Windows.Forms.AnchorStyles.Right)));
                treeView1.Location = new System.Drawing.Point(6, 19);


                // Finde namen
                var treeviews = gb.Controls.OfType<TreeView>();
                for (int a = 1; a < 1000; a++)
                {
                    string testname = "treeView" + a;
                    bool gefunden = false;
                    foreach (TreeView t in treeviews)
                    {
                        if (t.Name == testname)
                        {
                            gefunden = true;
                            break;
                        }
                    }

                    if (!gefunden)
                    {
                        treeView1.Name = testname;
                        break;
                    }
                }

                treeView1.Size = new System.Drawing.Size(gb.Size.Width - 12, gb.Size.Height - 25);

                treeView1.TabIndex = 0;
                gb.Controls.Add(treeView1);
            }
            catch (Exception ex)
            {
                fehlermeldung = ex.Message;
                return false;
            }

            return true;
        }


        /*Summary
       * TODO Testen
       * Erstelle nur ein neues Ordnerobjekt für beliebigen Pfad im aktuellen Ordner
       * Zwischenordner werden erstellt
       * */
        public bool AddOrdner(string pfad)
        {
            int index = 0;
            string[] split = pfad.Split('\\');
            for (int a = 0; a < split.Length; a++)
            {
                if (split[a] == this.pfad.Split('\\')[a])
                    index++;
            }

            // Wäre falscher Pfad
            if (index == 0) { fehlermeldung = "Unterordner kann für diesen Pfad nicht erstellt werden."; return false; }
            // Wäre genau derselbe Pfad
            if (split.Length - index == 0) { fehlermeldung = "Beide Ordner haben denselben Pfad"; return false; }

            string subordner = split[index];

            // existiert der Subordner schon?
            index = -1;
            bool neu = false;
            for (int a = 0; a < ordner.Count; a++)
                if (ordner[a].name == subordner)
                { index = a; break; }

            // Existiert noch nicht
            if (index == -1)
            {
                neu = true;
                string subordnerpfad = this.pfad + "\\" + subordner;
                ordner.Add(new ClassOrdner(subordnerpfad));
                index = ordner.Count - 1;
            }

            // Versuche zu erstellen
            bool erstellt = ordner[index].AddOrdner(pfad);
            if (!erstellt)
            {
                if (neu)
                    ordner.RemoveAt(index);
                return false;
            }

            // Aktuellen Ordner aktualisieren
            this.anzahlOrdner++;
            this.unterordner = true;
            return true;
        }

        /*Summary
        * TODO Testen
        * Erstelle eine ClassDatei im aktuellen Ordner
        * Zwischenordner werden erstellt
        * */
        public bool AddDatei(string pfad)
        {
            ClassDatei cd = new ClassDatei(pfad);
            return AddDatei(cd);
        }

        /*Summary
        * TODO Testen
        * Erstelle eine ClassDatei im aktuellen Ordner
        * Zwischenordner werden erstellt
        * */
        public bool AddDatei(ClassDatei datei)
        {
            int index = 0;
            string[] split = datei.pfad.Split('\\');
            string[] aktuellerordner = this.pfad.Split('\\');
            for (int a = 0; a < aktuellerordner.Length; a++)
            {
                if (split[a] == aktuellerordner[a])
                    index++;
            }

            // Wäre falscher Pfad
            if (index == 0) { fehlermeldung = "Unterordner kann für diesen Pfad nicht erstellt werden."; return false; }
            // Wäre genau derselbe Pfad
            if (split.Length - index == 0) { fehlermeldung = "Beide Ordner haben denselben Pfad"; return false; }

            // Dateinamen erreicht
            if (split.Length - index == 1)
            {
                dateien.Add(new ClassDatei(datei));
                anzahlDateien++;
                größe += datei.größe;
                return true;
            }

            string subordner = split[index];

            // existiert der Subordner schon?
            index = -1;
            bool neu = false;
            for (int a = 0; a < ordner.Count; a++)
                if (ordner[a].name == subordner)
                { index = a; break; }

            // Exisitert noch nicht
            if (index == -1)
            {
                neu = true;
                string subordnerpfad = this.pfad + "\\" + subordner;
                ordner.Add(new ClassOrdner(subordnerpfad));
                index = ordner.Count - 1;
            }

            // Versuche zu erstellen
            bool erstellt = ordner[index].AddDatei(datei);
            if (!erstellt)
            {
                if (neu)
                    ordner.RemoveAt(index);
                return false;
            }

            // Aktuellen Ordner aktualisieren
            this.anzahlDateien++;
            this.größe += datei.größe;

            return true;
        }


        /*Summary
       * Entferne den ordner physisch
       * */
        public bool EntfernePhysisch()
        {
            try
            {
                Directory.Delete(pfad);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        /*Summary
       * TODO Erstelle ein neues Ordnerobjekt und einen Ordner im Filesystem
       * */
        public bool AddOrdnerPhysical(string pfad)
        {
            try
            {
                bool objekterstellt = AddOrdner(pfad);
                if (!objekterstellt)
                {
                    return false;
                }
                if (Directory.Exists(pfad))
                {
                    fehlermeldung = "Ordner existiert physisch bereits";
                    return false;
                }
                Directory.CreateDirectory(pfad);
            }
            catch (Exception ex)
            {
                fehlermeldung = ex.Message;
                return false;
            }

            return true;
        }

        /*Summary
        * Prüfen, ob noch alles existiert
        * */
        public void Aktualisieren()
        {
            this.größe = this.anzahlDateien = this.anzahlOrdner = 0;

            foreach (ClassOrdner co in ordner)
            {
                co.Aktualisieren();
                this.anzahlDateien += co.anzahlDateien;
                this.anzahlOrdner += co.anzahlOrdner;
                this.größe += co.größe;
                this.anzahlOrdner++;
            }

            foreach (ClassDatei cd in dateien)
            {
                this.größe += cd.größe;
                this.anzahlDateien++;
            }
        }

        /*Summary
       * Prüfen, ob noch physisch alles existiert
       * */
        public void AktualisierenPhysisch()
        {
            if (!Directory.Exists(pfad)) return;

            for (int a = 0; a < ordner.Count; a++)
            {
                if (Directory.Exists(ordner[a].pfad))
                {
                    ordner[a].AktualisierenPhysisch();
                    this.anzahlOrdner += ordner[a].ordner.Count;
                }
                else
                    ordner.RemoveAt(a--);
            }

            this.anzahlOrdner = ordner.Count;

            for (int a = 0; a < dateien.Count; a++)
            {
                if (!File.Exists(dateien[a].pfad))
                    dateien.RemoveAt(a--);
            }
            this.anzahlDateien = dateien.Count;
        }

        /*Summary
       * Entferne beliebigen Ordner aus der Ordnerstruktur
       * */
        public bool EntferneOrdner(string ordnername)
        {
            bool gefunden = false;
            for (int a = 0; a < ordner.Count; a++)
            {
                if (ordner[a].name == ordnername)
                    gefunden = true;
            }

            if (!gefunden)
            {
                fehlermeldung = "Ordner konnte nicht gefunden werden!";
                return false;
            }



            return true;
        }

        /*Summary
        * Entferne Dateityp, falls im Namen vorhanden
        * */
        private string EntferneDateiTypAusName(string dateiname)
        {
            if (!dateiname.Contains('.')) return dateiname;

            string nameohnetyp = "";
            string[] splits = dateiname.Split('.');
            for (int a = 0; a < splits.Length - 1; a++)
            {
                if (nameohnetyp == "") nameohnetyp = splits[a];
                else nameohnetyp += "." + splits[a];
            }
            return nameohnetyp;
        }

        /*Summary
        * Entferne Datei aus einer Ordnerstruktur
        * */
        private bool EntferneDatei(string dateipfad, int a, ref long ursprünglicheGröße,
            ref int ursprünglicheanzahlDateien, ref int ursprünglicheanzahlOrdner, bool leereOrdnerLöschen)
        {
            // Ordner?
            if (a < dateipfad.Split('\\').Length - 1)
            {
                string ordnername = dateipfad.Split('\\')[a];
                for (int b = 0; b < ordner.Count; b++)
                {
                    ClassOrdner co = ordner[b];
                    if (co.name == ordnername && co.EntferneDatei(dateipfad, a + 1, ref ursprünglicheGröße,
                        ref ursprünglicheanzahlDateien, ref ursprünglicheanzahlOrdner, leereOrdnerLöschen))
                    {
                        if (leereOrdnerLöschen && co.anzahlDateien + co.anzahlOrdner == 0)
                        {
                            ordner.RemoveAt(b);
                            ursprünglicheanzahlOrdner--;
                        }
                        return true;
                    }
                }
            }
            else
            {
                string dateinameohnetyp = EntferneDateiTypAusName(dateipfad.Split('\\')[a]);
                for (int x = 0; x < dateien.Count; x++)
                    if (dateien[x].name == dateinameohnetyp)
                    {
                        ursprünglicheGröße -= dateien[x].größe;
                        ursprünglicheanzahlDateien--;
                        dateien.RemoveAt(x);
                        return true;
                    }
            }
            return false;
        }

        /*Summary
        * Entferne Datei aus einer Ordnerstruktur
        * */
        public bool EntferneDatei(string dateipfad)
        {
            int a = 0;
            while (dateipfad.Split('\\')[a] == pfad.Split('\\')[a]) a++;

            return EntferneDatei(dateipfad, a, ref größe, ref anzahlDateien, ref anzahlOrdner, true);
        }

        /*Summary
        * Hole Ordnergröße als String
        * */
        public string GetGröße()
        {
            long tempgröße = größe;
            int a = 0;
            while (tempgröße > 1024)
            {
                tempgröße /= 1024;
                a++;
            }


            switch (a)
            {
                case 0: return tempgröße + " Bytes";
                case 1: return tempgröße + " KB";
                case 2: return tempgröße + " MB";
                case 3: return tempgröße + " GB";
                case 4: return tempgröße + " TB";
            }
            return "";
        }

        public Dictionary<string,List<ClassDatei>> GetAlleDateitypenDateien()
        {
            Dictionary<string, List<ClassDatei>> dateitypendateien = new Dictionary<string, List<ClassDatei>>();
            foreach (ClassDatei cd in GetAllFiles())
            {
                if (dateitypendateien.ContainsKey(cd.dateityp))
                    dateitypendateien[cd.dateityp].Add(cd);
                else
                    dateitypendateien.Add(cd.dateityp, new List<ClassDatei>() { cd });
            }

            return dateitypendateien;
        }

        /*Summary
        * Hole alle Dateien innerhalb eines Ordners
        * 
        * */
        public List<ClassDatei> GetAllFiles()
        {
            List<ClassDatei> files = new List<ClassDatei>();

            foreach (ClassOrdner sub in ordner)
            {
                files.AddRange(sub.GetAllFiles());
            }

            foreach (ClassDatei d in dateien)
            {
                files.Add(d);
            }
            return files;
        }

        /*Summary
        * Erhalte Pfad, des übergeordneten Ordners
        * 
        * */
        public static string GetÜbergeordnetenOrdner(string ordnerpfad)
        {
            string überordnerpfad = "";
            string[] split = ordnerpfad.Split('\\');
            for (int a = 0; a < split.Length - 1; a++)
            {
                if (überordnerpfad == "") überordnerpfad = split[a];
                else überordnerpfad += "\\" + split[a];
            }
            return überordnerpfad;
        }



        /*Summary
        * Garantiere, dass alle physischen Ordner bis zu diesem Pfad erstellt wurden
        * 
        * */
        public static bool GarantiereOrdnerPhysisch(string pfad)
        {
            // ist dateipfad
            string ordnerpfad = pfad;
            if (pfad.Split('\\').Last().ToString().Split('.').Length >= 2)
                ordnerpfad = GetÜbergeordnetenOrdner(pfad);

            if (Directory.Exists(ordnerpfad))
                return true;
            else
            {
                // ist überhaupt Laufwerk vorhanden?
                string[] split = ordnerpfad.Split('\\');
                if (split.Length == 1) // nicht einmal Laufwerkpfad vorhanden
                    return false;

                bool garantiert = GarantiereOrdnerPhysisch(GetÜbergeordnetenOrdner(ordnerpfad));
                if (garantiert)
                    Directory.CreateDirectory(ordnerpfad);
            }
            return true;
        }


        private Dictionary<long, List<ClassDatei>> GetAlleDateigrößen()
        {
            Dictionary<long, List<ClassDatei>> dict = new Dictionary<long, List<ClassDatei>>();

            foreach (ClassOrdner o in ordner)
            {
                foreach (KeyValuePair<long, List<ClassDatei>> pair in o.GetAlleDateigrößen())
                {
                    if (dict.ContainsKey(pair.Key))
                        dict[pair.Key].AddRange(pair.Value);
                    else
                        dict.Add(pair.Key, pair.Value);
                }
            }

            foreach (ClassDatei d in dateien)
            {
                if (dict.ContainsKey(d.größe))
                    dict[d.größe].Add(d);
                else
                    dict.Add(d.größe, new List<ClassDatei>() { d });
            }

            return dict;
        }
        /*Summary
         * liefert alle Dateien im Ordner mit selber Dateigröße zurück
         * 
         * */
        public Dictionary<long, List<ClassDatei>> GetGleicheDateigröße()
        {
            Dictionary<long, List<ClassDatei>> dict = GetAlleDateigrößen();

            // entferne alle mit weniger als 2
            for (int a = 0; a < dict.Count; a++)
            {
                if (dict.ElementAt(a).Value.Count < 2)
                {
                    dict.Remove(dict.ElementAt(a).Key);
                    a--;
                }
            }
            return dict;
        }
        /*Summary
         * überprüft alle Dateien in einem Ordner, bei gleichen Ordnernamen
         * 
         * */
        public bool Equals(ClassOrdner compare, bool ignorierethumbsdb)
        {
            if (ignorierethumbsdb)
            {
                for (int a = 0; a < dateien.Count; a++)
                {
                    if (dateien[a].name.Contains("Thumbs.db"))
                    {
                        dateien.RemoveAt(a);
                        a--;
                    }
                }
                for (int a = 0; a < compare.dateien.Count; a++)
                {
                    if (compare.dateien[a].name.Contains("Thumbs.db"))
                    {
                        compare.dateien.RemoveAt(a);
                        a--;
                    }
                }
            }

            if (ordner.Count != compare.ordner.Count) return false;
            if (dateien.Count != compare.dateien.Count) return false;

            bool equals = false;
            foreach (ClassOrdner sub in compare.ordner)
            {
                string name = sub.name;

                // suche aktuellen ordner
                foreach (ClassOrdner thissub in ordner)
                {
                    if (thissub.name == name)
                        equals = thissub.Equals(sub, true);
                }

                if (!equals) return false;
            }

            // Überprüfe Dateien
            List<byte[]> thisfiles = ClassDatei.GetMD5s(dateien);
            List<byte[]> comparefiles = ClassDatei.GetMD5s(compare.dateien);

            while (thisfiles.Count > 0)
            {
                bool gefunden = false;

                for (int a = 0; a < comparefiles.Count; a++)
                {
                    if (SindIdentisch(comparefiles[a], thisfiles.First()))
                    {
                        comparefiles.RemoveAt(a);
                        thisfiles.RemoveAt(0);
                        gefunden = true;
                        break;
                    }
                }

                if (!gefunden) return false;
            }

            return true;
        }

        /*Summary
         * überprüft, ob zwei byte arrays identisch sind zb bei md5 vergleich
         * 
         * */
        private bool SindIdentisch(byte[] array1, byte[] array2)
        {
            for (int a = 0; a < array1.Length; a++)
            {
                if (array1[a] != array2[a])
                    return false;
            }
            return true;
        }
    }

    public class Error
    {
        public Exception exception;
        public enum OBJEKTTYP { istOrdner = 0, istDatei = 1, sonstiges = 2 };
        public OBJEKTTYP objekttyp = OBJEKTTYP.istDatei;
        public enum TYP { nichtkopiert = 0, nichtüberschrieben = 1, nichtgelöscht = 2, sonstiges = 3 };
        public TYP typ = TYP.sonstiges;
        public string fehlermeldung;
        public string pfad;

        public Error(Exception exception, string fehlermeldung, string pfad, TYP typ)
        {
            this.exception = exception;
            this.fehlermeldung = fehlermeldung;
            this.pfad = pfad;
            this.typ = typ;

            if (pfad == "")
                objekttyp = OBJEKTTYP.sonstiges;
            else if (!pfad.Contains("."))
                objekttyp = OBJEKTTYP.istOrdner;
        }
        public Error(string fehlermeldung, string pfad, TYP typ)
        {
            this.exception = null;
            this.fehlermeldung = fehlermeldung;
            this.pfad = pfad;
            this.typ = typ;

            if (pfad == "")
                objekttyp = OBJEKTTYP.sonstiges;
            else if (!pfad.Contains("."))
                objekttyp = OBJEKTTYP.istOrdner;
        }
    }

    public class ClassDatei
    {
        public string pfad;
        public long größe;
        public string nameMitTyp;
        public string dateityp;
        public string name;
        public string ordnerpfad;
        public byte[] md5;
        public long id;
        public FileInfo fileinfo;

        /*Summary
         * Copy Constructor
        * */
        public ClassDatei(ClassDatei cd)
        {
            this.pfad = cd.pfad;
            this.größe = cd.größe;
            this.nameMitTyp = cd.nameMitTyp;
            this.dateityp = cd.dateityp;
            this.name = cd.name;
            this.ordnerpfad = cd.ordnerpfad;
            this.id = cd.id;
            this.fileinfo = cd.fileinfo;
        }

        public ClassDatei(string pfad, long größe, string nameMitTyp, string dateityp, string name, string ordnerpfad,
            long id, FileInfo fileinfo)
        {
            this.pfad = pfad;
            this.größe = größe;
            this.nameMitTyp = nameMitTyp;
            this.dateityp = dateityp;
            this.name = name;
            this.ordnerpfad = ordnerpfad;
            this.id = id;
            this.fileinfo = fileinfo;
        }

        public ClassDatei(string pfad)
        {
            this.pfad = pfad;
            größe = new FileInfo(pfad).Length;
            this.nameMitTyp = pfad.Split('\\').Last();
            this.dateityp = nameMitTyp.Split('.').Last();
            this.name = GetName(nameMitTyp);
            this.ordnerpfad = GetOrdnerpfad();
            this.fileinfo = new FileInfo(pfad);
        }


        /*Summary
* statische Methode
* Erhalte für einen Dateipfad den entsprechenden Ordner
* */
        public static string GetOrdner(string dateipfad)
        {
            string ordnerpfad = "";
            string[] split = dateipfad.Split('\\');
            for (int a = 0; a < split.Length - 1; a++)
            {
                if (ordnerpfad == "") ordnerpfad = split[a];
                else ordnerpfad += "\\" + split[a];
            }
            return ordnerpfad;
        }

        public static bool SelektiereImExplorer(string dateipfad)
        {
            if (File.Exists(dateipfad))
            {
                string argument = "/select, \"" + dateipfad + "\"";
                Process.Start("explorer.exe", argument);
            }
            else
                return false;
            return true;
        }



        public bool SelektiereImExplorer()
        {
            return SelektiereImExplorer(pfad);
        }


        public static bool ÖffneDatei(string dateipfad)
        {
            if (File.Exists(dateipfad))
            {
                Process.Start(dateipfad);
            }
            else return false;

            return true;
        }

        public bool ÖffneDatei()
        {
            return ÖffneDatei(pfad);
        }

        public string GetOrdnerpfad()
        {
            string ordnerpfad = "";
            string[] split = pfad.Split('\\');
            for (int a = 0; a < split.Length - 1; a++)
            {
                if (ordnerpfad == "") ordnerpfad = split[a];
                else ordnerpfad += "\\" + split[a];
            }
            return ordnerpfad;
        }

        private string GetName(string namemittyp)
        {
            string name = "";
            string[] split = namemittyp.Split('.');
            for (int a = 0; a < split.Length - 1; a++)
            {
                if (name == "") name = split[a];
                else name += "." + split[a];
            }
            return name;
        }

        public bool UmbennenPhysisch(string neuerName)
        {
            name = neuerName;
            try
            {
                string neuerpfad = ordnerpfad + "\\" + name + "." + dateityp;
                File.Move(pfad, neuerpfad);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool UmbennenPhysisch(string neuerName, string neuerDateiTyp)
        {
            dateityp = neuerDateiTyp;
            return UmbennenPhysisch(neuerName);
        }


        /*Summary
     * Entferne die Datei physisch
     * */
        public bool EntfernePhysisch()
        {
            try
            {
                File.Delete(pfad);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /*Summary
        * Vergleiche ByteData von zwei Dateien
        * */
        public static bool IsEqual(byte[] datei1, byte[] datei2)
        {
            if (datei1.Length != datei2.Length) return false;

            for (int a = 0; a < datei1.Length; a++)
            {
                if (datei1[a] != datei2[a]) return false;
            }
            return true;
        }

        /*Summary
        * Vergleiche zwei Dateien auf Md5 Ähnlichkeit
        * */
        public static bool IsEqualMD5(string dateipfad1, string dateipfad2)
        {
            return IsEqual(GenerateMD5(dateipfad1), GenerateMD5(dateipfad2));
        }

        /*Summary
        * Hole ByteData einer Datei
        * */
        public static byte[] GenerateMD5(string dateipfad)
        {
            var md5 = MD5.Create();
            FileStream file = new FileStream(dateipfad, FileMode.Open, FileAccess.Read);
            byte[] val = md5.ComputeHash(file);
            file.Close();
            return val;
        }

        /*Summary
        * Hole ByteData einer Datei
        * */
        public byte[] GetMD5()
        {
            return md5;
        }

        /*Summary
        * Generiere ByteData einer Datei
        * */
        public bool GenerateMD5()
        {
            var md5 = MD5.Create();
            FileStream file = new FileStream(pfad, FileMode.Open, FileAccess.Read);
            this.md5 = md5.ComputeHash(file);
            file.Close();
            return true;
        }

        /*Summary
* Hole ByteData einer Liste von Dateien
* 
* */
        public static List<byte[]> GetMD5s(List<ClassDatei> dateien)
        {
            List<byte[]> md5s = new List<byte[]>();
            foreach (ClassDatei datei in dateien)
            {
                byte[] data = datei.GetMD5();
                md5s.Add(data);
            }
            return md5s;
        }


        public static string GetGröße(double größe)
        {
            long lgröße = Convert.ToInt64(größe);
            return GetGröße(lgröße);
        }

        public static string GetGröße(long größe, int nachkommastellen = 0)
        {
            long lTempgröße = größe;
            int a = 0;
            while (lTempgröße/1024 > 1024)
            {
                lTempgröße /= 1024;
                a++;
            }

            double tempgröße = lTempgröße;
            if (nachkommastellen >= 0)
            {
                tempgröße = Math.Round(tempgröße / 1024, nachkommastellen);
                a++;
            }

            switch (a)
            {
                case 0: return tempgröße + " Bytes";
                case 1: return tempgröße + " KB";
                case 2: return tempgröße + " MB";
                case 3: return tempgröße + " GB";
                case 4: return tempgröße + " TB";
            }
            return "";
        }

        public string GetGröße()
        {
            long tempgröße = größe;
            int a = 0;
            while (tempgröße > 1024)
            {
                tempgröße /= 1024;
                a++;
            }

            switch (a)
            {
                case 0: return tempgröße + " Bytes";
                case 1: return tempgröße + " KB";
                case 2: return tempgröße + " MB";
                case 3: return tempgröße + " GB";
                case 4: return tempgröße + " TB";
            }
            return "";
        }
    }
}
