namespace DynamicSlicing
{
    partial class FormCode
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
            this.richTextBoxcode = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxexamples = new System.Windows.Forms.ComboBox();
            this.buttonchoosecode = new System.Windows.Forms.Button();
            this.buttonformatecode = new System.Windows.Forms.Button();
            this.buttonreadfile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBoxcode
            // 
            this.richTextBoxcode.AcceptsTab = true;
            this.richTextBoxcode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxcode.Location = new System.Drawing.Point(12, 54);
            this.richTextBoxcode.Name = "richTextBoxcode";
            this.richTextBoxcode.Size = new System.Drawing.Size(569, 346);
            this.richTextBoxcode.TabIndex = 0;
            this.richTextBoxcode.Text = "";
            this.richTextBoxcode.TextChanged += new System.EventHandler(this.richTextBoxcode_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Code Examples:";
            // 
            // comboBoxexamples
            // 
            this.comboBoxexamples.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxexamples.FormattingEnabled = true;
            this.comboBoxexamples.Items.AddRange(new object[] {
            "Type manually",
            "Example 1",
            "Example 2",
            "Example 3",
            "Example 4",
            "Example 5",
            "Example 6"});
            this.comboBoxexamples.Location = new System.Drawing.Point(102, 27);
            this.comboBoxexamples.Name = "comboBoxexamples";
            this.comboBoxexamples.Size = new System.Drawing.Size(121, 21);
            this.comboBoxexamples.TabIndex = 3;
            this.comboBoxexamples.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // buttonchoosecode
            // 
            this.buttonchoosecode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonchoosecode.Enabled = false;
            this.buttonchoosecode.Location = new System.Drawing.Point(462, 406);
            this.buttonchoosecode.Name = "buttonchoosecode";
            this.buttonchoosecode.Size = new System.Drawing.Size(119, 23);
            this.buttonchoosecode.TabIndex = 4;
            this.buttonchoosecode.Text = "Choose this code";
            this.buttonchoosecode.UseVisualStyleBackColor = true;
            this.buttonchoosecode.Click += new System.EventHandler(this.buttonchoosecode_Click);
            // 
            // buttonformatecode
            // 
            this.buttonformatecode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonformatecode.Location = new System.Drawing.Point(367, 406);
            this.buttonformatecode.Name = "buttonformatecode";
            this.buttonformatecode.Size = new System.Drawing.Size(89, 23);
            this.buttonformatecode.TabIndex = 5;
            this.buttonformatecode.Text = "Format Code";
            this.buttonformatecode.UseVisualStyleBackColor = true;
            this.buttonformatecode.Click += new System.EventHandler(this.buttonformatecode_Click);
            // 
            // buttonreadfile
            // 
            this.buttonreadfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonreadfile.Location = new System.Drawing.Point(462, 25);
            this.buttonreadfile.Name = "buttonreadfile";
            this.buttonreadfile.Size = new System.Drawing.Size(119, 23);
            this.buttonreadfile.TabIndex = 6;
            this.buttonreadfile.Text = "Read Sourcefile";
            this.buttonreadfile.UseVisualStyleBackColor = true;
            this.buttonreadfile.Click += new System.EventHandler(this.buttondatei_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(452, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Please take a look at the example codes to see which formats are acceptable!";
            // 
            // FormCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 439);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonreadfile);
            this.Controls.Add(this.buttonformatecode);
            this.Controls.Add(this.buttonchoosecode);
            this.Controls.Add(this.comboBoxexamples);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBoxcode);
            this.MinimumSize = new System.Drawing.Size(379, 340);
            this.Name = "FormCode";
            this.ShowIcon = false;
            this.Text = "Pick Code";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCode_FormClosing);
            this.Load += new System.EventHandler(this.FormCode_Load);
            this.Shown += new System.EventHandler(this.FormCode_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxcode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxexamples;
        private System.Windows.Forms.Button buttonchoosecode;
        private System.Windows.Forms.Button buttonformatecode;
        private System.Windows.Forms.Button buttonreadfile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
    }
}