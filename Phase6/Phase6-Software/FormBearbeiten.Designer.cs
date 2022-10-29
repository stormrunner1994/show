namespace Phase6_Software
{
    partial class FormBearbeiten
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
            this.textBoxkategorie = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBoxfrage = new System.Windows.Forms.RichTextBox();
            this.richTextBoxantwort = new System.Windows.Forms.RichTextBox();
            this.checkBoxzurücksetzen = new System.Windows.Forms.CheckBox();
            this.buttonlöschen = new System.Windows.Forms.Button();
            this.buttonändern = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kategorie: ";
            // 
            // textBoxkategorie
            // 
            this.textBoxkategorie.Location = new System.Drawing.Point(76, 6);
            this.textBoxkategorie.Name = "textBoxkategorie";
            this.textBoxkategorie.Size = new System.Drawing.Size(111, 20);
            this.textBoxkategorie.TabIndex = 1;
            this.textBoxkategorie.TextChanged += new System.EventHandler(this.textBoxkategorie_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Antwort: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Frage: ";
            // 
            // richTextBoxfrage
            // 
            this.richTextBoxfrage.Location = new System.Drawing.Point(76, 32);
            this.richTextBoxfrage.Name = "richTextBoxfrage";
            this.richTextBoxfrage.Size = new System.Drawing.Size(210, 113);
            this.richTextBoxfrage.TabIndex = 8;
            this.richTextBoxfrage.Text = "";
            this.richTextBoxfrage.TextChanged += new System.EventHandler(this.richTextBoxfrage_TextChanged);
            // 
            // richTextBoxantwort
            // 
            this.richTextBoxantwort.Location = new System.Drawing.Point(76, 151);
            this.richTextBoxantwort.Name = "richTextBoxantwort";
            this.richTextBoxantwort.Size = new System.Drawing.Size(210, 117);
            this.richTextBoxantwort.TabIndex = 9;
            this.richTextBoxantwort.Text = "";
            this.richTextBoxantwort.TextChanged += new System.EventHandler(this.richTextBoxantwort_TextChanged);
            // 
            // checkBoxzurücksetzen
            // 
            this.checkBoxzurücksetzen.AutoSize = true;
            this.checkBoxzurücksetzen.Location = new System.Drawing.Point(76, 274);
            this.checkBoxzurücksetzen.Name = "checkBoxzurücksetzen";
            this.checkBoxzurücksetzen.Size = new System.Drawing.Size(154, 17);
            this.checkBoxzurücksetzen.TabIndex = 10;
            this.checkBoxzurücksetzen.Text = "Karteistatistik zurücksetzen";
            this.checkBoxzurücksetzen.UseVisualStyleBackColor = true;
            // 
            // buttonlöschen
            // 
            this.buttonlöschen.Location = new System.Drawing.Point(12, 297);
            this.buttonlöschen.Name = "buttonlöschen";
            this.buttonlöschen.Size = new System.Drawing.Size(116, 23);
            this.buttonlöschen.TabIndex = 11;
            this.buttonlöschen.Text = "Karteikarte entfernen";
            this.buttonlöschen.UseVisualStyleBackColor = true;
            this.buttonlöschen.Click += new System.EventHandler(this.buttonlöschen_Click);
            // 
            // buttonändern
            // 
            this.buttonändern.Location = new System.Drawing.Point(169, 297);
            this.buttonändern.Name = "buttonändern";
            this.buttonändern.Size = new System.Drawing.Size(137, 23);
            this.buttonändern.TabIndex = 12;
            this.buttonändern.Text = "Änderung übernehmen";
            this.buttonändern.UseVisualStyleBackColor = true;
            this.buttonändern.Click += new System.EventHandler(this.buttonändern_Click);
            // 
            // FormBearbeiten
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(319, 331);
            this.Controls.Add(this.buttonändern);
            this.Controls.Add(this.buttonlöschen);
            this.Controls.Add(this.checkBoxzurücksetzen);
            this.Controls.Add(this.richTextBoxantwort);
            this.Controls.Add(this.richTextBoxfrage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxkategorie);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(335, 370);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(335, 370);
            this.Name = "FormBearbeiten";
            this.ShowIcon = false;
            this.Text = "Karteikarte bearbeiten";
            this.Load += new System.EventHandler(this.FormBearbeiten_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxkategorie;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBoxfrage;
        private System.Windows.Forms.RichTextBox richTextBoxantwort;
        private System.Windows.Forms.CheckBox checkBoxzurücksetzen;
        private System.Windows.Forms.Button buttonlöschen;
        private System.Windows.Forms.Button buttonändern;
    }
}