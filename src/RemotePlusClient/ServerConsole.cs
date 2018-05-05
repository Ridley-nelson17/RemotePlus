﻿using Logging;
using RemotePlusLibrary.Extension.CommandSystem;
using RemotePlusLibrary.Extension.Gui;
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
using RemotePlusLibrary.Extension.CommandSystem.CommandClasses;

namespace RemotePlusClient
{
    public partial class ServerConsole : ThemedForm
    {
        public RichTextBoxLoggingMethod Logger { get; set; }
        public ConsoleSettings settings = null;
        public bool InputEnabled { get; set; } = true;
        public void ClearConsole() => richTextBox1.Clear();
        string scriptFile;
        public ServerConsole()
        {
            InitializeComponent();
        }
        public ServerConsole(string file)
        {
            scriptFile = file;
            InitializeComponent();
        }
        public ServerConsole(bool enableInput)
        {
            InputEnabled = enableInput;
            InitializeComponent();
            if (!InputEnabled)
            {
                HideInput();
            }
        }
        private void ServerConsole_Load(object sender, EventArgs e)
        {
            settings = new ConsoleSettings();
            #region Initialize Settings
            try
            {
                settings.Load();
            }
            catch (FileNotFoundException)
            {
                MainF.ConsoleObj.Logger.AddOutput("Created new console config file.", OutputLevel.Info, "ServerConsole");
                settings.Save();
            }
            #endregion Initialize Settings
            Logger = new RichTextBoxLoggingMethod()
            {
                Output = richTextBox1,
                DefaultInfoColor = Color.White,
                OverrideLogItemObjectColorValue = true,
            };
            Logger.DefaultInfoColor = settings.DefaultInfoColor;
            Logger.DefaultErrorColor = settings.DefaultErrorColor;
            Logger.DefaultWarningColor = settings.DefaultWarningColor;
            Logger.DefaultDebugColor = settings.DefaultDebugColor;
            richTextBox1.Font = (Font)TypeDescriptor.GetConverter(typeof(Font)).ConvertFromString(settings.DefaultFont);
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection acsc = new AutoCompleteStringCollection();
            acsc.AddRange(MainF.Remote.GetCommandsAsStrings().ToArray());
            textBox1.AutoCompleteCustomSource = acsc;
            if (!string.IsNullOrEmpty(scriptFile))
            {
                RunScriptFile();
            }
        }

        private void HideInput()
        {
            if(!InputEnabled)
            {
                textBox1.Visible = false;
            }
        }

        public void RunScriptFile()
        {
            try
            {
                Task.Run(() => MainF.Remote.ExecuteScript(File.ReadAllText(scriptFile)));
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("The file does not exist.");
            }
        }
        public void RunScriptFile(string f)
        {
            try
            {
                Task.Run(() => MainF.Remote.ExecuteScript(File.ReadAllText(f)));
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("The file does not exist.");
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string command = textBox1.Text;
                textBox1.Clear();
                textBox1.Enabled = false;
                var result = Task.Run(() => MainF.Remote.RunServerCommand(command, CommandExecutionMode.Client));
                result.Wait();
                textBox1.Enabled = true;
                PostResult(result.Result);
            }
        }

        private void PostResult(CommandPipeline result)
        {
            if (ClientApp.MainWindow.BottumPages.ContainsKey(CommandPipelineViewer.NAME))
            {
                ((CommandPipelineViewer)ClientApp.MainWindow.BottumPages[CommandPipelineViewer.NAME]).UpdatePipeline(result);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        internal void AppendText(string message)
        {
            richTextBox1.AppendText($"{message}\n");
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ConsoleSettingsDialogBox csd = new ConsoleSettingsDialogBox(settings))
            {
                csd.ShowDialog();
            }
        }

        private void clearToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
    }
}
