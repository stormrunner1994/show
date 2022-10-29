using Ordner_;
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
    public partial class FormDateienZeigen : Form
    {

        private List<ClassDatei> dateien = new List<ClassDatei>();
        private int ignoreCols = 1;
        private int irow = -1;


        public FormDateienZeigen(List<ClassDatei> dateien)
        {
            InitializeComponent();
            this.dateien = new List<ClassDatei>();
            this.dateien.AddRange(dateien);
        }

        private void FormDateienZeigen_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            foreach (ClassDatei cd in dateien)
            {
                dataGridView1.Rows.Add(new string[] { cd.GetGröße(), cd.pfad });
            }

            if (dataGridView1.Rows.Count == 1)
                label1.Text = dataGridView1.Rows.Count + " Datei";
            else
            label1.Text = dataGridView1.Rows.Count + " Dateien";
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right || e.RowIndex < 0 || e.ColumnIndex < ignoreCols ||
    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;
            irow = e.RowIndex;

            contextMenuStrip1.Show(MousePosition);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (irow == -1 ) return;

            dateien[irow].SelektiereImExplorer();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (irow == -1) return;

            dateien[irow].ÖffneDatei();
        }
    }
}
