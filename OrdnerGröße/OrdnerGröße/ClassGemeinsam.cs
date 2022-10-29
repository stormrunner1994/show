using Ordner_;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdnerGröße
{

    class Pfad
    {
        public string ordnerpfad = "";
        public List<ClassDatei> dateien = new List<ClassDatei>();

        public Pfad(string ordnerpfad)
        {
            this.ordnerpfad = ordnerpfad;
            this.dateien = new List<ClassDatei>();
        }

        public void AddDatei(ClassDatei datei)
        {
            dateien.Add(datei);
        }
    }

    class ClassGemeinsam
    {
        public List<Pfad> pfade = new List<Pfad>();
        public bool ordnerSindIdent = false;
        public long größe = 0; // größe aller duplikate in dem pfad
        public int anzahl = 0; // anzahl duplikate
        public string fehler = "";
         
        public List<string> GetOrdnerPfade()
        {
            List<string> ordnerpfade = new List<string>();
            foreach (Pfad p in pfade)
                ordnerpfade.Add(p.ordnerpfad);
            return ordnerpfade;
        }

        public int GetProzentAnteil()
        {
            int meiste = -1;
            foreach (string ordnerpfad in GetOrdnerPfade())
            { 
                ClassOrdner co = new ClassOrdner(ordnerpfad, false);
                if (meiste == -1 || meiste < co.anzahlDateien) meiste = co.anzahlDateien;
            }
            if (meiste <= 0) return -1;

            return anzahl * 100 / meiste;
        }

        public bool AddDatei(int index, ClassDatei datei)
        {
            if (!File.Exists(datei.pfad)) return false;

            pfade[index].AddDatei(datei);
            größe += new FileInfo(datei.pfad).Length;
            anzahl++;

            return true;
        }

        public string[] GetZeile()
        {
            string zeile = anzahl.ToString() + ";" + ClassDatei.GetGröße(größe);
            foreach (string pfad in GetOrdnerPfade())
                zeile += ";" + pfad;

            return zeile.Split(';');
        }
        public ClassGemeinsam()
        {

        }
        public ClassGemeinsam(List<string> ordnerpfade)
        {
            pfade = new List<Pfad>();
            foreach (string s in ordnerpfade)
                pfade.Add(new Pfad(s));

            bool sindGleich = true;
            for (int a = 1; a < ordnerpfade.Count; a++)
            {
                if (ordnerpfade[0] != ordnerpfade[a])
                {
                    sindGleich = false;
                    break;
                }
            }

            if (sindGleich)
                this.ordnerSindIdent = true;
        }

        private bool PfadExists(string pfad)
        {
            foreach (Pfad p in pfade)
            {
                if (p.ordnerpfad == pfad) return true;
            }
            return false;
        }


        public bool LöscheAusAnderenOrdnern(int indexNichtLöschen, ref List<ClassDatei> gelöschteDateien)
        {
            if (indexNichtLöschen > pfade.Count - 1) { fehler = "Kein Pfad gefunden"; return false; }

            // lösche idente, behalte also einen auf
            for (int a = 0; a < pfade.Count; a++)
            {
                if (a == indexNichtLöschen) continue;

                foreach (ClassDatei cd in pfade[a].dateien)
                {
                    try
                    {
                        if (File.Exists(cd.pfad))
                        {
                            File.Delete(cd.pfad);
                            gelöschteDateien.Add(cd);
                        }
                        else // File exisitiert nicht
                        {
                            fehler = "Datei existiert nicht: \n" + cd.pfad;
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        fehler = ex.Message;
                        return false;
                    }
                }
                pfade.RemoveAt(a--);
            }

            return true;
        }

        public bool SindGleicheOrdner(List<string> ordner)
        {
            List<string> temp = new List<string>();
            temp.AddRange(ordner);

            foreach (string ordnerpfad in GetOrdnerPfade())
            {
                if (ordner.Count == 0) return false;

                bool gefunden = false;
                for(int a = 0; a < temp.Count; a++)
                {
                    if (temp[a] == ordnerpfad)
                    {
                        gefunden = true;
                        temp.RemoveAt(a);
                        break;
                    }
                }

                if (!gefunden)
                    return false;
            }

            if (temp.Count > 0) return false;
            return true;
        }
    
           
    }
}
