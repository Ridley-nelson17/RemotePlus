﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using RemotePlusLibrary;
using System.Threading;
using System.Speech.Synthesis;
using System.ServiceModel.Description;
using RemotePlusLibrary.Extension;
using System.IO;
using Logging;
using System.Security.Principal;
using System.Management;
using System.Net.NetworkInformation;
using RemotePlusLibrary.Extension.CommandSystem;
using RemotePlusLibrary.Extension.WatcherSystem;
using RemotePlusLibrary.Core;
using System.Diagnostics;

namespace RemotePlusServer
{
    public delegate int CommandDelgate(params string[] args);
    public static partial class ServerManager
    {
        public static Dictionary<string, WatcherBase> Watchers { get; private set; }
        public static CMDLogging Logger { get; } = new CMDLogging();
        public static ServiceHost host { get; private set; } = null;
        public static RemoteImpl Remote { get; } = new RemoteImpl();
        public static Dictionary<string, CommandDelgate> Commands { get; } = new Dictionary<string, CommandDelgate>();
        public static VariableManager Variables { get; private set; }
        public static ServerSettings DefaultSettings { get; set; } = new ServerSettings();
        public static ServerExtensionLibraryCollection DefaultCollection { get; } = new ServerExtensionLibraryCollection();
        private static Stopwatch sw;
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                sw = new Stopwatch();
                sw.Start();
                Logger.DefaultFrom = "Server Host";
                InitalizeKnownTypes();
                InitializeCommands();
                ScanForServerSettingsFile();
                InitializeVariables();
                InitializeWatchers();
                LoadExtensionLibraries();
                if (CheckPrerequisites())
                {
                    RunInServerMode();
                }
                SaveLog();
            }
            catch(Exception ex)
            {
                Logger.AddOutput("Internal server error: " + ex.Message, OutputLevel.Error);
                Console.Write("Press any key to exit.");
                Console.ReadKey();
                SaveLog();
            }
        }

        private static void SaveLog()
        {
            try
            {
                if (DefaultSettings.LogOnShutdown)
                {
                    Logger.AddOutput("Saving log and closing.", OutputLevel.Info);
                    Logger.SaveLog($"ServerLogs\\{DateTime.Now.ToShortDateString().Replace('/', '-')} {DateTime.Now.ToShortTimeString().Replace(':', '-')}.txt");
                }
            }
            catch
            {

            }
        }

        private static void InitalizeKnownTypes()
        {
            Logger.AddOutput("Adding default known types.", OutputLevel.Info);
            DefaultKnownTypeManager.LoadDefaultTypes();
            DefaultKnownTypeManager.AddType(typeof(UserAccount));
        }

        private static void InitializeVariables()
        {
            if(File.Exists("Variables.xml"))
            {
                Logger.AddOutput("Loading variables.", OutputLevel.Info);
                Variables = VariableManager.Load();
            }
            else
            {
                Logger.AddOutput("There is no variable file. Beginning variable initialization.", OutputLevel.Warning);
                Variables = VariableManager.New();
                Variables.Add("Name", "RemotePlusServer");
                Logger.AddOutput("Saving file.", OutputLevel.Info);
                Variables.Save();
            }
        }

        private static void InitializeCommands()
        {
            Logger.AddOutput("Loading commands.", OutputLevel.Info);
            Commands.Add("ex", ExCommand);
            Commands.Add("ps", ProcessStartCommand);
            Commands.Add("help", Help);
            Commands.Add("logs", Logs);
            Commands.Add("vars", vars);
            Commands.Add("dateTime", dateTime);
            Commands.Add("processes", processes);
            Commands.Add("watchers", watchers);
        }

        static bool CheckPrerequisites()
        {
            Logger.AddOutput("Checking prerequisites.", OutputLevel.Info);
            //Check for prerequisites
            ServerPrerequisites.CheckPrivilleges();
            ServerPrerequisites.CheckNetworkInterfaces();
            ServerPrerequisites.CheckSettings();
            sw.Stop();
            // Check results
            if(Logger.errorcount >= 1 && Logger.warningcount == 0)
            {
                Logger.AddOutput($"Unable to start server. ({Logger.errorcount} errors) Elapsed time: {sw.Elapsed.ToString()}", OutputLevel.Error);
                return false;
            }
            else if(Logger.errorcount >= 1 && Logger.warningcount >= 1)
            {
                Logger.AddOutput($"Unable to start server. ({Logger.errorcount} errors, {Logger.warningcount} warnings) Elapsed time: {sw.Elapsed.ToString()}", OutputLevel.Error);
                return false;
            }
            else if(Logger.errorcount == 0 && Logger.warningcount >= 1)
            {
                Logger.AddOutput($"The server can start, but with warnings. ({Logger.warningcount} warnings) Elapsed time: {sw.Elapsed.ToString()}", OutputLevel.Warning);
                return true;
            }
            else
            {
                Logger.AddOutput(new LogItem(OutputLevel.Info, $"Validation passed. Elapsed time: {sw.Elapsed.ToString()}", "Server Host") { Color = ConsoleColor.Green });
                return true;
            }
        }
        static void LoadExtensionLibraries()
        {
            Logger.AddOutput("Loading extensions...", Logging.OutputLevel.Info);
            if (Directory.Exists("extensions"))
            {
                foreach (string files in Directory.GetFiles("extensions"))
                {
                    if (Path.GetExtension(files) == ".dll")
                    {
                        Logger.AddOutput($"Found extension file ({Path.GetFileName(files)})", Logging.OutputLevel.Info);
                        var lib = ServerExtensionLibrary.LoadServerLibrary(files);
                        DefaultCollection.Libraries.Add(lib.Name, lib);
                    }
                }
            }
            else
            {
                Logger.AddOutput("The extensions folder does not exist.", OutputLevel.Info);
            }
        }
        static void RunInServerMode()
        {
            string url = $"net.tcp://0.0.0.0:{DefaultSettings.PortNumber}/Remote";
            Remote.Setup();
            host = new ServiceHost(Remote);
            host.Description.Endpoints[0].Address = new EndpointAddress(url);
            host.Open();
            Logger.AddOutput($"Host ready. Server is listening on port {DefaultSettings.PortNumber}. Connect to configure server.", Logging.OutputLevel.Info);
            Console.ReadLine();
            host.Close();
        }
        static void ScanForServerSettingsFile()
        {
            if (!File.Exists("Configurations\\Server\\GlobalServerSettings.config"))
            {
                Logger.AddOutput("The server settings file does not exist. Creating server settings file.", OutputLevel.Warning);
                DefaultSettings.Save();
            }
            else
            {
                Logger.AddOutput("Loading server settings file.", OutputLevel.Info);
                try
                {
                    DefaultSettings.Load();
                }
                catch (Exception ex)
                {
#if DEBUG
                    Logger.AddOutput("Unable to load server settings. " + ex.ToString(), OutputLevel.Error);
#else
                    Logger.AddOutput("Unable to load server settings. " + ex.Message, OutputLevel.Error);
#endif
                }
            }
        }
        static void InitializeWatchers()
        {
            Logger.AddOutput("Initializing watchers.", OutputLevel.Info);
            Watchers = new Dictionary<string, WatcherBase>();
        }
        public static int Execute(string c)
        {
            try
            {
                bool FoundCommand = false;
                string[] ca = c.Split();
                foreach (KeyValuePair<string, CommandDelgate> k in Commands)
                {
                    if(ca[0] == k.Key)
                    {
                        FoundCommand = true;
                        return k.Value(ca);
                    }
                }
                if (!FoundCommand)
                {
                    Remote.Client.ClientCallback.TellMessageToServerConsole(new UILogItem(OutputLevel.Error, "Unknown command. Please type {help} for a list of commands", "Server Host"));
                    return (int)CommandStatus.Fail;
                }
                return -2;
            }
            catch (Exception ex)
            {
                Remote.Client.ClientCallback.TellMessageToServerConsole(new UILogItem(OutputLevel.Error,"Error whie executing command: " + ex.Message, "Server Host"));
                return (int)CommandStatus.Fail;
            }
        }
        static void Close()
        {
            host.Close();
            Environment.Exit(0);
        }
    }
}