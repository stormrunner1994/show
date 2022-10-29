using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicSlicing
{
    class Arrow
    {
        public Pen pen { get; set; }
        public Point start { get; set; }
        public Point ziel { get; set; }

        public string kategorie { get; set; } // data, control, sym

        public Arrow(Point start, Point ziel, string kategorie)
        {
            pen = new Pen(Color.Black, 3);
            pen.StartCap = LineCap.Flat;
            pen.EndCap = LineCap.ArrowAnchor;
            this.start = start;
            this.ziel = ziel;
            this.kategorie = kategorie;
        }

        public void SetColor(Color c)
        {
            pen.Color = c;
        }
    }
}
