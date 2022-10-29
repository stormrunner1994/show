using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace Background
{


    public partial class FormTimer : Form
    {
        public FormTimer()
        {
            InitializeComponent();
        }

        public FormTimer(List<string> voreinstellung, Dictionary<string, string> dictspeicherpfade)
        {
            InitializeComponent();
            this.dictspeicherpfade = dictspeicherpfade;
            this.voreinstellung = voreinstellung;
            this.window = new Windows(dictspeicherpfade);
        }

        private List<string> voreinstellung = new List<string>();
        private Dictionary<string, string> dictspeicherpfade;

        private int ianzahltöne = 0;
        private Button bt = new Button();
        private TextBox tb = new TextBox();
        private ProgressBar p = new ProgressBar();
        private int igb = 1;
        private int itab = 0;
        private Point ptgb = new Point(9, 9);
        private Dictionary<int, GroupBox> dictgb = new Dictionary<int, GroupBox>();
        // Enddatum
        private Dictionary<int, DateTime> dictende = new Dictionary<int, DateTime>();
        private Dictionary<int, int> dictlöschen = new Dictionary<int, int>();
        private bool hintergrundbild = false;
        private Windows window;

        private void FormTimer_Load(object sender, EventArgs e)
        {
            comboBoxdefault.SelectedIndex = 1;
            comboBoxanzahlton.SelectedIndex = 3;
            timerglobal.Start();

            if (voreinstellung.Count > 0)
            {
                int index = 1;
                // Voreinstellungen
                foreach (string zeile in voreinstellung)
                {
                    MNeueGroupbox();
                    // Bezeichnung;Zeit
                    string[] split = zeile.Split(';');
                    dictgb[index].Controls.OfType<TextBox>().ToList<TextBox>()[0].Text = split[0];
                    dictgb[index].Controls.OfType<TextBox>().ToList<TextBox>()[1].Text = split[1];
                    buttonstart_Click(dictgb[index].Controls.OfType<Button>().First(), e);
                    checkBoxvordergrundmeldung.Checked = false;
                    index++;
                }
            }
            else
                MNeueGroupbox();
            
        }

        private void MNeueGroupbox()
        {
            if (dictgb.Count > 0)
            {
                igb++;
                ptgb.Y += dictgb.First().Value.Height;
                label1.Location = new Point(label1.Location.X, label1.Location.Y + dictgb.First().Value.Height);
                label2.Location = new Point(label2.Location.X, label2.Location.Y + dictgb.First().Value.Height);
                comboBoxdefault.Location = new Point(comboBoxdefault.Location.X, comboBoxdefault.Location.Y + dictgb.First().Value.Height);
                comboBoxanzahlton.Location = new Point(comboBoxanzahlton.Location.X, comboBoxanzahlton.Location.Y + dictgb.First().Value.Height);
                buttonadd.Location = new Point(buttonadd.Location.X, buttonadd.Location.Y + dictgb.First().Value.Height);
                checkBoxvordergrundmeldung.Location = new Point(checkBoxvordergrundmeldung.Location.X, checkBoxvordergrundmeldung.Location.Y + dictgb.First().Value.Height);
            }

            // Erzeugen der Objekte
            TextBox tbbeschreibung = new TextBox();
            Label lbe = new Label();
            ProgressBar pb = new ProgressBar();
            Button btstart = new Button();
            TextBox tbzeit = new TextBox();
            Button btadd = new Button();
            Label lzeit = new Label();
            GroupBox gb = new GroupBox();

            // Labelbeschreibung
            lbe.AutoSize = true;
            lbe.Location = new System.Drawing.Point(6, 16);
            lbe.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lbe.Name = "lbe" + igb;
            lbe.Size = new System.Drawing.Size(78, 13);
            lbe.TabIndex = itab++;
            lbe.Text = "Beschreibung: ";

            // textBoxbeschreibung
            tbbeschreibung.Location = new System.Drawing.Point(9, 32);
            tbbeschreibung.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbbeschreibung.Name = "textBoxbeschreibung" + igb;
            tbbeschreibung.Size = new System.Drawing.Size(132, 20);
            tbbeschreibung.TabIndex = itab++;

            // lzeit
            lzeit.AutoSize = true;
            lzeit.Location = new System.Drawing.Point(144, 16);
            lzeit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            lzeit.Name = "lzeit" + igb;
            lzeit.Size = new System.Drawing.Size(31, 13);
            lzeit.TabIndex = itab++;
            lzeit.Text = "Zeit: ";

            // textBoxzeit
            tbzeit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            tbzeit.Location = new System.Drawing.Point(147, 32);
            tbzeit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            tbzeit.Name = "textBoxzeit" + igb;
            tbzeit.Size = new System.Drawing.Size(95, 20);
            tbzeit.TabIndex = itab++;

            // buttonstart 
            btstart.Image = Background.Properties.Resources.control_start;
            btstart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btstart.Click += new System.EventHandler(this.buttonstart_Click);
            btstart.Location = new System.Drawing.Point(248, 30);
            btstart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btstart.Name = "buttonstart" + igb;
            btstart.Size = new System.Drawing.Size(29, 23);
            btstart.TabIndex = itab++;
            btstart.UseVisualStyleBackColor = true;


            // progressBar
            pb.Location = new System.Drawing.Point(283, 30);
            pb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            pb.Name = "progressBar" + igb;
            pb.Value = 0;
            pb.Step = 1;
            pb.Size = new System.Drawing.Size(92, 23);
            pb.TabIndex = itab++;

            // buttonadd
            btadd.BackgroundImage = new Bitmap(Background.Properties.Resources.add);
            btadd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            btadd.Location = new System.Drawing.Point(13, 130);
            btadd.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            btadd.Name = "buttonadd" + igb;
            btadd.Size = new System.Drawing.Size(39, 35);
            btadd.TabIndex = itab++;
            btadd.UseVisualStyleBackColor = true;

            // groupBoxtimer
            gb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.groupBox_MouseDown);
            gb.AutoSize = true;
            gb.FlatStyle = FlatStyle.Standard;
            gb.Location = ptgb;
            gb.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gb.Name = "groupBoxtimer" + igb;
            gb.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            gb.Size = new System.Drawing.Size(343, 69);
            gb.TabStop = false;
            gb.Text = "Timer " + igb;


            gb.Controls.Add(lbe);
            gb.Controls.Add(tbbeschreibung);
            gb.Controls.Add(tbzeit);
            gb.Controls.Add(lzeit);
            gb.Controls.Add(btstart);
            gb.Controls.Add(pb);

            Controls.Add(gb);

            dictgb.Add(igb, gb);
        }


        private void buttonadd_Click(object sender, EventArgs e)
        {
            MNeueGroupbox();
        }

        private void buttonstart_Click(object sender, EventArgs e)
        {
            int gb = MGetIndexFromName((sender as Button).Name);
            hintergrundbild = false;
            MVorgang(gb);
        }

        private void MVorgang(int gb)
        {
            // Start
            if (!dictende.ContainsKey(gb))
            {
                dictgb[gb].BackColor = this.BackColor;
                if (dictgb[gb].Controls.OfType<TextBox>().ElementAt(1).Text != "")
                {
                    DateTime now = DateTime.Now;
                    DateTime dvalue = MGetEnde(now, gb);
                    int diff = Convert.ToInt32((dvalue - now).TotalSeconds);
                    if (diff != 0)
                    {
                        dictende.Add(gb, dvalue);
                        dictgb[gb].Controls.OfType<ProgressBar>().ToList().First().Maximum = diff;
                        dictgb[gb].Controls.OfType<ProgressBar>().ToList().First().Value = 0;

                        bt = dictgb[gb].Controls.OfType<Button>().First();
                        bt.Image = Background.Properties.Resources.control_stop;
                        dictgb[gb].Controls.OfType<TextBox>().ElementAt(1).Enabled = false;
                    }
                    else
                        MessageBox.Show("Bitte achten Sie auf eine korrekte Eingabe!");
                }
            }
            // Stop
            else
            {
                dictende.Remove(gb);
                bt = dictgb[gb].Controls.OfType<Button>().First();
                bt.Image = Background.Properties.Resources.control_start;
                dictgb[gb].Controls.OfType<TextBox>().ElementAt(1).Enabled = true;
            }
        }

        private int MIndexZeitTextBox(List<TextBox> list)
        {
            int ausgabe = -1;

            int index = 0;
            foreach (TextBox t in list)
            {
                if (t.Name.Contains("zeit"))
                {
                    ausgabe = index;
                    break;
                }
                index++;
            }

            return ausgabe;
        }

        private DateTime MGetEnde(DateTime start, int gb)
        {
            DateTime ausgabe = start;
            int iout;

            List<TextBox> list = dictgb[gb].Controls.OfType<TextBox>().ToList(); // ToLower();

            int index = MIndexZeitTextBox(list);
            string text = list[index].Text;

            try
            {

                if (text == "")
                    return ausgabe;


                while (text[0] == ' ')
                    text = text.Remove(0, 1);

                string zahl;
                string wert;
                Dictionary<string, int> dicteingabe = new Dictionary<string, int>();

                while (text.Length > 0)
                {
                    wert = zahl = "";
                    while (Int32.TryParse(text[0].ToString(), out iout))
                    {
                        zahl += text[0];
                        text = text.Remove(0, 1);
                    }

                    while (!Int32.TryParse(text[0].ToString(), out iout))
                    {
                        if (text[0] != ' ')
                            wert += text[0];
                        text = text.Remove(0, 1);

                        if (text.Length == 0)
                            break;
                    }
                    if (!dicteingabe.ContainsKey(wert))
                        dicteingabe.Add(wert, Convert.ToInt32(zahl));
                    else
                        dicteingabe[wert] += Convert.ToInt32(zahl);
                }

                ausgabe = MZeithinzurechnen(start, dicteingabe);
            }
            catch
            {
                if (Int32.TryParse(list[index].Text, out iout))
                {
                    Dictionary<string, int> dict = new Dictionary<string, int>();
                    dict.Add(comboBoxdefault.Text, Convert.ToInt32(list[index].Text));
                    ausgabe = MZeithinzurechnen(start, dict);
                }
                else
                    ausgabe = start;
            }

            return ausgabe;
        }

        private DateTime MZeithinzurechnen(DateTime start, Dictionary<string, int> dict)
        {
            DateTime ausgabe = start;

            // h
            if (dict.ContainsKey("h"))
                ausgabe = ausgabe.AddHours(dict["h"]);
            // min
            if (dict.ContainsKey("min"))
                ausgabe = ausgabe.AddMinutes(dict["min"]);
            // sek
            if (dict.ContainsKey("sek"))
                ausgabe = ausgabe.AddSeconds(dict["sek"]);
            // oder sec
            else if (dict.ContainsKey("sec"))
                ausgabe = ausgabe.AddSeconds(dict["sec"]);

            return ausgabe;
        }

        private int MGetIndexFromName(string name)
        {
            string strindex = "";
            int iout;

            for (int a = 0; a < name.Length; a++)
            {
                if (Int32.TryParse(name[a].ToString(), out iout))
                    strindex += name[a].ToString();
            }

            if (strindex == "")
                strindex = "0";

            return Convert.ToInt32(strindex);
        }

        private void timerglobal_Tick(object sender, EventArgs e)
        {
            if (window != null)
            {
                if (!hintergrundbild)
                {
                    window.HintergrundbildZurücksetzen();
                    hintergrundbild = false;
                    window.work = false;
                }
            }

            dictlöschen = new Dictionary<int, int>();
            DateTime now = DateTime.Now;
            foreach (KeyValuePair<int, DateTime> pair in dictende)
            {
                if (now <= pair.Value)
                {
                    p = dictgb[pair.Key].Controls.OfType<ProgressBar>().First();
                    if (p.Value+1 <= p.Maximum)
                     p.Value++;
                    tb = dictgb[pair.Key].Controls.OfType<TextBox>().ElementAt(1);
                    tb.Text = MRestzeit(now, pair.Value);
                }
                else
                    dictlöschen.Add(dictlöschen.Count, pair.Key);
            }

            if (dictlöschen.Count > 0)
            {
                // Durchgelaufene Timer
                foreach (KeyValuePair<int, int> pair in dictlöschen)
                {

                    dictgb[pair.Value].Controls.OfType<TextBox>().ElementAt(1).Text = "";
                    bt = dictgb[pair.Value].Controls.OfType<Button>().First();
                    bt.Image = Background.Properties.Resources.control_start;
                    dictende.Remove(pair.Value);
                    dictgb[pair.Value].BackColor = Color.Red;
                    dictgb[pair.Value].Controls.OfType<TextBox>().ElementAt(1).Enabled = true;
                }
                
                ianzahltöne = Convert.ToInt32(comboBoxanzahlton.Text);
                Thread thread = new Thread(new ThreadStart(MAlarm));
                thread.IsBackground = true;
                thread.Start();

                if (checkBoxvordergrundmeldung.Checked)
                {
                    ClassÜbergreifend.SetForegroundWindow(this.Handle);
                }
            }
        }
        
        private void MAlarm()
        {
            for (int a = 0; a < ianzahltöne; a++)
                Console.Beep(750, 500);
                
            hintergrundbild = true;
             window.TerminbildAlsHintergrund();
        }

        private string MRestzeit(DateTime now, DateTime ende)
        {
            string ausgabe = "";
            int sek = Convert.ToInt32((ende - now).TotalSeconds);
            int h = sek / 3600;
            sek -= h * 3600;
            int min = sek / 60;
            sek -= min * 60;

            string strh = h.ToString();
            string strmin = min.ToString();
            string strsek = sek.ToString();

            if (h < 10)
                strh = "0" + strh;
            if (min < 10)
                strmin = "0" + strmin;
            if (sek < 10)
                strsek = "0" + strsek;

            ausgabe = strsek + "sek";

            if (min != 0 || h != 0)
                ausgabe = strmin + "min " + ausgabe;
            if (h != 0)
                ausgabe = strh + "h " + ausgabe;

            return ausgabe;
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            hintergrundbild = false;
            if (e.KeyData == Keys.Enter)
            {
                int gb = MGetIndexFromName((sender as TextBox).Name);
                MVorgang(gb);

                e.SuppressKeyPress = true;
            }
        }

        private void groupBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && dictgb.Count>1)
            {
                igblöschen = Convert.ToInt32(((GroupBox)sender).Text.Remove(0, 5));
                contextMenuStrip1.Show(Cursor.Position);
            }
            else if (e.Button == MouseButtons.Left)
            {
                hintergrundbild = false;
            }
        }

        private int igblöschen;

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Pressed)
            {
                if (e.ClickedItem.Text == "Entfernen")
                {
                    Dictionary<int, GroupBox> temp = new Dictionary<int, GroupBox>();
                    Dictionary<int, DateTime> temp2 = new Dictionary<int, DateTime>();
                    int a = 1;
                    foreach (KeyValuePair<int, GroupBox> pair in dictgb)
                    {
                        if (pair.Key != igblöschen)
                        {
                            GroupBox gb = pair.Value;
                            gb.Text = "Timer " + a;

                            if (a >= igblöschen)
                                gb.Location = new Point(gb.Location.X, gb.Location.Y - dictgb.First().Value.Height);
                            temp.Add(a, gb);

                            if (dictende.ContainsKey(pair.Key))
                                temp2.Add(a, dictende[pair.Key]);
                            a++;
                        }
                        else
                        {
                            this.Controls.Remove(this.Controls.OfType<GroupBox>().ElementAt(pair.Key - 1));
                        }

                    }

                    dictgb = temp;
                    dictende = temp2;


                    // Schritt zurück
                    igb--;
                    ptgb.Y -= dictgb.First().Value.Height;
                    label1.Location = new Point(label1.Location.X, label1.Location.Y - dictgb.First().Value.Height);
                    label2.Location = new Point(label2.Location.X, label2.Location.Y - dictgb.First().Value.Height);
                    comboBoxdefault.Location = new Point(comboBoxdefault.Location.X, comboBoxdefault.Location.Y - dictgb.First().Value.Height);
                    comboBoxanzahlton.Location = new Point(comboBoxanzahlton.Location.X, comboBoxanzahlton.Location.Y - dictgb.First().Value.Height);
                    buttonadd.Location = new Point(buttonadd.Location.X, buttonadd.Location.Y - dictgb.First().Value.Height);
                    checkBoxvordergrundmeldung.Location = new Point(checkBoxvordergrundmeldung.Location.X, checkBoxvordergrundmeldung.Location.Y - dictgb.First().Value.Height);
                    this.Height -= dictgb.First().Value.Height;
                }
            }
        }
    }
}
