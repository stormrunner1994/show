using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ordner_;
using Invoker_;
using System.Diagnostics;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Dictionary<string, List<ClassDatei>> dateitypen = new Dictionary<string, List<ClassDatei>>();


        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView2.BackgroundColor = Color.White;
        }

        private void Init(string path)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ClassOrdner co = new ClassOrdner(path, true);
            dateitypen = co.GetAlleDateitypenDateien();
            sw.Stop();

            foreach (string key in dateitypen.Keys.OrderByDescending(i => GetSizeLong(dateitypen[i.ToString()])))
                Invoker.invokeAddRow(dataGridView1, new string[] { key, GetSize(dateitypen[key]) });
            Invoker.invokeTextSet(label1, "Loaded after " + sw.ElapsedMilliseconds / 1000 + " sec");
            Invoker.invokeEnable(button1, true);
            Invoker.invokeTextSet(label4, dateitypen.Count + " Filetypes");
        }

        private string GetSize(List<ClassDatei> files)
        {
            double size = 0;
            foreach (ClassDatei file in files)
                size += file.größe;
            
            return ClassDatei.GetGröße(size);
        }

        private long GetSizeLong(List<ClassDatei> files)
        {
            long size = 0;
            foreach (ClassDatei file in files)
                size += file.größe;

            return size;
        }


        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0) return;

            try
            {
                string file = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                Process.Start(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't open file\n" + ex.Message);
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0) return;

            try
            {
                int select = dataGridView2.SelectedRows[0].Index;
                string filepath = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                File.Delete(filepath);
                dataGridView2.Rows.RemoveAt(select);
                label3.Text = dataGridView2.Rows.Count + " Files";
                label1.Text = "Deleted " + filepath;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't delete file\n" + ex.Message);
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(sender, e);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;


            string filetype = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            if (!dateitypen.ContainsKey(filetype))
            {
                label1.Text = "Key not found";
                return;
            }

            dataGridView2.Rows.Clear();

            if (comboBox1.SelectedIndex == 0)
            {
                foreach (ClassDatei cd in dateitypen[filetype])
                    dataGridView2.Rows.Add(new string[] { cd.pfad, cd.GetGröße() });
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                foreach (ClassDatei cd in dateitypen[filetype].OrderByDescending(i => i.größe))
                    dataGridView2.Rows.Add(new string[] { cd.pfad, cd.GetGröße() });
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                foreach (ClassDatei cd in dateitypen[filetype].OrderBy(i => i.größe))
                    dataGridView2.Rows.Add(new string[] { cd.pfad, cd.GetGröße() });
            }


            Invoker.invokeTextSet(label3, dateitypen[filetype].Count + " Files");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            label1.Text = "Scanning...";
            string path = textBox1.Text;
            Thread init = new Thread(delegate () { Init(path); });
            init.Start();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            button1_Click(sender, e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = Directory.Exists(textBox1.Text);
        }
    }
}
