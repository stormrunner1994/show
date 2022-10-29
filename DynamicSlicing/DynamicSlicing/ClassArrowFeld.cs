using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicSlicing
{
    class ClassArrowFeld
    {
        List<Arrow> arrows = new List<Arrow>();
        Point startpunkt = new Point(250, 50);
        List<List<char>> datafeld = new List<List<char>>(); // spalten, zeilen
        List<List<char>> controlfeld = new List<List<char>>();
        List<List<char>> symfeld = new List<List<char>>();
        List<int[]> datadep = new List<int[]>();
        List<int[]> controldep = new List<int[]>();
        List<int[]> symdep = new List<int[]>();
        int pfeilabstand = 10; // Pixelabstand
        List<int> reihenhöhen = new List<int>();
        List<int> zellenbreite = new List<int>();
        int minwidth = 50;
        public List<int> widthOfCols { get; set; }

        public ClassArrowFeld(List<ETZeile> etZeilen, DataGridView grid)
        {
            widthOfCols = new List<int>();
            foreach (DataGridViewRow row in grid.Rows)
                reihenhöhen.Add(row.Height);
            foreach (DataGridViewColumn col in grid.Columns)
                zellenbreite.Add(col.Width);

            startpunkt = new Point(GetAbstand(0, 1, zellenbreite) +10, 50);

            foreach (ETZeile z in etZeilen)
            {
                foreach (int ziel in z.datadepencies)
                    datadep.Add(new int[] { z.eTZeileNr, ziel });

                foreach (int ziel in z.controldepencies)
                    controldep.Add(new int[] { z.eTZeileNr, ziel });

                foreach (int ziel in z.symmeticdepencies)
                    symdep.Add(new int[] { z.eTZeileNr, ziel });

                datafeld.Add(new List<char>() { ' ' }); // adde zeilen
                controlfeld.Add(new List<char>() { ' ' });
                symfeld.Add(new List<char>() { ' ' });
            }
            CalcArrows();
        }

        private void CalcArrows()
        {
            // Pfeile für Datadependency
            foreach (int[] verbindung in datadep)
            {
                FeldVerbindung feldverbindung = GetPosInArrowFeld(verbindung[0], verbindung[1], ref datafeld);
                // Nun die Koordinaten in Pixelpositionen für Pfeil
                int starty = startpunkt.Y + GetAbstand(0, feldverbindung.startzeile, reihenhöhen);
                int startx = startpunkt.X + feldverbindung.startspalte * pfeilabstand;
                int ziely = starty + GetAbstand(feldverbindung.startzeile, feldverbindung.zielzeile, reihenhöhen);
                int zielx = startx;
                arrows.Add(new Arrow(new Point(startx, starty), new Point(zielx, ziely), "data"));
            }

            int diff = GetDifference("data");
            if (diff > minwidth) widthOfCols.Add(diff);
            else widthOfCols.Add(minwidth);

            startpunkt.X += widthOfCols.Last();

            // Pfeile für Controldependency
            foreach (int[] verbindung in controldep)
            {
                FeldVerbindung feldverbindung = GetPosInArrowFeld(verbindung[0], verbindung[1], ref controlfeld);
                // Nun die Koordinaten in Pixelpositionen für Pfeil
                int starty = startpunkt.Y + GetAbstand(0, feldverbindung.startzeile, reihenhöhen);
                int startx = startpunkt.X + feldverbindung.startspalte * pfeilabstand;
                int ziely = starty + GetAbstand(feldverbindung.startzeile, feldverbindung.zielzeile, reihenhöhen);
                int zielx = startx;
                arrows.Add(new Arrow(new Point(startx, starty), new Point(zielx, ziely), "control"));
            }

            diff = GetDifference("control");
            if (diff > minwidth) widthOfCols.Add(diff);
            else widthOfCols.Add(minwidth);

            startpunkt.X += widthOfCols.Last();

            // Pfeile für Symmetricdependency
            foreach (int[] verbindung in symdep)
            {
                FeldVerbindung feldverbindung = GetPosInArrowFeld(verbindung[0], verbindung[1], ref symfeld);
                // Nun die Koordinaten in Pixelpositionen für Pfeil
                int starty = startpunkt.Y + GetAbstand(0, feldverbindung.startzeile, reihenhöhen);
                int startx = startpunkt.X + feldverbindung.startspalte * pfeilabstand;
                int ziely = starty + GetAbstand(feldverbindung.startzeile, feldverbindung.zielzeile, reihenhöhen);
                int zielx = startx;
                arrows.Add(new Arrow(new Point(startx, starty), new Point(zielx, ziely), "sym"));
            }

            diff = GetDifference("sym");
            if (diff > minwidth) widthOfCols.Add(diff);
            else widthOfCols.Add(minwidth);
        }

        private int GetDifference(string kategorie)
        {
            int smallestx = -1;
            int biggestx = -1;

            // Data Dependency
            foreach (Arrow arrow in arrows)
            {
                if (arrow.kategorie != kategorie) continue;

                if (smallestx == -1) smallestx = arrow.start.X;
                else if (arrow.start.X < smallestx) smallestx = arrow.start.X;
                if (biggestx == -1) biggestx = arrow.start.X;
                else if (arrow.start.X > biggestx) biggestx = arrow.start.X;
            }
            int diff = biggestx - smallestx + 20;
            return diff;
        }

        // Abstand über alle Zeilenhöhen hinweg
        private int GetAbstand(int startzeile, int zielzeile, List<int> ausrichtung)
        {
            // Berechne Gesamthöhe durch alle betroffenen Gridzeilen
            int sum = 0;
            for (int a = startzeile; a < zielzeile; a++)
                sum += ausrichtung[a];
            return sum;
        }

        private FeldVerbindung GetPosInArrowFeld(int startzeile, int zielzeile, ref List<List<char>> feld)
        {
            startzeile--; // Für Unterschied von Stelle im Grid und Koordinate
            zielzeile--;

            for (int spalte = 0; spalte < feld[0].Count; spalte++)
            {
                bool platzgefunden = true;
                // Teste Platz
                for (int zeile = startzeile; zeile <= zielzeile; zeile++)
                {
                    if (feld[zeile][spalte] == 'X')
                    {
                        platzgefunden = false;
                        break;
                    }
                }

                if (platzgefunden)
                {
                    for (int zeile = startzeile; zeile <= zielzeile; zeile++)
                        feld[zeile][spalte] = 'X'; // Zelle belegen
                    return new FeldVerbindung(startzeile, spalte, zielzeile, spalte);
                }
            }

            // Falls kein Platz im aktuellen Feld gefunden, erstelle neue Spalte
            for (int a = 0; a < feld.Count; a++)
                feld[a].Add(' ');

            int neuespalte = feld[0].Count - 1;
            for (int zeile = startzeile; zeile <= zielzeile; zeile++)
                feld[zeile][neuespalte] = 'X'; // Zelle belegen

            return new FeldVerbindung(startzeile, neuespalte, zielzeile, neuespalte);
        }

        public List<Arrow> GetArrows()
        {
            return arrows;
        }
    }

    class FeldVerbindung
    {
        public int startzeile { get; set; }
        public int startspalte { get; set; }
        public int zielzeile { get; set; }
        public int zielspalte { get; set; }

        public FeldVerbindung(int startzeile, int startspalte, int zielzeile, int zielspalte)
        {
            this.startzeile = startzeile;
            this.startspalte = startspalte;
            this.zielzeile = zielzeile;
            this.zielspalte = zielspalte;
        }
    }
}
