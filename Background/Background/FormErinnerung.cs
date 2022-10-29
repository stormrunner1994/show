using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Background
{
    public partial class FormErinnerung : Form
    {
        public FormErinnerung(string text)
        {
            InitializeComponent();
            erinnerung = text;
            fensteroffen = true;
        }

        private string erinnerung;
        private bool fensteroffen;

        private void FormErinnerung_Load(object sender, EventArgs e)
        {
            label1.Text = erinnerung;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fensteroffen = false;
            this.Close();
        }

        private void FormErinnerung_FormClosed(object sender, FormClosedEventArgs e)
        {
            fensteroffen = false;
        }

        public bool IstFensterOffen()
        {
            return fensteroffen;
        }
    }
}
