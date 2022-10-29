using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Background
{
    public partial class FormShortcuts : Form
    {
        public FormShortcuts(Dictionary<int, string> dictmusik, Dictionary<string, string> dictspeicherpfade)
        {
            this.dictmusik = dictmusik;
            listkeys = new List<string>();

            foreach (string key in dictmusik[0].Split(';'))
                listkeys.Add(key);

            InitializeComponent();
            this.dictspeicherpfade = dictspeicherpfade;

        }

    private Dictionary<string, string> dictspeicherpfade;
    private Dictionary<int, string> dictmusik;
      private List<string> listkeys;

        private void buttonspeichern_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != textBox2.Text && textBox1.Text != textBox3.Text && textBox2.Text != textBox3.Text)
            {
                // Datei abwandeln

                StreamWriter sw = new StreamWriter(dictspeicherpfade["Musik"]);
                sw.WriteLine(MGetKeys());

                for (int a = 1; a < dictmusik.Count; a++)
                {
                    sw.WriteLine(dictmusik[a]);
                }
                sw.Close();
                this.Close();
            }
            else
                MessageBox.Show("Eine Taste wird mehrfach verwendet!");
        }

        private void buttonabbrechen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormShortcuts_Load(object sender, EventArgs e)
        {
            textBox1.Text = listkeys[0];
            textBox2.Text = listkeys[1];
            textBox3.Text = listkeys[2];
            timerkeys.Start();
        }

        public string MGetKeys()
        {
            string strkeys = "";

            for (int a = 0; a < listkeys.Count; a++)
            {
                if (a > 0)
                    strkeys += ";";

                strkeys += listkeys[a];

            }
            return strkeys;
        }

        private void timerkeys_Tick(object sender, EventArgs e)
        {
            string key = ClassÜbergreifend.MTasteGedrückt();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            textBox1.Text = e.KeyData.ToString();
            listkeys[0] = textBox1.Text;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            textBox2.Text = e.KeyData.ToString();
            listkeys[1] = textBox2.Text;
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            textBox3.Text = e.KeyData.ToString();
            listkeys[2] = textBox3.Text;
        }


    }
}
