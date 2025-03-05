namespace TaskBarFolder
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsFolderBrowser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbFolderName = new System.Windows.Forms.TextBox();
            this.btnGridViewDown = new System.Windows.Forms.Button();
            this.btnGridViewUp = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dgvIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.dgvName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGridViewAdd = new System.Windows.Forms.Button();
            this.btnGridViewName = new System.Windows.Forms.Button();
            this.btnGridViewDefaultIcon = new System.Windows.Forms.Button();
            this.btnGridViewDelete = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnIco = new System.Windows.Forms.Button();
            this.btnIcoDefault = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbToolTips = new System.Windows.Forms.CheckBox();
            this.cbFileExt = new System.Windows.Forms.CheckBox();
            this.cbHidden = new System.Windows.Forms.CheckBox();
            this.cbSmallIcons = new System.Windows.Forms.CheckBox();
            this.imageListSmall = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "file.png");
            this.imageList1.Images.SetKeyName(2, "zip.png");
            this.imageList1.Images.SetKeyName(3, "lock.png");
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "TaskBarFolder";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            this.notifyIcon1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(117, 54);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // cmsFolderBrowser
            // 
            this.cmsFolderBrowser.AllowDrop = true;
            this.cmsFolderBrowser.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsFolderBrowser.Name = "contextMenuStrip1";
            this.cmsFolderBrowser.Size = new System.Drawing.Size(61, 4);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbFolderName);
            this.groupBox1.Controls.Add(this.btnGridViewDown);
            this.groupBox1.Controls.Add(this.btnGridViewUp);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.btnGridViewAdd);
            this.groupBox1.Controls.Add(this.btnGridViewName);
            this.groupBox1.Controls.Add(this.btnGridViewDefaultIcon);
            this.groupBox1.Controls.Add(this.btnGridViewDelete);
            this.groupBox1.Location = new System.Drawing.Point(12, 99);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 257);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folders";
            // 
            // tbFolderName
            // 
            this.tbFolderName.Location = new System.Drawing.Point(7, 231);
            this.tbFolderName.MaxLength = 20;
            this.tbFolderName.Name = "tbFolderName";
            this.tbFolderName.Size = new System.Drawing.Size(153, 20);
            this.tbFolderName.TabIndex = 10;
            this.tbFolderName.WordWrap = false;
            // 
            // btnGridViewDown
            // 
            this.btnGridViewDown.Enabled = false;
            this.btnGridViewDown.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnGridViewDown.Location = new System.Drawing.Point(328, 113);
            this.btnGridViewDown.Name = "btnGridViewDown";
            this.btnGridViewDown.Size = new System.Drawing.Size(30, 31);
            this.btnGridViewDown.TabIndex = 8;
            this.btnGridViewDown.Text = "ê";
            this.btnGridViewDown.UseVisualStyleBackColor = true;
            this.btnGridViewDown.Click += new System.EventHandler(this.btnGridViewDown_Click);
            // 
            // btnGridViewUp
            // 
            this.btnGridViewUp.Enabled = false;
            this.btnGridViewUp.Font = new System.Drawing.Font("Wingdings", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.btnGridViewUp.Location = new System.Drawing.Point(328, 76);
            this.btnGridViewUp.Name = "btnGridViewUp";
            this.btnGridViewUp.Size = new System.Drawing.Size(30, 31);
            this.btnGridViewUp.TabIndex = 8;
            this.btnGridViewUp.Text = "é";
            this.btnGridViewUp.UseVisualStyleBackColor = true;
            this.btnGridViewUp.Click += new System.EventHandler(this.btnGridViewUp_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvIcon,
            this.dgvName,
            this.dgvPath});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 30;
            this.dataGridView1.Size = new System.Drawing.Size(319, 177);
            this.dataGridView1.TabIndex = 9;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentDoubleClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragDrop);
            this.dataGridView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.dataGridView1_DragEnter);
            // 
            // dgvIcon
            // 
            this.dgvIcon.HeaderText = "Icon";
            this.dgvIcon.Image = global::TaskBarFolder.Properties.Resources.folder;
            this.dgvIcon.Name = "dgvIcon";
            this.dgvIcon.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIcon.Width = 34;
            // 
            // dgvName
            // 
            this.dgvName.HeaderText = "Name";
            this.dgvName.Name = "dgvName";
            this.dgvName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvName.Width = 41;
            // 
            // dgvPath
            // 
            this.dgvPath.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgvPath.HeaderText = "Path";
            this.dgvPath.Name = "dgvPath";
            this.dgvPath.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // btnGridViewAdd
            // 
            this.btnGridViewAdd.Location = new System.Drawing.Point(250, 202);
            this.btnGridViewAdd.Name = "btnGridViewAdd";
            this.btnGridViewAdd.Size = new System.Drawing.Size(75, 23);
            this.btnGridViewAdd.TabIndex = 7;
            this.btnGridViewAdd.Text = "add folder";
            this.btnGridViewAdd.UseVisualStyleBackColor = true;
            this.btnGridViewAdd.Click += new System.EventHandler(this.btnGridViewAdd_Click);
            // 
            // btnGridViewName
            // 
            this.btnGridViewName.Enabled = false;
            this.btnGridViewName.Location = new System.Drawing.Point(166, 229);
            this.btnGridViewName.Name = "btnGridViewName";
            this.btnGridViewName.Size = new System.Drawing.Size(75, 23);
            this.btnGridViewName.TabIndex = 6;
            this.btnGridViewName.Text = "set name";
            this.btnGridViewName.UseVisualStyleBackColor = true;
            this.btnGridViewName.Click += new System.EventHandler(this.btnGridViewName_Click);
            // 
            // btnGridViewDefaultIcon
            // 
            this.btnGridViewDefaultIcon.Enabled = false;
            this.btnGridViewDefaultIcon.Location = new System.Drawing.Point(6, 202);
            this.btnGridViewDefaultIcon.Name = "btnGridViewDefaultIcon";
            this.btnGridViewDefaultIcon.Size = new System.Drawing.Size(75, 23);
            this.btnGridViewDefaultIcon.TabIndex = 6;
            this.btnGridViewDefaultIcon.Text = "default icon";
            this.btnGridViewDefaultIcon.UseVisualStyleBackColor = true;
            this.btnGridViewDefaultIcon.Click += new System.EventHandler(this.btnGridViewDefaultIcon_Click);
            // 
            // btnGridViewDelete
            // 
            this.btnGridViewDelete.Enabled = false;
            this.btnGridViewDelete.Location = new System.Drawing.Point(166, 202);
            this.btnGridViewDelete.Name = "btnGridViewDelete";
            this.btnGridViewDelete.Size = new System.Drawing.Size(75, 23);
            this.btnGridViewDelete.TabIndex = 7;
            this.btnGridViewDelete.Text = "delete folder";
            this.btnGridViewDelete.UseVisualStyleBackColor = true;
            this.btnGridViewDelete.Click += new System.EventHandler(this.btnGridViewDelete_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMinimize.Location = new System.Drawing.Point(349, 5);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.btnMinimize.Size = new System.Drawing.Size(30, 27);
            this.btnMinimize.TabIndex = 5;
            this.btnMinimize.Text = "¯¯¯";
            this.btnMinimize.UseVisualStyleBackColor = true;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnIco
            // 
            this.btnIco.BackColor = System.Drawing.Color.Transparent;
            this.btnIco.BackgroundImage = global::TaskBarFolder.Properties.Resources.folder;
            this.btnIco.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnIco.Location = new System.Drawing.Point(6, 20);
            this.btnIco.Name = "btnIco";
            this.btnIco.Size = new System.Drawing.Size(45, 44);
            this.btnIco.TabIndex = 3;
            this.btnIco.UseVisualStyleBackColor = false;
            this.btnIco.Click += new System.EventHandler(this.btnIco_Click);
            // 
            // btnIcoDefault
            // 
            this.btnIcoDefault.Location = new System.Drawing.Point(57, 31);
            this.btnIcoDefault.Name = "btnIcoDefault";
            this.btnIcoDefault.Size = new System.Drawing.Size(75, 23);
            this.btnIcoDefault.TabIndex = 6;
            this.btnIcoDefault.Text = "default icon";
            this.btnIcoDefault.UseVisualStyleBackColor = true;
            this.btnIcoDefault.Click += new System.EventHandler(this.btnIcoDefault_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnIcoDefault);
            this.groupBox2.Controls.Add(this.btnIco);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(149, 81);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Taskbar-Icon";
            // 
            // cbToolTips
            // 
            this.cbToolTips.AutoSize = true;
            this.cbToolTips.Location = new System.Drawing.Point(182, 20);
            this.cbToolTips.Name = "cbToolTips";
            this.cbToolTips.Size = new System.Drawing.Size(87, 17);
            this.cbToolTips.TabIndex = 8;
            this.cbToolTips.Text = "show tooltips";
            this.cbToolTips.UseVisualStyleBackColor = true;
            this.cbToolTips.Click += new System.EventHandler(this.cbToolTips_Click);
            // 
            // cbFileExt
            // 
            this.cbFileExt.AutoSize = true;
            this.cbFileExt.Location = new System.Drawing.Point(182, 38);
            this.cbFileExt.Name = "cbFileExt";
            this.cbFileExt.Size = new System.Drawing.Size(120, 17);
            this.cbFileExt.TabIndex = 8;
            this.cbFileExt.Text = "show file extensions";
            this.cbFileExt.UseVisualStyleBackColor = true;
            this.cbFileExt.Click += new System.EventHandler(this.cbFileExt_Click);
            // 
            // cbHidden
            // 
            this.cbHidden.AutoSize = true;
            this.cbHidden.Location = new System.Drawing.Point(182, 56);
            this.cbHidden.Name = "cbHidden";
            this.cbHidden.Size = new System.Drawing.Size(107, 17);
            this.cbHidden.TabIndex = 8;
            this.cbHidden.Text = "show hidden files";
            this.cbHidden.UseVisualStyleBackColor = true;
            this.cbHidden.Click += new System.EventHandler(this.cbHidden_Click);
            // 
            // cbSmallIcons
            // 
            this.cbSmallIcons.AutoSize = true;
            this.cbSmallIcons.Location = new System.Drawing.Point(182, 74);
            this.cbSmallIcons.Name = "cbSmallIcons";
            this.cbSmallIcons.Size = new System.Drawing.Size(115, 17);
            this.cbSmallIcons.TabIndex = 8;
            this.cbSmallIcons.Text = "small icons (16x16)";
            this.cbSmallIcons.UseVisualStyleBackColor = true;
            this.cbSmallIcons.Click += new System.EventHandler(this.cbSmallIcons_Click);
            // 
            // imageListSmall
            // 
            this.imageListSmall.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListSmall.ImageStream")));
            this.imageListSmall.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListSmall.Images.SetKeyName(0, "folder.png");
            this.imageListSmall.Images.SetKeyName(1, "file.png");
            this.imageListSmall.Images.SetKeyName(2, "zip.png");
            this.imageListSmall.Images.SetKeyName(3, "lock.png");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(386, 368);
            this.ControlBox = false;
            this.Controls.Add(this.cbSmallIcons);
            this.Controls.Add(this.cbHidden);
            this.Controls.Add(this.cbFileExt);
            this.Controls.Add(this.cbToolTips);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(338, 273);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "TaskBarFolder";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        public System.Windows.Forms.ContextMenuStrip cmsFolderBrowser;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnIco;
        private System.Windows.Forms.Button btnIcoDefault;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGridViewUp;
        private System.Windows.Forms.Button btnGridViewDown;
        private System.Windows.Forms.CheckBox cbToolTips;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnGridViewAdd;
        private System.Windows.Forms.Button btnGridViewDefaultIcon;
        private System.Windows.Forms.Button btnGridViewDelete;
        private System.Windows.Forms.DataGridViewImageColumn dgvIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPath;
        private System.Windows.Forms.CheckBox cbFileExt;
        private System.Windows.Forms.CheckBox cbHidden;
        private System.Windows.Forms.CheckBox cbSmallIcons;
        private System.Windows.Forms.ImageList imageListSmall;
        private System.Windows.Forms.TextBox tbFolderName;
        private System.Windows.Forms.Button btnGridViewName;
    }
}

