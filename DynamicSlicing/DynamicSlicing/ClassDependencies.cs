using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSlicing
{
    class ClassDependencies
    {
        public List<ETZeile> etZeilen;
        public bool finished = false;
        public bool datafinished = false;
        public bool controlfinished = false;
        public bool symmetricfinished = false;
        private int index = 0;
        private int a = 1;

        public ClassDependencies(List<ETZeile> etZeilen)
        {
            this.etZeilen = etZeilen;
        }


        private string DataStep()
        {
            string defVariable = etZeilen[index].defVariable;
            // Suche nach Verbindungen, solange gesuchte variable nicht verändert wird
            while (index < etZeilen.Count)
            {
                if (etZeilen[a].GetRefVariablen().Contains(defVariable))
                {
                    if (etZeilen[a].parent != null
                        && etZeilen[index].parent != null
                        && etZeilen[a].parent.funktion == "else" && etZeilen[index].parent.funktion == "if"
                        || etZeilen[a].funktion == "for")
                    {
                    }
                    else if (!etZeilen[index].datadepencies.Contains(etZeilen[a].eTZeileNr))
                    {
                        etZeilen[index].datadepencies.Add(etZeilen[a].eTZeileNr);
                        return etZeilen[index].eTZeileNr + " datadependency to " + etZeilen[a].eTZeileNr;
                    }
                }

                // Gesuchte Variable wird wieder verändert
                if (etZeilen[a].defVariable == defVariable || a >= etZeilen.Count -1)
                {
                    // zurücksetzen
                    if (index + 1 < etZeilen.Count -1)
                    {
                        index++;
                        defVariable = etZeilen[index].defVariable;
                        a = index + 1;
                    }
                    else
                    {
                        datafinished = true;
                        index = 0;
                        a = index + 1;
                        break;
                    }
                }
                else 
                    a++;
            }
            return "datadependencies finished";
        }

        public string FindDataDependenciesSteps()
        {
            if (datafinished) return "";

            string verlauf = DataStep();
            return verlauf;
        }

        private string ControlStep()
        {
            string funktion = etZeilen[index].funktion;
            if (a + 1 == etZeilen.Count)
            {
                if (index + 1 == etZeilen.Count)
                {
                    controlfinished = true;
                    index = 0;
                    a = index + 1;
                    return "Control finished";
                }
                else
                {

                    index++;
                    funktion = etZeilen[index].funktion;
                    a = index + 1;
                }
            }

            while (index < etZeilen.Count)
            {
                funktion = etZeilen[index].funktion;
                // wir betrachten nur while und if und else zeilen
                while (etZeilen[index].funktion != "if" && etZeilen[index].funktion != "while"
                    && etZeilen[index].funktion != "else" && etZeilen[index].funktion != "for")
                {
                    if (index + 1 == etZeilen.Count)
                    {
                        controlfinished = true;
                        index = 0;
                        a = index + 1;
                        return "Control finished";
                    }

                    index++;
                    a = index + 1;
                    funktion = etZeilen[index].funktion;
                }

                // suche alle Zeilen, die der funktion des Parents angehören
                while (a < etZeilen.Count)
                {
                    // Abbruch, wenn auf sich selbst verweist
                    if (etZeilen[index].dateiZeileNr == etZeilen[a].dateiZeileNr)
                    {
                        if (index + 1 == etZeilen.Count)
                        {
                            controlfinished = true;
                            index = 0;
                            a = index + 1;
                            return "Control finished";
                        }
                        else
                        {
                            index++;
                            funktion = etZeilen[index].funktion;
                            a = index + 1;
                        }
                    }
                    else if (etZeilen[a].parent != null && etZeilen[a].parent.funktion == funktion
   && !etZeilen[index].controldepencies.Contains(etZeilen[a].eTZeileNr))
                    {
                        etZeilen[index].controldepencies.Add(etZeilen[a].eTZeileNr);
                        string text = etZeilen[index].eTZeileNr + " control dependency to " + etZeilen[a].eTZeileNr;
                        a++;
                        return text;
                    }
                    else if (a + 1 == etZeilen.Count)
                    {
                        if (index + 1 == etZeilen.Count)
                        {
                            controlfinished = true;
                            index = 0;
                            a = index + 1;
                            return "Control finished";
                        }
                        else
                        {
                            index++;
                            funktion = etZeilen[index].funktion;
                            a = index + 1;
                        }
                    }
                    else
                        a++;
                }
            }
            return "Control finished";
        }

        public string FindControlDependenciesSteps()
        {
            if (controlfinished) return "";

            string verlauf = ControlStep();
            return verlauf;
        }

        private string SymmetricStep()
        {            
            if (a + 1 == etZeilen.Count)
            {
                if (index + 1 == etZeilen.Count)
                {
                    controlfinished = true;
                    index = 0;
                    a = index + 1;

                    symmetricfinished = finished = true;
                    return "Symmetric finished";
                }
                else
                {
                    index++;
                    a = index + 1;
                }
            }

            while (index < etZeilen.Count)
            {
                string funktion = etZeilen[index].funktion;
                // wir betrachten nur while und for zeilen
                while (funktion != "while" && funktion != "for")
                {
                    if (index + 1 == etZeilen.Count)
                    {
                        symmetricfinished = finished = true;
                        return "Symmetric finshed";
                    }
                    else
                    {
                        index++;
                        a = index + 1;
                        funktion = etZeilen[index].funktion;
                    }
                }

                // suche alle Zeilen, die der funktion des Parents angehören
                while (a < etZeilen.Count)
                {
                    if (etZeilen[a].dateiZeileNr == etZeilen[index].dateiZeileNr && !etZeilen[index].symmeticdepencies.Contains(etZeilen[a].eTZeileNr))
                    {
                        etZeilen[index].symmeticdepencies.Add(etZeilen[a].eTZeileNr);
                        string text = etZeilen[index].eTZeileNr + " symmetric dependency to " + etZeilen[a].eTZeileNr;
                        index++;
                        a = index + 1;
                        return text;
                    }
                    else
                    {
                        if (a + 1 == etZeilen.Count)
                        {
                            if (index + 1 == etZeilen.Count)
                            {
                                symmetricfinished = finished = true;
                                index = 0;
                                a = index + 1;
                                return "Symmetric finished";
                            }
                            else
                            {                                
                                index++;
                                a = index + 1;
                                break;
                            }
                        }
                        else
                            a++;
                    }
                }
            }
            return "";
        }

        public string FindSymmetricDependenciesSteps()
        {
            if (symmetricfinished) return "";

            string verlauf = SymmetricStep();
            return verlauf;
        }

        private List<int> FindDataDependencies(int index, List<ETZeile> zeilen)
        {
            List<int>  datadepencies = new List<int>();
            string defVariable = zeilen[index].defVariable;
            // Suche nach Verbindungen, solange gesuchte variable nicht verändert wird
            for (int a = index + 1; a < zeilen.Count; a++)
            {
                if (zeilen[a].GetRefVariablen().Contains(defVariable))
                {
                    if (zeilen[a].parent != null
                        && zeilen[index].parent != null
                        && zeilen[a].parent.funktion == "else" && zeilen[index].parent.funktion == "if"
                        || zeilen[a].funktion == "for")
                    {
                    }
                    else if (!datadepencies.Contains(zeilen[a].eTZeileNr))
                        datadepencies.Add(zeilen[a].eTZeileNr);
                }

                // Gesuchte Variable wird wieder verändert
                if (zeilen[a].defVariable == defVariable) break;
            }
            return datadepencies;
        }

        private List<int> FindControlDependencies(int index, List<ETZeile> zeilen)
        {
            List<int> controldepencies = new List<int>();
            string funktion = zeilen[index].funktion;
            // wir betrachten nur while und if und else zeilen
            if (zeilen[index].funktion != "if" && zeilen[index].funktion != "while"
                && zeilen[index].funktion != "else" && zeilen[index].funktion != "for") return new List<int>();

            // suche alle Zeilen, die der funktion des Parents angehören
            for (int a = index + 1; a < zeilen.Count; a++)
            {
                if (zeilen[a].parent != null && zeilen[a].parent.funktion == funktion
                    && !controldepencies.Contains(zeilen[a].eTZeileNr))
                {
                    // Abbruch, wenn auf sich selbst verweist
                    if (etZeilen[index].dateiZeileNr == etZeilen[a].dateiZeileNr)
                        break;
                    controldepencies.Add(zeilen[a].eTZeileNr);
                }
                else break;
            }
            return controldepencies;
        }

        private List<int> FindSymmetricDependencies(int index, List<ETZeile> zeilen)
        {
            List<int>  symmeticdepencies = new List<int>();
            string funktion = zeilen[index].funktion;
            // wir betrachten nur while und for zeilen
            if (funktion != "while" && funktion != "for") return new List<int>();

            // suche alle Zeilen, die der funktion des Parents angehören
            for (int a = index + 1; a < zeilen.Count; a++)
            {
                if (zeilen[a].dateiZeileNr == zeilen[index].dateiZeileNr && !symmeticdepencies.Contains(zeilen[a].eTZeileNr))
                {
                    symmeticdepencies.Add(zeilen[a].eTZeileNr);
                    break;
                }
            }
            return symmeticdepencies;
        }

        public List<ETZeile> FindDependencies()
        {
            // ET steht, jetzt Dependencies bestimmen
            for (int a = 0; a < etZeilen.Count; a++)
                etZeilen[a].datadepencies = FindDataDependencies(a, etZeilen);
            for (int a = 0; a < etZeilen.Count; a++)
                etZeilen[a].controldepencies = FindControlDependencies(a, etZeilen);
            for (int a = 0; a < etZeilen.Count; a++)
                etZeilen[a].symmeticdepencies = FindSymmetricDependencies(a, etZeilen);

            return etZeilen;
        }

    }
}
