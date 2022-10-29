namespace Phase6_Software
{
    partial class FormVorhanden
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
            this.label2 = new System.Windows.Forms.Label();
            this.buttonnein = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Diese Karteikarte existiert bereits.";
            // 
            // buttonja
            // 
            this.buttonja.Location = new System.Drawing.Point(11, 55);
            this.buttonja.Margin = new System.Windows.Forms.Padding(4);
            this.buttonja.Name = "buttonja";
            this.buttonja.Size = new System.Drawing.Size(88, 26);
            this.buttonja.TabIndex = 1;
            this.buttonja.Text = "Ja";
            this.buttonja.UseVisualStyleBackColor = true;
            this.buttonja.Click += new System.EventHandler(this.buttonja_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Trotzdem hinzufügen?";
            // 
            // buttonnein
            // 
            this.buttonnein.Location = new System.Drawing.Point(128, 55);
            this.buttonnein.Margin = new System.Windows.Forms.Padding(4);
            this.buttonnein.Name = "buttonnein";
            this.buttonnein.Size = new System.Drawing.Size(88, 26);
            this.buttonnein.TabIndex = 3;
            this.buttonnein.Text = "Nein";
            this.buttonnein.UseVisualStyleBackColor = true;
            this.buttonnein.Click += new System.EventHandler(this.buttonnein_Click);
            // 
            // FormVorhanden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 94);
            this.Controls.Add(this.buttonnein);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonja);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(233, 122);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(233, 122);
            this.Name = "FormVorhanden";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.FormVorhanden_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonja;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonnein;
    }
}