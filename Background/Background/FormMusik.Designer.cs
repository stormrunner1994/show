namespace Background
{
    partial class FormMusik
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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.buttonabbrechen = new System.Windows.Forms.Button();
            this.buttonspeichern = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(189, 17);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Musik bei Programmstart abspielen";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Dateiinhalt (\'!\' vor dem Pfad verhindert Abspielen):";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(12, 48);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(624, 156);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // buttonabbrechen
            // 
            this.buttonabbrechen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonabbrechen.Location = new System.Drawing.Point(561, 210);
            this.buttonabbrechen.Name = "buttonabbrechen";
            this.buttonabbrechen.Size = new System.Drawing.Size(75, 23);
            this.buttonabbrechen.TabIndex = 3;
            this.buttonabbrechen.Text = "Abbrechen";
            this.buttonabbrechen.UseVisualStyleBackColor = true;
            this.buttonabbrechen.Click += new System.EventHandler(this.buttonabbrechen_Click);
            // 
            // buttonspeichern
            // 
            this.buttonspeichern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonspeichern.Location = new System.Drawing.Point(477, 210);
            this.buttonspeichern.Name = "buttonspeichern";
            this.buttonspeichern.Size = new System.Drawing.Size(75, 23);
            this.buttonspeichern.TabIndex = 4;
            this.buttonspeichern.Text = "Speichern";
            this.buttonspeichern.UseVisualStyleBackColor = true;
            this.buttonspeichern.Click += new System.EventHandler(this.buttonspeichern_Click);
            // 
            // FormMusik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 245);
            this.Controls.Add(this.buttonspeichern);
            this.Controls.Add(this.buttonabbrechen);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.Name = "FormMusik";
            this.ShowIcon = false;
            this.Text = "Musikdatei bearbeiten";
            this.Load += new System.EventHandler(this.FormMusik_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button buttonabbrechen;
        private System.Windows.Forms.Button buttonspeichern;
    }
}