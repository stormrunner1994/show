namespace Background
{
    partial class FormGoogleDrive
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
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonneuverbinden = new System.Windows.Forms.Button();
            this.checkBoxgelöschteanzeigen = new System.Windows.Forms.CheckBox();
            this.buttonupload = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.buttondownload = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxdownloadpfad = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(12, 34);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(316, 342);
            this.treeView1.TabIndex = 0;
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rootordner: BackGround";
            // 
            // buttonneuverbinden
            // 
            this.buttonneuverbinden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonneuverbinden.Location = new System.Drawing.Point(424, 4);
            this.buttonneuverbinden.Name = "buttonneuverbinden";
            this.buttonneuverbinden.Size = new System.Drawing.Size(106, 23);
            this.buttonneuverbinden.TabIndex = 2;
            this.buttonneuverbinden.Text = "Neu verbinden";
            this.buttonneuverbinden.UseVisualStyleBackColor = true;
            this.buttonneuverbinden.Click += new System.EventHandler(this.buttonneuverbinden_Click);
            // 
            // checkBoxgelöschteanzeigen
            // 
            this.checkBoxgelöschteanzeigen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxgelöschteanzeigen.AutoSize = true;
            this.checkBoxgelöschteanzeigen.Location = new System.Drawing.Point(410, 34);
            this.checkBoxgelöschteanzeigen.Name = "checkBoxgelöschteanzeigen";
            this.checkBoxgelöschteanzeigen.Size = new System.Drawing.Size(120, 17);
            this.checkBoxgelöschteanzeigen.TabIndex = 3;
            this.checkBoxgelöschteanzeigen.Text = "Gelöschte anzeigen";
            this.checkBoxgelöschteanzeigen.UseVisualStyleBackColor = true;
            this.checkBoxgelöschteanzeigen.CheckedChanged += new System.EventHandler(this.checkBoxgelöschteanzeigen_CheckedChanged);
            // 
            // buttonupload
            // 
            this.buttonupload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonupload.Enabled = false;
            this.buttonupload.Location = new System.Drawing.Point(118, 19);
            this.buttonupload.Name = "buttonupload";
            this.buttonupload.Size = new System.Drawing.Size(75, 23);
            this.buttonupload.TabIndex = 4;
            this.buttonupload.Text = "Hochladen";
            this.buttonupload.UseVisualStyleBackColor = true;
            this.buttonupload.Click += new System.EventHandler(this.buttonupload_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(61, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Datei wurde hochgeladen!";
            this.label2.Visible = false;
            // 
            // buttondownload
            // 
            this.buttondownload.Location = new System.Drawing.Point(118, 45);
            this.buttondownload.Name = "buttondownload";
            this.buttondownload.Size = new System.Drawing.Size(75, 23);
            this.buttondownload.TabIndex = 6;
            this.buttondownload.Text = "Download";
            this.buttondownload.UseVisualStyleBackColor = true;
            this.buttondownload.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Datei wurde heruntergeladen!";
            this.label3.Visible = false;
            // 
            // textBoxdownloadpfad
            // 
            this.textBoxdownloadpfad.Location = new System.Drawing.Point(6, 19);
            this.textBoxdownloadpfad.Name = "textBoxdownloadpfad";
            this.textBoxdownloadpfad.Size = new System.Drawing.Size(187, 20);
            this.textBoxdownloadpfad.TabIndex = 8;
            this.textBoxdownloadpfad.Text = "C:\\BackGround\\";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonupload);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(334, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(199, 70);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datei hochladen:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.textBoxdownloadpfad);
            this.groupBox2.Controls.Add(this.buttondownload);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(334, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(199, 94);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datei herunterladen:";
            // 
            // FormGoogleDrive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 388);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxgelöschteanzeigen);
            this.Controls.Add(this.buttonneuverbinden);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Name = "FormGoogleDrive";
            this.Text = "GoogleDrive";
            this.Load += new System.EventHandler(this.FormGoogleDrive_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonneuverbinden;
        private System.Windows.Forms.CheckBox checkBoxgelöschteanzeigen;
        private System.Windows.Forms.Button buttonupload;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttondownload;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxdownloadpfad;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}