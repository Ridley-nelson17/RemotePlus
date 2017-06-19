﻿namespace RemotePlusClient
{
    partial class MainF
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Speak");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Beep");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("File Transfer");
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_open = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.emi_open = new System.Windows.Forms.ToolStripMenuItem();
            this.tcRight = new System.Windows.Forms.TabControl();
            this.tcr_extensions = new System.Windows.Forms.TabPage();
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.tcLeft = new System.Windows.Forms.TabControl();
            this.tcl_da = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.connectMenuItem = new System.Windows.Forms.MenuItem();
            this.consoleMenuItem = new System.Windows.Forms.MenuItem();
            this.settingsMenuItem = new System.Windows.Forms.MenuItem();
            this.switchUserMenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.command_browse_menuItem = new System.Windows.Forms.MenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tcRight.SuspendLayout();
            this.tcr_extensions.SuspendLayout();
            this.tcLeft.SuspendLayout();
            this.tcl_da.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mi_open});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(104, 26);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // mi_open
            // 
            this.mi_open.Name = "mi_open";
            this.mi_open.Size = new System.Drawing.Size(103, 22);
            this.mi_open.Text = "Open";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emi_open});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(104, 26);
            this.contextMenuStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip2_ItemClicked);
            // 
            // emi_open
            // 
            this.emi_open.Name = "emi_open";
            this.emi_open.Size = new System.Drawing.Size(103, 22);
            this.emi_open.Text = "Open";
            this.emi_open.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // tcRight
            // 
            this.tcRight.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcRight.Controls.Add(this.tcr_extensions);
            this.tcRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.tcRight.Location = new System.Drawing.Point(652, 0);
            this.tcRight.Name = "tcRight";
            this.tcRight.SelectedIndex = 0;
            this.tcRight.Size = new System.Drawing.Size(183, 426);
            this.tcRight.TabIndex = 6;
            // 
            // tcr_extensions
            // 
            this.tcr_extensions.Controls.Add(this.treeView2);
            this.tcr_extensions.Location = new System.Drawing.Point(4, 4);
            this.tcr_extensions.Name = "tcr_extensions";
            this.tcr_extensions.Padding = new System.Windows.Forms.Padding(3);
            this.tcr_extensions.Size = new System.Drawing.Size(175, 400);
            this.tcr_extensions.TabIndex = 1;
            this.tcr_extensions.Text = "Extensions";
            this.tcr_extensions.UseVisualStyleBackColor = true;
            // 
            // treeView2
            // 
            this.treeView2.ContextMenuStrip = this.contextMenuStrip2;
            this.treeView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView2.Location = new System.Drawing.Point(3, 3);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(169, 394);
            this.treeView2.TabIndex = 5;
            // 
            // tcLeft
            // 
            this.tcLeft.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcLeft.Controls.Add(this.tcl_da);
            this.tcLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.tcLeft.Location = new System.Drawing.Point(0, 0);
            this.tcLeft.Name = "tcLeft";
            this.tcLeft.SelectedIndex = 0;
            this.tcLeft.Size = new System.Drawing.Size(174, 426);
            this.tcLeft.TabIndex = 7;
            // 
            // tcl_da
            // 
            this.tcl_da.Controls.Add(this.treeView1);
            this.tcl_da.Location = new System.Drawing.Point(4, 4);
            this.tcl_da.Name = "tcl_da";
            this.tcl_da.Padding = new System.Windows.Forms.Padding(3);
            this.tcl_da.Size = new System.Drawing.Size(166, 400);
            this.tcl_da.TabIndex = 1;
            this.tcl_da.Text = "Default actions";
            this.tcl_da.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "nd_speak";
            treeNode1.Text = "Speak";
            treeNode2.Name = "nd_beep";
            treeNode2.Text = "Beep";
            treeNode3.Name = "nd_FileTransfer";
            treeNode3.Text = "File Transfer";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this.treeView1.Size = new System.Drawing.Size(160, 394);
            this.treeView1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(174, 271);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(478, 155);
            this.tabControl1.TabIndex = 8;
            // 
            // tabControl2
            // 
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(174, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(478, 271);
            this.tabControl2.TabIndex = 9;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5,
            this.menuItem8,
            this.menuItem2,
            this.menuItem10});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.connectMenuItem,
            this.consoleMenuItem,
            this.settingsMenuItem,
            this.switchUserMenuItem});
            this.menuItem1.Text = "Server";
            // 
            // connectMenuItem
            // 
            this.connectMenuItem.Index = 0;
            this.connectMenuItem.Text = "Connect";
            this.connectMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // consoleMenuItem
            // 
            this.consoleMenuItem.Enabled = false;
            this.consoleMenuItem.Index = 1;
            this.consoleMenuItem.Text = "Console";
            this.consoleMenuItem.Click += new System.EventHandler(this.consoleToolStripMenuItem_Click);
            // 
            // settingsMenuItem
            // 
            this.settingsMenuItem.Enabled = false;
            this.settingsMenuItem.Index = 2;
            this.settingsMenuItem.Text = "Settings";
            this.settingsMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // switchUserMenuItem
            // 
            this.switchUserMenuItem.Enabled = false;
            this.switchUserMenuItem.Index = 3;
            this.switchUserMenuItem.Text = "Switch user";
            this.switchUserMenuItem.Click += new System.EventHandler(this.switchUserMenuItem_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem7,
            this.menuItem4});
            this.menuItem5.Text = "Extensions";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.Text = "Load";
            this.menuItem6.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "View";
            this.menuItem7.Click += new System.EventHandler(this.getExtensionsToolStripMenuItem_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "Show requests";
            this.menuItem4.Click += new System.EventHandler(this.showRequests_MenuItem);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 2;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9});
            this.menuItem8.Text = "Windows";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Text = "Close Top";
            this.menuItem9.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 3;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3});
            this.menuItem2.Text = "Scripts";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "Load using console";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 4;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.command_browse_menuItem});
            this.menuItem10.Text = "Commands";
            // 
            // command_browse_menuItem
            // 
            this.command_browse_menuItem.Index = 0;
            this.command_browse_menuItem.Text = "Browse";
            this.command_browse_menuItem.Click += new System.EventHandler(this.command_browse_menuItem_Click);
            // 
            // MainF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 426);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tcLeft);
            this.Controls.Add(this.tcRight);
            this.Menu = this.mainMenu1;
            this.Name = "MainF";
            this.Text = "RemotePlusClient";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainF_FormClosing);
            this.Load += new System.EventHandler(this.MainF_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.tcRight.ResumeLayout(false);
            this.tcr_extensions.ResumeLayout(false);
            this.tcLeft.ResumeLayout(false);
            this.tcl_da.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mi_open;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem emi_open;
        private System.Windows.Forms.TabControl tcRight;
        private System.Windows.Forms.TabPage tcr_extensions;
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.TabControl tcLeft;
        private System.Windows.Forms.TabPage tcl_da;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem connectMenuItem;
        private System.Windows.Forms.MenuItem consoleMenuItem;
        private System.Windows.Forms.MenuItem settingsMenuItem;
        private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItem8;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem switchUserMenuItem;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem command_browse_menuItem;
    }
}