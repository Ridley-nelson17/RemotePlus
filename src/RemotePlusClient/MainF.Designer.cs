﻿using RemotePlusLibrary.Extension.Gui;
using System.Windows.Forms;

namespace RemotePlusClient
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
        protected override void InitializeTheme(Theme t)
        {
            this.BackColor = t.BackgroundColor;
            this.ForeColor = t.TextForgroundColor;
            this.treeView1.BackColor = t.TreeViewBackgroundColor;
            this.treeView1.ForeColor = t.TreeViewForegrondColor;
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mi_open = new System.Windows.Forms.ToolStripMenuItem();
            this.emi_open = new System.Windows.Forms.ToolStripMenuItem();
            this.tcLeft = new System.Windows.Forms.TabControl();
            this.tcl_da = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.cms_extensionFormBottom = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.cms_extensionFormTop = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.connectMenuItem = new System.Windows.Forms.MenuItem();
            this.consoleMenuItem = new System.Windows.Forms.MenuItem();
            this.settingsMenuItem = new System.Windows.Forms.MenuItem();
            this.switchUserMenuItem = new System.Windows.Forms.MenuItem();
            this.browseFile_MenuItem = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.mi_closeConsoleArea = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.command_browse_menuItem = new System.Windows.Forms.MenuItem();
            this.mi_pipeLineBrowser = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.configure_menuItem = new System.Windows.Forms.MenuItem();
            this.sendEmail_menuItem = new System.Windows.Forms.MenuItem();
            this.menuItem11 = new System.Windows.Forms.MenuItem();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem12 = new System.Windows.Forms.MenuItem();
            this.contextMenuStrip1.SuspendLayout();
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
            // emi_open
            // 
            this.emi_open.Name = "emi_open";
            this.emi_open.Size = new System.Drawing.Size(103, 22);
            this.emi_open.Text = "Open";
            this.emi_open.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // tcLeft
            // 
            this.tcLeft.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tcLeft.Controls.Add(this.tcl_da);
            this.tcLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.tcLeft.HotTrack = true;
            this.tcLeft.Location = new System.Drawing.Point(0, 0);
            this.tcLeft.Name = "tcLeft";
            this.tcLeft.SelectedIndex = 0;
            this.tcLeft.Size = new System.Drawing.Size(174, 393);
            this.tcLeft.TabIndex = 7;
            // 
            // tcl_da
            // 
            this.tcl_da.Controls.Add(this.treeView1);
            this.tcl_da.Location = new System.Drawing.Point(4, 4);
            this.tcl_da.Name = "tcl_da";
            this.tcl_da.Padding = new System.Windows.Forms.Padding(3);
            this.tcl_da.Size = new System.Drawing.Size(166, 367);
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
            this.treeView1.Size = new System.Drawing.Size(160, 361);
            this.treeView1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.ContextMenuStrip = this.cms_extensionFormBottom;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(174, 238);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(615, 155);
            this.tabControl1.TabIndex = 8;
            // 
            // cms_extensionFormBottom
            // 
            this.cms_extensionFormBottom.Name = "cms_extensionFormBottom";
            this.cms_extensionFormBottom.Size = new System.Drawing.Size(61, 4);
            // 
            // tabControl2
            // 
            this.tabControl2.ContextMenuStrip = this.cms_extensionFormTop;
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.HotTrack = true;
            this.tabControl2.Location = new System.Drawing.Point(174, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(615, 238);
            this.tabControl2.TabIndex = 9;
            // 
            // cms_extensionFormTop
            // 
            this.cms_extensionFormTop.Name = "cms_extensionFormTop";
            this.cms_extensionFormTop.Size = new System.Drawing.Size(61, 4);
            this.cms_extensionFormTop.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cms_extensionFormTop_ItemClicked);
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
            // browseFile_MenuItem
            // 
            this.browseFile_MenuItem.Enabled = false;
            this.browseFile_MenuItem.Index = 4;
            this.browseFile_MenuItem.Text = "Browse Files";
            this.browseFile_MenuItem.Click += new System.EventHandler(this.browseFile_MenuItem_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.connectMenuItem,
            this.consoleMenuItem,
            this.settingsMenuItem,
            this.switchUserMenuItem,
            this.browseFile_MenuItem});
            this.menuItem1.Text = "Server";
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
            // menuItem5
            // 
            this.menuItem5.Index = 2;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem7,
            this.menuItem4});
            this.menuItem5.Text = "Extensions";
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 0;
            this.menuItem9.Text = "Close Top";
            this.menuItem9.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // mi_closeConsoleArea
            // 
            this.mi_closeConsoleArea.Index = 1;
            this.mi_closeConsoleArea.Text = "Close Console Area";
            this.mi_closeConsoleArea.Click += new System.EventHandler(this.mi_closeConsoleArea_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 3;
            this.menuItem8.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem9,
            this.mi_closeConsoleArea});
            this.menuItem8.Text = "Windows";
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 0;
            this.menuItem3.Text = "Load using console";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 4;
            this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem3});
            this.menuItem2.Text = "Scripts";
            // 
            // command_browse_menuItem
            // 
            this.command_browse_menuItem.Index = 0;
            this.command_browse_menuItem.Text = "Browse";
            this.command_browse_menuItem.Click += new System.EventHandler(this.command_browse_menuItem_Click);
            // 
            // mi_pipeLineBrowser
            // 
            this.mi_pipeLineBrowser.Index = 1;
            this.mi_pipeLineBrowser.Text = "Pipeline Browser";
            this.mi_pipeLineBrowser.Click += new System.EventHandler(this.mi_pipeLineBrowser_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 5;
            this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.command_browse_menuItem,
            this.mi_pipeLineBrowser});
            this.menuItem10.Text = "Commands";
            // 
            // configure_menuItem
            // 
            this.configure_menuItem.Index = 0;
            this.configure_menuItem.Text = "Configure";
            this.configure_menuItem.Click += new System.EventHandler(this.configure_menuItem_Click);
            // 
            // sendEmail_menuItem
            // 
            this.sendEmail_menuItem.Index = 1;
            this.sendEmail_menuItem.Text = "Send Email";
            this.sendEmail_menuItem.Click += new System.EventHandler(this.sendEmail_menuItem_Click);
            // 
            // menuItem11
            // 
            this.menuItem11.Index = 6;
            this.menuItem11.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.configure_menuItem,
            this.sendEmail_menuItem});
            this.menuItem11.Text = "Email";
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem12,
            this.menuItem1,
            this.menuItem5,
            this.menuItem8,
            this.menuItem2,
            this.menuItem10,
            this.menuItem11});
            // 
            // menuItem12
            // 
            this.menuItem12.Index = 0;
            this.menuItem12.Text = "Client";
            // 
            // MainF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 393);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tcLeft);
            this.Menu = this.mainMenu1;
            this.Name = "MainF";
            this.Text = "RemotePlusClient";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainF_FormClosing);
            this.Load += new System.EventHandler(this.MainF_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tcLeft.ResumeLayout(false);
            this.tcl_da.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mi_open;
        private System.Windows.Forms.ToolStripMenuItem emi_open;
        private System.Windows.Forms.TabControl tcLeft;
        private System.Windows.Forms.TabPage tcl_da;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabControl tabControl2;
        private ContextMenuStrip cms_extensionFormTop;
        private MenuItem connectMenuItem;
        private MenuItem consoleMenuItem;
        private MenuItem settingsMenuItem;
        private MenuItem switchUserMenuItem;
        private MenuItem browseFile_MenuItem;
        private MenuItem menuItem1;
        private MenuItem menuItem6;
        private MenuItem menuItem7;
        private MenuItem menuItem4;
        private MenuItem menuItem5;
        private MenuItem menuItem9;
        private MenuItem mi_closeConsoleArea;
        private MenuItem menuItem8;
        private MenuItem menuItem3;
        private MenuItem menuItem2;
        private MenuItem command_browse_menuItem;
        private MenuItem mi_pipeLineBrowser;
        private MenuItem menuItem10;
        private MenuItem configure_menuItem;
        private MenuItem sendEmail_menuItem;
        private MenuItem menuItem11;
        private MainMenu mainMenu1;
        private ContextMenuStrip cms_extensionFormBottom;
        private MenuItem menuItem12;
    }
}