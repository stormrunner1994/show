using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Background
{
    public class Termin
    {
        public Termin(int index, int gruppenindex,DateTime datum, string uhrzeit, string grund, string beschreibung, bool erinnert)
        {
            this.index = index;
            this.gruppenindex = gruppenindex;
            this.datum = datum;
            this.uhrzeit = uhrzeit;
            this.grund = grund;
            this.beschreibung = beschreibung;
            this.erinnert = erinnert;
        }

        public int index { get; set; }
        public int gruppenindex { get; set; }
        public DateTime datum { get; set; }
        public string uhrzeit { get; set; }
        public string grund { get; set; }
        public string beschreibung { get; set; }
        public bool erinnert { get; set; }

        public string ToLine()
        {
            return index + ";" + gruppenindex + ";" + datum.ToShortDateString() + ";" + uhrzeit + ";" + grund + ";" + beschreibung + ";" + erinnert;
        }

    }
}
