using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Operation;

namespace DynamicSlicing
{
    class DynamicSlicing
    {
        public List<ETZeile> etDefault { get; set; }

        public List<int> inslice { get; set; }

        public List<ETZeile> etZeilen { get; set; }

        private Dictionary<string, int> output;

        public List<string> fehlermeldungen { get; set; }

        private Ebene e;

        private ClassExecutionTrace cet;

        private ClassDependencies cd;

        private ClassSlice cs;

        public int steps = 1;

        public bool finished = false;
        public bool dependenciesFinished = false;

        public DynamicSlicing(string code)
        {
            this.inslice = new List<int>();
            this.fehlermeldungen = new List<string>();
            List<string> zeilen = new List<string>();

            foreach (string zeile in code.Split('\n'))
                zeilen.Add(zeile);

            e = new Ebene(zeilen, 0, 0, null);

            cet = new ClassExecutionTrace();
            etDefault = cet.ToDefaultExcutionTrace(e);
        }

        public void Reset()
        {
            dependenciesFinished = finished = false;
            steps = 1;
            inslice = new List<int>();
            etZeilen = new List<ETZeile>();
            output = new Dictionary<string, int>();
            fehlermeldungen = new List<string>();
        }

        public List<string> getTestCaseVariablen()
        {
            List<string> testcase = new List<string>();
            List<string> defs = new List<string>();
            foreach (ETZeile z in etDefault)
            {    
                // gibt es Variable, die bisher nicht definiert aber trotzdem verwendet wird?
                foreach (string r in z.refVariablen)
                {
                    if (!defs.Contains(r) && !testcase.Contains(r))
                    {
                        if (z.funktion == "for")
                        {
                            if (r != z.defVariable)
                                testcase.Add(r);
                        }
                        else
                            testcase.Add(r);
                    }
                }

                if (!defs.Contains(z.defVariable) && z.funktion != "read" && z.defVariable != "")
                    defs.Add(z.defVariable);
                
            }
            testcase.Sort();
            return testcase;
        }

        public Dictionary<string, int> getOutput()
        {
            return output;
        }

        public string RunStepwise(Dictionary<string, int> dictTestcase)
        {
            Dictionary<string, int> testcase = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in dictTestcase)
                testcase.Add(pair.Key, pair.Value);

            steps++;
            cet = new ClassExecutionTrace(e, testcase);
            string verlauf = cet.ExecutionTraceStep();
            etZeilen = cet.GetExecutionTrace();
            return verlauf;
        }

        public string NextStep(int nthelement)
        {
            steps++;
            string verlauf = "";
            if (!cet.finished)
            {
                verlauf = cet.ExecutionTraceStep();
                etZeilen = cet.GetExecutionTrace();
                if (cet.finished)
                {
                    verlauf += "ET finished";
                    cd = new ClassDependencies(etZeilen);
                }
            }
            // als nächstes Dependencies
            else if (!cd.finished)
            {
                if (!cd.datafinished)
                    verlauf = cd.FindDataDependenciesSteps();
                else if (!cd.controlfinished)
                    verlauf = cd.FindControlDependenciesSteps();
                else if (!cd.symmetricfinished)
                {
                    verlauf = cd.FindSymmetricDependenciesSteps();
                    if (nthelement == -1)
                        nthelement = etZeilen.Last().eTZeileNr;

                    cs = new ClassSlice(etZeilen, nthelement);
                }

                etZeilen = cd.etZeilen;
            }
            // zuletzt Slice
            else if (!cs.finished)
            {
                dependenciesFinished = true;
                verlauf = cs.FindSliceSteps();
                inslice = cs.inslice;
                etZeilen = cs.etZeilen;
            }
            else
            {
                verlauf = "Dynamic Slicing finished";
                finished = true;
            }

            return verlauf;
        }

        public void Run(Dictionary<string, int> dictTestcase, int nthelement)
        {
            Dictionary<string, int> testcase = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> pair in dictTestcase)
                testcase.Add(pair.Key, pair.Value);

            cet = new ClassExecutionTrace(testcase);
            etZeilen = cet.ToExecutionTrace(e);
            if (etZeilen.Count == 0)
            {
                fehlermeldungen.Add("Input results in endless loop.");
                return;
            }

            output = cet.GetVariablen();
            cd = new ClassDependencies(etZeilen);
            dependenciesFinished = true;
            etZeilen = cd.FindDependencies();

            if (nthelement == -1)
                nthelement = etZeilen.Last().eTZeileNr;

            cs = new ClassSlice(etZeilen, nthelement);
            inslice = cs.GetInslice();
            etZeilen = cs.etZeilen;
        }

        public string ExecutionTraceStep()
        {
            string verlauf = cet.ExecutionTraceStep();
            return verlauf;
        }

        public void UpdateInSlice(int nthelement)
        {
            cs = new ClassSlice(etZeilen, nthelement);
            inslice = cs.GetInslice();
            etZeilen = cs.etZeilen;
        }



        public string GetDynamicSlice()
        {
            string output = "";

            foreach (ETZeile et in etZeilen)
            {
                string zeile = et.dateiZeileNr + "^" + et.eTZeileNr + "\t" + et.zeile + "\t\t"
                    + et.GetDataDependencies() + "\t\t" + et.GetControlDependencies()
                    + "\t\t" + et.GetSymmetricDependencies() + "\t\t" + et.GetSliceStatus();
                ;
                if (output != "")
                    output += "\n" + zeile;
                else
                    output = zeile;
            }
            output += "\n\nIn slice: " + Helper.ListToString(inslice);
            return output;
        }

        public string GetInSlice()
        {
            if (inslice.Count == 0) return "Slice = undefined";

            string output = "Slice = {";
            for (int a = 0; a < inslice.Count; a++)
            {
                if (a == 0) output += inslice[a];
                else output += "," + inslice[a];
            }
            output += "}";
            return output;
        }
    }
}
