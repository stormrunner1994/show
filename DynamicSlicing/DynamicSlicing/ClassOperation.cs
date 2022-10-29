using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Operation
{
    class ClassOperation
    {
        public ClassOperation(string ausdruck, string trenner)
        {
            this.value = 0;
            this.klammern = new List<ClassOperation>();
            this.operationen = new List<ClassOperation>();
            this.trenner = trenner;
            this.ausdruck = ausdruck;

            ausdruckAufsplitten();
        }

        public double value;
        private string trenner;
        private List<ClassOperation> klammern;
        private List<ClassOperation> operationen;
        private string ausdruck;

        private bool äußereKlammerEntfernen()
        {
            if (ausdruck.Length == 0 || (ausdruck.Length > 0 && (ausdruck.First() != '(' || ausdruck.Last() != ')')))
                return false;

            int counter = 0;
            for (int a = 1; a < ausdruck.Length - 1; a++)
            {
                if (ausdruck[a] == '(')
                    counter++;
                else if (ausdruck[a] == ')')
                {
                    counter--;
                    if (counter < 0)
                        return false;
                }
            }

            if (counter == 0)
                return true;

            return false;
        }

        private void ausdruckAufsplitten()
        {
            // Entferne äußere Klammern
            if (äußereKlammerEntfernen())
            {
                ausdruck = ausdruck.Remove(0, 1);
                ausdruck = ausdruck.Remove(ausdruck.Length - 1, 1);
            }

            double dobout;
            // control splits
            if (ausdruck.Contains(">=") && nichtInKlammer(">="))
            {
                splitteNach(">=");
            }
            else if (ausdruck.Contains("<=") && nichtInKlammer("<="))
            {
                splitteNach("<=");
            }
            else if (ausdruck.Contains("==") && nichtInKlammer("=="))
            {
                splitteNach("==");
            }
            else if (ausdruck.Contains("!=") && nichtInKlammer("!="))
            {
                splitteNach("!=");
            }
            else if (ausdruck.Contains("<") && nichtInKlammer("<"))
            {
                splitteNach("<");
            }
            else if (ausdruck.Contains(">") && nichtInKlammer(">"))
            {
                splitteNach(">");
            }
            // rechen splits
            else if (ausdruck.Contains("+") && nichtInKlammer("+"))
            {
                    splitteNach("+");
            }
            else if (ausdruck.Contains("-") && nichtInKlammer("-"))
            {
                    splitteNach("-");
            }
            else if (ausdruck.Contains("*") && nichtInKlammer("*"))
                splitteNach("*");
            else if (ausdruck.Contains("/") && nichtInKlammer("/"))
                splitteNach("/");
            else if (ausdruck.Contains("^") && nichtInKlammer("^"))
                splitteNach("^");
            else if (!double.TryParse(ausdruck, out dobout)) // keine Zahl
            {
                string substring = "";
                string tempausdruck = ausdruck;
                while (tempausdruck.Length > 0 && tempausdruck.First() != '(')
                {
                    substring += tempausdruck.First();
                    tempausdruck = tempausdruck.Remove(0, 1);
                }

                if (substring == "sin")
                    operationen.Add(new ClassOperation(tempausdruck, "sin"));
                else if (substring == "cos")
                    operationen.Add(new ClassOperation(tempausdruck, "cos"));
            }
            else
            {

            }
        }

        private bool nichtInKlammer(string split)
        {
            // gibt es zumindest einen Ausdruck, der nicht in Klammern steht?
            string[] ausdrücke = GetSplits(ausdruck, split);
            foreach (string ausdruck in ausdrücke)
            {
                if (ausdruck.Contains('(') && ausdruck.Contains(')') || (!ausdruck.Contains('(') && !ausdruck.Contains(')')))
                    return true;
            }
            return false;
        }

        private string[] GetSplits(string ausdruck, string split)
        {
            List<string> ausdrücke = new List<string>();
            string subausdruck = "";
            for (int a = 0; a < ausdruck.Length; a++)
            {
                if (split.First() == ausdruck[a])
                {
                    bool gefunden = true;

                    // split überhaupt noch möglich?
                    if (a + split.Length >= ausdruck.Length)
                        gefunden = false;
                    else
                    {
                        // prüfe, ob split erreicht
                        for (int x = 1; x < split.Length; x++)
                        {
                            if (ausdruck[a + x] != split[x])
                            {
                                gefunden = false;
                                break;
                            }
                        }
                    }

                    // split gefunden
                    if (gefunden)
                    {
                        ausdrücke.Add(subausdruck.Trim());
                        subausdruck = "";
                        a += split.Length - 1;
                    }
                }
                else
                    subausdruck += ausdruck[a];
            }
            ausdrücke.Add(subausdruck.Trim());
            return ausdrücke.ToArray();
        }
        
        private void splitteNach(string split)
        {
            string[] ausdrücke = GetSplits(ausdruck, split);
            string merke = "";
            foreach (string ausdruck in ausdrücke)
            {
                // ist nicht in Klammer
                if (ausdruck.Contains('(') && ausdruck.Contains(')') || (!ausdruck.Contains('(') && !ausdruck.Contains(')')))
                {
                    if (merke != "")
                    {
                        operationen.Add(new ClassOperation(merke, split.ToString()));
                        merke = "";
                    }
                    operationen.Add(new ClassOperation(ausdruck, split.ToString()));
                }
                else
                {
                    if (merke == "")
                        merke = ausdruck;
                    else
                        merke += split + ausdruck;
                }
            }
        }

        private Dictionary<int, ClassKlammer> Klammersplit(string ausdruck)
        {
            Dictionary<int, ClassKlammer> dictklammersplit = new Dictionary<int, ClassKlammer>();
            bool klammerbereich = false;
            int klammerninnerhalb = 0;
            string bereich = "";

            for (int a = 0; a < ausdruck.Length; a++)
            {
                if (ausdruck[a] == '(')
                {
                    if (klammerbereich)
                        klammerninnerhalb++;
                    else if (bereich != "")
                    {
                        dictklammersplit.Add(dictklammersplit.Count, new ClassKlammer(bereich, false));
                        bereich = "";
                        klammerbereich = true;
                    }
                }
                else if (ausdruck[a] == ')')
                {
                    if (klammerninnerhalb == 0)
                    {
                        dictklammersplit.Add(dictklammersplit.Count, new ClassKlammer(bereich, true));
                        bereich = "";
                        klammerbereich = false;
                    }
                    else
                        klammerninnerhalb--;
                }
                else
                    bereich += ausdruck[a];
            }

            if (bereich != "")
                dictklammersplit.Add(dictklammersplit.Count, new ClassKlammer(bereich, false));

            return dictklammersplit;
        }

        private List<ClassStrich> Strichesplit(string ausdruck)
        {
            if (ausdruck == "" || (ausdruck.Split('+').Length < 2 && ausdruck.Split('-').Length < 2))
                return new List<ClassStrich>();

            List<ClassStrich> strichsplits = new List<ClassStrich>();
            bool plus = true;
            string bereich = "";

            for (int a = 0; a < ausdruck.Length; a++)
            {
                if (ausdruck[a] == '+')
                {
                    strichsplits.Add(new ClassStrich(bereich, plus));
                    bereich = "";
                    plus = true;
                }
                else if (ausdruck[a] == '-')
                {
                    strichsplits.Add(new ClassStrich(bereich, plus));
                    bereich = "";
                    plus = false;
                }
                else
                    bereich += ausdruck[a];
            }

            if (ausdruck.Last() == '+' || ausdruck.Last() == '-')
                strichsplits.Add(new ClassStrich("", plus));
            else if (bereich != "")
                strichsplits.Add(new ClassStrich(bereich, plus));

            return strichsplits;
        }

        private int countKlammern(string ausdruck)
        {
            int klammern = 0;
            for (int a = 0; a < ausdruck.Length; a++)
                if (ausdruck[a] == '(')
                    klammern++;
            return klammern;
        }

        private int countStriche(string ausdruck)
        {
            int striche = 0;

            for (int a = 0; a < ausdruck.Length; a++)
            {
                if (ausdruck[a] == '+' || ausdruck[a] == '-')
                    striche++;
            }
            return striche;
        }

        private int countPunkte(string ausdruck)
        {
            int striche = 0;

            for (int a = 0; a < ausdruck.Length; a++)
            {
                if (ausdruck[a] == '*' || ausdruck[a] == '/')
                    striche++;
            }
            return striche;
        }

        public List<string> getVariablen()
        {
            int iout;
            List<string> variablen = new List<string>();
            if (operationen.Count > 0)
            {
                foreach (ClassOperation o in operationen)
                {
                    // Block
                    if (o.operationen.Count > 0)
                    {
                        List<string> sub = o.getVariablen();
                        foreach (string s in sub)
                        {
                            if (!variablen.Contains(s))
                                variablen.Add(s);
                        }
                    }
                    // einfacher wert
                    else
                    {
                        // nicht vorhanden und keine Zahl
                        if (!variablen.Contains(o.ausdruck) && !Int32.TryParse(o.ausdruck, out iout))
                            variablen.Add(o.ausdruck);
                    }
                }
            }
            else if (!variablen.Contains(ausdruck) && !Int32.TryParse(ausdruck, out iout))
                variablen.Add(ausdruck);
            return variablen;
        }

        public bool check(Dictionary<string, int> dictvariablen)
        {
            if (operationen.Count != 2) return false;

            double links = operationen[0].calculate(dictvariablen);
            double rechts = operationen[1].calculate(dictvariablen);

            if (operationen[0].trenner == ">")
                return (links > rechts) ? true : false;
            else if (operationen[0].trenner == "<")
                return (links < rechts) ? true : false;
            else if (operationen[0].trenner == "<=")
                return (links <= rechts) ? true : false;
            else if (operationen[0].trenner == ">=")
                return (links >= rechts) ? true : false;
            else if (operationen[0].trenner == "==")
                return (links == rechts) ? true : false;
            else if (operationen[0].trenner == "!=")
                return (links != rechts) ? true : false;

            return false;
        }

        public double calculate(Dictionary<string, double> dictvariablen)
        {
            double dobout;
            if (double.TryParse(ausdruck.Trim(), out dobout))
                return Convert.ToDouble(ausdruck.Trim());
            else if (dictvariablen.ContainsKey(ausdruck.Trim()))
                return dictvariablen[ausdruck.Trim()];

            double ergebnis = 0;
            for (int a = 0; a < operationen.Count; a++)
            {
                ClassOperation co = operationen[a];

                if (a == 0)
                {
                    ergebnis = co.calculate(dictvariablen);
                    if (operationen.Count == 1)
                    {
                        if (co.trenner == "sin")
                            ergebnis = Math.Sin(ergebnis * (Math.PI / 180));
                        else if (co.trenner == "cos")
                            ergebnis = Math.Cos(ergebnis * (Math.PI / 180));
                    }
                }
                else
                {
                    if (co.trenner == "+")
                        ergebnis += co.calculate(dictvariablen);
                    else if (co.trenner == "-")
                        ergebnis -= co.calculate(dictvariablen);
                    else if (co.trenner == "*")
                        ergebnis *= co.calculate(dictvariablen);
                    else if (co.trenner == "/")
                        ergebnis /= co.calculate(dictvariablen);
                    else if (co.trenner == "^")
                        ergebnis = Math.Pow(ergebnis, co.calculate(dictvariablen));

                }
            }

            return ergebnis;
        }

        public int calculate(Dictionary<string, int> dictvariablen)
        {
            int iout;
            if (Int32.TryParse(ausdruck.Trim(), out iout))
                return Convert.ToInt32(ausdruck.Trim());
            else if (dictvariablen.ContainsKey(ausdruck.Trim()))
                return dictvariablen[ausdruck.Trim()];

            int ergebnis = 0;
            for (int a = 0; a < operationen.Count; a++)
            {
                ClassOperation co = operationen[a];

                if (a == 0)
                {
                    ergebnis = co.calculate(dictvariablen);
                    if (operationen.Count == 1)
                    {
                        if (co.trenner == "sin")
                            ergebnis = Convert.ToInt32(Math.Sin(ergebnis * (Math.PI / 180)));
                        else if (co.trenner == "cos")
                            ergebnis = Convert.ToInt32(Math.Cos(ergebnis * (Math.PI / 180)));
                    }
                }
                else
                {
                    if (co.trenner == "+")
                        ergebnis += co.calculate(dictvariablen);
                    else if (co.trenner == "-")
                        ergebnis -= co.calculate(dictvariablen);
                    else if (co.trenner == "*")
                        ergebnis *= co.calculate(dictvariablen);
                    else if (co.trenner == "/")
                        ergebnis /= co.calculate(dictvariablen);
                    else if (co.trenner == "^")
                        ergebnis = Convert.ToInt32(Math.Pow(ergebnis, co.calculate(dictvariablen)));
                }
            }

            return ergebnis;
        }


    }

    class ClassKlammer
    {
        public ClassKlammer(string ausdruck, bool innerhalb)
        {
            this.ausdruck = ausdruck;
            this.innerhalb = innerhalb;
        }

        public string ausdruck { get; }
        public bool innerhalb { get; }
    }

    class ClassStrich
    {
        public ClassStrich(string ausdruck, bool plus)
        {
            this.ausdruck = ausdruck;
            this.plus = plus;
        }

        public string ausdruck { get; }
        public bool plus { get; }
    }



}
