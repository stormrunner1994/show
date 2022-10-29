using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSlicing
{
    class ClassCheckCode
    {
        private string error;
        private string code;
        private string[] zeilen;
        private List<string> datentypen;
        public ClassCheckCode (string code)
        {
            datentypen = new List<string>();
            datentypen.Add("int");
            datentypen.Add("double");
            datentypen.Add("Integer");
            datentypen.Add("Double");
            this.code = code;
            StartePrüfung();
        }

        private void StartePrüfung()
        {
            if (code.Trim().Length == 0)
            {
                error = "No code"; return;
            }
            zeilen = code.Split('\n');

            string klammern = KlammernKorrekt();
            if (klammern != "")
            {
                error = klammern;
                return;
            }            

            for (int a = 0; a < zeilen.Length; a++)
            {
                string zeile = zeilen[a];
                if (zeile.Trim() == ""
                    || zeile.Trim() == "{"
                    || zeile.Trim() == "}") continue;

                // Zuweisungen
                string zuweisung = ZuweisungKorrekt(zeile);
                if (zuweisung != "" && zuweisung != "kein")
                {
                    error = "Line " + (a + 1) + ") " + zuweisung; return;
                }

                // while
            }
        }

        private string ZuweisungKorrekt(string zeile)
        {
            try
            {
                // als Zuweisung erkannt
                // int a = 3
                // a = 3
                // int a = 3;
                // a = 3;
                // a = a - 1 - 3 - 4 + 5 * 6 + (2 - b)
                // existiert ein alleinstehendes "="
                if (zeile.IndexOf('=') <= 0
                    || zeile[zeile.IndexOf('=') - 1] == '='
                    || zeile[zeile.IndexOf('=') + 1] == '=')
                    return "kein";

                string[] splits = zeile.Split(' ');

                if (splits.Length < 3) return "wrong assignment";

                int a = 0;
                // Datentypen?
                if (datentypen.Contains(splits.First().Trim())) a++;

                // Gleichheitszeichen an richtiger stelle?
                if (splits[a + 1].Trim() != "=") return "'=' at wrong location";


                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string KlammernKorrekt()
        {
            int rundeklammern = 0;
            int geschweifteklammern = 0;
            for (int i = 0; i < zeilen.Length; i++)
            {
                string zeile = zeilen[i];
                for (int a = 0; a < zeile.Length; a++)
                {
                    if (code[a] == '(') rundeklammern++;
                    else if (code[a] == ')') rundeklammern--;
                    else if (code[a] == '{') geschweifteklammern++;
                    else if (code[a] == '}') geschweifteklammern++;
                }

                if (rundeklammern < 0)
                    return "check your rounded brackets in line " + (i + 1);
                if (geschweifteklammern < 0)
                    return "check your curly brackets in line " + (i + 1);
            }

            if (rundeklammern != 0)
                return "check your rounded brackets";
            if (geschweifteklammern != 0)
                return "check your curly brackets";
            return "";
        }

    }
}
