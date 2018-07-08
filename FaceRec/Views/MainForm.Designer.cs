namespace FaceRec.Views
{
    partial class MainForm
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
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.recognitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitToolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureRecognitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.videoRecognitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitToolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.systemOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainPictureBox = new System.Windows.Forms.PictureBox();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.statusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.speedToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.picturePanel = new System.Windows.Forms.Panel();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).BeginInit();
            this.mainStatusStrip.SuspendLayout();
            this.picturePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recognitionToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.HelpToolStripMenuItem,
            this.loginToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(993, 25);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "菜单栏";
            // 
            // recognitionToolStripMenuItem
            // 
            this.recognitionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.splitToolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.recognitionToolStripMenuItem.Name = "recognitionToolStripMenuItem";
            this.recognitionToolStripMenuItem.Size = new System.Drawing.Size(58, 21);
            this.recognitionToolStripMenuItem.Text = "识别(&F)";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.startToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.startToolStripMenuItem.Text = "开始(&S)";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.P)));
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.stopToolStripMenuItem.Text = "停止(&P)";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // splitToolStripMenuItem1
            // 
            this.splitToolStripMenuItem1.Name = "splitToolStripMenuItem1";
            this.splitToolStripMenuItem1.Size = new System.Drawing.Size(153, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.exitToolStripMenuItem.Text = "退出(&X)";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pictureRecognitionToolStripMenuItem,
            this.videoRecognitionToolStripMenuItem,
            this.splitToolStripMenuItem2,
            this.systemOptionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.toolsToolStripMenuItem.Text = "工具(&T)";
            // 
            // pictureRecognitionToolStripMenuItem
            // 
            this.pictureRecognitionToolStripMenuItem.Name = "pictureRecognitionToolStripMenuItem";
            this.pictureRecognitionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            this.pictureRecognitionToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.pictureRecognitionToolStripMenuItem.Text = "图片识别(&M)...";
            this.pictureRecognitionToolStripMenuItem.Click += new System.EventHandler(this.pictureRecognitionToolStripMenuItem_Click);
            // 
            // videoRecognitionToolStripMenuItem
            // 
            this.videoRecognitionToolStripMenuItem.Name = "videoRecognitionToolStripMenuItem";
            this.videoRecognitionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
            this.videoRecognitionToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.videoRecognitionToolStripMenuItem.Text = "视频识别(&V)...";
            this.videoRecognitionToolStripMenuItem.Click += new System.EventHandler(this.videoRecognitionToolStripMenuItem_Click);
            // 
            // splitToolStripMenuItem2
            // 
            this.splitToolStripMenuItem2.Name = "splitToolStripMenuItem2";
            this.splitToolStripMenuItem2.Size = new System.Drawing.Size(194, 6);
            // 
            // systemOptionsToolStripMenuItem
            // 
            this.systemOptionsToolStripMenuItem.Name = "systemOptionsToolStripMenuItem";
            this.systemOptionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.O)));
            this.systemOptionsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.systemOptionsToolStripMenuItem.Text = "系统设置(&O)...";
            this.systemOptionsToolStripMenuItem.Click += new System.EventHandler(this.systemOptionsToolStripMenuItem_Click);
            // 
            // HelpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(61, 21);
            this.HelpToolStripMenuItem.Text = "帮助(&H)";
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.manualToolStripMenuItem.Text = "使用帮助(&F1)";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.aboutToolStripMenuItem.Text = "关于(&A)";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(67, 21);
            this.loginToolStripMenuItem.Text = "登录(&L)...";
            // 
            // mainPictureBox
            // 
            this.mainPictureBox.Location = new System.Drawing.Point(19, 3);
            this.mainPictureBox.Name = "mainPictureBox";
            this.mainPictureBox.Size = new System.Drawing.Size(500, 400);
            this.mainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.mainPictureBox.TabIndex = 1;
            this.mainPictureBox.TabStop = false;
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusToolStripStatusLabel,
            this.speedToolStripStatusLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 478);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(993, 22);
            this.mainStatusStrip.TabIndex = 2;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // statusToolStripStatusLabel
            // 
            this.statusToolStripStatusLabel.Name = "statusToolStripStatusLabel";
            this.statusToolStripStatusLabel.Size = new System.Drawing.Size(35, 17);
            this.statusToolStripStatusLabel.Text = "状态:";
            // 
            // speedToolStripStatusLabel
            // 
            this.speedToolStripStatusLabel.Name = "speedToolStripStatusLabel";
            this.speedToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // picturePanel
            // 
            this.picturePanel.AutoScroll = true;
            this.picturePanel.Controls.Add(this.mainPictureBox);
            this.picturePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picturePanel.Location = new System.Drawing.Point(0, 25);
            this.picturePanel.Name = "picturePanel";
            this.picturePanel.Size = new System.Drawing.Size(993, 453);
            this.picturePanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(993, 500);
            this.Controls.Add(this.picturePanel);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenuStrip);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "慧眼人脸智能识别系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainPictureBox)).EndInit();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.picturePanel.ResumeLayout(false);
            this.picturePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem recognitionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator splitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.PictureBox mainPictureBox;
        private System.Windows.Forms.ToolStripMenuItem systemOptionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pictureRecognitionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem videoRecognitionToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator splitToolStripMenuItem2;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel speedToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel statusToolStripStatusLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Panel picturePanel;
    }
}