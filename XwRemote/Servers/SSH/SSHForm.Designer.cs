namespace XwRemote.Servers
{
    partial class SSHForm
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
            if (disposing)
            {
                this.StopResourceMonitor();
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.loadingCircle1 = new MRG.Controls.UI.LoadingCircle();
            this.statusLabel = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.statusBarPanel = new System.Windows.Forms.Panel();
            this.resourceFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.cpuStatusLabel = new System.Windows.Forms.Label();
            this.memoryStatusLabel = new System.Windows.Forms.Label();
            this.diskStatusLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            this.statusBarPanel.SuspendLayout();
            this.resourceFlowPanel.SuspendLayout();
            // 
            // loadingCircle1
            // 
            this.loadingCircle1.Active = false;
            this.loadingCircle1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.loadingCircle1.Color = System.Drawing.Color.DarkGray;
            this.loadingCircle1.InnerCircleRadius = 5;
            this.loadingCircle1.Location = new System.Drawing.Point(300, 223);
            this.loadingCircle1.Name = "loadingCircle1";
            this.loadingCircle1.NumberSpoke = 12;
            this.loadingCircle1.OuterCircleRadius = 11;
            this.loadingCircle1.RotationSpeed = 100;
            this.loadingCircle1.Size = new System.Drawing.Size(75, 69);
            this.loadingCircle1.SpokeThickness = 2;
            this.loadingCircle1.StylePreset = MRG.Controls.UI.LoadingCircle.StylePresets.MacOSX;
            this.loadingCircle1.TabIndex = 1;
            this.loadingCircle1.Text = "loadingCircle1";
            this.loadingCircle1.Visible = false;
            //
            // statusLabel
            //
            this.statusLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(305, 163);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(70, 13);
            this.statusLabel.TabIndex = 2;
            this.statusLabel.Text = "Connecting...";
            this.statusLabel.Visible = false;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonConnect.Location = new System.Drawing.Point(280, 298);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(114, 35);
            this.buttonConnect.TabIndex = 3;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            //
            // statusBarPanel
            //
            this.statusBarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(43)))));
            this.statusBarPanel.Controls.Add(this.resourceFlowPanel);
            this.statusBarPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBarPanel.Location = new System.Drawing.Point(0, 469);
            this.statusBarPanel.Name = "statusBarPanel";
            this.statusBarPanel.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.statusBarPanel.Size = new System.Drawing.Size(676, 30);
            this.statusBarPanel.TabIndex = 4;
            this.statusBarPanel.Visible = false;
            //
            // resourceFlowPanel
            //
            this.resourceFlowPanel.AutoSize = true;
            this.resourceFlowPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resourceFlowPanel.BackColor = System.Drawing.Color.Transparent;
            this.resourceFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resourceFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.resourceFlowPanel.Location = new System.Drawing.Point(12, 6);
            this.resourceFlowPanel.Name = "resourceFlowPanel";
            this.resourceFlowPanel.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.resourceFlowPanel.Size = new System.Drawing.Size(652, 18);
            this.resourceFlowPanel.TabIndex = 0;
            this.resourceFlowPanel.WrapContents = false;
            this.resourceFlowPanel.Controls.Add(this.cpuStatusLabel);
            this.resourceFlowPanel.Controls.Add(this.memoryStatusLabel);
            this.resourceFlowPanel.Controls.Add(this.diskStatusLabel);
            //
            // cpuStatusLabel
            //
            this.cpuStatusLabel.AutoSize = true;
            this.cpuStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cpuStatusLabel.ForeColor = System.Drawing.Color.White;
            this.cpuStatusLabel.Location = new System.Drawing.Point(3, 0);
            this.cpuStatusLabel.Margin = new System.Windows.Forms.Padding(3, 0, 18, 0);
            this.cpuStatusLabel.Name = "cpuStatusLabel";
            this.cpuStatusLabel.Size = new System.Drawing.Size(56, 15);
            this.cpuStatusLabel.TabIndex = 0;
            this.cpuStatusLabel.Text = "CPU: --";
            //
            // memoryStatusLabel
            //
            this.memoryStatusLabel.AutoSize = true;
            this.memoryStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memoryStatusLabel.ForeColor = System.Drawing.Color.White;
            this.memoryStatusLabel.Location = new System.Drawing.Point(80, 0);
            this.memoryStatusLabel.Margin = new System.Windows.Forms.Padding(3, 0, 18, 0);
            this.memoryStatusLabel.Name = "memoryStatusLabel";
            this.memoryStatusLabel.Size = new System.Drawing.Size(84, 15);
            this.memoryStatusLabel.TabIndex = 1;
            this.memoryStatusLabel.Text = "Memory: --";
            //
            // diskStatusLabel
            //
            this.diskStatusLabel.AutoSize = true;
            this.diskStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.diskStatusLabel.ForeColor = System.Drawing.Color.White;
            this.diskStatusLabel.Location = new System.Drawing.Point(182, 0);
            this.diskStatusLabel.Margin = new System.Windows.Forms.Padding(3, 0, 18, 0);
            this.diskStatusLabel.Name = "diskStatusLabel";
            this.diskStatusLabel.Size = new System.Drawing.Size(62, 15);
            this.diskStatusLabel.TabIndex = 2;
            this.diskStatusLabel.Text = "Disk: --";
            //
            // SSHForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(676, 499);
            this.Controls.Add(this.statusBarPanel);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.loadingCircle1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SSHForm";
            this.Text = "SSHForm";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Shown += new System.EventHandler(this.OnShown);
            this.resourceFlowPanel.ResumeLayout(false);
            this.resourceFlowPanel.PerformLayout();
            this.statusBarPanel.ResumeLayout(false);
            this.statusBarPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MRG.Controls.UI.LoadingCircle loadingCircle1;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Panel statusBarPanel;
        private System.Windows.Forms.FlowLayoutPanel resourceFlowPanel;
        private System.Windows.Forms.Label cpuStatusLabel;
        private System.Windows.Forms.Label memoryStatusLabel;
        private System.Windows.Forms.Label diskStatusLabel;
    }
}