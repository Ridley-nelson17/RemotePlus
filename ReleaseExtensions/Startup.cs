﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemotePlusLibrary.Extension;
using RemotePlusServer;
using RemotePlusLibrary.Extension.CommandSystem;

namespace ReleaseExtensions
{
    public class Startup : ILibraryStartup
    {
        void ILibraryStartup.Init()
        {
            ServerManager.Logger.AddOutput("Welcome to \"ReleaseExtension.\" This library contains some useful tools that demonstrates the powers of \"RemotePlus\"", Logging.OutputLevel.Info);
            ServerManager.Commands.Add("releaseExtensionAbout", releaseExtensionAbout);
            ServerManager.Logger.AddOutput("Adding watchers.", Logging.OutputLevel.Info);
            ServerManager.Watchers.Add("HddWatcher", new HddWatcher());
        }
        [CommandHelp("Describes about the ReleaseExtensionsLibrary.")]
        int releaseExtensionAbout(string[] args)
        {
            ServerManager.Remote.Client.ClientCallback.TellMessageToServerConsole(new Logging.UILogItem(Logging.OutputLevel.Info, "ReleaseExtension is a test of the extension system."));
            return (int)CommandStatus.Success;
        }
    }
}