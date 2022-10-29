using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DynamicSlicing
{
    public partial class CodeZeigen : Form
    {
        public CodeZeigen(string code)
        {
            InitializeComponent();
            this.code = code;
        }

        private string code;

        private void CodeZeigen_Load(object sender, EventArgs e)
        {
            string[] zeilen = code.Split('\n');
            richTextBox1.Text = "";
            for (int a = 0; a < zeilen.Length; a++)
            {
                if (a == 0) richTextBox1.Text += (a) + ")\t" + zeilen[a];
                else richTextBox1.Text +="\n" +  (a) + ")\t" + zeilen[a];
            }
        }
    }
}
