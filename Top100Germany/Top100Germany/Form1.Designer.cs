namespace Top100Germany
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.nameErsetzenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortiereToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nachDifferenzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nachNeuenPunktenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nichtZugewieseneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ausgestiegeneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(577, 34);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(76, 47);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.Visible = false;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Link:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(53, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(516, 20);
            this.textBox1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 87);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(761, 380);
            this.dataGridView1.TabIndex = 3;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(66, 60);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(161, 21);
            this.comboBox1.TabIndex = 4;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Verlauf:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(494, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "Neuladen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nameErsetzenToolStripMenuItem,
            this.sortiereToolStripMenuItem,
            this.nichtZugewieseneToolStripMenuItem,
            this.ausgestiegeneToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(788, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // nameErsetzenToolStripMenuItem
            // 
            this.nameErsetzenToolStripMenuItem.Name = "nameErsetzenToolStripMenuItem";
            this.nameErsetzenToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.nameErsetzenToolStripMenuItem.Text = "Name ersetzen";
            this.nameErsetzenToolStripMenuItem.Click += new System.EventHandler(this.nameErsetzenToolStripMenuItem_Click);
            // 
            // sortiereToolStripMenuItem
            // 
            this.sortiereToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nachDifferenzToolStripMenuItem,
            this.nachNeuenPunktenToolStripMenuItem});
            this.sortiereToolStripMenuItem.Name = "sortiereToolStripMenuItem";
            this.sortiereToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.sortiereToolStripMenuItem.Text = "Sortiere";
            // 
            // nachDifferenzToolStripMenuItem
            // 
            this.nachDifferenzToolStripMenuItem.Name = "nachDifferenzToolStripMenuItem";
            this.nachDifferenzToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.nachDifferenzToolStripMenuItem.Text = "nach Differenz";
            this.nachDifferenzToolStripMenuItem.Click += new System.EventHandler(this.nachDifferenzToolStripMenuItem_Click);
            // 
            // nachNeuenPunktenToolStripMenuItem
            // 
            this.nachNeuenPunktenToolStripMenuItem.Name = "nachNeuenPunktenToolStripMenuItem";
            this.nachNeuenPunktenToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.nachNeuenPunktenToolStripMenuItem.Text = "nach neuen Punkten";
            this.nachNeuenPunktenToolStripMenuItem.Click += new System.EventHandler(this.nachNeuenPunktenToolStripMenuItem_Click);
            // 
            // nichtZugewieseneToolStripMenuItem
            // 
            this.nichtZugewieseneToolStripMenuItem.Enabled = false;
            this.nichtZugewieseneToolStripMenuItem.Name = "nichtZugewieseneToolStripMenuItem";
            this.nichtZugewieseneToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.nichtZugewieseneToolStripMenuItem.Text = "Nicht zugewiesene";
            this.nichtZugewieseneToolStripMenuItem.Click += new System.EventHandler(this.nichtZugewieseneToolStripMenuItem_Click);
            // 
            // ausgestiegeneToolStripMenuItem
            // 
            this.ausgestiegeneToolStripMenuItem.Name = "ausgestiegeneToolStripMenuItem";
            this.ausgestiegeneToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.ausgestiegeneToolStripMenuItem.Text = "Ausgestiegene";
            this.ausgestiegeneToolStripMenuItem.Click += new System.EventHandler(this.ausgestiegeneToolStripMenuItem_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Letzten zwei Tage",
            "Wöchentliche",
            "Monatliche",
            "Alle"});
            this.comboBox2.Location = new System.Drawing.Point(233, 60);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 21);
            this.comboBox2.TabIndex = 11;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 479);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nameErsetzenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sortiereToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nichtZugewieseneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ausgestiegeneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nachDifferenzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nachNeuenPunktenToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}

