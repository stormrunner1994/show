using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DynamicSlicing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            f = new FormCode(TESTEN);
            dictTestcase = new Dictionary<string, int>();
            buttonstartedynamicSlicing.Enabled = false;
        }

        private const bool TESTEN = false;

        private FormCode f;
        private string code = "";
        private DynamicSlicing dyn;
        private Dictionary<string, int> dictTestcase;
        private List<string> variablesOfInterest;
        private bool checkTextbox = false;
        private int nthelement = -1;
        private List<Arrow> arrows = new List<Arrow>();

        /// <summary>
        /// Zeichen das Gridfeld komplett
        /// </summary>
        private void PrintGrid()
        {
            dataGridViewtable.Columns.Clear();
            List<ETZeile> etZeilen = dyn.etZeilen;

            if (etZeilen == null) return;

            dataGridViewtable.ColumnHeadersDefaultCellStyle.Font =
                new Font(dataGridViewtable.Font, FontStyle.Bold);

            dataGridViewtable.ColumnCount = 6;
            dataGridViewtable.Columns[0].Name = "Execution Trace";
            dataGridViewtable.Columns[1].Name = "Data Dep.";
            dataGridViewtable.Columns[2].Name = "Control Dep.";
            dataGridViewtable.Columns[3].Name = "Sym. Dep.";
            dataGridViewtable.Columns[4].Name = "Slice";
            dataGridViewtable.Columns[5].Name = "Variables";

            foreach (ETZeile et in etZeilen)
                dataGridViewtable.Rows.Add(et.GetArrayZeile(false));

            // Spaltenbreite anpassen            
            for (int i = 0; i <= dataGridViewtable.Columns.Count - 1; i++)
            {
                if (i != 0)
                {
                    if (i != dataGridViewtable.Columns.Count - 1)
                        dataGridViewtable.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopCenter;
                }

                dataGridViewtable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                //store autosized widths
                int colw = dataGridViewtable.Columns[i].Width;
                //remove autosizing
                dataGridViewtable.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                dataGridViewtable.Columns[i].Width = colw;
            }

            // Pfeile einzeichnen
            if (DependenciesVorhanden(dyn.etZeilen))
            {
                arrows.Clear();
                // Pfeile einzeichnen
                ClassArrowFeld caf = new ClassArrowFeld(dyn.etZeilen, dataGridViewtable);
                arrows = caf.GetArrows();
                dataGridViewtable.Invalidate();

                // Passe Col Breite nach Pfeilen an
                for (int a = 0; a < caf.widthOfCols.Count; a++)
                    dataGridViewtable.Columns[a + 1].Width = caf.widthOfCols[a];
            }

            richTextBoxresults.Text = dyn.GetInSlice();

            // Hole Werte von Variablen of interest
            if (variablesOfInterest != null)
            {
                string outputs = "";
                foreach (string v in variablesOfInterest)
                {
                    if (!dyn.getOutput().ContainsKey(v)) continue;

                    if (outputs != "") outputs += "," + v + "= " + dyn.getOutput()[v].ToString();
                    else outputs += v + "= " + dyn.getOutput()[v].ToString();
                }
                richTextBoxresults.Text += "\n" + outputs;
            }

            // Fehlermeldungen
            if (dyn.fehlermeldungen.Count > 0)
            {
                richTextBoxresults.Text += "Error occurred";
            }

            foreach (string f in dyn.fehlermeldungen)
            {
                richTextBoxresults.Text += "\n" + f;
            }

            // Erkenne Betriebssystem
            // Ist nicht Windows
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                richTextBoxresults.Text += "\nNotice that Mono doesn't support the arrows. For Data and Control" +
                    "Dependencies the arrows show down and for symetric dependencies the arrows show in both sides.";
            }
        }


        private bool DependenciesVorhanden(List<ETZeile> etZeilen)
        {
            foreach (ETZeile zeile in etZeilen)
            {
                if (zeile.datadepencies.Count > 0 || zeile.controldepencies.Count > 0 || zeile.symmeticdepencies.Count > 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Führe DynamicSlicing aus
        /// </summary>
        private void RunDynamicSlicing(bool zwischenschritte)
        {
            // ist Testcase eingetragen?
            if (dictTestcase == null)
            {
                MessageBox.Show("Please select Testcase!");
                return;
            }

            dyn.Reset();
            // mit zwischenschritten
            if (zwischenschritte)
            {
                groupBoxhistory.Enabled = true;
                buttonnextstep.Enabled = buttonskiptoend.Enabled = true;
                richTextBoxhistory.Text = dyn.steps + ") " + dyn.RunStepwise(dictTestcase);
                PrintGrid();
            }
            else
            {
                groupBoxhistory.Enabled = false;
                Stopwatch sw = new Stopwatch();
                sw.Start();
                dyn.Run(dictTestcase, nthelement);

                if (dyn.fehlermeldungen.Count > 0)
                {
                    string strfehler = "";
                    foreach (string fehler in dyn.fehlermeldungen)
                    {
                        if (strfehler == "") strfehler += fehler;
                        else strfehler += "\n" + fehler;
                    }
                    MessageBox.Show(strfehler);
                    return;
                }

                // für initial fall berechne slice für letzte zeile
                if (nthelement == -1)
                {
                    checkTextbox = false;
                    nthelement = dyn.etZeilen.Last().eTZeileNr;
                    textBoxnthelement.Text = nthelement.ToString();
                    checkTextbox = true;
                }

                PrintGrid();
                textBoxnthelement.Enabled = true;
                sw.Stop();
                labelstopwatch.Visible = true;
                labelstopwatch.Text = "Fertig nach " + sw.ElapsedMilliseconds + " ms";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            groupBoxhistory.Enabled = false;
            // Textboxen in groubox deaktivieren
            foreach (TextBox tb in groupBox1.Controls.OfType<TextBox>())
                tb.Enabled = false;


            // Testen
            if (TESTEN)
            {
                buttoncode_Click(sender, e);
                /*
                textBoxtestcase.Text = textBoxtestcase.Text.Insert(2, "0");
                textBoxtestcase.Text = textBoxtestcase.Text.Insert(6, "2");
                textBoxtestcase.Text = textBoxtestcase.Text.Insert(10, "1");
                */
                //checkBoxintermediatesteps.Checked = true;
                buttonstartedynamicSlicing_Click(sender, e);
            }

            labelstopwatch.Visible = false;
        }

        /// <summary>
        /// textBoxtestinput füllen, um benötigte Variablenwerte anzufordern
        /// </summary>
        private void prepareTestcase(List<string> testcase)
        {
            // erlaubt x=3,y=3"
            textBoxtestcase.Text = "";

            if (textBoxtestcase.Text != "") return;

            foreach (string t in testcase)
            {
                if (textBoxtestcase.Text != "") textBoxtestcase.Text += "," + t + "=";
                else textBoxtestcase.Text += t + "=";
            }
        }

        private void buttoncode_Click(object sender, EventArgs e)
        {
            try
            {
                f.ShowDialog();

                // kein leerer Code akzeptiert
                if (f.code == null || f.code == "" || code == f.code) return;

                dataGridViewtable.Rows.Clear();
                arrows.Clear();
                textBoxnthelement.Enabled = false;
                code = f.code;
                buttonshowcode.Enabled = true;
                buttonshowcode.ForeColor = Color.Black;
                buttonshowcode.Text = "Show Code";
                dyn = new DynamicSlicing(code);
                nthelement = -1;
                textBoxnthelement.Text = nthelement.ToString();
                textBoxvariablesofinterest.Enabled = true;

                // Testcase erforderlich
                if (dyn.getTestCaseVariablen().Count > 0)
                {
                    textBoxtestcase.BackColor = Color.Red;
                    textBoxtestcase.Enabled = true;
                    prepareTestcase(dyn.getTestCaseVariablen());
                }
                else
                {
                    textBoxtestcase.Text = "";
                    textBoxtestcase.Enabled = false;
                    dictTestcase = new Dictionary<string, int>();
                    buttonstartedynamicSlicing.Enabled = checkBoxintermediatesteps.Enabled = true;
                }
                checkTextbox = true;
            }
            catch (Exception ex)
            {
                if (!f.isShown)
                {
                    MessageBox.Show(ex.Message);
                    f = new FormCode(TESTEN);
                    f.ShowDialog();
                }
            }
        }

        private void buttonergebnisausvorlesung_Click(object sender, EventArgs e)
        {
            try
            {
                new Ergebnis().Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void buttonstartedynamicSlicing_Click(object sender, EventArgs e)
        {
            // Lösche Pfeile
            arrows.Clear();
            dataGridViewtable.Refresh();

            groupBox1.Enabled = true;
            richTextBoxhistory.Text = "";
            RunDynamicSlicing(checkBoxintermediatesteps.Checked);
            this.Refresh();     // Combines Invalidate() and Update()
        }

        private void buttoncodezeigen_Click(object sender, EventArgs e)
        {
            new CodeZeigen(code).Show();
        }



        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {
            if (arrows.Count == 0)
                return;


            for (int a = 0; a < arrows.Count; a++)
            {
                Arrow arrow = arrows[a];

                // Färbung
                if (arrow.kategorie == "data")
                    arrow.SetColor(Color.Blue);
                else if (arrow.kategorie == "control")
                    arrow.SetColor(Color.Green);
                else if (arrow.kategorie == "sym")
                    arrow.SetColor(Color.DarkViolet);

                if (!dyn.dependenciesFinished && a == arrows.Count - 1)
                    arrow.SetColor(Color.Red);

                if (arrow.kategorie == "sym")
                    arrow.pen.StartCap = LineCap.ArrowAnchor;
                e.Graphics.DrawLine(arrow.pen, arrow.start, arrow.ziel);
            }
        }

        /// <summary>
        /// Teste, ob die Testcase Variablen valide Werte bekommen haben
        /// </summary>
        private void textBoxtestcase_TextChanged(object sender, EventArgs e)
        {
            if (!checkTextbox) return;

            // Falls leer, fülle mit Variablen auf, die benötigt werden
            if (textBoxtestcase.Text == "")
            {
                if (dyn.getTestCaseVariablen().Count > 0)
                {
                    textBoxtestcase.BackColor = Color.Red;
                    textBoxtestcase.Enabled = true;
                    prepareTestcase(dyn.getTestCaseVariablen());
                }
            }


            dictTestcase = new Dictionary<string, int>();
            buttonstartedynamicSlicing.Enabled = checkBoxintermediatesteps.Enabled = false;
            try
            {
                string[] inputs = textBoxtestcase.Text.Trim().Split(',');

                foreach (string s in inputs)
                {
                    string[] splits = s.Split('=');
                    if (splits.Length == 2)
                        dictTestcase.Add(splits[0].Trim(), Convert.ToInt32(splits[1].Trim()));
                }

                // Wurden alle geforderten Variablen gesetzt?
                if (equals(dictTestcase.Keys.ToList(), dyn.getTestCaseVariablen()))
                {
                    buttonstartedynamicSlicing.Enabled = checkBoxintermediatesteps.Enabled = true;
                    textBoxtestcase.BackColor = SystemColors.Window;
                }
                else
                {
                    dictTestcase = null;
                    textBoxtestcase.BackColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                dictTestcase = null;
                textBoxtestcase.BackColor = Color.Red;
                //MessageBox.Show("Input hat nicht das richtige Format!\n\nErlaubt ist z.B.: x=3,y=3");
                return;
            }
        }

        private bool equals(List<string> list1, List<string> list2)
        {
            for (int a = 0; a < list1.Count; a++)
            {
                for (int b = 0; b < list2.Count; b++)
                {
                    if (list1[a] == list2[b])
                    {
                        list1.RemoveAt(a);
                        list2.RemoveAt(b);
                        a--;
                        break;
                    }
                }
            }

            if (list1.Count + list2.Count == 0) return true;
            return false;
        }

        private void buttonshowformular_Click(object sender, EventArgs e)
        {
            new Formformular().Show();
        }

        private void updateInSlice(int nthelement)
        {
            dyn.UpdateInSlice(nthelement);
            PrintGrid();
        }

        private void textBoxnthelement_TextChanged(object sender, EventArgs e)
        {
            if (!checkTextbox) return;

            string text = ((TextBox)sender).Text;
            int iout;

            if (Int32.TryParse(text, out iout) && dyn.etZeilen != null)
            {
                nthelement = Convert.ToInt32(text);
                updateInSlice(nthelement);
            }
        }
        private void textBoxvariablesofinterest_TextChanged(object sender, EventArgs e)
        {
            string text = ((TextBox)sender).Text;

            if (text == "") return;

            string[] splits = text.Split(',');

            if (splits.Length > 0)
            {
                variablesOfInterest = new List<string>();
                foreach (string s in splits)
                    variablesOfInterest.Add(s.Trim());

                PrintGrid();
            }
        }

        private void textBoxvariablesofinterest_Click(object sender, EventArgs e)
        {
            if (((TextBox)sender).ForeColor == SystemColors.InactiveCaption)
            {
                ((TextBox)sender).Text = "";
                ((TextBox)sender).ForeColor = SystemColors.WindowText;
            }
        }

        private void textBoxvariablesofinterest_Enter(object sender, EventArgs e)
        {
            textBoxvariablesofinterest_Click(sender, e);
        }

        private void buttonnextstep_Click(object sender, EventArgs e)
        {
            if (!dyn.finished)
            {
                richTextBoxhistory.Text += "\n" + dyn.steps + ") " + dyn.NextStep(nthelement);
                if (dyn.finished)
                    buttonnextstep.Enabled = buttonskiptoend.Enabled = false;
                PrintGrid();
            }
        }


        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            richTextBoxhistory.SelectionStart = richTextBoxhistory.Text.Length;
            richTextBoxhistory.ScrollToCaret();
        }

        private void buttonskiptoend_Click(object sender, EventArgs e)
        {
            while (!dyn.finished)
                richTextBoxhistory.Text += "\n" + dyn.steps + ") " + dyn.NextStep(nthelement);
            buttonnextstep.Enabled = buttonskiptoend.Enabled = false;
            PrintGrid();
        }

        private void textBoxtestcase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && buttonstartedynamicSlicing.Enabled)
                buttonstartedynamicSlicing_Click(sender, e);
        }


        private void dataGridViewtable_Scroll(object sender, ScrollEventArgs e)
        {
            int old = e.OldValue;
            int news = e.NewValue;

            // Pfeile umpositionieren bzw abschneiden, wenn Platz nicht reicht
            //if (DependenciesVorhanden(dyn.etZeilen))
            //    DrawArrows();
        }

        private void pickSourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttoncode_Click(sender, e);
        }

        private void showCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttoncodezeigen_Click(sender, e);
        }

        private void howToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormHowTo().Show();
        }

    }
}
