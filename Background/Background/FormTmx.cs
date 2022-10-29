using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Background
{
    public partial class FormTmx : Form
    {
        public FormTmx()
        {
            InitializeComponent();
        }

        public FormTmx(bool programmstart)
        {
            InitializeComponent();
            this.Size = new Size(20, 20);
            this.bprogrammstart = programmstart;
        }



        private List<string> links;
        private int index;
        private Stopwatch sw;
        private bool bchecken = false;
        private bool bprogrammstart = false;
        private bool bnurerster = true;

        public bool GetNurErster()
        {
            return bnurerster;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sw = new Stopwatch();
            sw.Start();
            comboBox1.Items.Clear();

            for (int a = 1; a < links.Count + 1; a++)
                comboBox1.Items.Add(a);

            index = 0;
            richTextBoxauswertung.Text = "";
            bchecken = true;
            LinkÖffnen();
        }

        private void LinkÖffnen()
        {
            webBrowser1.Navigate(links[index]);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (bchecken)
            {
                string html = webBrowser1.DocumentText;
                if (BinNochErster(html))
                {
                    if (richTextBoxauswertung.Text != "")
                        richTextBoxauswertung.Text += "\nErster";
                    else
                        richTextBoxauswertung.Text += "Erster";
                }
                else
                {
                    if (richTextBoxauswertung.Text != "")
                        richTextBoxauswertung.Text += "\nNicht Erster";
                    else
                        richTextBoxauswertung.Text += "Nicht Erster";
                }

                if (index < links.Count - 1)
                {
                    index++;
                    LinkÖffnen();
                }
                else
                {
                    sw.Stop();
                    bchecken = false;
                    label1.Text = "Fertig nach " + sw.ElapsedMilliseconds + " ms";

                    if (richTextBoxauswertung.Text.Contains("Nicht"))
                        bnurerster = false;
                    
                }
            }
        }

        private bool BinNochErster(string html)
        {
            int index1 = GetPosition(0, html, "STORM|Runner");

            if (index1 != -1)
            {
                int index2 = GetPosition(index1, 400, html, "Top 10");
                if (index2 != -1)
                    return true;
            }

            return false;
        }

        private int GetPosition(int startindex, string html, string wort)
        {
            int index = startindex;

            while (wort.Length + index < html.Length)
            {
                bool gefunden = true; ;
                for (int a = 0; a < wort.Length; a++)
                {
                    if (html[index + a] != wort[a])
                    {
                        gefunden = false;
                        break;
                    }
                }

                if (gefunden)
                    return index;

                index++;
            }

            return -1;
        }

        private int GetPosition(int startindex, int suchlänge, string html, string wort)
        {
            int index = startindex;

            while (wort.Length + index < startindex + suchlänge + 2)
            {
                bool gefunden = true; ;
                for (int a = 0; a < wort.Length; a++)
                {
                    if (html[index + a] != wort[a])
                    {
                        gefunden = false;
                        break;
                    }
                }

                if (gefunden)
                    return index;

                index++;
            }

            return -1;
        }

        private void FormTmx_Load(object sender, EventArgs e)
        {
            links = new List<string>();
            /*links.Add("https://united.tm-exchange.com/main.aspx?action=trackshow&id=5096195#auto");
            links.Add("https://united.tm-exchange.com/main.aspx?action=trackshow&id=5086400#auto");
            links.Add("https://united.tm-exchange.com/main.aspx?action=trackshow&id=5058738#auto");
            links.Add("https://united.tm-exchange.com/main.aspx?action=trackshow&id=5100834#auto");
            links.Add("https://united.tm-exchange.com/main.aspx?action=trackshow&id=5089344#auto");*/
            links.Add("https://tmnforever.tm-exchange.com/main.aspx?action=trackshow&id=6790427#auto");

            if (links.Count == 0)
                return;

            richTextBoxlinks.Text = links.First();

            for (int a = 1; a < links.Count; a++)
                richTextBoxlinks.Text += "\n" + links[a];

           button1_Click(sender, e);

            if (bprogrammstart)
            {
                timer1.Interval = richTextBoxlinks.Text.Split('\n').Length * 1000;
                timer1.Start();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != "")
            {
                webBrowser1.Navigate(links.ElementAt(Convert.ToInt32(comboBox1.SelectedItem.ToString()) -1));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
                this.Close();
        }
    }
}
