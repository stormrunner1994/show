using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Background
{
    public partial class FormAktuelleDateien : Form
    {
        public FormAktuelleDateien(string pfadneu, string pfadalt)
        {
            InitializeComponent();
            behalten = false;
            this.pfadalt = pfadalt;
            this.pfadneu = pfadneu;
        }

        private bool behalten;
        private string pfadneu;
        private string pfadalt;

        private void FormAktuelleDateien_Load(object sender, EventArgs e)
        {
            textBox1.Text = pfadneu;
        }

        public bool MGetBehalten()
        {
            return behalten;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            behalten = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(pfadneu);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start(pfadalt);
        }
    }
}
