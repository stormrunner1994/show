using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Top100Germany
{
    public partial class FormName : Form
    {
        public FormName(string pfad)
        {
            InitializeComponent();
            this.pfad = pfad;
        }

        private string pfad;
        public bool offen = false;
        private List<string> accounts;

        private void FormName_Load(object sender, EventArgs e)
        {
            textBoxvorher.Text = textBoxnachher.Text = textBox1.Text = "";
            listBox1.Items.Clear();
            this.offen = true;
            accounts = new List<string>();
            string[] files = Directory.GetFiles(pfad);

            // Adde alle Accounts aus allen files
            foreach (string file in files)
            {
                StreamReader sr = new StreamReader(file);
                while (!sr.EndOfStream)
                {
                    string zeile = sr.ReadLine();
                    if (zeile == "") continue;

                    string acc = zeile.Split(';')[1];

                    if (!accounts.Contains(acc))
                        accounts.Add(acc);
                }
                sr.Close();
            }

            foreach (string a in accounts)
                listBox1.Items.Add(a);

            labelanzahlaccounts.Text = listBox1.Items.Count.ToString();
        }

        private void FormName_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.offen = false;
        }

   
        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            foreach (string acc in accounts)
            {
                if (acc.ToLower().Contains(textBox1.Text.ToLower()))
                    listBox1.Items.Add(acc);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                buttonvorher.Enabled = buttonnachher.Enabled = false;
            else
                buttonvorher.Enabled = buttonnachher.Enabled = true;
        }

        private void buttonvorher_Click(object sender, EventArgs e)
        {
            textBoxvorher.Text = listBox1.SelectedItem.ToString();
            listBox1.SelectedIndex = -1;
        }

        private void buttonnachher_Click(object sender, EventArgs e)
        {
            textBoxnachher.Text = listBox1.SelectedItem.ToString();
            listBox1.SelectedIndex = -1;
        }

        private void textBoxvorher_TextChanged(object sender, EventArgs e)
        {
            if (textBoxvorher.Text.Trim() == "" || textBoxnachher.Text.Trim() == "")
                button2.Enabled = false;
            else
                button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(pfad);
            foreach (string file in files)
            {
                string text = "";
                StreamReader sr = new StreamReader(file);
                while (!sr.EndOfStream)
                {
                    string zeile = sr.ReadLine();
                    string[] splits = zeile.Split(';');

                    if (splits.Length != 3) continue;

                    if (splits[1].ToString() == textBoxvorher.Text)
                    {
                        if (text != "") text += "\n" + splits[0] + ";" + textBoxnachher.Text + ";" + splits[2];
                        else text += splits[0] + ";" + textBoxnachher.Text + ";" + splits[2];
                    }
                    else
                    {
                        if (text != "") text += "\n" + zeile;
                        else text += zeile;
                    }
                }
                sr.Close();

                StreamWriter sw = new StreamWriter(file);
                sw.WriteLine(text);
                sw.Close();
            }

            FormName_Load(sender, e);
        }

        private void textBoxnachher_TextChanged(object sender, EventArgs e)
        {
            if (textBoxvorher.Text.Trim() == "" || textBoxnachher.Text.Trim() == "")
                button2.Enabled = false;
            else
                button2.Enabled = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && button1.Enabled)
                button1_Click(sender, e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }
    }
}
