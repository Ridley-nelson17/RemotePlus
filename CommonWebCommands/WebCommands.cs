﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using RemotePlusLibrary.Extension.CommandSystem;
using RemotePlusServer;
using RemotePlusLibrary.Extension.CommandSystem.CommandClasses;

namespace CommonWebCommands
{
    public static class WebCommands
    {
        [CommandHelp("Starts a new chrome seassion.")]
        public static CommandResponse chrome(CommandRequest args, CommandPipeline pipe)
        {
            try
            {
                ServerManager.DefaultService.Remote.RunProgram("cmd.exe", $"/c \"start chrome.exe {args.Arguments[1]}\"");
                ServerManager.DefaultService.Remote.Client.ClientCallback.TellMessageToServerConsole(new Logging.UILogItem(Logging.OutputLevel.Info, "chrome started", "WebCommands"));
                return new CommandResponse((int)CommandStatus.Success);
            }
            catch
            {
                return new CommandResponse((int)CommandStatus.Fail);
                throw;
            }
        }
        [CommandHelp("Starts a new internet explorer seassion.")]
        public static CommandResponse ie(CommandRequest args, CommandPipeline pipe)
        {
            try
            {
                ServerManager.DefaultService.Remote.RunProgram("cmd.exe", $"/c \"start iexplore.exe {args.Arguments[1]}\"");
                ServerManager.DefaultService.Remote.Client.ClientCallback.TellMessageToServerConsole(new Logging.UILogItem(Logging.OutputLevel.Info, "chrome started", "WebCommands"));
                return new CommandResponse((int)CommandStatus.Success);
            }
            catch
            {
                return new CommandResponse((int)CommandStatus.Fail);
                throw;
            }
        }
        [CommandHelp("Starts a new Opera seassion")]
        public static CommandResponse opera(CommandRequest args, CommandPipeline pipe)
        {
            try
            {
                ServerManager.DefaultService.Remote.RunProgram("cmd.exe", $"/c \"start opera.exe {args.Arguments[1]}\"");
                ServerManager.DefaultService.Remote.Client.ClientCallback.TellMessageToServerConsole(new Logging.UILogItem(Logging.OutputLevel.Info, "Opera started", "WebCommands"));
                return new CommandResponse((int)CommandStatus.Success);
            }
            catch
            {
                return new CommandResponse((int)CommandStatus.Fail);
                throw;
            }
        }
        [CommandHelp("Starts a new Firefox seassion")]
        public static CommandResponse firefox(CommandRequest args, CommandPipeline pipe)
        {
            try
            {
                ServerManager.DefaultService.Remote.RunProgram("cmd.exe", $"/c \"start firefox.exe {args.Arguments[1]}\"");
                ServerManager.DefaultService.Remote.Client.ClientCallback.TellMessageToServerConsole(new Logging.UILogItem(Logging.OutputLevel.Info, "Firefox started", "WebCommands"));
                return new CommandResponse((int)CommandStatus.Success);
            }
            catch
            {
                return new CommandResponse((int)CommandStatus.Fail);
                throw;
            }
        }
    }
}