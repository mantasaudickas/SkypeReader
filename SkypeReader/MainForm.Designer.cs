namespace SkypeReader
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSkypeDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.listContacts = new System.Windows.Forms.ListBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeCompleteChatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textHistory = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(811, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSkypeDatabaseToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitApplicationToolStripMenuItem});
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.filesToolStripMenuItem.Text = "Files";
            // 
            // openSkypeDatabaseToolStripMenuItem
            // 
            this.openSkypeDatabaseToolStripMenuItem.Name = "openSkypeDatabaseToolStripMenuItem";
            this.openSkypeDatabaseToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.openSkypeDatabaseToolStripMenuItem.Text = "Open Skype Database ..";
            this.openSkypeDatabaseToolStripMenuItem.Click += new System.EventHandler(this.openSkypeDatabaseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(194, 6);
            // 
            // exitApplicationToolStripMenuItem
            // 
            this.exitApplicationToolStripMenuItem.Name = "exitApplicationToolStripMenuItem";
            this.exitApplicationToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exitApplicationToolStripMenuItem.Text = "Exit Application";
            this.exitApplicationToolStripMenuItem.Click += new System.EventHandler(this.exitApplicationToolStripMenuItem_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.listContacts);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.textHistory);
            this.splitContainer.Size = new System.Drawing.Size(811, 507);
            this.splitContainer.SplitterDistance = 270;
            this.splitContainer.TabIndex = 2;
            // 
            // listContacts
            // 
            this.listContacts.ContextMenuStrip = this.contextMenuStrip;
            this.listContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listContacts.FormattingEnabled = true;
            this.listContacts.Location = new System.Drawing.Point(0, 0);
            this.listContacts.Name = "listContacts";
            this.listContacts.Size = new System.Drawing.Size(270, 507);
            this.listContacts.TabIndex = 0;
            this.listContacts.SelectedIndexChanged += new System.EventHandler(this.listContacts_SelectedIndexChanged);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeCompleteChatToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(201, 26);
            // 
            // removeCompleteChatToolStripMenuItem
            // 
            this.removeCompleteChatToolStripMenuItem.Name = "removeCompleteChatToolStripMenuItem";
            this.removeCompleteChatToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.removeCompleteChatToolStripMenuItem.Text = "Remove Complete Chat";
            this.removeCompleteChatToolStripMenuItem.Click += new System.EventHandler(this.removeCompleteChatToolStripMenuItem_Click);
            // 
            // textHistory
            // 
            this.textHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textHistory.Location = new System.Drawing.Point(0, 0);
            this.textHistory.MinimumSize = new System.Drawing.Size(20, 20);
            this.textHistory.Name = "textHistory";
            this.textHistory.Size = new System.Drawing.Size(537, 507);
            this.textHistory.TabIndex = 0;
            this.textHistory.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.textHistory_Navigating);
            this.textHistory.NewWindow += new System.ComponentModel.CancelEventHandler(this.textHistory_NewWindow);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 531);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Skype Reader";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSkypeDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitApplicationToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListBox listContacts;
        private System.Windows.Forms.WebBrowser textHistory;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem removeCompleteChatToolStripMenuItem;
    }
}

