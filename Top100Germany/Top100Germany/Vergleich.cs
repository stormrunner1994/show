using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Top100Germany
{
    class Vergleich
    {
        public string name { get; set; }
        public int vorher { get; set; }
        public int neu { get; set; }

        public int differenz { get; set; }

        public Vergleich (string name, int vorher, int neu)
        {
            this.name = name;
            this.vorher = vorher;
            this.neu = neu;
            this.differenz = neu - vorher;
        }

        public string[] GetZeile()
        {
            return (name + ";" + vorher + ";" + neu + ";" + differenz).Split(';');
        }
    }
}
