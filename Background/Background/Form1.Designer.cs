namespace Background
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBoxtermine = new System.Windows.Forms.GroupBox();
            this.listViewtermine = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBoxaufgaben = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonneu = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonbearbeiten = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonentfernen = new System.Windows.Forms.ToolStripButton();
            this.listBoxaufgaben = new System.Windows.Forms.ListBox();
            this.buttonaktualisieren = new System.Windows.Forms.Button();
            this.buttongoogledrive = new System.Windows.Forms.Button();
            this.groupBoxnotizen = new System.Windows.Forms.GroupBox();
            this.richTextBoxnotizen = new System.Windows.Forms.RichTextBox();
            this.contextMenuStriptermin = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.timeraktuell = new System.Windows.Forms.Timer(this.components);
            this.buttontimer = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.timerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.voreinstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.musikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abspielenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dateiÄndernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortcutsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemusik = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.pCModusÄndernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.einstellungenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tMXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aufgabenlisteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelDateiÖffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aufgabenlisteZeigenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.missionenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStriptaskleiste = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.timerkeys = new System.Windows.Forms.Timer(this.components);
            this.timerterminfenster = new System.Windows.Forms.Timer(this.components);
            this.timermusik = new System.Windows.Forms.Timer(this.components);
            this.buttonhelp = new System.Windows.Forms.Button();
            this.groupBoxtermine.SuspendLayout();
            this.groupBoxaufgaben.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.groupBoxnotizen.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxtermine
            // 
            this.groupBoxtermine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxtermine.Controls.Add(this.listViewtermine);
            this.groupBoxtermine.Location = new System.Drawing.Point(12, 28);
            this.groupBoxtermine.Name = "groupBoxtermine";
            this.groupBoxtermine.Size = new System.Drawing.Size(468, 228);
            this.groupBoxtermine.TabIndex = 0;
            this.groupBoxtermine.TabStop = false;
            this.groupBoxtermine.Text = "Termine: ";
            // 
            // listViewtermine
            // 
            this.listViewtermine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewtermine.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewtermine.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewtermine.HideSelection = false;
            this.listViewtermine.Location = new System.Drawing.Point(6, 19);
            this.listViewtermine.MultiSelect = false;
            this.listViewtermine.Name = "listViewtermine";
            this.listViewtermine.ShowItemToolTips = true;
            this.listViewtermine.Size = new System.Drawing.Size(456, 204);
            this.listViewtermine.TabIndex = 0;
            this.listViewtermine.UseCompatibleStateImageBehavior = false;
            this.listViewtermine.View = System.Windows.Forms.View.Details;
            this.listViewtermine.DoubleClick += new System.EventHandler(this.listViewtermine_DoubleClick);
            this.listViewtermine.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewtermine_KeyDown);
            this.listViewtermine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listViewtermine_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Datum";
            this.columnHeader1.Width = 103;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Uhrzeit";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 76;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Grund";
            this.columnHeader3.Width = 361;
            // 
            // groupBoxaufgaben
            // 
            this.groupBoxaufgaben.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxaufgaben.Controls.Add(this.toolStrip1);
            this.groupBoxaufgaben.Controls.Add(this.listBoxaufgaben);
            this.groupBoxaufgaben.Location = new System.Drawing.Point(12, 265);
            this.groupBoxaufgaben.Name = "groupBoxaufgaben";
            this.groupBoxaufgaben.Size = new System.Drawing.Size(468, 150);
            this.groupBoxaufgaben.TabIndex = 0;
            this.groupBoxaufgaben.TabStop = false;
            this.groupBoxaufgaben.Text = "Aufgaben: ";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonneu,
            this.toolStripButtonbearbeiten,
            this.toolStripButtonentfernen});
            this.toolStrip1.Location = new System.Drawing.Point(435, 16);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStrip1.Size = new System.Drawing.Size(30, 131);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonneu
            // 
            this.toolStripButtonneu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonneu.Image = global::Background.Properties.Resources.add;
            this.toolStripButtonneu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonneu.Name = "toolStripButtonneu";
            this.toolStripButtonneu.Size = new System.Drawing.Size(25, 28);
            this.toolStripButtonneu.Text = "Hinzufügen";
            this.toolStripButtonneu.Click += new System.EventHandler(this.toolStripButtonneu_Click);
            // 
            // toolStripButtonbearbeiten
            // 
            this.toolStripButtonbearbeiten.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonbearbeiten.Enabled = false;
            this.toolStripButtonbearbeiten.Image = global::Background.Properties.Resources.arrow_refresh;
            this.toolStripButtonbearbeiten.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonbearbeiten.Name = "toolStripButtonbearbeiten";
            this.toolStripButtonbearbeiten.Size = new System.Drawing.Size(25, 28);
            this.toolStripButtonbearbeiten.Text = "Bearbeiten";
            this.toolStripButtonbearbeiten.Click += new System.EventHandler(this.toolStripButtonbearbeiten_Click);
            // 
            // toolStripButtonentfernen
            // 
            this.toolStripButtonentfernen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonentfernen.Enabled = false;
            this.toolStripButtonentfernen.Image = global::Background.Properties.Resources.delete;
            this.toolStripButtonentfernen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonentfernen.Name = "toolStripButtonentfernen";
            this.toolStripButtonentfernen.Size = new System.Drawing.Size(25, 28);
            this.toolStripButtonentfernen.Text = "Entfernen";
            this.toolStripButtonentfernen.Click += new System.EventHandler(this.toolStripButtonentfernen_Click);
            // 
            // listBoxaufgaben
            // 
            this.listBoxaufgaben.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxaufgaben.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxaufgaben.FormattingEnabled = true;
            this.listBoxaufgaben.ItemHeight = 15;
            this.listBoxaufgaben.Location = new System.Drawing.Point(6, 19);
            this.listBoxaufgaben.Name = "listBoxaufgaben";
            this.listBoxaufgaben.Size = new System.Drawing.Size(427, 94);
            this.listBoxaufgaben.TabIndex = 0;
            this.listBoxaufgaben.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxaufgaben_KeyDown);
            this.listBoxaufgaben.MouseDown += new System.Windows.Forms.MouseEventHandler(this.listBoxaufgaben_MouseDown);
            // 
            // buttonaktualisieren
            // 
            this.buttonaktualisieren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonaktualisieren.Location = new System.Drawing.Point(622, 423);
            this.buttonaktualisieren.Name = "buttonaktualisieren";
            this.buttonaktualisieren.Size = new System.Drawing.Size(86, 23);
            this.buttonaktualisieren.TabIndex = 1;
            this.buttonaktualisieren.Text = "Aktualisieren";
            this.buttonaktualisieren.UseVisualStyleBackColor = true;
            this.buttonaktualisieren.Click += new System.EventHandler(this.buttonaktualisieren_Click);
            // 
            // buttongoogledrive
            // 
            this.buttongoogledrive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttongoogledrive.Enabled = false;
            this.buttongoogledrive.Location = new System.Drawing.Point(532, 423);
            this.buttongoogledrive.Name = "buttongoogledrive";
            this.buttongoogledrive.Size = new System.Drawing.Size(86, 23);
            this.buttongoogledrive.TabIndex = 2;
            this.buttongoogledrive.Text = "Google Drive";
            this.buttongoogledrive.UseVisualStyleBackColor = true;
            this.buttongoogledrive.Click += new System.EventHandler(this.buttongoogledrive_Click);
            // 
            // groupBoxnotizen
            // 
            this.groupBoxnotizen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxnotizen.Controls.Add(this.richTextBoxnotizen);
            this.groupBoxnotizen.Location = new System.Drawing.Point(486, 28);
            this.groupBoxnotizen.Name = "groupBoxnotizen";
            this.groupBoxnotizen.Size = new System.Drawing.Size(222, 388);
            this.groupBoxnotizen.TabIndex = 3;
            this.groupBoxnotizen.TabStop = false;
            this.groupBoxnotizen.Text = "Notizen: ";
            // 
            // richTextBoxnotizen
            // 
            this.richTextBoxnotizen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxnotizen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxnotizen.Location = new System.Drawing.Point(6, 19);
            this.richTextBoxnotizen.Name = "richTextBoxnotizen";
            this.richTextBoxnotizen.Size = new System.Drawing.Size(210, 363);
            this.richTextBoxnotizen.TabIndex = 4;
            this.richTextBoxnotizen.Text = "";
            // 
            // contextMenuStriptermin
            // 
            this.contextMenuStriptermin.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStriptermin.Name = "contextMenuStriptermin";
            this.contextMenuStriptermin.Size = new System.Drawing.Size(61, 4);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 423);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // timeraktuell
            // 
            this.timeraktuell.Interval = 60000;
            this.timeraktuell.Tick += new System.EventHandler(this.timeraktuell_Tick);
            // 
            // buttontimer
            // 
            this.buttontimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttontimer.Location = new System.Drawing.Point(441, 423);
            this.buttontimer.Name = "buttontimer";
            this.buttontimer.Size = new System.Drawing.Size(86, 23);
            this.buttontimer.TabIndex = 6;
            this.buttontimer.Text = "Timer";
            this.buttontimer.UseVisualStyleBackColor = true;
            this.buttontimer.Click += new System.EventHandler(this.buttontimer_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timerToolStripMenuItem,
            this.musikToolStripMenuItem,
            this.pCModusÄndernToolStripMenuItem,
            this.einstellungenToolStripMenuItem,
            this.tMXToolStripMenuItem,
            this.aufgabenlisteToolStripMenuItem,
            this.missionenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(720, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // timerToolStripMenuItem
            // 
            this.timerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.voreinstellungenToolStripMenuItem});
            this.timerToolStripMenuItem.Name = "timerToolStripMenuItem";
            this.timerToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.timerToolStripMenuItem.Text = "Timer";
            // 
            // voreinstellungenToolStripMenuItem
            // 
            this.voreinstellungenToolStripMenuItem.Name = "voreinstellungenToolStripMenuItem";
            this.voreinstellungenToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.voreinstellungenToolStripMenuItem.Text = "Voreinstellungen";
            this.voreinstellungenToolStripMenuItem.Click += new System.EventHandler(this.voreinstellungenToolStripMenuItem_Click);
            // 
            // musikToolStripMenuItem
            // 
            this.musikToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abspielenToolStripMenuItem,
            this.dateiÄndernToolStripMenuItem,
            this.playlistToolStripMenuItem,
            this.shortcutsToolStripMenuItem,
            this.toolStripMenuItemusik,
            this.toolStripTextBox1});
            this.musikToolStripMenuItem.Name = "musikToolStripMenuItem";
            this.musikToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.musikToolStripMenuItem.Text = "Musik";
            // 
            // abspielenToolStripMenuItem
            // 
            this.abspielenToolStripMenuItem.Name = "abspielenToolStripMenuItem";
            this.abspielenToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.abspielenToolStripMenuItem.Text = "Playlist erstellen"; // 
            // dateiÄndernToolStripMenuItem
            // 
            this.dateiÄndernToolStripMenuItem.Name = "dateiÄndernToolStripMenuItem";
            this.dateiÄndernToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.dateiÄndernToolStripMenuItem.Text = "Datei ändern";
            this.dateiÄndernToolStripMenuItem.Click += new System.EventHandler(this.dateiÄndernToolStripMenuItem_Click);
            // 
            // playlistToolStripMenuItem
            // 
            this.playlistToolStripMenuItem.Name = "playlistToolStripMenuItem";
            this.playlistToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.playlistToolStripMenuItem.Text = "Playlist";
            // 
            // shortcutsToolStripMenuItem
            // 
            this.shortcutsToolStripMenuItem.Name = "shortcutsToolStripMenuItem";
            this.shortcutsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.shortcutsToolStripMenuItem.Text = "Shortcuts bearbeiten";
            this.shortcutsToolStripMenuItem.Click += new System.EventHandler(this.shortcutsToolStripMenuItem_Click);
            // 
            // toolStripMenuItemusik
            // 
            this.toolStripMenuItemusik.Name = "toolStripMenuItemusik";
            this.toolStripMenuItemusik.Size = new System.Drawing.Size(183, 22);
            this.toolStripMenuItemusik.Text = "00:00 min";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.ForeColor = System.Drawing.SystemColors.ScrollBar;
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBox1.Text = "Suchen";this.toolStripTextBox1.Click += new System.EventHandler(this.toolStripTextBox1_Click);
            // 
            // pCModusÄndernToolStripMenuItem
            // 
            this.pCModusÄndernToolStripMenuItem.Name = "pCModusÄndernToolStripMenuItem";
            this.pCModusÄndernToolStripMenuItem.Size = new System.Drawing.Size(114, 20);
            this.pCModusÄndernToolStripMenuItem.Text = "PC Modus ändern";
            this.pCModusÄndernToolStripMenuItem.Click += new System.EventHandler(this.pCModusÄndernToolStripMenuItem_Click);
            // 
            // einstellungenToolStripMenuItem
            // 
            this.einstellungenToolStripMenuItem.Name = "einstellungenToolStripMenuItem";
            this.einstellungenToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.einstellungenToolStripMenuItem.Text = "Einstellungen";
            this.einstellungenToolStripMenuItem.Click += new System.EventHandler(this.einstellungenToolStripMenuItem_Click);
            // 
            // tMXToolStripMenuItem
            // 
            this.tMXToolStripMenuItem.Name = "tMXToolStripMenuItem";
            this.tMXToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.tMXToolStripMenuItem.Text = "TMX";
            this.tMXToolStripMenuItem.Click += new System.EventHandler(this.tMXToolStripMenuItem_Click);
            // 
            // aufgabenlisteToolStripMenuItem
            // 
            this.aufgabenlisteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excelDateiÖffnenToolStripMenuItem,
            this.aufgabenlisteZeigenToolStripMenuItem});
            this.aufgabenlisteToolStripMenuItem.Name = "aufgabenlisteToolStripMenuItem";
            this.aufgabenlisteToolStripMenuItem.Size = new System.Drawing.Size(92, 20);
            this.aufgabenlisteToolStripMenuItem.Text = "Aufgabenliste";
            // 
            // excelDateiÖffnenToolStripMenuItem
            // 
            this.excelDateiÖffnenToolStripMenuItem.Name = "excelDateiÖffnenToolStripMenuItem";
            this.excelDateiÖffnenToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.excelDateiÖffnenToolStripMenuItem.Text = "Excel-Datei öffnen";
            this.excelDateiÖffnenToolStripMenuItem.Click += new System.EventHandler(this.excelDateiÖffnenToolStripMenuItem_Click);
            // 
            // aufgabenlisteZeigenToolStripMenuItem
            // 
            this.aufgabenlisteZeigenToolStripMenuItem.Name = "aufgabenlisteZeigenToolStripMenuItem";
            this.aufgabenlisteZeigenToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.aufgabenlisteZeigenToolStripMenuItem.Text = "Aufgabenliste zeigen";
            this.aufgabenlisteZeigenToolStripMenuItem.Click += new System.EventHandler(this.aufgabenlisteZeigenToolStripMenuItem_Click);
            // 
            // missionenToolStripMenuItem
            // 
            this.missionenToolStripMenuItem.Name = "missionenToolStripMenuItem";
            this.missionenToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.missionenToolStripMenuItem.Text = "Missionen";
            this.missionenToolStripMenuItem.Click += new System.EventHandler(this.missionenToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "BackGround Musik";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStriptaskleiste
            // 
            this.contextMenuStriptaskleiste.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStriptaskleiste.Name = "contextMenuStriptaskleiste";
            this.contextMenuStriptaskleiste.Size = new System.Drawing.Size(61, 4);
            this.contextMenuStriptaskleiste.Text = "BackGround";
            // 
            // timerkeys
            // 
            this.timerkeys.Interval = 80;
            // 
            // timerterminfenster
            // 
            this.timerterminfenster.Interval = 1000;
            this.timerterminfenster.Tick += new System.EventHandler(this.timerterminfenster_Tick);
            // 
            // timermusik
            // 
            this.timermusik.Interval = 1000;
            this.timermusik.Tick += new System.EventHandler(this.timermusik_Tick);
            // 
            // axWindowsMediaPlayer1
            // 
            // buttonhelp
            // 
            this.buttonhelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonhelp.Image = global::Background.Properties.Resources.help;
            this.buttonhelp.Location = new System.Drawing.Point(12, 418);
            this.buttonhelp.Name = "buttonhelp";
            this.buttonhelp.Size = new System.Drawing.Size(20, 20);
            this.buttonhelp.TabIndex = 4;
            this.buttonhelp.UseVisualStyleBackColor = true;
            this.buttonhelp.Click += new System.EventHandler(this.buttonhelp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 489);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.buttontimer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBoxaufgaben);
            this.Controls.Add(this.buttonaktualisieren);
            this.Controls.Add(this.buttonhelp);
            this.Controls.Add(this.groupBoxnotizen);
            this.Controls.Add(this.buttongoogledrive);
            this.Controls.Add(this.groupBoxtermine);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(734, 513);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "BackGround";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxtermine.ResumeLayout(false);
            this.groupBoxaufgaben.ResumeLayout(false);
            this.groupBoxaufgaben.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBoxnotizen.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxtermine;
        private System.Windows.Forms.GroupBox groupBoxaufgaben;
        private System.Windows.Forms.Button buttonaktualisieren;
        private System.Windows.Forms.Button buttongoogledrive;
        private System.Windows.Forms.ListView listViewtermine;
        private System.Windows.Forms.ListBox listBoxaufgaben;
        private System.Windows.Forms.GroupBox groupBoxnotizen;
        private System.Windows.Forms.RichTextBox richTextBoxnotizen;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Button buttonhelp;
        private System.Windows.Forms.ContextMenuStrip contextMenuStriptermin;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonneu;
        private System.Windows.Forms.ToolStripButton toolStripButtonbearbeiten;
        private System.Windows.Forms.ToolStripButton toolStripButtonentfernen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timeraktuell;
        private System.Windows.Forms.Button buttontimer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem timerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem musikToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abspielenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dateiÄndernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem voreinstellungenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playlistToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStriptaskleiste;
        private System.Windows.Forms.Timer timerkeys;
        private System.Windows.Forms.ToolStripMenuItem shortcutsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pCModusÄndernToolStripMenuItem;
        private System.Windows.Forms.Timer timerterminfenster;
        private System.Windows.Forms.ToolStripMenuItem einstellungenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tMXToolStripMenuItem;
        private System.Windows.Forms.Timer timermusik;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemusik;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripMenuItem aufgabenlisteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem missionenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelDateiÖffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aufgabenlisteZeigenToolStripMenuItem;
    }
}

