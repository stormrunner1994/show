namespace Background
{
    partial class FormDateien
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listBoxdateien = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonneu = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonbearbeiten = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonentfernen = new System.Windows.Forms.ToolStripButton();
            this.listBoxpfade = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.listBoxdateien);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 150);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dateiauflistung: ";
            // 
            // listBoxdateien
            // 
            this.listBoxdateien.FormattingEnabled = true;
            this.listBoxdateien.Location = new System.Drawing.Point(6, 19);
            this.listBoxdateien.Name = "listBoxdateien";
            this.listBoxdateien.Size = new System.Drawing.Size(252, 121);
            this.listBoxdateien.TabIndex = 3;
            this.listBoxdateien.DoubleClick += new System.EventHandler(this.listBoxdateien_DoubleClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.toolStrip1);
            this.groupBox2.Controls.Add(this.listBoxpfade);
            this.groupBox2.Location = new System.Drawing.Point(12, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(264, 99);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pfade: ";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonneu,
            this.toolStripButtonbearbeiten,
            this.toolStripButtonentfernen});
            this.toolStrip1.Location = new System.Drawing.Point(237, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(24, 80);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonneu
            // 
            this.toolStripButtonneu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonneu.Image = global::Background.Properties.Resources.add;
            this.toolStripButtonneu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonneu.Name = "toolStripButtonneu";
            this.toolStripButtonneu.Size = new System.Drawing.Size(21, 20);
            this.toolStripButtonneu.Text = "Hinzufügen";
            this.toolStripButtonneu.Click += new System.EventHandler(this.toolStripButtonneu_Click);
            // 
            // toolStripButtonbearbeiten
            // 
            this.toolStripButtonbearbeiten.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonbearbeiten.Enabled = false;
            this.toolStripButtonbearbeiten.Image = global::Background.Properties.Resources.arrow_refresh;
            this.toolStripButtonbearbeiten.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonbearbeiten.Name = "toolStripButtonbearbeiten";
            this.toolStripButtonbearbeiten.Size = new System.Drawing.Size(21, 20);
            this.toolStripButtonbearbeiten.Text = "Bearbeiten";
            this.toolStripButtonbearbeiten.Click += new System.EventHandler(this.toolStripButtonbearbeiten_Click);
            // 
            // toolStripButtonentfernen
            // 
            this.toolStripButtonentfernen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonentfernen.Enabled = false;
            this.toolStripButtonentfernen.Image = global::Background.Properties.Resources.delete;
            this.toolStripButtonentfernen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonentfernen.Name = "toolStripButtonentfernen";
            this.toolStripButtonentfernen.Size = new System.Drawing.Size(21, 20);
            this.toolStripButtonentfernen.Text = "Entfernen";
            this.toolStripButtonentfernen.Click += new System.EventHandler(this.toolStripButtonentfernen_Click);
            // 
            // listBoxpfade
            // 
            this.listBoxpfade.FormattingEnabled = true;
            this.listBoxpfade.Location = new System.Drawing.Point(6, 19);
            this.listBoxpfade.Name = "listBoxpfade";
            this.listBoxpfade.Size = new System.Drawing.Size(224, 69);
            this.listBoxpfade.TabIndex = 0;
            this.listBoxpfade.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxpfade_MouseDown);
            // 
            // FormDateien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 279);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(304, 317);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(304, 317);
            this.Name = "FormDateien";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Dateien";
            this.Load += new System.EventHandler(this.FormPfade_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBoxpfade;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonneu;
        private System.Windows.Forms.ToolStripButton toolStripButtonbearbeiten;
        private System.Windows.Forms.ToolStripButton toolStripButtonentfernen;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ListBox listBoxdateien;
    }
}