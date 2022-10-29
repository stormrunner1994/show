using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Phase6_Software
{
    public class ClassKarteikarte
    {
        public int ID { get; set; }
        public int IDtemp { get; set; }
        public string Kategorie { get; set; }
        public string Frage { get; set; }
        public string Antwort { get; set; }
        public int Phase { get; set; }
        public int Richtige { get; set; }
        public int Falsche { get; set; }
        public DateTime Datum { get; set; }
    }
}
