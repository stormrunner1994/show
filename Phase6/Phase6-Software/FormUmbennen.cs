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
    public partial class FormUmbennen : Form
    {
        public FormUmbennen(string profilname,List<string> listvorhandeneprofile)
        {
            this.profilname = profilname;
            this.listvorhandeneprofile = listvorhandeneprofile;
            InitializeComponent();
        }

        private string profilname;
        private List<string> listvorhandeneprofile;

        public string MGetProfilname()
        {
            return profilname;
        }

        private void FormUmbennen_Load(object sender, EventArgs e)
        {
            textBox1.Text = profilname;
            int iBildschirmhöhe = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            int iBildschirmbreite = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            this.Location = new Point((iBildschirmbreite / 2) - this.Width / 2, (iBildschirmhöhe / 2) - this.Height / 2);
        }

        private void buttonumbennen_Click(object sender, EventArgs e)
        {
            textBox1.Text = Program.MLeerstellenEntfernen(textBox1.Text);
            // ist Eingabe gültig?
            if (textBox1.Text != "")
            {
                if (textBox1.Text.Contains("\n") || textBox1.Text.Contains("\r"))
                {
                    MessageBox.Show("Ihre Eingabe darf keinen Zeilenumbruch enthalten!");
                    return;
                }
                else if (textBox1.Text.Contains(";"))
                {
                    MessageBox.Show("Ihre Eingabe darf kein Semikolon enthalten!");
                    return;
                }
                else if (textBox1.Text.Contains("."))
                {
                    MessageBox.Show("Ihre Eingabe darf keinen Punkt enthalten!");
                    return;
                }

                // ist Profil neu?
                if (!MContains(listvorhandeneprofile, textBox1.Text))
                    profilname = textBox1.Text;
                else
                {
                    MessageBox.Show("Das Profil ist bereits vorhanden!");
                    return;
                }

                this.Close();
            }       
        }

        private bool MContains(List<string> list,string test)
        {
            for (int a  = 0; a < list.Count; a++)
            {
                if (list[a].ToString().ToLower() == test.ToLower())
                    return true;
            }
            
            return false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buttonumbennen_Click(sender, e);
            }
        }
    }
}
