using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Phase6_Software
{
    public partial class FormBearbeiten : Form
    {
        public FormBearbeiten(string kategorie, string frage, string antwort)
        {
            this.kategorie = kategorie;
            this.frage = frage;
            this.antwort = antwort;
            status = "ändern";
            InitializeComponent();
        }

       public string kategorie { get;set;}
       public string frage { get; set; }
       public string antwort { get; set; }
       public string status { get; set; } // ändern, zurücksetzen, löschen


        private void FormBearbeiten_Load(object sender, EventArgs e)
        {
            textBoxkategorie.Text = kategorie;
            richTextBoxfrage.Text = frage;
            richTextBoxantwort.Text =antwort;;
        }

        private void buttonlöschen_Click(object sender, EventArgs e)
        {
            status = "löschen";
            this.Close();
        }

        private void buttonändern_Click(object sender, EventArgs e)
        {
            if (checkBoxzurücksetzen.Checked)
            {
                status = "zurücksetzen";
            }

            this.Close();
        }

        private void richTextBoxfrage_TextChanged(object sender, EventArgs e)
        {
            MButtonStatus();
            frage = richTextBoxfrage.Text;
        }

        private void MButtonStatus()
        {
            if (textBoxkategorie.Text != "" && richTextBoxantwort.Text != "" && richTextBoxfrage.Text != "")
                buttonändern.Enabled = true;
            else
                buttonändern.Enabled = false;
        }

        private void textBoxkategorie_TextChanged(object sender, EventArgs e)
        {
            MButtonStatus();
            kategorie = textBoxkategorie.Text;
        }

        private void richTextBoxantwort_TextChanged(object sender, EventArgs e)
        {
            MButtonStatus();
            antwort = richTextBoxantwort.Text;
        }
    }
}
