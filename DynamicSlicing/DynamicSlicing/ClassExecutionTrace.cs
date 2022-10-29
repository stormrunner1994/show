using Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSlicing
{
    class ClassExecutionTrace
    {
        private List<Ebene> ebenen; // als stapel
        private Dictionary<string, int> variablen;
        private int etZeilenNr = 1;
        private List<ETZeile> et;
        private List<ETZeile> parents; // als Stapel
        private List<int> indices; // als stapel
        private bool bforeinstieg = false;
        public bool finished { get; set; }

        public List<ETZeile> GetExecutionTrace()
        {
            return et;
        }

        public ClassExecutionTrace()
        {

        }

        public ClassExecutionTrace(Dictionary<string, int> variablen)
        {
            this.variablen = variablen;
        }

        public ClassExecutionTrace(Ebene e, Dictionary<string, int> variablen)
        {
            ebenen = new List<Ebene>() { e };
            this.variablen = variablen;
            this.parents = new List<ETZeile>() { null };
            etZeilenNr = 1;
            et = new List<ETZeile>();
            indices = new List<int>() { 0 };
            this.finished = false;
        }

        public Dictionary<string, int> GetVariablen()
        {
            return variablen;
        }

        private string InsertVariablesFor(string zeile)
        {
            List<string> splits = zeile.Split(' ').ToList();
            bool gefunden = false;
            do
            {
                gefunden = false;
                foreach (KeyValuePair<string, int> pair in variablen)
                {
                    for (int a = 0; a < splits.Count; a++)
                    {
                        // Tausche aus
                        if (splits[a] == pair.Key)
                        {
                            splits[a] = pair.Value.ToString();
                            gefunden = true;
                        }
                    }
                }
            }
            while (gefunden);

            string neu = "";
            foreach (string s in splits)
            {
                if (neu != "") neu += " " + s;
                else neu += s;
            }

            return neu;
        }

        private string InsertVariables(string zeile)
        {
            List<string> splits = zeile.Split(' ').ToList();
            bool gefunden = false;
            do
            {
                gefunden = false;
                foreach (KeyValuePair<string, int> pair in variablen)
                {
                    for (int a  = 0; a < splits.Count; a++)
                    {
                        // Tausche aus
                        if (splits[a] == pair.Key)
                        {
                            splits[a] = pair.Value.ToString();
                            gefunden = true;
                        }
                    }
                }
            }
            while (gefunden);

            string neu = "";
            foreach (string s in splits)
            {
                if (neu != "") neu += " " + s;
                else neu += s;
            }

            return neu;
        }

        public string ExecutionTraceStep()
        {
            // Bereits fertig
            if (indices.Count == 0) return "";

            string verlauf = "";

            // Ende von Ebene erreicht?
            while (indices.Last() == ebenen.Last().ebenen.Count)
            {
                // letztes element von jedem Stack löschen
                indices.RemoveAt(indices.Count - 1);
                ebenen.RemoveAt(ebenen.Count - 1);
                parents.RemoveAt(parents.Count - 1);
                if (indices.Count > 0)
                    indices[indices.Count - 1]++;
                else // Ende erreicht
                {
                    finished = true;
                    return verlauf;
                }
            }

            // ignoriere unnötige zeilen
            while (UnnötigeZeile(ebenen.Last().ebenen[indices.Last()].zeile))
            {
                if (ebenen.Last().ebenen[indices.Last()].ebenen.Count > 0)
                {
                    ebenen.Add(ebenen.Last().ebenen[indices.Last()]);
                    parents.Add(parents.Last());
                    indices.Add(0);
                }
                else
                {
                    if (indices.Last() + 1 < ebenen.Last().ebenen.Count)
                        indices[indices.Count - 1]++;
                    else
                    {
                        // letztes element von jedem Stack löschen
                        indices.RemoveAt(indices.Count - 1);
                        ebenen.RemoveAt(ebenen.Count - 1);
                        parents.RemoveAt(parents.Count - 1);
                        if (indices.Count > 0)
                            indices[indices.Count - 1]++;
                        else
                        {
                            finished = true;
                            return verlauf;
                        }
                    }
                }
            }

            Ebene e = ebenen.Last();
            int index = indices.Last();

            Ebene subEbene = e.ebenen[index];
            ETZeile zeile = new ETZeile(e.ebenen[index].dateizeilenr, e.ebenen[index].zeile,
                e.ebenen[index].funktion, etZeilenNr, parents.Last());
            zeile.SetVariablen(variablen);
            // else wird nicht im ET aufgeführt
            if (zeile.funktion != "else")
            {
                zeile.eTZeileNr = etZeilenNr++;
                et.Add(zeile);
            }

            if (subEbene.funktion == "zuweisung")
            {
                Zuweisung(zeile, ref variablen);
                indices[indices.Count - 1]++;
                verlauf = zeile.defVariable + " gets value " + variablen[zeile.defVariable];
            }
            else if (zeile.funktion == "if")
            {
                bool inif = InIfStatement(e, zeile, variablen, ref index);
                indices[indices.Count - 1] = index +1;
                if (inif)
                {
                    // Bereite nächste Ebene vor
                    ebenen.Add(subEbene);
                    parents.Add(zeile);
                    indices.Add(0);
                }
                verlauf = "(" + InsertVariables(zeile.operation) + ") evaluates to " + inif;
            }
            else if (zeile.funktion == "else")
            {
                ETZeile ifZeile = new ETZeile(e.ebenen[index - 1].dateizeilenr, e.ebenen[index - 1].zeile, e.ebenen[index - 1].funktion, etZeilenNr, parents.Last());
                ebenen.Add(subEbene);
                parents.Add(ifZeile);
                indices.Add(0);
                verlauf = ExecutionTraceStep();
            }
            else if (zeile.funktion == "while")
            {
                bool inwhile = InWhileStatement(e, zeile, variablen, ref index);
                if (inwhile)
                {
                    ebenen.Add(subEbene);
                    parents.Add(zeile);
                    indices[indices.Count - 1]--; // while wird wiederholt
                    indices.Add(0);
                }
                else
                    indices[indices.Count - 1] = index + 1;
                verlauf = "(" + InsertVariables(zeile.operation) + ") evaluates to " + inwhile;
            }
            else if (zeile.funktion == "for")
            {
                bool infor = InForStatement(e, zeile, ref bforeinstieg, variablen, ref index);
                if (infor)
                {
                    ebenen.Add(subEbene);
                    parents.Add(zeile);
                    indices[indices.Count - 1]--; // for wird wiederholt
                    indices.Add(0);
                }
                else
                    indices[indices.Count - 1] = index + 1;
                verlauf = "(" + InsertVariablesFor(zeile.operation.Split(';')[1]) + ") evaluates to " + infor;
            }
            else if (zeile.funktion == "read")
            {
                verlauf = zeile.defVariable + " gets value " + variablen[zeile.defVariable];
                indices[indices.Count - 1]++;
            }
            else if (zeile.funktion == "write")
            {
                verlauf = "write";
                indices[indices.Count - 1]++;
            }

            if (et.Count > 500) // endlosschleife erkannt
            {
                et =  new List<ETZeile>();
                verlauf = "endless loop found";
            }

            return verlauf;
        }

        private List<ETZeile> ToExecutionTrace(Ebene e, ref Dictionary<string,
            int> variablen, ETZeile etParent)
        {
            List<ETZeile> et = new List<ETZeile>();

            bool bforeinstieg = false;
            for (int a = 0; a < e.ebenen.Count; a++)
            {
                // ignoriere unnötige zeilen
                if (UnnötigeZeile(e.ebenen[a].zeile))
                {
                    if (e.ebenen[a].ebenen.Count > 0)
                        et.AddRange(ToExecutionTrace(e.ebenen[a], ref variablen, etParent));
                    continue;
                }

                Ebene subEbene = e.ebenen[a];
                ETZeile zeile = new ETZeile(e.ebenen[a].dateizeilenr, e.ebenen[a].zeile, e.ebenen[a].funktion, etZeilenNr, etParent);
                zeile.SetVariablen(variablen);

                // else wird nicht im ET aufgeführt
                if (zeile.funktion != "else")
                {
                    zeile.eTZeileNr = etZeilenNr++;
                    et.Add(zeile);
                }

                if (subEbene.funktion == "zuweisung")
                {
                    Zuweisung(zeile, ref variablen);
                }
                else if (zeile.funktion == "if")
                {
                    bool inif = InIfStatement(e, zeile, variablen, ref a);
                    if (inif)
                    {
                        List<ETZeile> subZeilen = ToExecutionTrace(subEbene, ref variablen, zeile);
                        et.AddRange(subZeilen);
                    }
                }
                else if (zeile.funktion == "else")
                {
                    ETZeile ifZeile = new ETZeile(e.ebenen[a - 1].dateizeilenr, e.ebenen[a - 1].zeile, e.ebenen[a - 1].funktion, etZeilenNr, etParent);
                    List<ETZeile> subZeilen = ToExecutionTrace(subEbene,  ref variablen, ifZeile);
                    et.AddRange(subZeilen);
                }
                else if (zeile.funktion == "while")
                {
                    bool inwhile = InWhileStatement(e, zeile, variablen, ref a);
                    if (inwhile)
                    {
                        List<ETZeile> subZeilen = ToExecutionTrace(subEbene,  ref variablen, zeile);
                        et.AddRange(subZeilen);
                        a--;
                    }
                }
                else if (zeile.funktion == "for")
                {
                    bool infor = InForStatement(e, zeile, ref bforeinstieg, variablen, ref a);
                    if (infor)
                    {
                        List<ETZeile> subZeilen = ToExecutionTrace(subEbene, ref variablen, zeile);
                        et.AddRange(subZeilen);
                        a--;
                    }
                }

                if (et.Count > 500) // endlosschleife erkannt
                {
                    return new List<ETZeile>();
                }
            }

            return et;
        }

        public List<ETZeile> ToExecutionTrace(Ebene e)
        {
            etZeilenNr = 1;
            return ToExecutionTrace(e,  ref variablen, null);
        }

        private void Zuweisung(ETZeile zeile, ref Dictionary<string, int> variablen)
        {
            ClassOperation op = new ClassOperation(zeile.operation, "");
            int value = op.calculate(variablen);
            if (!variablen.ContainsKey(zeile.defVariable))
                variablen.Add(zeile.defVariable, value);
            else
                variablen[zeile.defVariable] = value;
        }

        private bool BedingungErfüllt(string ausdruck, Dictionary<string, int> variablen)
        {
            ClassOperation co = new ClassOperation(ausdruck, "");
            return co.check(variablen);

        }

        private void Incrementieren(string ausdruck, ref Dictionary<string, int> variablen)
        {
            string zahl1 = "";
            string zahl2 = "";
            int iout;
            if (ausdruck.Contains("--"))
            {
                zahl1 = ausdruck.Split('-').First();
                variablen[zahl1]--;
            }
            else if (ausdruck.Contains("++"))
            {
                zahl1 = ausdruck.Split('+').First();
                variablen[zahl1]++;
            }
            else if (ausdruck.Contains("+="))
            {
                string[] splits = ausdruck.Split('+');
                zahl1 = splits[0];
                zahl2 = splits[1].Remove(0, 1).Trim();

                if (Int32.TryParse(zahl2, out iout))
                    variablen[zahl1] += Convert.ToInt32(zahl2);
                else if (variablen.ContainsKey(zahl2))
                    variablen[zahl1] += variablen[zahl2];
            }
            else if (ausdruck.Contains("-="))
            {
                string[] splits = ausdruck.Split('-');
                zahl1 = splits[0];
                zahl2 = splits[1].Remove(0, 1).Trim();

                if (Int32.TryParse(zahl2, out iout))
                    variablen[zahl1] -= Convert.ToInt32(zahl2);
                else if (variablen.ContainsKey(zahl2))
                    variablen[zahl1] -= variablen[zahl2];
            }
        }

        private bool InForStatement(Ebene e, ETZeile zeile, ref bool bforeinstieg, Dictionary<string, int> variablen, ref int a)
        {
            string[] operationen = zeile.operation.Split(';');
            // For Einstieg
            if (!bforeinstieg)
            {
                bforeinstieg = true;
                // Zuerst zuweisen
                ClassOperation op = new ClassOperation(operationen.First(), "");
                int value = op.calculate(variablen);
                if (!variablen.ContainsKey(zeile.defVariable))
                    variablen.Add(zeile.defVariable, value);
                else
                    variablen[zeile.defVariable] = value;

                // Dann Control prüfen
                string control = operationen[1];
                if (!BedingungErfüllt(control, variablen)) // Springe nicht in Controlzweig
                {
                    return false;
                }
                return true;
            }
            else
            {
                // Zuerst incrementieren oder decrementieren
                Incrementieren(operationen[2], ref variablen);

                string control = operationen[1];
                if (!BedingungErfüllt(control, variablen)) // Springe nicht in Controlzweig
                    return false;
                return true;
            }
        }

        private bool InIfStatement(Ebene e, ETZeile zeile, Dictionary<string, int> variablen, ref int a)
        {
            if (!BedingungErfüllt(zeile.operation, variablen)) // Springe nicht in Controlzweig
            {
                return false;
            }
            else //Bedingung erfüllt
            {
                // else vorhanden? dann überspringe
                if (a + 1 < e.ebenen.Count && e.ebenen[a + 1].funktion == "else")
                    a++;
                return true;
            }
        }

        private bool InWhileStatement(Ebene e, ETZeile zeile, Dictionary<string, int> variablen, ref int a)
        {
            if (!BedingungErfüllt(zeile.operation, variablen)) // Springe nicht in Controlzweig
                return false;
            return true;
        }

        private bool UnnötigeZeile(string zeile)
        {
            if (!zeile.Contains("begin") &&
               !zeile.Contains("end") &&
               !zeile.Contains("fi") &&
               !zeile.Contains("od") &&
               !zeile.Contains("class") &&
               !zeile.Contains("public") &&
               !zeile.Contains("private") &&
               !zeile.Contains("protected"))
                return false;
            return true;
        }

        private List<ETZeile> ToDefaultExcutionTrace(Ebene e, int index, ETZeile parent)
        {
            List<ETZeile> et = new List<ETZeile>();

            foreach (Ebene ue in e.ebenen)
            {
                if (UnnötigeZeile(ue.zeile))
                {
                    if (ue.ebenen.Count != 0)
                    {
                        et.AddRange(ToDefaultExcutionTrace(ue, index, parent));
                        index = et.Last().eTZeileNr;
                    }

                    continue;
                }

                // Einfache Zeilen
                if (ue.ebenen.Count == 0)
                    et.Add(new ETZeile(ue.dateizeilenr, ue.zeile, ue.funktion, index, parent));
                else // while oder if verzweigungen
                {
                    ETZeile remember = parent;
                    parent = new ETZeile(ue.dateizeilenr, ue.zeile, ue.funktion, index, parent);
                    et.Add(parent);
                    et.AddRange(ToDefaultExcutionTrace(ue, ++index, parent));
                    index = et.Last().eTZeileNr;

                    // ruf den ursprünglichen Parent wieder auf
                    parent = remember;

                    if (ue.funktion == "while" || ue.funktion == "for")  // while und for werden ein zweites mal hinzugefügt
                        et.Add(new ETZeile(ue.dateizeilenr, ue.zeile, ue.funktion, ++index, parent));
                }

                index++;
            }

            return et;
        }
    
        public List<ETZeile> ToDefaultExcutionTrace(Ebene e)
        {
            return ToDefaultExcutionTrace(e, 0, null);
        }

    }
}
