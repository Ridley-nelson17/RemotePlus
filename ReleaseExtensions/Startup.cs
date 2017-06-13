﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemotePlusLibrary.Extension;
using RemotePlusServer;
using RemotePlusLibrary.Extension.CommandSystem;
using System.IO;
using System.Reflection;
using RemotePlusLibrary.Extension.ClientModule;

namespace ReleaseExtensions
{
    public sealed class Startup : ILibraryStartup, IClientCommandLibraryStartup
    {
        void ILibraryStartup.ClientInit()
        {
            
        }

        void ILibraryStartup.Init()
        {
            ServerManager.Logger.AddOutput(new Logging.LogItem(Logging.OutputLevel.Info, "Welcome to \"ReleaseExtension.\" This library contains some useful tools that demonstrates the powers of \"RemotePlus\"", "ReleaseExtensions") { Color = Console.ForegroundColor});
            ServerManager.DefaultService.Commands.Add("releaseExtensionAbout", releaseExtensionAbout);
            ServerManager.Logger.AddOutput(new Logging.LogItem(Logging.OutputLevel.Info, "Adding watchers.", "ReleaseExtensions") { Color = Console.ForegroundColor });
            ServerManager.Watchers.Add("HddWatcher", new HddWatcher());
            ServerManager.Watchers.Add("SerialWatcher", new SerialWatcher());
        }

        void IClientCommandLibraryStartup.ModuleLibraryInit()
        {
            RemotePlusClientCmd.ClientCmdManager.LocalCommands.Add("#sayHello", client_sayHello);
        }

        [CommandHelp("Describes about the ReleaseExtensionsLibrary.")]
        int releaseExtensionAbout(string[] args)
        {
            ServerManager.DefaultService.Remote.Client.ClientCallback.TellMessageToServerConsole(new Logging.UILogItem(Logging.OutputLevel.Info, "ReleaseExtension is a test of the extension system."));
            return (int)CommandStatus.Success;
        }
        [CommandHelp("Say hello")]
        int client_sayHello(string[] args)
        {
            RemotePlusClientCmd.ClientCmdManager.Logger.AddOutput("Hello", Logging.OutputLevel.Info);
            return (int)CommandStatus.Success;
        }
    }
}