using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSlicing
{
    class ClassSlice
    {
        public bool finished = false;

        public List<ETZeile> etZeilen;

        public List<int> inslice;

        public int nthelement;

        private int a; // index for etZeilen
        private int d; // index for datadependencies
        private int c; // index for controldependencies
        private int s; // index for symmetricdependencies

        public ClassSlice(List<ETZeile> etZeilen, int nthelement)
        {
            a = nthelement - 1;
            d = 0;
            c = 0;
            s = 0;
            this.etZeilen = etZeilen;
            this.inslice = new List<int>();
            this.nthelement = nthelement -1;
        }


        private string SliceStep()
        {
            while (a > -1)
            {
                // prüfe Datadependencies
                while (d < etZeilen[a].datadepencies.Count)
                {
                    int index = etZeilen[a].datadepencies[d];
                    if (inslice.Contains(etZeilen[index - 1].dateiZeileNr))
                    {
                        if (!inslice.Contains(etZeilen[a].dateiZeileNr))
                        {
                            inslice.Add(etZeilen[a].dateiZeileNr);
                            d++;
                            return "slice from " + etZeilen[index - 1].eTZeileNr + " to " + etZeilen[a].eTZeileNr;
                        }
                        else
                            d++;
                    }
                    else
                    {
                        if (d + 1 == etZeilen[a].datadepencies.Count)
                        {
                            // weiter zu control dependencies
                            break;
                        }
                        else
                            d++;
                    }
                }

                // prüfe Controldependencies
                while (c < etZeilen[a].controldepencies.Count)
                {
                    int index = GetIndex(etZeilen[a].controldepencies[c]);

                    if (inslice.Contains(etZeilen[index].dateiZeileNr))
                    {
                        if (!inslice.Contains(etZeilen[a].dateiZeileNr))
                        {
                            inslice.Add(etZeilen[a].dateiZeileNr);
                            c++;
                            return "slice from " + etZeilen[index].eTZeileNr + " to " + etZeilen[a].eTZeileNr;
                        }
                        else
                        {
                            if (c + 1 == etZeilen[a].controldepencies.Count)
                            {
                                // weiter zu symmetric dependencies
                                break;
                            }
                            else
                                c++;
                        }
                    }
                    else
                        c++;
                }

                // prüfe Controldependencies
                while (s < etZeilen[a].symmeticdepencies.Count)
                {
                    int index = GetIndex(etZeilen[a].controldepencies[s]);

                    if (inslice.Contains(etZeilen[index].dateiZeileNr)
                        && !inslice.Contains(etZeilen[a].dateiZeileNr))
                    {
                        inslice.Add(etZeilen[a].dateiZeileNr);
                        s++;
                        return "slice from " + etZeilen[index].eTZeileNr + " to " + etZeilen[a].eTZeileNr;

                    }
                    else if (inslice.Contains(etZeilen[a].dateiZeileNr)
                        && !inslice.Contains(etZeilen[index].dateiZeileNr))
                    {
                        inslice.Add(etZeilen[index].dateiZeileNr);
                        s++;
                        return "slice from " + etZeilen[a].eTZeileNr + " to " + etZeilen[index].eTZeileNr;
                    }
                    else
                    {
                        if (s + 1 == etZeilen[a].controldepencies.Count)
                            break;
                        else
                            s++;
                    }
                }


                a--;
                d = 0;
                c = 0;
                s = 0;
            }

            finished = true;
            return "Slicing finished";
        }

        public string FindSliceSteps()
        {
            // fehlerhafte eingabe
            if (nthelement < 1 || nthelement > etZeilen.Count)
            {
                foreach (ETZeile et in etZeilen)
                    et.inSlice = false;
                inslice = new List<int>();
                finished = true;
                return "Slicing finished";
            }

            if (finished) return "";

            // füge einmal letzte Zeile zum Slice hinzu
            if (inslice.Count == 0)
                inslice = new List<int>() { etZeilen[a].dateiZeileNr };

            string verlauf = SliceStep();

            inslice.Sort();

            if (etZeilen.Last().funktion == "")
                inslice.RemoveAt(inslice.Count - 1);

            // entsprechende Zeilen auf 'in slice = true' setzen
            foreach (ETZeile et in etZeilen)
            {
                if (inslice.Contains(et.dateiZeileNr))
                    et.inSlice = true;
                else
                    et.inSlice = false;
            }

            return verlauf;
        }

        private int GetIndex(int etZeilenNr)
        {
            for (int a = 0; a < etZeilen.Count; a++)
            {
                if (etZeilen[a].eTZeileNr == etZeilenNr) return a;
            }
            return -1;
        }

        public List<int> GetInslice()
        {
            // fehlerhafte eingabe
            if (nthelement < 0 || nthelement >= etZeilen.Count)
            {
                foreach (ETZeile et in etZeilen)
                    et.inSlice = false;
                inslice = new List<int>();
                return inslice;
            }

            inslice = new List<int>() { etZeilen[nthelement].dateiZeileNr };

            // Dependencies sind bestimmt, jetzt in slice prüfen
            for (int a = nthelement; a > -1; a--)
            {
                // prüfe Datadependencies
                foreach (int i in etZeilen[a].datadepencies)
                {
                    int index = GetIndex(i);

                    if (inslice.Contains(etZeilen[index].dateiZeileNr))
                    {
                        if (!inslice.Contains(etZeilen[a].dateiZeileNr))
                            inslice.Add(etZeilen[a].dateiZeileNr);
                    }
                }

                // prüfe Controldependencies
                foreach (int i in etZeilen[a].controldepencies)
                {
                    int index = GetIndex(i - 1);

                    // Ignoriere Zeilen, die nicht im ET auftauchen, weil das Testcase sie nicht benötigt
                    if (index == -1) continue;

                    if (inslice.Contains(etZeilen[i - 1].dateiZeileNr))
                    {
                        if (!inslice.Contains(etZeilen[a].dateiZeileNr))
                            inslice.Add(etZeilen[a].dateiZeileNr);
                    }
                }
            }

            inslice.Sort();

            if (etZeilen.Last().funktion == "")
                inslice.RemoveAt(inslice.Count - 1);

            // entsprechende Zeilen auf 'in slice = true' setzen
            foreach (ETZeile et in etZeilen)
            {
                if (inslice.Contains(et.dateiZeileNr))
                    et.inSlice = true;
                else
                    et.inSlice = false;
            }

            finished = true;
            return inslice;
        }

    }
}
