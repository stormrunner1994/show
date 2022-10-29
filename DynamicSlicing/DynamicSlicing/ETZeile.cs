using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Operation;

namespace DynamicSlicing
{
    public class ETZeile
    {
        public string zeile { get; set; }
        public int eTZeileNr { get; set; }
        public int dateiZeileNr { get; set; }
        public string funktion { get; set; } // if, while, zuweisung, for, else
        public string operation { get; set; }
        public List<string> refVariablen { get; set; }
        public string defVariable { get; set; }
        public List<int> datadepencies { get; set; }
        public List<int> controldepencies { get; set; }
        public List<int> symmeticdepencies { get; set; }
        public bool inSlice { get; set; }
        public ETZeile parent { get; set; }

        public List<Dictionary<string, int>> variablen; // für Variablen Historie

        public ETZeile(int dateiZeileNr, string zeile, string funktion, int eTZeileNr, ETZeile parent)
        {
            this.variablen = new List<Dictionary<string, int>>();
            this.datadepencies = new List<int>();
            this.controldepencies = new List<int>();
            this.symmeticdepencies = new List<int>();
            this.dateiZeileNr = dateiZeileNr;
            this.operation = "";
            this.zeile = zeile;
            this.funktion = funktion;

            this.eTZeileNr = eTZeileNr;
            this.refVariablen = GetRefVariablen();
            this.defVariable = GetDefVariable();
            this.parent = parent;
            this.inSlice = false;
        }

        public void SetVariablen(Dictionary<string, int> variablen)
        {
            this.variablen = new List<Dictionary<string, int>>();
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in variablen)
                dict.Add(pair.Key, pair.Value);
            this.variablen.Add(dict);
        }

        public string GetSliceStatus()
        {
            if (inSlice) return "X";
            return "";
        }

        public string[] GetArrayZeile(bool depWerte)
        {
            if (depWerte)
            {
                return new string[]{
                    dateiZeileNr + "^" + eTZeileNr + "   " + this.zeile,
                    GetDataDependencies(), GetControlDependencies(), GetSymmetricDependencies()
                    ,GetSliceStatus(), GetVariablen() };
            }
            else
            {
               return new string[]{ dateiZeileNr + "^" + eTZeileNr + "   " + this.zeile,
                    "", "", ""
                    ,GetSliceStatus(), GetVariablen() };
            }
        }

        public string GetVariablen()
        {
            string output = "";

            for (int a = 0; a < variablen.Count; a++)
            {
                string spalte = "";
                Dictionary<string, int> dict = variablen[a];
                foreach (KeyValuePair<string, int> pair in dict)
                {
                    Helper.Append(ref spalte, pair.Key + " = " + pair.Value, ";");
                }
                Helper.Append(ref output, spalte, "|");
            }
            return output;
        }

        public string GetDataDependencies()
        {
            return Helper.ListToString(datadepencies);
        }

        public string GetControlDependencies()
        {
            return Helper.ListToString(controldepencies);
        }

        public string GetSymmetricDependencies()
        {
            return Helper.ListToString(symmeticdepencies);
        }

        public string GetDefVariableFor()
        {
            string def = "";
            string tempzeile = zeile;
            // "for (" entfernen
            tempzeile = tempzeile.Remove(0, tempzeile.IndexOf('(') + 1);
            tempzeile = tempzeile.Remove(tempzeile.IndexOf(')'), tempzeile.Length - tempzeile.IndexOf(')'));

            string[] semmikolon = tempzeile.Split(';');
            if (semmikolon.Length != 3) return "";

            string[] splits = semmikolon[0].Split('=');
            if (splits.Length != 2) return def; // fehlerhafte zeile

            tempzeile = splits.First();
            def = tempzeile.Trim();

            if (def.Contains("int"))
            {
                def = def.Replace("int","");
            }

            return def.Trim();
        }

        public string GetDefVariable()
        {
            string def = "";
            string tempzeile;

            if (funktion == "if" || funktion == "while" || funktion == "write" || funktion == "else") return def;

            if (funktion == "read")
            {
                tempzeile = zeile.Remove(0, zeile.IndexOf('(')+1);
                int index = tempzeile.IndexOf(')');
                tempzeile = tempzeile.Remove(index, tempzeile.Length - index);
                return tempzeile;
            }

            if (funktion == "for") return GetDefVariableFor();

            string[] splits = zeile.Split('=');
            if (splits.Length != 2) return def; // fehlerhafte zeile

            tempzeile = splits.First();
            def = tempzeile.Trim();

            return def;
        }

        public List<string> GetRefVariablen()
        {
            if (funktion == "while" || funktion == "if")
                return GetRefVariablenControl();
            else if (funktion == "zuweisung")
                return GetRefVariablenZuweisung();
            else if (funktion == "for")
                return GetRefVariablenFor();


            return GetRefVariablenSonstiges();
        }
        public List<string> GetRefVariablenFor()
        {
            List<string> reffor = new List<string>();
            string tempzeile = zeile;
            // "for (" entfernen
            tempzeile = tempzeile.Remove(0, tempzeile.IndexOf('(') + 1);
            tempzeile = tempzeile.Remove(tempzeile.IndexOf(')'), tempzeile.Length - tempzeile.IndexOf(')'));

            string[] semmikolon = tempzeile.Split(';');
            if (semmikolon.Length != 3) return reffor;

            // Erst zuweisung prüfen
            string merke = zeile;
            this.zeile = semmikolon[0];
            List<string> zuweis = GetRefVariablenZuweisung();
            foreach (string z in zuweis)
            {
                if (!reffor.Contains(z))
                    reffor.Add(z);
            }

            // dann operator control
            this.zeile = semmikolon[1];
            List<string> control = GetRefVariablenControl();
            foreach (string c in control)
            {
                if (!reffor.Contains(c))
                    reffor.Add(c.Trim());
            }

            this.zeile = merke;
            this.operation = semmikolon[0].Split('=')[1].Trim() + ";" + semmikolon[1].Trim() + ";" + semmikolon[2].Trim();
            return reffor;
        }

        public List<string> GetRefVariablenSonstiges()
        {
            // erwarte hier bisher nur fälle wie "write(x)"
            List<string> sonstige = new List<string>();
            if (zeile.Contains("else") || zeile.Contains("read"))
                return sonstige;

            string tempzeile = zeile.Remove(0, zeile.IndexOf('(') + 1);

            tempzeile = tempzeile.Remove(tempzeile.IndexOf(')'), tempzeile.Length - tempzeile.IndexOf(')'));


            ClassOperation co = new ClassOperation(tempzeile, "");
            sonstige = co.getVariablen();

            return sonstige;
        }

        public List<string> GetRefVariablenZuweisung()
        {
            // gehe von fällen wie "x = t + c;" und "x = t + 1;" aus
            List<string> zuweisungen = new List<string>();
            string[] gleich = zeile.Split('=');
            if (gleich.Length != 2) return zuweisungen; // fehlerhafte zeile

            string tempzeile = gleich[1].Trim();

            if (tempzeile.Last() == ';')
                tempzeile = tempzeile.Replace(";", "");
            this.operation = tempzeile.Trim();

            // Entferne Operatoren
            ClassOperation co = new ClassOperation(tempzeile, "");
            zuweisungen = co.getVariablen();
            return zuweisungen;
        }

        public List<string> GetRefVariablenControl()
        {
            List<string> controls = new List<string>();
            // Filtere den Klammernbereich
            string tempzeile = zeile;
            int index = zeile.IndexOf('(');
            if (index > -1)
            {
                tempzeile = zeile.Remove(0, index + 1);
                index = tempzeile.IndexOf(')');

                if (index > -1)
                    tempzeile = tempzeile.Remove(index, tempzeile.Length - index);
            }
            if (tempzeile == "")
            {

            }

            this.operation = tempzeile;
            ClassOperation co = new ClassOperation(tempzeile, "");
            foreach (string s in co.getVariablen())
                controls.Add(s.Trim());
            return controls;
        }

    }
}
