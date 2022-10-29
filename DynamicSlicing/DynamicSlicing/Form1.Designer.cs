namespace DynamicSlicing
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewtable = new System.Windows.Forms.DataGridView();
            this.buttonpickcode = new System.Windows.Forms.Button();
            this.labelstopwatch = new System.Windows.Forms.Label();
            this.buttonergebnisausvorlesung = new System.Windows.Forms.Button();
            this.richTextBoxresults = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxvariablesofinterest = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonshowformular = new System.Windows.Forms.Button();
            this.textBoxnthelement = new System.Windows.Forms.TextBox();
            this.textBoxtestcase = new System.Windows.Forms.TextBox();
            this.buttonstartedynamicSlicing = new System.Windows.Forms.Button();
            this.buttonshowcode = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.howToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pickSourceCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxhistory = new System.Windows.Forms.GroupBox();
            this.buttonskiptoend = new System.Windows.Forms.Button();
            this.buttonnextstep = new System.Windows.Forms.Button();
            this.richTextBoxhistory = new System.Windows.Forms.RichTextBox();
            this.checkBoxintermediatesteps = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewtable)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBoxhistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewtable
            // 
            this.dataGridViewtable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewtable.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewtable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewtable.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewtable.Enabled = false;
            this.dataGridViewtable.Location = new System.Drawing.Point(12, 87);
            this.dataGridViewtable.Name = "dataGridViewtable";
            this.dataGridViewtable.RowHeadersVisible = false;
            this.dataGridViewtable.Size = new System.Drawing.Size(738, 541);
            this.dataGridViewtable.TabIndex = 0;
            this.dataGridViewtable.Scroll += new System.Windows.Forms.ScrollEventHandler(this.dataGridViewtable_Scroll);
            this.dataGridViewtable.Paint += new System.Windows.Forms.PaintEventHandler(this.dataGridView1_Paint);
            // 
            // buttonpickcode
            // 
            this.buttonpickcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonpickcode.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonpickcode.Location = new System.Drawing.Point(431, 27);
            this.buttonpickcode.Name = "buttonpickcode";
            this.buttonpickcode.Size = new System.Drawing.Size(155, 28);
            this.buttonpickcode.TabIndex = 1;
            this.buttonpickcode.Text = "Pick source code";
            this.buttonpickcode.UseVisualStyleBackColor = true;
            this.buttonpickcode.Visible = false;
            this.buttonpickcode.Click += new System.EventHandler(this.buttoncode_Click);
            // 
            // labelstopwatch
            // 
            this.labelstopwatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelstopwatch.AutoSize = true;
            this.labelstopwatch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelstopwatch.Location = new System.Drawing.Point(12, 727);
            this.labelstopwatch.Name = "labelstopwatch";
            this.labelstopwatch.Size = new System.Drawing.Size(45, 16);
            this.labelstopwatch.TabIndex = 2;
            this.labelstopwatch.Text = "label1";
            // 
            // buttonergebnisausvorlesung
            // 
            this.buttonergebnisausvorlesung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonergebnisausvorlesung.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonergebnisausvorlesung.Location = new System.Drawing.Point(899, 721);
            this.buttonergebnisausvorlesung.Name = "buttonergebnisausvorlesung";
            this.buttonergebnisausvorlesung.Size = new System.Drawing.Size(173, 28);
            this.buttonergebnisausvorlesung.TabIndex = 3;
            this.buttonergebnisausvorlesung.Text = "Ergebnis aus Vorlesung zeigen";
            this.buttonergebnisausvorlesung.UseVisualStyleBackColor = true;
            this.buttonergebnisausvorlesung.Click += new System.EventHandler(this.buttonergebnisausvorlesung_Click);
            // 
            // richTextBoxresults
            // 
            this.richTextBoxresults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxresults.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxresults.Location = new System.Drawing.Point(12, 634);
            this.richTextBoxresults.Name = "richTextBoxresults";
            this.richTextBoxresults.ReadOnly = true;
            this.richTextBoxresults.Size = new System.Drawing.Size(738, 81);
            this.richTextBoxresults.TabIndex = 4;
            this.richTextBoxresults.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBoxvariablesofinterest);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonshowformular);
            this.groupBox1.Controls.Add(this.textBoxnthelement);
            this.groupBox1.Controls.Add(this.textBoxtestcase);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(756, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 128);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Slicing Criterion:";
            // 
            // textBoxvariablesofinterest
            // 
            this.textBoxvariablesofinterest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxvariablesofinterest.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBoxvariablesofinterest.Location = new System.Drawing.Point(139, 96);
            this.textBoxvariablesofinterest.Name = "textBoxvariablesofinterest";
            this.textBoxvariablesofinterest.Size = new System.Drawing.Size(171, 22);
            this.textBoxvariablesofinterest.TabIndex = 8;
            this.textBoxvariablesofinterest.Text = "seperate with komma";
            this.textBoxvariablesofinterest.Click += new System.EventHandler(this.textBoxvariablesofinterest_Click);
            this.textBoxvariablesofinterest.TextChanged += new System.EventHandler(this.textBoxvariablesofinterest_TextChanged);
            this.textBoxvariablesofinterest.Enter += new System.EventHandler(this.textBoxvariablesofinterest_Enter);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "variables of interest:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "n^th element of trajectory:";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "test case:";
            // 
            // buttonshowformular
            // 
            this.buttonshowformular.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonshowformular.Location = new System.Drawing.Point(197, 11);
            this.buttonshowformular.Name = "buttonshowformular";
            this.buttonshowformular.Size = new System.Drawing.Size(113, 23);
            this.buttonshowformular.TabIndex = 4;
            this.buttonshowformular.Text = "Show Formula";
            this.buttonshowformular.UseVisualStyleBackColor = true;
            this.buttonshowformular.Click += new System.EventHandler(this.buttonshowformular_Click);
            // 
            // textBoxnthelement
            // 
            this.textBoxnthelement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxnthelement.Location = new System.Drawing.Point(170, 68);
            this.textBoxnthelement.Name = "textBoxnthelement";
            this.textBoxnthelement.Size = new System.Drawing.Size(140, 22);
            this.textBoxnthelement.TabIndex = 3;
            this.textBoxnthelement.TextChanged += new System.EventHandler(this.textBoxnthelement_TextChanged);
            // 
            // textBoxtestcase
            // 
            this.textBoxtestcase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxtestcase.Location = new System.Drawing.Point(77, 40);
            this.textBoxtestcase.Name = "textBoxtestcase";
            this.textBoxtestcase.Size = new System.Drawing.Size(233, 22);
            this.textBoxtestcase.TabIndex = 1;
            this.textBoxtestcase.TextChanged += new System.EventHandler(this.textBoxtestcase_TextChanged);
            this.textBoxtestcase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxtestcase_KeyDown);
            // 
            // buttonstartedynamicSlicing
            // 
            this.buttonstartedynamicSlicing.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonstartedynamicSlicing.Location = new System.Drawing.Point(12, 44);
            this.buttonstartedynamicSlicing.Name = "buttonstartedynamicSlicing";
            this.buttonstartedynamicSlicing.Size = new System.Drawing.Size(155, 28);
            this.buttonstartedynamicSlicing.TabIndex = 5;
            this.buttonstartedynamicSlicing.Text = "Start Dynamic Slicing";
            this.buttonstartedynamicSlicing.UseVisualStyleBackColor = true;
            this.buttonstartedynamicSlicing.Click += new System.EventHandler(this.buttonstartedynamicSlicing_Click);
            // 
            // buttonshowcode
            // 
            this.buttonshowcode.Enabled = false;
            this.buttonshowcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonshowcode.ForeColor = System.Drawing.Color.Red;
            this.buttonshowcode.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonshowcode.Location = new System.Drawing.Point(595, 27);
            this.buttonshowcode.Name = "buttonshowcode";
            this.buttonshowcode.Size = new System.Drawing.Size(155, 28);
            this.buttonshowcode.TabIndex = 7;
            this.buttonshowcode.Text = "no code chosen yet";
            this.buttonshowcode.UseVisualStyleBackColor = true;
            this.buttonshowcode.Visible = false;
            this.buttonshowcode.Click += new System.EventHandler(this.buttoncodezeigen_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.howToToolStripMenuItem,
            this.pickSourceCodeToolStripMenuItem,
            this.showCodeToolStripMenuItem,
            this.einstellungenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1084, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // howToToolStripMenuItem
            // 
            this.howToToolStripMenuItem.Name = "howToToolStripMenuItem";
            this.howToToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.howToToolStripMenuItem.Text = "How To";
            this.howToToolStripMenuItem.Click += new System.EventHandler(this.howToToolStripMenuItem_Click);
            // 
            // pickSourceCodeToolStripMenuItem
            // 
            this.pickSourceCodeToolStripMenuItem.Name = "pickSourceCodeToolStripMenuItem";
            this.pickSourceCodeToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.pickSourceCodeToolStripMenuItem.Text = "Pick source code";
            this.pickSourceCodeToolStripMenuItem.Click += new System.EventHandler(this.pickSourceCodeToolStripMenuItem_Click);
            // 
            // showCodeToolStripMenuItem
            // 
            this.showCodeToolStripMenuItem.Name = "showCodeToolStripMenuItem";
            this.showCodeToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.showCodeToolStripMenuItem.Text = "Show Code";
            this.showCodeToolStripMenuItem.Click += new System.EventHandler(this.showCodeToolStripMenuItem_Click);
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.einstellungenToolStripMenuItem.Text = "Config";
            this.einstellungenToolStripMenuItem.Visible = false;
            // 
            // groupBoxhistory
            // 
            this.groupBoxhistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxhistory.Controls.Add(this.buttonskiptoend);
            this.groupBoxhistory.Controls.Add(this.buttonnextstep);
            this.groupBoxhistory.Controls.Add(this.richTextBoxhistory);
            this.groupBoxhistory.Location = new System.Drawing.Point(756, 161);
            this.groupBoxhistory.Name = "groupBoxhistory";
            this.groupBoxhistory.Size = new System.Drawing.Size(328, 554);
            this.groupBoxhistory.TabIndex = 9;
            this.groupBoxhistory.TabStop = false;
            this.groupBoxhistory.Text = "History:";
            // 
            // buttonskiptoend
            // 
            this.buttonskiptoend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonskiptoend.Location = new System.Drawing.Point(87, 525);
            this.buttonskiptoend.Name = "buttonskiptoend";
            this.buttonskiptoend.Size = new System.Drawing.Size(75, 23);
            this.buttonskiptoend.TabIndex = 14;
            this.buttonskiptoend.Text = "Skip to end";
            this.buttonskiptoend.UseVisualStyleBackColor = true;
            this.buttonskiptoend.Click += new System.EventHandler(this.buttonskiptoend_Click);
            // 
            // buttonnextstep
            // 
            this.buttonnextstep.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonnextstep.Location = new System.Drawing.Point(6, 525);
            this.buttonnextstep.Name = "buttonnextstep";
            this.buttonnextstep.Size = new System.Drawing.Size(75, 23);
            this.buttonnextstep.TabIndex = 11;
            this.buttonnextstep.Text = "Next step";
            this.buttonnextstep.UseVisualStyleBackColor = true;
            this.buttonnextstep.Click += new System.EventHandler(this.buttonnextstep_Click);
            // 
            // richTextBoxhistory
            // 
            this.richTextBoxhistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxhistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxhistory.Location = new System.Drawing.Point(6, 19);
            this.richTextBoxhistory.Name = "richTextBoxhistory";
            this.richTextBoxhistory.Size = new System.Drawing.Size(310, 500);
            this.richTextBoxhistory.TabIndex = 10;
            this.richTextBoxhistory.Text = "";
            this.richTextBoxhistory.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // checkBoxintermediatesteps
            // 
            this.checkBoxintermediatesteps.AutoSize = true;
            this.checkBoxintermediatesteps.Enabled = false;
            this.checkBoxintermediatesteps.Location = new System.Drawing.Point(173, 51);
            this.checkBoxintermediatesteps.Name = "checkBoxintermediatesteps";
            this.checkBoxintermediatesteps.Size = new System.Drawing.Size(111, 17);
            this.checkBoxintermediatesteps.TabIndex = 12;
            this.checkBoxintermediatesteps.Text = "intermediate steps";
            this.checkBoxintermediatesteps.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 761);
            this.Controls.Add(this.checkBoxintermediatesteps);
            this.Controls.Add(this.groupBoxhistory);
            this.Controls.Add(this.buttonshowcode);
            this.Controls.Add(this.buttonstartedynamicSlicing);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.richTextBoxresults);
            this.Controls.Add(this.buttonergebnisausvorlesung);
            this.Controls.Add(this.labelstopwatch);
            this.Controls.Add(this.buttonpickcode);
            this.Controls.Add(this.dataGridViewtable);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(651, 469);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Dynamic Slicing";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewtable)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxhistory.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewtable;
        private System.Windows.Forms.Button buttonpickcode;
        private System.Windows.Forms.Label labelstopwatch;
        private System.Windows.Forms.Button buttonergebnisausvorlesung;
        private System.Windows.Forms.RichTextBox richTextBoxresults;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxnthelement;
        private System.Windows.Forms.TextBox textBoxtestcase;
        private System.Windows.Forms.Button buttonstartedynamicSlicing;
        private System.Windows.Forms.Button buttonshowcode;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.Button buttonshowformular;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxvariablesofinterest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxhistory;
        private System.Windows.Forms.RichTextBox richTextBoxhistory;
        private System.Windows.Forms.Button buttonnextstep;
        private System.Windows.Forms.CheckBox checkBoxintermediatesteps;
        private System.Windows.Forms.Button buttonskiptoend;
        private System.Windows.Forms.ToolStripMenuItem howToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pickSourceCodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCodeToolStripMenuItem;
    }
}

