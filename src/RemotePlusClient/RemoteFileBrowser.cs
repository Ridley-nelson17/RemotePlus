﻿using RemotePlusLibrary.Extension.Gui;
using RemotePlusLibrary.FileTransfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemotePlusClient
{
    //Credit goes to Microsoft article at https://docs.microsoft.com/en-us/dotnet/framework/winforms/controls/creating-an-explorer-style-interface-with-the-listview-and-treeview
    public partial class RemoteFileBrowser : ThemedForm
    {
        public int CountValue
        {
            get
            {
                return fileBrowser1.CountLabel;
            }
            set
            {
                fileBrowser1.CountLabel = value;
            }
        }
        public RemoteFileBrowser()
        {
            InitializeComponent();
        }

        private void RemoteFileBrowser_Load(object sender, EventArgs e)
        {
            MainF.ConsoleObj.Logger.AddOutput("Downloading file data from server. This may take a while.", Logging.OutputLevel.Info);
            progressWorker.DoWork += ProgressWorker_DoWork;
            progressWorker.RunWorkerAsync();
        }

        private void ProgressWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Invoke((Action)(() => fileBrowser1.StatusMessage = "Downloading file data"));
            int num = 0;
            PopulateTreeView(() =>
            {
                fileBrowser1.CountLabel = (num++);
            });
        }

        void PopulateTreeView(Action callback)
        {
            TreeNode rootNode = null;
            RemoteDirectory info = MainF.Remote.GetRemoteFiles(false);
            MainF.ConsoleObj.Logger.AddOutput("Finished populating file browser.", Logging.OutputLevel.Info);
            this.Invoke((Action)(() => fileBrowser1.StatusMessage = "Populating Tree View"));
            rootNode = new TreeNode(info.Name);
            rootNode.Tag = info;
            GetDirectories(info.GetDirectories(), rootNode, callback);
            this.Invoke((Action)(() => fileBrowser1.Directories.Add(rootNode)));
            fileBrowser1.StatusMessage = "Finsished";
        }

        private void GetDirectories(RemoteDirectory[] subDirs, TreeNode nodeToAdd, Action callback)
        {
            TreeNode aNode;
            RemoteDirectory[] subSubDirs;
            int number = subDirs.Length;
            foreach (RemoteDirectory subDir in subDirs)
            {
                try
                {
                    //MainF.ConsoleObj.Logger.AddOutput($"Adding {subDir.FullName}.", Logging.OutputLevel.Info);
                    callback();
                    aNode = new TreeNode(subDir.Name, 0, 0);
                    aNode.Tag = subDir;
                    aNode.ImageKey = "folder";
                    subSubDirs = subDir.GetDirectories();
                    if (subSubDirs.Length != 0)
                    {
                        GetDirectories(subSubDirs, aNode, callback);
                    }
                    nodeToAdd.Nodes.Add(aNode);
                }
                catch (Exception ex)
                {
                    MainF.ConsoleObj.Logger.AddOutput($"Directory population failed to be loaded: {ex.Message}", Logging.OutputLevel.Warning);
                }
            }
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            
        }
    }
}