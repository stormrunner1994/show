using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TmLadder;

namespace Top100Germany
{
    public partial class Form2 : Form
    {
        public Form2(List<Spieler> nichtzugewiesene)
        {
            InitializeComponent();
            this.nichtzugewiesene = nichtzugewiesene;
        }

        private List<Spieler> nichtzugewiesene;

        private void Form2_Load(object sender, EventArgs e)
        {
            foreach(Spieler s in nichtzugewiesene)
            {
                if (richTextBox1.Text != "") richTextBox1.Text += "\n" + s.getZeile();
                else richTextBox1.Text += s.getZeile();
            }
        }
    }
}
