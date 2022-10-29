using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Operation;

namespace DynamicSlicing
{
    class Ebene
    {
        public int dateizeilenr { get; set; }
        public int iEbene { get; set; }
        public string zeile { get; set; }
        public string funktion { get; set; }
        public List<Ebene> ebenen { get; set; }
        public int index { get; set; }
        public Ebene parent { get; set; }

        public Ebene(string ezeile, int iEbene, int edateizeilenr, Ebene parent)
        {
            this.dateizeilenr = edateizeilenr;
            this.index = edateizeilenr;
            this.zeile = ezeile;
            this.ebenen = new List<Ebene>();
            this.iEbene = iEbene;
            this.funktion = GetFunktion();
            this.parent = parent;
        }

        public Ebene(List<string> ezeilen, int eindex, int iEbene, Ebene parent)
        {
            this.index = eindex; // Position in zeilenliste
            this.dateizeilenr = eindex;
            this.ebenen = new List<Ebene>();
            this.iEbene = iEbene;
            this.parent = parent;

            while (this.index < ezeilen.Count)
            {
                zeile = ezeilen[this.index];
                int tabs = 0;
                while (zeile.Length > 0 && zeile.First() == '\t')
                {
                    zeile = zeile.Remove(0, 1);
                    tabs++;
                }

                if (tabs > iEbene)
                {
                    Ebene temp = new Ebene(ezeilen, this.index, this.iEbene + 1, this.ebenen.Last());
                    this.ebenen.RemoveAt(this.ebenen.Count - 1);
                    this.ebenen.Add(temp);
                    this.index = temp.index;
                }
                else if (tabs == iEbene)
                {
                    this.ebenen.Add(new Ebene(zeile, this.iEbene, this.index, this.parent));
                    this.index++;
                }
                else
                {
                    break;
                }
            }

            if (iEbene != 0)
            {
                this.zeile = this.parent.zeile;
                this.funktion = this.parent.funktion;
                this.dateizeilenr = this.parent.dateizeilenr;
                this.parent = this.parent.parent;
            }
            else if (this.ebenen.Count == 1)
            {
                this.zeile = "";
            }
        }

        private void EbeneUpdaten(Ebene neu)
        {
            neu.dateizeilenr = this.ebenen.Last().dateizeilenr;
            neu.iEbene = this.ebenen.Last().iEbene;
            neu.zeile = this.ebenen.Last().zeile;
            neu.funktion = this.ebenen.Last().funktion;
            neu.parent = this.parent;
            this.ebenen.RemoveAt(this.ebenen.Count - 1);
            this.ebenen.Add(neu);
        }

        public string PrintEbene()
        {
            return PrintEbene(this);
        }

        private string GetFunktion()
        {
            string funktion = "zuweisung";
            if (zeile.Contains("if"))
                funktion = "if";
            else if (zeile.Contains("while"))
                funktion = "while";
            else if (zeile.Contains("for"))
                funktion = "for";
            else if (zeile.Contains("else"))
                funktion = "else";
            else if (zeile.Contains("write"))
                funktion = "write";
            else if (zeile.Contains("read"))
                funktion = "read";
            else if (!zeile.Contains("=")) // fehler/unnötige zeile gefunden?
                funktion = "";

            return funktion;
        }

        public static string PrintEbene(Ebene e)
        {
            string print = "";

            foreach (Ebene ue in e.ebenen)
            {
                if (print != "")
                {
                    if (ue.zeile != "")
                        print += "\n" + ue.zeile;
                    else
                        print += "\n" + PrintEbene(ue);
                }
                else
                {
                    if (ue.zeile != "")
                        print += ue.zeile;
                    else
                        print += PrintEbene(ue);
                }
            }

            return print;
        }
    }
}
