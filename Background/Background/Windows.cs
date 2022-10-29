using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Background
{
    class Windows
    {
        public Windows(Dictionary<string, string> dictspeicherpfade)
        {
            this.dictspeicherpfade = dictspeicherpfade;
            hintergrundpfad = GetOriginalWallpaperPath();
            terminbild = Application.StartupPath + "\\termin.png";
        }

        public bool work = true;

        private Dictionary<string, string> dictspeicherpfade;
        private string hintergrundpfad;
        private string terminbild;
        private List<FormErinnerung> erinnerungen;


        private string GetCurrentWallpaperPath()
        {
            RegistryKey wallPaper = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false);
            string WallpaperPath = wallPaper.GetValue("WallPaper").ToString();
            wallPaper.Close();            

            return WallpaperPath;
        }

        private string GetOriginalWallpaperPath()
        {
            StreamReader sr = new StreamReader(dictspeicherpfade["Einstellungen"]);
            sr.ReadLine();
            string WallpaperPath = sr.ReadLine();
            sr.Close();
            return WallpaperPath;
        }
        

        [DllImport("user32.dll")]
        private static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, String pvParam, UInt32 fWinIni);

        private static UInt32 SPI_SETDESKWALLPAPER = 20;
        private static UInt32 SPIF_UPDATEINIFILE = 0x1;

        public void TerminbildAlsHintergrund()
        {
            string Filename = terminbild;
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, Filename, SPIF_UPDATEINIFILE);
        }

        public void HintergrundbildZurücksetzen()
        {
            string Filename = hintergrundpfad;
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, Filename, SPIF_UPDATEINIFILE);
        }

        public bool TerminBildWirdAngezeigt()
        {
            if (GetCurrentWallpaperPath() == terminbild)
                return true;
            return false;
        }

        public void AusgabenAnzeigen(List<string> ausgaben)
        {
            erinnerungen = new List<FormErinnerung>();
            TerminbildAlsHintergrund();

            foreach (string ausgabe in ausgaben)
            {
                erinnerungen.Add(new FormErinnerung(ausgabe));
            }

            foreach (FormErinnerung erinnerung in erinnerungen)
                erinnerung.Show();
        }

        public bool FensterOffen()
        {
            foreach(FormErinnerung erinnerung in erinnerungen)
            {
                if (erinnerung.IstFensterOffen())
                    return true;
            }
            
            HintergrundbildZurücksetzen();
            return false;
        }



    }
}
