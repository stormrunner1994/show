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
    public partial class FormMissionen : Form
    {
        public FormMissionen()
        {
            InitializeComponent();
        }

        private Missionen missionen;
        private Dictionary<int,RichTextBox> dictRichTextBoxen;

        private void FormMissionen_Load(object sender, EventArgs e)
        {
            missionen = new Missionen();
            dictRichTextBoxen = new Dictionary<int, RichTextBox>();

            if (missionen.getMissionen().Count < 1)
                return;

            

            foreach (Mission m in missionen.getMissionen())
            {
            }
                
        }

        private void addeRichTextBox(Mission m, ref RichTextBox rt)
        {
            rt.Text = m.toString();
        }
        
        private void aktualisierenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMissionen_Load(sender, e);
        }

        private void missionenAnpassenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MissionenAnpassen ma = new MissionenAnpassen();
            ma.ShowDialog();
            FormMissionen_Load(sender, e);
        }
    }
}
