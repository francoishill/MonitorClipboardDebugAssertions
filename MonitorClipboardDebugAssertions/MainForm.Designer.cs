namespace MonitorClipboardDebugAssertions
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.notifyIconTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenuStripTrayIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.changeProcessnameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeProcessargumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changerootAlbionDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.contextMenuStripTrayIcon.SuspendLayout();
			this.SuspendLayout();
			// 
			// notifyIconTrayIcon
			// 
			this.notifyIconTrayIcon.ContextMenuStrip = this.contextMenuStripTrayIcon;
			this.notifyIconTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconTrayIcon.Icon")));
			this.notifyIconTrayIcon.Text = "Monitoring clipboard for debug assertions";
			this.notifyIconTrayIcon.Visible = true;
			// 
			// contextMenuStripTrayIcon
			// 
			this.contextMenuStripTrayIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeProcessnameToolStripMenuItem,
            this.changeProcessargumentsToolStripMenuItem,
            this.changerootAlbionDirectoryToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
			this.contextMenuStripTrayIcon.Name = "contextMenuStripTrayIcon";
			this.contextMenuStripTrayIcon.Size = new System.Drawing.Size(229, 148);
			// 
			// changeProcessnameToolStripMenuItem
			// 
			this.changeProcessnameToolStripMenuItem.Name = "changeProcessnameToolStripMenuItem";
			this.changeProcessnameToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.changeProcessnameToolStripMenuItem.Text = "Change process &name";
			this.changeProcessnameToolStripMenuItem.Click += new System.EventHandler(this.changeProcessnameToolStripMenuItem_Click);
			// 
			// changeProcessargumentsToolStripMenuItem
			// 
			this.changeProcessargumentsToolStripMenuItem.Name = "changeProcessargumentsToolStripMenuItem";
			this.changeProcessargumentsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.changeProcessargumentsToolStripMenuItem.Text = "Change process &arguments";
			this.changeProcessargumentsToolStripMenuItem.Click += new System.EventHandler(this.changeProcessargumentsToolStripMenuItem_Click);
			// 
			// changerootAlbionDirectoryToolStripMenuItem
			// 
			this.changerootAlbionDirectoryToolStripMenuItem.Name = "changerootAlbionDirectoryToolStripMenuItem";
			this.changerootAlbionDirectoryToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.changerootAlbionDirectoryToolStripMenuItem.Text = "Change &root Albion directory";
			this.changerootAlbionDirectoryToolStripMenuItem.Click += new System.EventHandler(this.changerootAlbionDirectoryToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(225, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(225, 6);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Opacity = 0D;
			this.ShowInTaskbar = false;
			this.Text = "Form1";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.contextMenuStripTrayIcon.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIconTrayIcon;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripTrayIcon;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeProcessnameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeProcessargumentsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem changerootAlbionDirectoryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
	}
}

