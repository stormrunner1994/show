namespace Background
{
    partial class FormTimer
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
            this.timerglobal = new System.Windows.Forms.Timer(this.components);
            this.buttonadd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxdefault = new System.Windows.Forms.ComboBox();
            this.comboBoxanzahlton = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxvordergrundmeldung = new System.Windows.Forms.CheckBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.entfernenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerglobal
            // 
            this.timerglobal.Interval = 1000;
            this.timerglobal.Tick += new System.EventHandler(this.timerglobal_Tick);
            // 
            // buttonadd
            // 
            this.buttonadd.Image = global::Background.Properties.Resources.add;
            this.buttonadd.Location = new System.Drawing.Point(12, 108);
            this.buttonadd.Name = "buttonadd";
            this.buttonadd.Size = new System.Drawing.Size(26, 23);
            this.buttonadd.TabIndex = 16;
            this.buttonadd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.buttonadd.UseVisualStyleBackColor = true;
            this.buttonadd.Click += new System.EventHandler(this.buttonadd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Zeitspanne: ";
            // 
            // comboBoxdefault
            // 
            this.comboBoxdefault.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxdefault.FormattingEnabled = true;
            this.comboBoxdefault.Items.AddRange(new object[] {
            "sek",
            "min",
            "h"});
            this.comboBoxdefault.Location = new System.Drawing.Point(115, 110);
            this.comboBoxdefault.Name = "comboBoxdefault";
            this.comboBoxdefault.Size = new System.Drawing.Size(48, 21);
            this.comboBoxdefault.TabIndex = 18;
            // 
            // comboBoxanzahlton
            // 
            this.comboBoxanzahlton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxanzahlton.FormattingEnabled = true;
            this.comboBoxanzahlton.Items.AddRange(new object[] {
            "0",
            "1",
            "3",
            "5",
            "10",
            "20",
            "30"});
            this.comboBoxanzahlton.Location = new System.Drawing.Point(235, 110);
            this.comboBoxanzahlton.Name = "comboBoxanzahlton";
            this.comboBoxanzahlton.Size = new System.Drawing.Size(37, 21);
            this.comboBoxanzahlton.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Alarmtöne: ";
            // 
            // checkBoxvordergrundmeldung
            // 
            this.checkBoxvordergrundmeldung.AutoSize = true;
            this.checkBoxvordergrundmeldung.Location = new System.Drawing.Point(278, 112);
            this.checkBoxvordergrundmeldung.Name = "checkBoxvordergrundmeldung";
            this.checkBoxvordergrundmeldung.Size = new System.Drawing.Size(124, 17);
            this.checkBoxvordergrundmeldung.TabIndex = 21;
            this.checkBoxvordergrundmeldung.Text = "Vordergrundmeldung";
            this.checkBoxvordergrundmeldung.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entfernenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(126, 26);
            this.contextMenuStrip1.Text = "Entfernen";
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // entfernenToolStripMenuItem
            // 
            this.entfernenToolStripMenuItem.Name = "entfernenToolStripMenuItem";
            this.entfernenToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.entfernenToolStripMenuItem.Text = "Entfernen";
            // 
            // FormTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(402, 138);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonadd);
            this.Controls.Add(this.checkBoxvordergrundmeldung);
            this.Controls.Add(this.comboBoxdefault);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxanzahlton);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(418, 660);
            this.MinimumSize = new System.Drawing.Size(418, 75);
            this.Name = "FormTimer";
            this.ShowIcon = false;
            this.Text = "Timer";
            this.Load += new System.EventHandler(this.FormTimer_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerglobal;
        private System.Windows.Forms.Button buttonadd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxdefault;
        private System.Windows.Forms.ComboBox comboBoxanzahlton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxvordergrundmeldung;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem entfernenToolStripMenuItem;
    }
}