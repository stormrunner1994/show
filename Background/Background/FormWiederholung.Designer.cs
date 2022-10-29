namespace Background
{
    partial class FormWiederholung
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonja = new System.Windows.Forms.Button();
            this.buttonnein = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sollen alle Wiederholungstermine gelöscht werden?";
            // 
            // buttonja
            // 
            this.buttonja.Location = new System.Drawing.Point(15, 65);
            this.buttonja.Name = "buttonja";
            this.buttonja.Size = new System.Drawing.Size(75, 23);
            this.buttonja.TabIndex = 1;
            this.buttonja.Text = "Ja";
            this.buttonja.UseVisualStyleBackColor = true;
            this.buttonja.Click += new System.EventHandler(this.buttonja_Click);
            // 
            // buttonnein
            // 
            this.buttonnein.Location = new System.Drawing.Point(155, 65);
            this.buttonnein.Name = "buttonnein";
            this.buttonnein.Size = new System.Drawing.Size(107, 23);
            this.buttonnein.TabIndex = 2;
            this.buttonnein.Text = "Nur dieser einzelne";
            this.buttonnein.UseVisualStyleBackColor = true;
            this.buttonnein.Click += new System.EventHandler(this.buttonnein_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Fenster schließen, um abzubrechen!";
            // 
            // FormWiederholung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 110);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonnein);
            this.Controls.Add(this.buttonja);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 139);
            this.MinimumSize = new System.Drawing.Size(300, 139);
            this.Name = "FormWiederholung";
            this.ShowIcon = false;
            this.Text = "Wiederholungstermin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonja;
        private System.Windows.Forms.Button buttonnein;
        private System.Windows.Forms.Label label2;
    }
}