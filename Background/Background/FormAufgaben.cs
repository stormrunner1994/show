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
    public partial class FormAufgaben : Form
    {

        public FormAufgaben(string ändern)
        {
            InitializeComponent();
            aufgabe = ändern;
        }

        public FormAufgaben()
        {
            InitializeComponent();
            aufgabe = "";
        }

        private string aufgabe;

        public string MGetAufgabe()
        {
            return aufgabe;
        }

        private void FormAufgaben_Load(object sender, EventArgs e)
        {
            // Aufgabe ändern?
            if (aufgabe != "")
            {
                textBoxaufgabe.Text = aufgabe;
                buttonanpassen.Text = "Ändern";
            }
        }

        private void buttonanpassen_Click(object sender, EventArgs e)
        {
            aufgabe = textBoxaufgabe.Text;
            this.Close();
        }

        private void textBoxaufgabe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                buttonanpassen_Click(sender, e);
        }
    }
}
