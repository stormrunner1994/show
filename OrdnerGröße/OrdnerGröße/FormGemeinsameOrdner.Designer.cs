namespace OrdnerGröße
{
    partial class FormGemeinsameOrdner
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGemeinsameOrdner));
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemOrdnerÖffnen = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemNurDieseBehalten = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDateienAnzeigen = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ersteDateiÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(6, 19);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(337, 45);
            this.trackBar1.TabIndex = 2;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(349, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ordner sind zu 100% identisch";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.trackBar1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(531, 92);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Regler";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sortieren nach:";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Größe der Datei",
            "Anzahl der Duplikate"});
            this.comboBox1.Location = new System.Drawing.Point(116, 57);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 110);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(532, 218);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOrdnerÖffnen,
            this.ToolStripMenuItemNurDieseBehalten,
            this.toolStripMenuItemDateienAnzeigen,
            this.ersteDateiÖffnenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 92);
            // 
            // ToolStripMenuItemOrdnerÖffnen
            // 
            this.ToolStripMenuItemOrdnerÖffnen.Name = "ToolStripMenuItemOrdnerÖffnen";
            this.ToolStripMenuItemOrdnerÖffnen.Size = new System.Drawing.Size(173, 22);
            this.ToolStripMenuItemOrdnerÖffnen.Text = "Ordner öffnen";
            this.ToolStripMenuItemOrdnerÖffnen.Click += new System.EventHandler(this.ordnerÖffnenToolStripMenuItem_Click);
            // 
            // ToolStripMenuItemNurDieseBehalten
            // 
            this.ToolStripMenuItemNurDieseBehalten.Name = "ToolStripMenuItemNurDieseBehalten";
            this.ToolStripMenuItemNurDieseBehalten.Size = new System.Drawing.Size(173, 22);
            this.ToolStripMenuItemNurDieseBehalten.Text = "Nur diese behalten";
            this.ToolStripMenuItemNurDieseBehalten.Click += new System.EventHandler(this.nurDieseBehaltenToolStripMenuItem_Click);
            // 
            // toolStripMenuItemDateienAnzeigen
            // 
            this.toolStripMenuItemDateienAnzeigen.Name = "toolStripMenuItemDateienAnzeigen";
            this.toolStripMenuItemDateienAnzeigen.Size = new System.Drawing.Size(173, 22);
            this.toolStripMenuItemDateienAnzeigen.Text = "Dateien anzeigen";
            this.toolStripMenuItemDateienAnzeigen.Click += new System.EventHandler(this.toolStripMenuItemDateienAnzeigen_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(419, 60);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 13);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // ersteDateiÖffnenToolStripMenuItem
            // 
            this.ersteDateiÖffnenToolStripMenuItem.Name = "ersteDateiÖffnenToolStripMenuItem";
            this.ersteDateiÖffnenToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ersteDateiÖffnenToolStripMenuItem.Text = "Erste Datei öffnen";
            this.ersteDateiÖffnenToolStripMenuItem.Click += new System.EventHandler(this.ersteDateiÖffnenToolStripMenuItem_Click);
            // 
            // FormGemeinsameOrdner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(556, 340);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormGemeinsameOrdner";
            this.ShowIcon = false;
            this.Text = "Gemeinsame Ordner";
            this.Load += new System.EventHandler(this.FormGemeinsameOrdner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOrdnerÖffnen;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemNurDieseBehalten;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDateienAnzeigen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem ersteDateiÖffnenToolStripMenuItem;
    }
}