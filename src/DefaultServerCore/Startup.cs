﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterLogger;
using RemotePlusServer.Core;
using RemotePlusServer.Core.ServerCore;
using static RemotePlusServer.Core.DefaultCommands;
using RemotePlusServer;
using System.ServiceModel.Description;
using RemotePlusLibrary;
using RemotePlusLibrary.Contracts;
using BetterLogger.Loggers;

namespace DefaultServerCore
{
    public class Startup : IServerCoreStartup
    {
        public void AddServices(IServiceCollection services)
        {
            services.UseLogger(logFactory =>
            {
                logFactory.AddLogger(new ConsoleLogger()
                {
                    Settings = new ConsoleLoggerOptions()
                });
            });
            services.UseServer(() =>
            {
                string endpointAddress = "Remote";
                ServerStartup._remote = new RemoteImpl();
                var service = ServerRemotePlusService.Create(typeof(IRemote), ServerStartup._remote, ServerManager.DefaultSettings.PortNumber, endpointAddress, (m, o) => ServerManager.Logger.Log(m, o), null);
                ServiceThrottlingBehavior throt = new ServiceThrottlingBehavior();
                throt.MaxConcurrentCalls = int.MaxValue;
                service.Host.Description.Behaviors.Add(throt);
                ServerManager.Logger.Log("Attaching server events.", LogLevel.Debug);
                service.HostClosed += Host_Closed;
                service.HostClosing += Host_Closing;
                service.HostFaulted += Host_Faulted;
                service.HostOpened += Host_Opened;
                service.HostOpening += Host_Opening;
                service.HostUnknownMessageReceived += Host_UnknownMessageReceived;
                return service;
            });
            services.UseServer(() =>
            {
                IRemotePlusService<FileTransferServciceInterface> fts = null;
                ServerManager.Logger.Log("Adding file transfer service.", BetterLogger.LogLevel.Info);
                var binding = RemotePlusLibrary.Core._ConnectionFactory.BuildBinding();
                binding.TransferMode = System.ServiceModel.TransferMode.Streamed;
                fts = FileTransferService.CreateNotSingle(typeof(RemotePlusLibrary.FileTransfer.Service.IFileTransferContract), ServerManager.DefaultSettings.PortNumber, binding, "FileTransfer", null);
                fts.HostClosed += Host_Closed;
                fts.HostClosing += Host_Closing;
                fts.HostFaulted += Host_Faulted;
                fts.HostOpened += Host_Opened;
                fts.HostOpening += Host_Opening;
                fts.HostUnknownMessageReceived += Host_UnknownMessageReceived;
                return fts;
            });
        }

        void IServerCoreStartup.InitializeServer(IServerBuilder builder)
        {
            builder.InitializeKnownTypes()
                .LoadServerConfig()
                .InitializeDefaultGlobals()
                .InitializeScriptingEngine((options) => { })
                .OpenMexForRemotePlus()
                .OpenMexForFileTransfer()
                .LoadExtensionLibraries()
                .InitializeVariables()
                .AddTask(() => ServerManager.Logger.Log("Loading Commands.", LogLevel.Info))
                .AddCommand("ps", ProcessStartCommand)
                .AddCommand("help", Help)
                .AddCommand("logs", Logs)
                .AddCommand("vars", vars)
                .AddCommand("dateTime", dateTime)
                .AddCommand("processes", processes)
                .AddCommand("version", version)
                .AddCommand("encrypt", svm_encyptFile)
                .AddCommand("decrypt", svm_decryptFile)
                .AddCommand("beep", svm_beep)
                .AddCommand("speak", svm_speak)
                .AddCommand("showMessageBox", svm_showMessageBox)
                .AddCommand("path", path)
                .AddCommand("cd", cd)
                .AddCommand("echo", echo)
                .AddCommand("load-extensionLibrary", loadExtensionLibrary)
                .AddCommand("cp", cp)
                .AddCommand("deleteFile", deleteFile)
                .AddCommand("echoFile", echoFile)
                .AddCommand("ls", ls)
                .AddCommand("genMan", genMan)
                .AddCommand("scp", scp)
                .AddCommand("resetStaticScript", resetStaticScript)
                .AddCommand("requestFile", requestFile);
        }
        private void Host_UnknownMessageReceived(object sender, System.ServiceModel.UnknownMessageReceivedEventArgs e)
        {
            ServerManager.Logger.Log($"The server encountered an unknown message sent by the client. Message: {e.Message.ToString()}", LogLevel.Error);
        }

        private void Host_Opening(object sender, EventArgs e)
        {
            ServerManager.Logger.Log("Opening server.", LogLevel.Info);
        }

        private void Host_Opened(object sender, EventArgs e)
        {
            if (ServerManager.DefaultSettings.DiscoverySettings.DiscoveryBehavior == RemotePlusLibrary.Configuration.ServerSettings.ProxyConnectionMode.Connect)
            {
                ServerManager.Logger.Log($"Host ready. Server is now part of the proxy cluster. Connect to proxy server to configure this server.", BetterLogger.LogLevel.Info);
            }
            else
            {
                ServerManager.Logger.Log($"Host ready. Server is listening on port {ServerManager.DefaultSettings.PortNumber}. Connect to configure server.", BetterLogger.LogLevel.Info);
            }
        }

        private void Host_Faulted(object sender, EventArgs e)
        {
            ServerManager.Logger.Log("The server state has been transferred to the faulted state.", LogLevel.Error);
        }

        private void Host_Closing(object sender, EventArgs e)
        {
            ServerManager.Logger.Log("Closing the server.", BetterLogger.LogLevel.Info);
        }

        private void Host_Closed(object sender, EventArgs e)
        {
            ServerManager.Logger.Log("The server is now closed.", BetterLogger.LogLevel.Info);
        }
    }
}
