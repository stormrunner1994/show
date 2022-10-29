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
    public partial class Ergebnis : Form
    {
        public Ergebnis()
        {
            InitializeComponent();
            pictureBox1.Image = Properties.Resources.ergebnis;
        }

    }
}
