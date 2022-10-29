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
    public partial class FormMehrfach : Form
    {
        public FormMehrfach(Dictionary<string, List<ClassKarteikarte>> mehrfache, Color hintergrundfarbeaußen, Color hintergrundfarbeinnen)
        {
            InitializeComponent();
            this.mehrfache = mehrfache;
            this.BackColor = hintergrundfarbeaußen;
            panel1.BackColor = hintergrundfarbeinnen;
        }

        Dictionary<string, List<ClassKarteikarte>> mehrfache;
        Dictionary<int, List<CheckBox>> markierte;


        private void FormMehrfach_Load(object sender, EventArgs e)
        {
            markierte = new Dictionary<int, List<CheckBox>>();
            int y = 10;
            panel1.AutoScroll = true;

            // label: Frage;Antwort checkbox1: Phase x1 checkbox1: Phase x2 ...
            foreach (KeyValuePair<string, List<ClassKarteikarte>> pair in mehrfache)
            {
                int x = 10;
                Label l = new Label();
                l.Text = pair.Key;
                l.Location = new Point(x, y);
                x += l.Text.Length + 20;

                panel1.Controls.Add(l);
                markierte.Add(markierte.Count, new List<CheckBox>());

                foreach (ClassKarteikarte k in pair.Value)
                {
                    CheckBox c = new CheckBox();
                    c.Text = "Phase " + k.Phase.ToString();
                    c.Width = 80;
                    c.Enabled = true;
                    x += c.Width + 20;
                    c.Checked = true;
                    c.Location = new Point(x, y);
                    panel1.Controls.Add(c);
                    markierte[markierte.Count - 1].Add(c);
                }

                y += 30;
            }

        }

        public List<ClassKarteikarte> löschen { get; set; }

        private void FormMehrfach_FormClosing(object sender, FormClosingEventArgs e)
        {
            löschen = new List<ClassKarteikarte>();
            int reihe = 0;
            foreach (KeyValuePair<string, List<ClassKarteikarte>> pair in mehrfache)
            {
                for (int a = 0; a < pair.Value.Count; a++)
                {
                    if (!markierte[reihe][a].Checked)
                        löschen.Add(pair.Value[a]);
                }
            }
        }
    }
}
