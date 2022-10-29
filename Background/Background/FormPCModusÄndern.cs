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
    public partial class FormPCModusÄndern : Form
    {
        public FormPCModusÄndern()
        {
            InitializeComponent();
        }

        public bool FormWirdangezeigt = false;

    private void FormPCModusÄndern_Load(object sender, EventArgs e)
        {
            textBox1.Text = "50";
            comboBox1.SelectedIndex = 1;
            comboBoxmodi.SelectedIndex = 2;
            buttonstarten.Focus();
            progressBar1.Visible = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            double iout;
            if (Double.TryParse(textBox1.Text,out iout))
            {
                buttonstarten.Enabled = true;
            }
            else
                buttonstarten.Enabled = false;
        }

        private void buttonstarten_Click(object sender, EventArgs e)
        {
            if (buttonstarten.Text == "Starten")
            {
                textBox1.Enabled = false;
                double max = 0;
                if (comboBox1.SelectedIndex == 0)
                    max = Convert.ToDouble(textBox1.Text);
                else if (comboBox1.SelectedIndex == 1)
                    max = Convert.ToDouble(textBox1.Text) * 60;
                else if (comboBox1.SelectedIndex == 2)
                    max = Convert.ToDouble(textBox1.Text) * 60 * 60;

                if (max.ToString().Contains('.'))
                    Math.Round(max, MidpointRounding.ToEven);

                int iout;
                if (Int32.TryParse(max.ToString(), out iout))
                {
                    progressBar1.Visible = true;
                    buttonstarten.Text = "Abbrechen";
                    progressBar1.Maximum = Convert.ToInt32(max);
                    progressBar1.Value = 0;
                    timer1.Start();
                }
                else
                    MessageBox.Show("Der Wert ist zu groß!");
            }
            else if (buttonstarten.Text == "Abbrechen")
            {
                textBox1.Enabled = true;
                buttonstarten.Text = "Starten";
                timer1.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value++;
                if (comboBox1.SelectedIndex == 0)
                    textBox1.Text = (progressBar1.Maximum - progressBar1.Value).ToString();
                else if (comboBox1.SelectedIndex == 1)
                    textBox1.Text = (Math.Round(Convert.ToDouble(progressBar1.Maximum - progressBar1.Value)/60,2)).ToString();
                else if (comboBox1.SelectedIndex == 2)
                    textBox1.Text = (Math.Round(Convert.ToDouble(progressBar1.Maximum - progressBar1.Value) /60/ 60, 2)).ToString();
            }
            else
            {
                if (comboBoxmodi.SelectedIndex == 0)
                    Process.Start("shutdown", "/l /t 5 -f");
                else if (comboBoxmodi.SelectedIndex == 1)
                    Process.Start("shutdown", "/r /t 5 -f");
                else if (comboBoxmodi.SelectedIndex == 2)
                    Process.Start("shutdown", "/s /t 5 -f");
            }
        }

        private void FormPCModusÄndern_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormWirdangezeigt = false;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buttonstarten_Click(sender, e);
            }
        }
    }
}
