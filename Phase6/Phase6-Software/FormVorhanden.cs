using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Phase6_Software
{
    public partial class FormVorhanden : Form
    {
        public FormVorhanden()
        {
            InitializeComponent();
        }

        public FormVorhanden(string text) : this()
        {
            strtext = text;
            erstellen = false;
        }

        private bool erstellen;
        private string strtext;
        private int iBildschirmhöhe;
        private int iBildschirmbreite;

        private void buttonja_Click(object sender, EventArgs e)
        {
            erstellen = true;
            this.Close();
        }

        public bool GetErstellen()
        {
            return erstellen;
        }

        private void FormVorhanden_Load(object sender, EventArgs e)
        {
            if (strtext != null)
            {
                label1.Text = strtext;
                label2.Visible = false;
            }

            // immer in Bildschirmmitte laden
            iBildschirmhöhe = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            iBildschirmbreite = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.Location = new Point((iBildschirmbreite / 2) - this.Width / 2, (iBildschirmhöhe / 2) - this.Height / 2);

            buttonnein.Select();
        }

        private void buttonnein_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
