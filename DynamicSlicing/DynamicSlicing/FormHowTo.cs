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
    public partial class FormHowTo : Form
    {
        public FormHowTo()
        {
            InitializeComponent();
        }

        private void FormHowTo_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "First read the file 'HowTo.pdf'\n\n" +
                "- Click on Button 'pick source code'\n" +
"- Type or select code\n" +
"- Formate code that checks for errors\n" +
"- Choose code to close window\n" +
"- If needed set values for input variables\n" +
"- Start dynamic slicing \n" +
"- Use intermediate steps optionally"; 
        }
    }
}
