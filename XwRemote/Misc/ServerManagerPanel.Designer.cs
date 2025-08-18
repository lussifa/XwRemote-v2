namespace XwRemote
{
    partial class ServerManagerPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.treeServers = new System.Windows.Forms.TreeView();
            this.tabStrip1 = new Messir.Windows.Forms.TabStrip();
            this.FilterGrouped = new Messir.Windows.Forms.TabStripButton();
            this.FilterOrdered = new Messir.Windows.Forms.TabStripButton();
            this.FilterFavorites = new Messir.Windows.Forms.TabStripButton();
            this.contextGroup = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectBtn = new System.Windows.Forms.Button();
            this.NewFTP = new System.Windows.Forms.Button();
            this.NewRDP = new System.Windows.Forms.Button();
            this.NewVNC = new System.Windows.Forms.Button();
            this.NewSSH = new System.Windows.Forms.Button();
            this.EditServer = new System.Windows.Forms.Button();
            this.DeleteServer = new System.Windows.Forms.Button();
            this.contextServer = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.makeCopyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsRDP = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsSSH = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsFTP = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsSFTP = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsAWSS3 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsAzureFile = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsVNC = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAsIE = new System.Windows.Forms.ToolStripMenuItem();
            this.ExportServer = new System.Windows.Forms.ToolStripMenuItem();
            this.NewIE = new System.Windows.Forms.Button();
            this.NewSFTP = new System.Windows.Forms.Button();
            this.NewS3 = new System.Windows.Forms.Button();
            this.NewAzureFile = new System.Windows.Forms.Button();
            this.textSearch = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.headerLabel = new System.Windows.Forms.Label();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.tabStrip1.SuspendLayout();
            this.contextGroup.SuspendLayout();
            this.contextServer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.treeServers);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(250, 350);
            this.toolStripContainer1.Location = new System.Drawing.Point(3, 30);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(250, 375);
            this.toolStripContainer1.TabIndex = 0;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.tabStrip1);
            this.toolStripContainer1.TopToolStripPanel.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            // 
            // treeServers
            // 
            this.treeServers.AllowDrop = true;
            this.treeServers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeServers.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeServers.HideSelection = false;
            this.treeServers.LineColor = System.Drawing.Color.SkyBlue;
            this.treeServers.Location = new System.Drawing.Point(0, 0);
            this.treeServers.Name = "treeServers";
            this.treeServers.ShowNodeToolTips = true;
            this.treeServers.Size = new System.Drawing.Size(250, 350);
            this.treeServers.TabIndex = 0;
            this.treeServers.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.treeServers_AfterCollapse);
            this.treeServers.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.treeServers_AfterExpand);
            this.treeServers.DoubleClick += new System.EventHandler(this.treeServers_DoubleClick);
            this.treeServers.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tree_KeyDown);
            this.treeServers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeServers_MouseUp);
            // 
            // tabStrip1
            // 
            this.tabStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FilterGrouped,
            this.FilterOrdered,
            this.FilterFavorites});
            this.tabStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.tabStrip1.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tabStrip1.Location = new System.Drawing.Point(0, 0);
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.Size = new System.Drawing.Size(250, 25);
            this.tabStrip1.TabIndex = 0;
            this.tabStrip1.Text = "tabStrip1";
            // 
            // FilterGrouped
            // 
            this.FilterGrouped.Image = global::XwRemote.Properties.Resources.group;
            this.FilterGrouped.Name = "FilterGrouped";
            this.FilterGrouped.IsSelected = true;
            this.FilterGrouped.Size = new System.Drawing.Size(75, 25);
            this.FilterGrouped.Text = "Grouped";
            this.FilterGrouped.Click += new System.EventHandler(this.FilterGrouped_Click);
            // 
            // FilterOrdered
            // 
            this.FilterOrdered.Image = global::XwRemote.Properties.Resources.nogroup;
            this.FilterOrdered.Name = "FilterOrdered";
            this.FilterOrdered.Size = new System.Drawing.Size(70, 25);
            this.FilterOrdered.Text = "Ordered";
            this.FilterOrdered.Click += new System.EventHandler(this.FilterOrdered_Click);
            // 
            // FilterFavorites
            // 
            this.FilterFavorites.Image = global::XwRemote.Properties.Resources.favs;
            this.FilterFavorites.Name = "FilterFavorites";
            this.FilterFavorites.Size = new System.Drawing.Size(75, 25);
            this.FilterFavorites.Text = "Favorites";
            this.FilterFavorites.Click += new System.EventHandler(this.FilterFavorites_Click);
            // 
            // contextGroup
            // 
            this.contextGroup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGroupToolStripMenuItem,
            this.renameGroupToolStripMenuItem,
            this.deleteGroupToolStripMenuItem});
            this.contextGroup.Name = "contextGroup";
            this.contextGroup.Size = new System.Drawing.Size(148, 70);
            // 
            // addGroupToolStripMenuItem
            // 
            this.addGroupToolStripMenuItem.Name = "addGroupToolStripMenuItem";
            this.addGroupToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addGroupToolStripMenuItem.Text = "Add Group";
            this.addGroupToolStripMenuItem.Click += new System.EventHandler(this.AddGroup_Click);
            // 
            // renameGroupToolStripMenuItem
            // 
            this.renameGroupToolStripMenuItem.Name = "renameGroupToolStripMenuItem";
            this.renameGroupToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.renameGroupToolStripMenuItem.Text = "Rename Group";
            this.renameGroupToolStripMenuItem.Click += new System.EventHandler(this.RenameGroup_Click);
            // 
            // deleteGroupToolStripMenuItem
            // 
            this.deleteGroupToolStripMenuItem.Name = "deleteGroupToolStripMenuItem";
            this.deleteGroupToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.deleteGroupToolStripMenuItem.Text = "Delete Group";
            this.deleteGroupToolStripMenuItem.Click += new System.EventHandler(this.DeleteGroup_Click);
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConnectBtn.Image = global::XwRemote.Properties.Resources.connect;
            this.ConnectBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ConnectBtn.Location = new System.Drawing.Point(3, 411);
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(80, 23);
            this.ConnectBtn.TabIndex = 1;
            this.ConnectBtn.TabStop = false;
            this.ConnectBtn.Text = "Connect";
            this.ConnectBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ConnectBtn.UseVisualStyleBackColor = true;
            this.ConnectBtn.Click += new System.EventHandler(this.Connect_Click);
            // 
            // NewFTP
            // 
            this.NewFTP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewFTP.Image = global::XwRemote.Properties.Resources.ftp;
            this.NewFTP.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewFTP.Location = new System.Drawing.Point(89, 411);
            this.NewFTP.Name = "NewFTP";
            this.NewFTP.Size = new System.Drawing.Size(80, 23);
            this.NewFTP.TabIndex = 2;
            this.NewFTP.TabStop = false;
            this.NewFTP.Text = "New FTP";
            this.NewFTP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewFTP.UseVisualStyleBackColor = true;
            this.NewFTP.Click += new System.EventHandler(this.NewFTP_Click);
            // 
            // NewRDP
            // 
            this.NewRDP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewRDP.Image = global::XwRemote.Properties.Resources.rdp;
            this.NewRDP.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewRDP.Location = new System.Drawing.Point(175, 411);
            this.NewRDP.Name = "NewRDP";
            this.NewRDP.Size = new System.Drawing.Size(80, 23);
            this.NewRDP.TabIndex = 3;
            this.NewRDP.TabStop = false;
            this.NewRDP.Text = "New RDP";
            this.NewRDP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewRDP.UseVisualStyleBackColor = true;
            this.NewRDP.Click += new System.EventHandler(this.NewRDP_Click);
            // 
            // NewVNC
            // 
            this.NewVNC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewVNC.Image = global::XwRemote.Properties.Resources.vnc;
            this.NewVNC.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewVNC.Location = new System.Drawing.Point(3, 440);
            this.NewVNC.Name = "NewVNC";
            this.NewVNC.Size = new System.Drawing.Size(80, 23);
            this.NewVNC.TabIndex = 4;
            this.NewVNC.TabStop = false;
            this.NewVNC.Text = "New VNC";
            this.NewVNC.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewVNC.UseVisualStyleBackColor = true;
            this.NewVNC.Click += new System.EventHandler(this.NewVNC_Click);
            // 
            // NewSSH
            // 
            this.NewSSH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewSSH.Image = global::XwRemote.Properties.Resources.ssh;
            this.NewSSH.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewSSH.Location = new System.Drawing.Point(89, 440);
            this.NewSSH.Name = "NewSSH";
            this.NewSSH.Size = new System.Drawing.Size(80, 23);
            this.NewSSH.TabIndex = 5;
            this.NewSSH.TabStop = false;
            this.NewSSH.Text = "New SSH";
            this.NewSSH.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewSSH.UseVisualStyleBackColor = true;
            this.NewSSH.Click += new System.EventHandler(this.newSSH_Click);
            // 
            // EditServer
            // 
            this.EditServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EditServer.Image = global::XwRemote.Properties.Resources.pencil;
            this.EditServer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.EditServer.Location = new System.Drawing.Point(175, 440);
            this.EditServer.Name = "EditServer";
            this.EditServer.Size = new System.Drawing.Size(80, 23);
            this.EditServer.TabIndex = 6;
            this.EditServer.TabStop = false;
            this.EditServer.Text = "Edit";
            this.EditServer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.EditServer.UseVisualStyleBackColor = true;
            this.EditServer.Click += new System.EventHandler(this.EditServer_Click);
            // 
            // DeleteServer
            // 
            this.DeleteServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteServer.Image = global::XwRemote.Properties.Resources.delete;
            this.DeleteServer.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.DeleteServer.Location = new System.Drawing.Point(3, 469);
            this.DeleteServer.Name = "DeleteServer";
            this.DeleteServer.Size = new System.Drawing.Size(80, 23);
            this.DeleteServer.TabIndex = 7;
            this.DeleteServer.TabStop = false;
            this.DeleteServer.Text = "Delete";
            this.DeleteServer.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.DeleteServer.UseVisualStyleBackColor = true;
            this.DeleteServer.Click += new System.EventHandler(this.DeleteServer_Click);
            // 
            // contextServer
            // 
            this.contextServer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editServerToolStripMenuItem,
            this.deleteServerToolStripMenuItem,
            this.addToFavoritesToolStripMenuItem,
            this.toolStripSeparator3,
            this.makeCopyToolStripMenuItem,
            this.ExportServer});
            this.contextServer.Name = "contextServer";
            this.contextServer.Size = new System.Drawing.Size(161, 120);
            // 
            // editServerToolStripMenuItem
            // 
            this.editServerToolStripMenuItem.Name = "editServerToolStripMenuItem";
            this.editServerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.editServerToolStripMenuItem.Text = "Edit Server";
            this.editServerToolStripMenuItem.Click += new System.EventHandler(this.EditServer_Click);
            // 
            // deleteServerToolStripMenuItem
            // 
            this.deleteServerToolStripMenuItem.Name = "deleteServerToolStripMenuItem";
            this.deleteServerToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.deleteServerToolStripMenuItem.Text = "Delete Server";
            this.deleteServerToolStripMenuItem.Click += new System.EventHandler(this.DeleteServer_Click);
            // 
            // addToFavoritesToolStripMenuItem
            // 
            this.addToFavoritesToolStripMenuItem.Name = "addToFavoritesToolStripMenuItem";
            this.addToFavoritesToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.addToFavoritesToolStripMenuItem.Text = "Add to Favorites";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // makeCopyToolStripMenuItem
            // 
            this.makeCopyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyAsRDP,
            this.copyAsSSH,
            this.copyAsFTP,
            this.copyAsSFTP,
            this.copyAsAWSS3,
            this.copyAsAzureFile,
            this.copyAsVNC,
            this.copyAsIE});
            this.makeCopyToolStripMenuItem.Name = "makeCopyToolStripMenuItem";
            this.makeCopyToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.makeCopyToolStripMenuItem.Text = "Make Copy";
            // 
            // copyAsRDP
            // 
            this.copyAsRDP.Name = "copyAsRDP";
            this.copyAsRDP.Size = new System.Drawing.Size(140, 22);
            this.copyAsRDP.Text = "Copy as RDP";
            // 
            // copyAsSSH
            // 
            this.copyAsSSH.Name = "copyAsSSH";
            this.copyAsSSH.Size = new System.Drawing.Size(140, 22);
            this.copyAsSSH.Text = "Copy as SSH";
            // 
            // copyAsFTP
            // 
            this.copyAsFTP.Name = "copyAsFTP";
            this.copyAsFTP.Size = new System.Drawing.Size(140, 22);
            this.copyAsFTP.Text = "Copy as FTP";
            // 
            // copyAsSFTP
            // 
            this.copyAsSFTP.Name = "copyAsSFTP";
            this.copyAsSFTP.Size = new System.Drawing.Size(140, 22);
            this.copyAsSFTP.Text = "Copy as SFTP";
            // 
            // copyAsAWSS3
            // 
            this.copyAsAWSS3.Name = "copyAsAWSS3";
            this.copyAsAWSS3.Size = new System.Drawing.Size(140, 22);
            this.copyAsAWSS3.Text = "Copy as S3";
            // 
            // copyAsAzureFile
            // 
            this.copyAsAzureFile.Name = "copyAsAzureFile";
            this.copyAsAzureFile.Size = new System.Drawing.Size(140, 22);
            this.copyAsAzureFile.Text = "Copy as Azure";
            // 
            // copyAsVNC
            // 
            this.copyAsVNC.Name = "copyAsVNC";
            this.copyAsVNC.Size = new System.Drawing.Size(140, 22);
            this.copyAsVNC.Text = "Copy as VNC";
            // 
            // copyAsIE
            // 
            this.copyAsIE.Name = "copyAsIE";
            this.copyAsIE.Size = new System.Drawing.Size(140, 22);
            this.copyAsIE.Text = "Copy as IE";
            // 
            // ExportServer
            // 
            this.ExportServer.Name = "ExportServer";
            this.ExportServer.Size = new System.Drawing.Size(160, 22);
            this.ExportServer.Text = "Export Server";
            // 
            // NewIE
            // 
            this.NewIE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewIE.Image = global::XwRemote.Properties.Resources.IE;
            this.NewIE.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewIE.Location = new System.Drawing.Point(89, 469);
            this.NewIE.Name = "NewIE";
            this.NewIE.Size = new System.Drawing.Size(80, 23);
            this.NewIE.TabIndex = 8;
            this.NewIE.TabStop = false;
            this.NewIE.Text = "New IE";
            this.NewIE.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewIE.UseVisualStyleBackColor = true;
            this.NewIE.Click += new System.EventHandler(this.newIE_Click);
            // 
            // NewSFTP
            // 
            this.NewSFTP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewSFTP.Image = global::XwRemote.Properties.Resources.sftp;
            this.NewSFTP.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewSFTP.Location = new System.Drawing.Point(175, 469);
            this.NewSFTP.Name = "NewSFTP";
            this.NewSFTP.Size = new System.Drawing.Size(80, 23);
            this.NewSFTP.TabIndex = 9;
            this.NewSFTP.TabStop = false;
            this.NewSFTP.Text = "New SFTP";
            this.NewSFTP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewSFTP.UseVisualStyleBackColor = true;
            this.NewSFTP.Click += new System.EventHandler(this.NewSFTP_Click);
            // 
            // NewS3
            // 
            this.NewS3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewS3.Image = global::XwRemote.Properties.Resources.s3;
            this.NewS3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewS3.Location = new System.Drawing.Point(3, 498);
            this.NewS3.Name = "NewS3";
            this.NewS3.Size = new System.Drawing.Size(80, 23);
            this.NewS3.TabIndex = 10;
            this.NewS3.TabStop = false;
            this.NewS3.Text = "New S3";
            this.NewS3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewS3.UseVisualStyleBackColor = true;
            this.NewS3.Click += new System.EventHandler(this.NewS3_Click);
            // 
            // NewAzureFile
            // 
            this.NewAzureFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NewAzureFile.Image = global::XwRemote.Properties.Resources.azure;
            this.NewAzureFile.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.NewAzureFile.Location = new System.Drawing.Point(89, 498);
            this.NewAzureFile.Name = "NewAzureFile";
            this.NewAzureFile.Size = new System.Drawing.Size(80, 23);
            this.NewAzureFile.TabIndex = 11;
            this.NewAzureFile.TabStop = false;
            this.NewAzureFile.Text = "New Azure";
            this.NewAzureFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.NewAzureFile.UseVisualStyleBackColor = true;
            this.NewAzureFile.Click += new System.EventHandler(this.NewAzureFile_Click);
            // 
            // textSearch
            // 
            this.textSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textSearch.Location = new System.Drawing.Point(25, 527);
            this.textSearch.Name = "textSearch";
            this.textSearch.Size = new System.Drawing.Size(228, 20);
            this.textSearch.TabIndex = 12;
            this.textSearch.TextChanged += new System.EventHandler(this.textSearch_TextChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::XwRemote.Properties.Resources.magnifier;
            this.pictureBox1.Location = new System.Drawing.Point(3, 527);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 20);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // headerLabel
            // 
            this.headerLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.headerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.headerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(66)))), ((int)(((byte)(139)))));
            this.headerLabel.Location = new System.Drawing.Point(0, 0);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Padding = new System.Windows.Forms.Padding(5);
            this.headerLabel.Size = new System.Drawing.Size(256, 27);
            this.headerLabel.TabIndex = 14;
            this.headerLabel.Text = "Server Manager";
            this.headerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ServerManagerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.headerLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textSearch);
            this.Controls.Add(this.NewAzureFile);
            this.Controls.Add(this.NewS3);
            this.Controls.Add(this.NewSFTP);
            this.Controls.Add(this.NewIE);
            this.Controls.Add(this.DeleteServer);
            this.Controls.Add(this.EditServer);
            this.Controls.Add(this.NewSSH);
            this.Controls.Add(this.NewVNC);
            this.Controls.Add(this.NewRDP);
            this.Controls.Add(this.NewFTP);
            this.Controls.Add(this.ConnectBtn);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "ServerManagerPanel";
            this.Size = new System.Drawing.Size(256, 550);
            this.Load += new System.EventHandler(this.OnLoad);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.tabStrip1.ResumeLayout(false);
            this.tabStrip1.PerformLayout();
            this.contextGroup.ResumeLayout(false);
            this.contextServer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.TreeView treeServers;
        private Messir.Windows.Forms.TabStrip tabStrip1;
        private Messir.Windows.Forms.TabStripButton FilterGrouped;
        private Messir.Windows.Forms.TabStripButton FilterOrdered;
        private Messir.Windows.Forms.TabStripButton FilterFavorites;
        private System.Windows.Forms.ContextMenuStrip contextGroup;
        private System.Windows.Forms.ToolStripMenuItem addGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteGroupToolStripMenuItem;
        private System.Windows.Forms.Button ConnectBtn;
        private System.Windows.Forms.Button NewFTP;
        private System.Windows.Forms.Button NewRDP;
        private System.Windows.Forms.Button NewVNC;
        private System.Windows.Forms.Button NewSSH;
        private System.Windows.Forms.Button EditServer;
        private System.Windows.Forms.Button DeleteServer;
        private System.Windows.Forms.ContextMenuStrip contextServer;
        private System.Windows.Forms.ToolStripMenuItem editServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToFavoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem makeCopyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAsRDP;
        private System.Windows.Forms.ToolStripMenuItem copyAsSSH;
        private System.Windows.Forms.ToolStripMenuItem copyAsFTP;
        private System.Windows.Forms.ToolStripMenuItem copyAsSFTP;
        private System.Windows.Forms.ToolStripMenuItem copyAsAWSS3;
        private System.Windows.Forms.ToolStripMenuItem copyAsAzureFile;
        private System.Windows.Forms.ToolStripMenuItem copyAsVNC;
        private System.Windows.Forms.ToolStripMenuItem copyAsIE;
        private System.Windows.Forms.ToolStripMenuItem ExportServer;
        private System.Windows.Forms.Button NewIE;
        private System.Windows.Forms.Button NewSFTP;
        private System.Windows.Forms.Button NewS3;
        private System.Windows.Forms.Button NewAzureFile;
        private System.Windows.Forms.TextBox textSearch;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label headerLabel;
    }
}