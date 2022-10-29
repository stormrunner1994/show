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

namespace OrdnerGröße
{
    public partial class FormIgnoriertePfade : Form
    {
        public FormIgnoriertePfade()
        {
            InitializeComponent();
        }

        private List<string> ignoriertePfade = new List<string>();

        public FormIgnoriertePfade(List<string> ignoriertePfade)
        {
            InitializeComponent();
            this.ignoriertePfade = ignoriertePfade;
        }

        public List<string> GetIgnoriertePfade()
        {
            return ignoriertePfade;
        }

        private void FormIgnoriertePfade_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            foreach(string ignoriert in ignoriertePfade)
            {
                if (richTextBox1.Text != "") richTextBox1.Text += "\n" + ignoriert;
                else richTextBox1.Text = ignoriert;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            string[] pfade = richTextBox1.Text.Split('\n');

            bool problem = false;
            foreach (string s in pfade)
                if (!Directory.Exists(s)) { problem = true; break; }

            if (richTextBox1.Text == "")
                problem = false;

            if (problem)
            {
                label1.Visible = true; button1.Enabled = false;
            }
            else
            {
                label1.Visible = false; button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {            
            ignoriertePfade.Clear();

            if (richTextBox1.Text != "")
            {
                string[] pfade = richTextBox1.Text.Split('\n');
                foreach (string s in pfade)
                    ignoriertePfade.Add(s);
            }
            this.Close();
        }
    }
}
