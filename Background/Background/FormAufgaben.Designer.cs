namespace Background
{
    partial class FormAufgaben
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
            this.textBoxaufgabe = new System.Windows.Forms.TextBox();
            this.buttonanpassen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Aufgabe: ";
            // 
            // textBoxaufgabe
            // 
            this.textBoxaufgabe.Location = new System.Drawing.Point(106, 18);
            this.textBoxaufgabe.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxaufgabe.Name = "textBoxaufgabe";
            this.textBoxaufgabe.Size = new System.Drawing.Size(148, 26);
            this.textBoxaufgabe.TabIndex = 1;
            this.textBoxaufgabe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxaufgabe_KeyDown);
            // 
            // buttonanpassen
            // 
            this.buttonanpassen.Location = new System.Drawing.Point(266, 15);
            this.buttonanpassen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonanpassen.Name = "buttonanpassen";
            this.buttonanpassen.Size = new System.Drawing.Size(112, 35);
            this.buttonanpassen.TabIndex = 2;
            this.buttonanpassen.Text = "Hinzufügen";
            this.buttonanpassen.UseVisualStyleBackColor = true;
            this.buttonanpassen.Click += new System.EventHandler(this.buttonanpassen_Click);
            // 
            // FormAufgaben
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(390, 58);
            this.Controls.Add(this.buttonanpassen);
            this.Controls.Add(this.textBoxaufgabe);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(412, 108);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(412, 108);
            this.Name = "FormAufgaben";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Aufgaben";
            this.Load += new System.EventHandler(this.FormAufgaben_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxaufgabe;
        private System.Windows.Forms.Button buttonanpassen;
    }
}