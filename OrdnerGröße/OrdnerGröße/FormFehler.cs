using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdnerGröße
{
    public partial class FormFehler : Form
    {
        public FormFehler(string fehler)
        {
            InitializeComponent();
            this.fehler = fehler;
        }

        private string fehler;

        private void FormFehler_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = fehler;
        }
    }
}
