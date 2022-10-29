using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace Background
{
    class ClassÜbergreifend
    {
        private static List<char> verboten = new List<char>() { ' ', '\n', '\r' };
        public static Dictionary<string, string> dictspeicherpfade { get; set; }

        public static string MKürzen(string text)
        {
            while (text.Length > 0 && verboten.Contains(text[0]))
                text = text.Remove(0, 1);

            while (text.Length > 0 && verboten.Contains(text[text.Length - 1]))
                text = text.Remove(text.Length - 1, 1);

            return text;
        }

        public static void SpeichereTermineInDatei(List<Termin> termine)
        {
            // in Datei schreiben
            string datei = "Index;GruppenIndex;Datum;Uhrzeit;Grund;Beschreibung;Erinnert";

            foreach (Termin t in termine)
                datei += '\n' + t.ToLine();

            StreamWriter sw = new StreamWriter(dictspeicherpfade["Termine"]);
            sw.WriteLine(datei);
            sw.Close();
        }

        [DllImport("user32.dll")]
        public extern static int SetForegroundWindow(IntPtr HWnd);

        [DllImport("user32.dll")]
        static extern int GetAsyncKeyState(Int32 i);
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy,
                               int dwData, int dwExtraInfo);
        
        public static string MTasteGedrückt()
        {
            string pressedkey = "";
            //sleeping for while, this will reduce load on cpu
            for (Int32 i = 0; i < 255; i++)
            {
                int keyState = GetAsyncKeyState(i);
                if (keyState == 1 || keyState == -32767)
                {
                    pressedkey = ((Keys)i).ToString();                  
                    
                    break;
                }
            }

            return pressedkey;
        }

    }
}
