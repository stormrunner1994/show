using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Background
{
    public partial class FormWiederholung : Form
    {
        public FormWiederholung()
        {
            InitializeComponent();
        }

        private int modus = 0;

        public int MGetModus()
        {
            return modus;
        }

        private void buttonja_Click(object sender, EventArgs e)
        {
            modus = 2;
            this.Close();
        }

        private void buttonnein_Click(object sender, EventArgs e)
        {
            modus = 1;
            this.Close();
        }
    }
}
