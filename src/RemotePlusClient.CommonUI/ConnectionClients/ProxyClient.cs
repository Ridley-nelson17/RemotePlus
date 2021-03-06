﻿using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Speech.Synthesis;
using System.Windows.Forms;
using RemotePlusLibrary;
using RemotePlusLibrary.Configuration.ServerSettings;
using RemotePlusLibrary.Discovery;
using RemotePlusLibrary.Extension.CommandSystem;
using RemotePlusLibrary.Extension.CommandSystem.CommandClasses;
using RemotePlusLibrary.FileTransfer.BrowserClasses;
using RemotePlusLibrary.Scripting;
using RemotePlusLibrary.Security.AccountSystem;

namespace RemotePlusClient.CommonUI.ConnectionClients
{
    public class ProxyClient : DuplexClientBase<IProxyRemote>, IProxyRemote
    {
        public ProxyClient(object callbackInstance) : base(callbackInstance)
        {
        }

        public ProxyClient(InstanceContext callbackInstance) : base(callbackInstance)
        {
        }

        public ProxyClient(object callbackInstance, string endpointConfigurationName) : base(callbackInstance, endpointConfigurationName)
        {
        }

        public ProxyClient(object callbackInstance, ServiceEndpoint endpoint) : base(callbackInstance, endpoint)
        {
        }

        public ProxyClient(InstanceContext callbackInstance, string endpointConfigurationName) : base(callbackInstance, endpointConfigurationName)
        {
        }

        public ProxyClient(InstanceContext callbackInstance, ServiceEndpoint endpoint) : base(callbackInstance, endpoint)
        {
        }

        public ProxyClient(object callbackInstance, string endpointConfigurationName, string remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public ProxyClient(object callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public ProxyClient(object callbackInstance, System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress) : base(callbackInstance, binding, remoteAddress)
        {
        }

        public ProxyClient(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public ProxyClient(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }

        public ProxyClient(InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress) : base(callbackInstance, binding, remoteAddress)
        {
        }

        public void Beep(int Hertz, int Duration)
        {
            base.Channel.Beep(Hertz, Duration);
        }

        public void DecryptFile(string fileName, string password)
        {
            base.Channel.DecryptFile(fileName, password);
        }

        public void Disconnect()
        {
            base.Channel.Disconnect();
        }

        public void EncryptFile(string fileName, string password)
        {
            base.Channel.EncryptFile(fileName, password);
        }

        public string GetCommandHelpDescription(string command)
        {
            return Channel.GetCommandHelpDescription(command);
        }

        public string GetCommandHelpPage(string command)
        {
            return Channel.GetCommandHelpPage(command);
        }

        public IEnumerable<CommandDescription> GetCommands()
        {
            return Channel.GetCommands();
        }

        public IEnumerable<string> GetCommandsAsStrings()
        {
            return Channel.GetCommandsAsStrings();
        }


        public UserAccount GetLoggedInUser()
        {
            return Channel.GetLoggedInUser();
        }

        public IDirectory GetRemoteFiles(string path, bool useRequest)
        {
            return Channel.GetRemoteFiles(path, useRequest);
        }

        public ServerSettings GetServerSettings()
        {
            return Channel.GetServerSettings();
        }

        public void PlaySound(string FileName)
        {
            Channel.PlaySound(FileName);
        }

        public void PlaySoundLoop(string FileName)
        {
            Channel.PlaySoundLoop(FileName);
        }

        public void PlaySoundSync(string FileName)
        {
            Channel.PlaySoundSync(FileName);
        }

        public void Register(RegisterationObject Settings)
        {
            Channel.Register(Settings);
        }

        public void Restart()
        {
            Channel.Restart();
        }

        public void RunProgram(string Program, string Argument, bool shell, bool ignore)
        {
            Channel.RunProgram(Program, Argument, shell, ignore);
        }

        public CommandPipeline RunServerCommand(string Command, CommandExecutionMode commandMode)
        {
            return Channel.RunServerCommand(Command, commandMode);
        }

        public DialogResult ShowMessageBox(string Message, string Caption, MessageBoxIcon Icon, MessageBoxButtons Buttons)
        {
            return Channel.ShowMessageBox(Message, Caption, Icon, Buttons);
        }

        public void Speak(string Message, VoiceGender Gender, VoiceAge Age)
        {
            Channel.Speak(Message, Gender, Age);
        }

        public void SwitchUser()
        {
            Channel.SwitchUser();
        }

        public void Connect()
        {
            base.Open();
        }

        public bool ExecuteScript(string script)
        {
            return Channel.ExecuteScript(script);
        }

        public ScriptGlobalInformation[] GetScriptGlobals()
        {
            return Channel.GetScriptGlobals();
        }

        public string ReadFileAsString(string fileName)
        {
            return Channel.ReadFileAsString(fileName);
        }
        public override string ToString()
        {
            return base.ToString();
        }

        public Guid[] GetServers()
        {
            return Channel.GetServers();
        }

        public void ProxyRegister()
        {
            Channel.ProxyRegister();
        }

        public void SelectServer(int serverPosition)
        {
            Channel.SelectServer(serverPosition);
        }

        public void ProxyDisconnect()
        {
            Channel.ProxyDisconnect();
        }

        public void SelectServer(Guid guid)
        {
            Channel.SelectServer(guid);
        }

        public Guid GetSelectedServerGuid()
        {
            return Channel.GetSelectedServerGuid();
        }

        public CommandPipeline ExecuteProxyCommand(string command, CommandExecutionMode mode)
        {
            return Channel.ExecuteProxyCommand(command, mode);
        }

        public void UpdateServerSettings(ServerSettings Settings)
        {
            Channel.UpdateServerSettings(Settings);
        }

        public void SendSignal(SignalMessage signal)
        {
            Channel.SendSignal(signal);
        }

        public void UploadBytesToPackageSystem(byte[] data, int length, string name)
        {
            Channel.UploadBytesToPackageSystem(data, length, name);
        }
    }
}
