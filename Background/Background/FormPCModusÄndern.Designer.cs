namespace Background
{
    partial class FormPCModusÄndern
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonstarten = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.comboBoxmodi = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Den PC in ";
            // 
            // buttonstarten
            // 
            this.buttonstarten.Location = new System.Drawing.Point(429, 49);
            this.buttonstarten.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonstarten.Name = "buttonstarten";
            this.buttonstarten.Size = new System.Drawing.Size(112, 35);
            this.buttonstarten.TabIndex = 2;
            this.buttonstarten.Text = "Starten";
            this.buttonstarten.UseVisualStyleBackColor = true;
            this.buttonstarten.Click += new System.EventHandler(this.buttonstarten_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(18, 94);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(524, 35);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 3;
            // 
            // comboBoxmodi
            // 
            this.comboBoxmodi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxmodi.FormattingEnabled = true;
            this.comboBoxmodi.Items.AddRange(new object[] {
            "Abmelden",
            "Neustarten",
            "Herunterfahren"});
            this.comboBoxmodi.Location = new System.Drawing.Point(360, 8);
            this.comboBoxmodi.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBoxmodi.Name = "comboBoxmodi";
            this.comboBoxmodi.Size = new System.Drawing.Size(180, 28);
            this.comboBoxmodi.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Sekunden",
            "Minuten",
            "Stunden"});
            this.comboBox1.Location = new System.Drawing.Point(232, 8);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(116, 28);
            this.comboBox1.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(114, 9);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(108, 26);
            this.textBox1.TabIndex = 6;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormPCModusÄndern
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 109);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.comboBoxmodi);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonstarten);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(572, 165);
            this.MinimumSize = new System.Drawing.Size(572, 165);
            this.Name = "FormPCModusÄndern";
            this.ShowIcon = false;
            this.Text = "PC-Modus ändern";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPCModusÄndern_FormClosing);
            this.Load += new System.EventHandler(this.FormPCModusÄndern_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonstarten;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox comboBoxmodi;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
    }
}