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
    public partial class FormEnterOrdnerpfad : Form
    {
        public FormEnterOrdnerpfad()
        {
            InitializeComponent();
        }

        public string pfad = "";
        public bool abbrechen = false;
        public bool unterordner = true;

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pfad = textBox1.Text;
            unterordner = checkBox1.Checked;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            abbrechen = true;
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = Directory.Exists(textBox1.Text);
        }

        private void FormEnterOrdnerpfad_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }
    }
}
