using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Background
{
    public partial class MissionenAnpassen : Form
    {
        public MissionenAnpassen()
        {
            InitializeComponent();
        }

        private string file = "mission.csv";

        private void MissionenAnpassen_Load(object sender, EventArgs e)
        {
            if (File.Exists(file))
            {
                StreamReader sr = new StreamReader(file);
                richTextBox1.Text = sr.ReadToEnd();
                sr.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter sw = new StreamWriter(file);
            sw.WriteLine(richTextBox1.Text);
            sw.Close();
            this.Close();
        }
    }
}
