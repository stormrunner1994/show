namespace OrdnerGröße
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.buttonpfad = new System.Windows.Forms.Button();
            this.textBoxpfad = new System.Windows.Forms.TextBox();
            this.buttonstarten = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.labelstatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labeltop100größe = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labeltop100 = new System.Windows.Forms.Label();
            this.progressBartop100 = new System.Windows.Forms.ProgressBar();
            this.listBoxtop100 = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.labelduplikate = new System.Windows.Forms.Label();
            this.progressBarduplikate = new System.Windows.Forms.ProgressBar();
            this.buttongemeinsam = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listBoxduplikate = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.checkBoxselbeDateityp = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelselbegröße = new System.Windows.Forms.Label();
            this.progressBarselbergröße = new System.Windows.Forms.ProgressBar();
            this.listBoxselbegröße = new System.Windows.Forms.ListBox();
            this.checkBoxunterordner = new System.Windows.Forms.CheckBox();
            this.buttonfehler = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checkedListBoxaufgaben = new System.Windows.Forms.CheckedListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ordnerstrukturSpeichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ladenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoriertePfadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leereOrdnerLöschenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.letzteAktionRückgängigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.Location = new System.Drawing.Point(5, 4);
            this.treeView1.Margin = new System.Windows.Forms.Padding(2);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(508, 297);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // buttonpfad
            // 
            this.buttonpfad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonpfad.Location = new System.Drawing.Point(508, 37);
            this.buttonpfad.Margin = new System.Windows.Forms.Padding(2);
            this.buttonpfad.Name = "buttonpfad";
            this.buttonpfad.Size = new System.Drawing.Size(31, 25);
            this.buttonpfad.TabIndex = 1;
            this.buttonpfad.Text = "...";
            this.buttonpfad.UseVisualStyleBackColor = true;
            this.buttonpfad.Click += new System.EventHandler(this.buttonpfad_Click);
            // 
            // textBoxpfad
            // 
            this.textBoxpfad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxpfad.Location = new System.Drawing.Point(12, 40);
            this.textBoxpfad.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxpfad.Name = "textBoxpfad";
            this.textBoxpfad.Size = new System.Drawing.Size(493, 20);
            this.textBoxpfad.TabIndex = 2;
            this.textBoxpfad.TextChanged += new System.EventHandler(this.textBoxpfad_TextChanged);
            this.textBoxpfad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxpfad_KeyDown);
            // 
            // buttonstarten
            // 
            this.buttonstarten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonstarten.Enabled = false;
            this.buttonstarten.Location = new System.Drawing.Point(458, 65);
            this.buttonstarten.Margin = new System.Windows.Forms.Padding(2);
            this.buttonstarten.Name = "buttonstarten";
            this.buttonstarten.Size = new System.Drawing.Size(82, 25);
            this.buttonstarten.TabIndex = 3;
            this.buttonstarten.Text = "Starten";
            this.buttonstarten.UseVisualStyleBackColor = true;
            this.buttonstarten.Click += new System.EventHandler(this.buttonstarten_Click);
            // 
            // labelstatus
            // 
            this.labelstatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelstatus.AutoSize = true;
            this.labelstatus.Location = new System.Drawing.Point(239, 457);
            this.labelstatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelstatus.Name = "labelstatus";
            this.labelstatus.Size = new System.Drawing.Size(81, 13);
            this.labelstatus.TabIndex = 4;
            this.labelstatus.Text = "Aktueller Status";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 457);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "label2";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Enabled = false;
            this.tabControl1.Location = new System.Drawing.Point(11, 119);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.MinimumSize = new System.Drawing.Size(378, 82);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(525, 331);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.treeView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(517, 305);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ordnerstruktur";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.labeltop100größe);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.labeltop100);
            this.tabPage2.Controls.Add(this.progressBartop100);
            this.tabPage2.Controls.Add(this.listBoxtop100);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(517, 305);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Top100";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Leave += new System.EventHandler(this.tabPage2_Leave);
            // 
            // labeltop100größe
            // 
            this.labeltop100größe.AutoSize = true;
            this.labeltop100größe.Location = new System.Drawing.Point(367, 15);
            this.labeltop100größe.Name = "labeltop100größe";
            this.labeltop100größe.Size = new System.Drawing.Size(79, 13);
            this.labeltop100größe.TabIndex = 12;
            this.labeltop100größe.Text = "Top100 Größe:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(294, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Selektiere Item und rechtsklicke auf Items für mehr Optionen.";
            // 
            // labeltop100
            // 
            this.labeltop100.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labeltop100.AutoSize = true;
            this.labeltop100.Location = new System.Drawing.Point(5, 245);
            this.labeltop100.Name = "labeltop100";
            this.labeltop100.Size = new System.Drawing.Size(35, 13);
            this.labeltop100.TabIndex = 10;
            this.labeltop100.Text = "label5";
            this.labeltop100.Visible = false;
            // 
            // progressBartop100
            // 
            this.progressBartop100.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBartop100.Location = new System.Drawing.Point(8, 216);
            this.progressBartop100.Name = "progressBartop100";
            this.progressBartop100.Size = new System.Drawing.Size(504, 26);
            this.progressBartop100.TabIndex = 9;
            this.progressBartop100.Visible = false;
            // 
            // listBoxtop100
            // 
            this.listBoxtop100.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxtop100.FormattingEnabled = true;
            this.listBoxtop100.Location = new System.Drawing.Point(5, 30);
            this.listBoxtop100.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxtop100.Name = "listBoxtop100";
            this.listBoxtop100.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxtop100.Size = new System.Drawing.Size(510, 277);
            this.listBoxtop100.TabIndex = 7;
            this.listBoxtop100.SelectedIndexChanged += new System.EventHandler(this.listBoxtop100_SelectedIndexChanged);
            this.listBoxtop100.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxtop100_KeyDown);
            this.listBoxtop100.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxtop100_MouseDoubleClick);
            this.listBoxtop100.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxtop100_MouseDown);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.labelduplikate);
            this.tabPage3.Controls.Add(this.progressBarduplikate);
            this.tabPage3.Controls.Add(this.buttongemeinsam);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.listBoxduplikate);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(517, 305);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "gefundene Duplikate";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Leave += new System.EventHandler(this.tabPage3_Leave);
            // 
            // labelduplikate
            // 
            this.labelduplikate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelduplikate.AutoSize = true;
            this.labelduplikate.Location = new System.Drawing.Point(3, 272);
            this.labelduplikate.Name = "labelduplikate";
            this.labelduplikate.Size = new System.Drawing.Size(35, 13);
            this.labelduplikate.TabIndex = 11;
            this.labelduplikate.Text = "label5";
            this.labelduplikate.Visible = false;
            // 
            // progressBarduplikate
            // 
            this.progressBarduplikate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarduplikate.Location = new System.Drawing.Point(6, 243);
            this.progressBarduplikate.Name = "progressBarduplikate";
            this.progressBarduplikate.Size = new System.Drawing.Size(508, 26);
            this.progressBarduplikate.TabIndex = 10;
            this.progressBarduplikate.Visible = false;
            // 
            // buttongemeinsam
            // 
            this.buttongemeinsam.Location = new System.Drawing.Point(397, 28);
            this.buttongemeinsam.Name = "buttongemeinsam";
            this.buttongemeinsam.Size = new System.Drawing.Size(117, 23);
            this.buttongemeinsam.TabIndex = 3;
            this.buttongemeinsam.Text = "Gemeinsame Ordner";
            this.buttongemeinsam.UseVisualStyleBackColor = true;
            this.buttongemeinsam.Click += new System.EventHandler(this.buttongemeinsam_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 33);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Alle Duplikate nehmen ein.";
            // 
            // listBoxduplikate
            // 
            this.listBoxduplikate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxduplikate.FormattingEnabled = true;
            this.listBoxduplikate.Location = new System.Drawing.Point(5, 56);
            this.listBoxduplikate.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxduplikate.Name = "listBoxduplikate";
            this.listBoxduplikate.Size = new System.Drawing.Size(509, 251);
            this.listBoxduplikate.TabIndex = 0;
            this.listBoxduplikate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxduplikate_MouseDoubleClick);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.checkBoxselbeDateityp);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.labelselbegröße);
            this.tabPage4.Controls.Add(this.progressBarselbergröße);
            this.tabPage4.Controls.Add(this.listBoxselbegröße);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(517, 305);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "mit selber Größe";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // checkBoxselbeDateityp
            // 
            this.checkBoxselbeDateityp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxselbeDateityp.AutoSize = true;
            this.checkBoxselbeDateityp.Location = new System.Drawing.Point(392, 7);
            this.checkBoxselbeDateityp.Name = "checkBoxselbeDateityp";
            this.checkBoxselbeDateityp.Size = new System.Drawing.Size(114, 17);
            this.checkBoxselbeDateityp.TabIndex = 14;
            this.checkBoxselbeDateityp.Text = "nur selber Dateityp";
            this.checkBoxselbeDateityp.UseVisualStyleBackColor = true;
            this.checkBoxselbeDateityp.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 14);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Alle Dateien selber Größe nehmen ein:";
            // 
            // labelselbegröße
            // 
            this.labelselbegröße.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelselbegröße.AutoSize = true;
            this.labelselbegröße.Location = new System.Drawing.Point(3, 247);
            this.labelselbegröße.Name = "labelselbegröße";
            this.labelselbegröße.Size = new System.Drawing.Size(35, 13);
            this.labelselbegröße.TabIndex = 12;
            this.labelselbegröße.Text = "label5";
            this.labelselbegröße.Visible = false;
            // 
            // progressBarselbergröße
            // 
            this.progressBarselbergröße.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarselbergröße.Location = new System.Drawing.Point(3, 218);
            this.progressBarselbergröße.Name = "progressBarselbergröße";
            this.progressBarselbergröße.Size = new System.Drawing.Size(503, 26);
            this.progressBarselbergröße.TabIndex = 10;
            this.progressBarselbergröße.Visible = false;
            // 
            // listBoxselbegröße
            // 
            this.listBoxselbegröße.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxselbegröße.FormattingEnabled = true;
            this.listBoxselbegröße.Location = new System.Drawing.Point(5, 29);
            this.listBoxselbegröße.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxselbegröße.Name = "listBoxselbegröße";
            this.listBoxselbegröße.Size = new System.Drawing.Size(501, 277);
            this.listBoxselbegröße.TabIndex = 8;
            this.listBoxselbegröße.DoubleClick += new System.EventHandler(this.listBoxselbegröße_DoubleClick);
            // 
            // checkBoxunterordner
            // 
            this.checkBoxunterordner.AutoSize = true;
            this.checkBoxunterordner.Location = new System.Drawing.Point(11, 67);
            this.checkBoxunterordner.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxunterordner.Name = "checkBoxunterordner";
            this.checkBoxunterordner.Size = new System.Drawing.Size(115, 17);
            this.checkBoxunterordner.TabIndex = 8;
            this.checkBoxunterordner.Text = "Unterordner prüfen";
            this.checkBoxunterordner.UseVisualStyleBackColor = true;
            // 
            // buttonfehler
            // 
            this.buttonfehler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonfehler.Location = new System.Drawing.Point(477, 450);
            this.buttonfehler.Margin = new System.Windows.Forms.Padding(2);
            this.buttonfehler.Name = "buttonfehler";
            this.buttonfehler.Size = new System.Drawing.Size(58, 19);
            this.buttonfehler.TabIndex = 9;
            this.buttonfehler.Text = "Fehler";
            this.buttonfehler.UseVisualStyleBackColor = true;
            this.buttonfehler.Visible = false;
            this.buttonfehler.Click += new System.EventHandler(this.buttonfehler_Click);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(11, 457);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(25, 13);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // checkedListBoxaufgaben
            // 
            this.checkedListBoxaufgaben.FormattingEnabled = true;
            this.checkedListBoxaufgaben.Items.AddRange(new object[] {
            "Ordnerstruktur",
            "Top100",
            "Duplikate"});
            this.checkedListBoxaufgaben.Location = new System.Drawing.Point(196, 65);
            this.checkedListBoxaufgaben.Name = "checkedListBoxaufgaben";
            this.checkedListBoxaufgaben.Size = new System.Drawing.Size(118, 49);
            this.checkedListBoxaufgaben.TabIndex = 11;
            this.checkedListBoxaufgaben.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxaufgaben_ItemCheck);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Aufgaben: ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ordnerstrukturSpeichernToolStripMenuItem,
            this.ignoriertePfadeToolStripMenuItem,
            this.leereOrdnerLöschenToolStripMenuItem,
            this.letzteAktionRückgängigToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(543, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ordnerstrukturSpeichernToolStripMenuItem
            // 
            this.ordnerstrukturSpeichernToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.speichernToolStripMenuItem,
            this.ladenToolStripMenuItem});
            this.ordnerstrukturSpeichernToolStripMenuItem.Name = "ordnerstrukturSpeichernToolStripMenuItem";
            this.ordnerstrukturSpeichernToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.ordnerstrukturSpeichernToolStripMenuItem.Text = "Sicherung";
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.speichernToolStripMenuItem.Text = "Speichern";
            this.speichernToolStripMenuItem.Click += new System.EventHandler(this.speichernToolStripMenuItem_Click);
            // 
            // ladenToolStripMenuItem
            // 
            this.ladenToolStripMenuItem.Name = "ladenToolStripMenuItem";
            this.ladenToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.ladenToolStripMenuItem.Text = "Laden";
            this.ladenToolStripMenuItem.Click += new System.EventHandler(this.ladenToolStripMenuItem_Click);
            // 
            // ignoriertePfadeToolStripMenuItem
            // 
            this.ignoriertePfadeToolStripMenuItem.Name = "ignoriertePfadeToolStripMenuItem";
            this.ignoriertePfadeToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
            this.ignoriertePfadeToolStripMenuItem.Text = "Ignorierte Pfade";
            this.ignoriertePfadeToolStripMenuItem.Click += new System.EventHandler(this.ignoriertePfadeToolStripMenuItem_Click);
            // 
            // leereOrdnerLöschenToolStripMenuItem
            // 
            this.leereOrdnerLöschenToolStripMenuItem.Enabled = false;
            this.leereOrdnerLöschenToolStripMenuItem.Name = "leereOrdnerLöschenToolStripMenuItem";
            this.leereOrdnerLöschenToolStripMenuItem.Size = new System.Drawing.Size(131, 20);
            this.leereOrdnerLöschenToolStripMenuItem.Text = "Leere Ordner löschen";
            this.leereOrdnerLöschenToolStripMenuItem.Click += new System.EventHandler(this.leereOrdnerLöschenToolStripMenuItem_Click);
            // 
            // letzteAktionRückgängigToolStripMenuItem
            // 
            this.letzteAktionRückgängigToolStripMenuItem.Name = "letzteAktionRückgängigToolStripMenuItem";
            this.letzteAktionRückgängigToolStripMenuItem.Size = new System.Drawing.Size(151, 20);
            this.letzteAktionRückgängigToolStripMenuItem.Text = "Letzte Aktion rückgängig";
            this.letzteAktionRückgängigToolStripMenuItem.Visible = false;
            this.letzteAktionRückgängigToolStripMenuItem.Click += new System.EventHandler(this.letzteAktionRückgängigToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(150, 48);
            this.contextMenuStrip1.Text = "nix";
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItem1.Text = "Ordner öffnen";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItem2.Text = "Datei öffnen";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 476);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkedListBoxaufgaben);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonfehler);
            this.Controls.Add(this.checkBoxunterordner);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelstatus);
            this.Controls.Add(this.buttonstarten);
            this.Controls.Add(this.textBoxpfad);
            this.Controls.Add(this.buttonpfad);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(551, 443);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Ordnerstruktur";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button buttonpfad;
        private System.Windows.Forms.TextBox textBoxpfad;
        private System.Windows.Forms.Button buttonstarten;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label labelstatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox listBoxtop100;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox listBoxduplikate;
        private System.Windows.Forms.CheckBox checkBoxunterordner;
        private System.Windows.Forms.Button buttonfehler;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListBox listBoxselbegröße;
        private System.Windows.Forms.Button buttongemeinsam;
        private System.Windows.Forms.CheckedListBox checkedListBoxaufgaben;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar progressBartop100;
        private System.Windows.Forms.ProgressBar progressBarduplikate;
        private System.Windows.Forms.ProgressBar progressBarselbergröße;
        private System.Windows.Forms.Label labeltop100;
        private System.Windows.Forms.Label labelduplikate;
        private System.Windows.Forms.Label labelselbegröße;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ordnerstrukturSpeichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem letzteAktionRückgängigToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ladenToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labeltop100größe;
        private System.Windows.Forms.CheckBox checkBoxselbeDateityp;
        private System.Windows.Forms.ToolStripMenuItem ignoriertePfadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leereOrdnerLöschenToolStripMenuItem;
    }
}

