using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Background
{
    public partial class FormEinstellungen : Form
    {
        public FormEinstellungen(Dictionary<string, string> dictspeicherpfade)
        {
            InitializeComponent();
            this.dictspeicherpfade = dictspeicherpfade;
            einstellungen = new List<string>();
        }

        private Dictionary<string, string> dictspeicherpfade;
        private List<string> einstellungen;

        public List<string> getEinstellungen()
        {
            return einstellungen;
        }

        private void FormEinstellungen_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(dictspeicherpfade["Einstellungen"]);
            sr.ReadLine();
            textBox1.Text = sr.ReadLine();
            einstellungen.Add(textBox1.Text);
            textBox2.Text = sr.ReadLine();
            einstellungen.Add(textBox2.Text);
            sr.Close();
        }

        private void buttonspeichern_Click(object sender, EventArgs e)
        {
            einstellungen = new List<string>();
            StreamWriter sw = new StreamWriter(dictspeicherpfade["Einstellungen"]);
            sw.WriteLine("Einstellungen");
            sw.WriteLine(textBox1.Text);
            sw.WriteLine(textBox2.Text);
            einstellungen.Add(textBox1.Text);
            einstellungen.Add(textBox2.Text);
            sw.Close();
            this.Close();
        }

        private void buttonpfad_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "" && openFileDialog1.FileName != null)
                textBox1.Text = openFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Windows(dictspeicherpfade).HintergrundbildZurücksetzen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "" && openFileDialog1.FileName != null)
                textBox2.Text = openFileDialog1.FileName;
        }
        
    }
}
