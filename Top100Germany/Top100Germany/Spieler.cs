using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmLadder
{
    public class Spieler
    {
        public int rang { get; set; }

        public string name { get; set; }

        public int punkte { get; set; }

        public Spieler (int rang, string name, int punkte)
        {
            this.rang = rang;
            this.name = name;
            this.punkte = punkte;
        }

        public string getZeile()
        {
            return rang + ";" + name + ";" + punkte;
        }
    }
}
