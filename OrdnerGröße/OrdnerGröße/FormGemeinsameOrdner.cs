using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ordner_;
using Invoker_;

namespace OrdnerGröße
{
    public partial class FormGemeinsameOrdner : Form
    {
        private enum Sort { nachanzahl = 0, nachgröße = 1, nichts = 2 };
        Sort s = Sort.nichts;
        private Dictionary<double, ClassMD5> dictduplikate;
        private DataGridViewCellMouseEventArgs mouseEventArgs;
        private List<ClassGemeinsam> gemeinsame;
        private List<string> ignoriertePfade;
        private int datagridCols = 0;
        private Thread thread;
        private int ignoreCols = 0;
        public List<ClassDatei> gelöschteDateien = new List<ClassDatei>();
        private List<ToolStripItem> context = new List<ToolStripItem>();

        public FormGemeinsameOrdner(Dictionary<double, ClassMD5> dictduplikate, List<string> ignoriertePfade)
        {
            InitializeComponent();
            this.dictduplikate = dictduplikate;
            this.ignoriertePfade = new List<string>(ignoriertePfade);
            trackBar1.Value = 100;
        }


        private bool WirdIgnoriert(string ordnerpfad)
        {
            foreach (string ignoriert in ignoriertePfade)
            {
                if (ignoriert.Contains(ordnerpfad))
                    return true;
            }

            return false;
        }

        private void ZeichneGemeinsameThread(int prozent)
        {
            Invoker.invokeEnable(pictureBox1, true);
            Invoker.invokeVisible(pictureBox1, true);
            List<ClassGemeinsam> gemeinsameOrdner = new List<ClassGemeinsam>();
            foreach (KeyValuePair<double, ClassMD5> pair in dictduplikate)
            {
                List<string> ordnerliste = new List<string>();

                // Bekomme jeweilige Ordnerpfade
                foreach (ClassDatei cd in pair.Value.duplikate)
                {
                    string ordnerpfad = cd.ordnerpfad;
                    if (File.Exists(cd.pfad) && !WirdIgnoriert(ordnerpfad))
                    {
                        ordnerliste.Add(ordnerpfad);
                    }
                }

                if (ordnerliste.Count < 2) continue;

                // Schon vorhanden?
                int index = -1;
                for (int a = 0; a < gemeinsameOrdner.Count; a++)
                {
                    if (gemeinsameOrdner[a].SindGleicheOrdner(ordnerliste))
                    {
                        index = a;
                        break;
                    }
                }

                if (index == -1) // neue Gemeinsame Verbindung
                {
                    gemeinsameOrdner.Add(new ClassGemeinsam(ordnerliste));
                    index = gemeinsameOrdner.Count - 1;
                }

                // Füge Dateien hinzu
                for (int a = 0; a < pair.Value.duplikate.Count; a++)
                {
                    ClassDatei cd = pair.Value.duplikate[a];
                    string ordnerpfad = cd.ordnerpfad;

                    if (ordnerliste.Contains(ordnerpfad) && !gemeinsameOrdner[index].AddDatei(a, cd))
                    {
                        MessageBox.Show("Fehler aufgetreten!\nDatei:\n" + cd.pfad + "\nkonnte nicht zugeteilt werden");
                        break;
                    }
                }
            }

            gemeinsame = new List<ClassGemeinsam>();
            datagridCols = -1;
            // Sortieren nach Anzahl der Dateien 
            foreach (ClassGemeinsam cg in gemeinsameOrdner.OrderByDescending(i => i.anzahl))
            {
                int iprozent = cg.GetProzentAnteil();
                if (iprozent >= prozent)
                {
                    if (datagridCols == -1 || cg.GetOrdnerPfade().Count > datagridCols) datagridCols = cg.GetOrdnerPfade().Count;
                    gemeinsame.Add(cg);
                }
            }

            try
            {
                dataGridView1.Invoke((MethodInvoker)(() => ZeichneDataGrid()));
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }

            Invoker.invokeEnable(pictureBox1, false);
            Invoker.invokeVisible(pictureBox1, false);
        }

        private void ZeichneGemeinsame(int prozent)
        {
            // 3 Versuche, um Thread zu beenden
            for (int a = 0; a < 3; a++)
            {
                if (thread != null && thread.IsAlive)
                    thread.Abort();
                else
                    break;
            }

            thread = new Thread(delegate () { ZeichneGemeinsameThread(prozent); });
            thread.Start();
        }

        private void ZeichneDataGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("colAnz", "Anzahl");
            dataGridView1.Columns.Add("colGrö", "Größe");
            ignoreCols = dataGridView1.Columns.Count;
            for (int a = 0; a < datagridCols; a++)
                dataGridView1.Columns.Add("col" + (a + 1), (a + 1) + ". Pfad");


            // Sortiere
            if (s == Sort.nachanzahl)
            {
                foreach (ClassGemeinsam cg in gemeinsame.OrderByDescending(i => i.anzahl))
                    dataGridView1.Rows.Add(cg.GetZeile());
            }
            else if (s == Sort.nachgröße)
            {
                foreach (ClassGemeinsam cg in gemeinsame.OrderByDescending(i => i.größe))
                    dataGridView1.Rows.Add(cg.GetZeile());
            }

            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                int colw = dataGridView1.Columns[i].Width;
                dataGridView1.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridView1.Columns[i].Width = colw;
            }
        }



        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = "Ordner sind zu " + trackBar1.Value + "% identisch";
            
            ZeichneGemeinsame(trackBar1.Value);
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < ignoreCols ||
                dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;
            mouseEventArgs = e;

            contextMenuStrip1.Show(MousePosition);
        }

        private void ordnerÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int irow = mouseEventArgs.RowIndex;
            int icol = mouseEventArgs.ColumnIndex;

            string ordnerpfad = dataGridView1.Rows[irow].Cells[icol].Value.ToString();
            if (Directory.Exists(ordnerpfad))
            {
                Process.Start(ordnerpfad);
            }
            else
                MessageBox.Show("Explorer konnte Ordner nicht öffen!");
        }

        private void FormGemeinsameOrdner_Load(object sender, EventArgs e)
        {
            context = new List<ToolStripItem>();
            foreach (ToolStripItem ts in contextMenuStrip1.Items)
                context.Add(ts);

            comboBox1.SelectedIndex = 0;
            ZeichneGemeinsame(trackBar1.Value);
        }

        // Welche Reihe wurde selektiert?
        private ClassGemeinsam GetAktuelleGemeinsame(int iRow)
        {
            ClassGemeinsam cg = new ClassGemeinsam();
            if (s == Sort.nachanzahl)
                cg = gemeinsame.OrderByDescending(i => i.anzahl).ElementAt(iRow);
            else if (s == Sort.nachgröße)
                cg = gemeinsame.OrderByDescending(i => i.größe).ElementAt(iRow);
            else
            {
                MessageBox.Show("Es konnte kein Item ausgewählt werden!");
                return cg;
            }
            return cg;
        }

        private void nurDieseBehaltenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gemeinsame == null || gemeinsame.Count == 0)
            {
                MessageBox.Show("Keine Elemente vorhanden!"); return;
            }

            int iColumn = mouseEventArgs.ColumnIndex;
            int iRow = mouseEventArgs.RowIndex;
            iColumn -= ignoreCols;

            ClassGemeinsam cg = GetAktuelleGemeinsame(iRow);        

            if (!cg.LöscheAusAnderenOrdnern(iColumn, ref gelöschteDateien))
            {
                MessageBox.Show(cg.fehler + "\nEs konnten nicht alle Dateien gelöscht werden!");
                return;
            }

            // Cg element entfernen
            for (int a  = 0; a< gemeinsame.Count; a++)
            {
                if (cg.SindGleicheOrdner(gemeinsame[a].GetOrdnerPfade()))
                {
                    gemeinsame.RemoveAt(a);
                    break;
                }
            }


            // Gemeinsame updaten
            for (int a = 0; a < gemeinsame.Count; a++)
            {
                for (int b = 0; b < gemeinsame.ElementAt(a).pfade.Count; b++)
                {
                    Pfad p = gemeinsame.ElementAt(a).pfade[b];
                    for (int c = 0; c < p.dateien.Count; c++)
                    {
                        // Datei noch enthalten, die aber schon gelöscht wurde?
                        if (gelöschteDateien.Contains(p.dateien[c]))
                            p.dateien.RemoveAt(c--);
                    }

                    if (p.dateien.Count == 0)
                        gemeinsame.ElementAt(a).pfade.RemoveAt(b--);
                }
                if (gemeinsame.ElementAt(a).pfade.Count == 0)
                    gemeinsame.RemoveAt(a--);
            }

            ZeichneDataGrid();
        }

        private void toolStripMenuItemDateienAnzeigen_Click(object sender, EventArgs e)
        {
            if (gemeinsame == null || gemeinsame.Count == 0)
            {
                MessageBox.Show("Keine Elemente vorhanden!"); return;
            }

            int iColumn = mouseEventArgs.ColumnIndex - ignoreCols;
            int iRow = mouseEventArgs.RowIndex;
            ClassGemeinsam cg = GetAktuelleGemeinsame(iRow);
            FormDateienZeigen fdz = new FormDateienZeigen(cg.pfade[iColumn].dateien);
            fdz.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool zeichne = (s != Sort.nichts);

            if (comboBox1.SelectedIndex == 0) s = Sort.nachgröße;
            else s = Sort.nachanzahl;

            if (zeichne)
                ZeichneDataGrid();
        }

        private void ersteDateiÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gemeinsame == null || gemeinsame.Count == 0)
            {
                MessageBox.Show("Keine Elemente vorhanden!"); return;
            }

            int iColumn = mouseEventArgs.ColumnIndex - ignoreCols;
            int iRow = mouseEventArgs.RowIndex;
            ClassGemeinsam cg = GetAktuelleGemeinsame(iRow);
            if (!cg.pfade[iColumn].dateien.First().ÖffneDatei())
                MessageBox.Show("Datei konnte nicht geöffnet werden!\n" + cg.pfade[iColumn].dateien.First().pfad);
        }
    }
}
