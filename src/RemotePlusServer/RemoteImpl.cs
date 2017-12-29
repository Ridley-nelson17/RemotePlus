﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using RemotePlusLibrary;
using System.Windows.Forms;
using Logging;
using System.Speech.Synthesis;
using RemotePlusLibrary.Core;
using RemotePlusLibrary.Extension;
using System.Diagnostics;
using RemotePlusLibrary.Extension.CommandSystem;
using System.IO;
using RemotePlusLibrary.Extension.Programmer;
using RemotePlusLibrary.Core.EmailService;
using RemotePlusLibrary.Extension.CommandSystem.CommandClasses;
using RemotePlusServer.ExtensionSystem;
using RemotePlusLibrary.FileTransfer;

namespace RemotePlusServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant,
        InstanceContextMode = InstanceContextMode.Single,
        IncludeExceptionDetailInFaults = true,
        UseSynchronizationContext = false)]
    [GlobalException(typeof(GlobalErrorHandler))]
    public class RemoteImpl : IRemote
    {
        const string OPERATION_COMPLETED = "Operation_Completed";
        public RemoteImpl()
        {

        }
        internal void Setup()
        {
            ServerManager.Logger.AddOutput("Added temperary extensions into dictionary.", OutputLevel.Debug);
            _allExtensions = ServerManager.DefaultCollection.GetAllExtensions();
        }
        public RegistirationObject Settings { get; private set; }
        public Client<IRemoteClient> Client { get; set; }
        public bool Registered { get; private set; }
        public UserAccount LoggedInUser { get; private set; }
        private Dictionary<string, ServerExtension> _allExtensions;
        void CheckRegisteration(string Action)
        {
            var l = ServerManager.Logger.AddOutput($"Checking registiration for action {Action}.", OutputLevel.Info);
            Client.ClientCallback.TellMessage(new UILogItem(l.Level, l.Message, l.From));
            if (!Registered)
            {
                ServerManager.Logger.AddOutput("The client is not registired to the server.", OutputLevel.Error);
                OperationContext.Current.GetCallbackChannel<IRemoteClient>().Disconnect("you must be registered.");
            }
        }
        public void Beep(int Hertz, int Duration)
        {
            CheckRegisteration("beep");
            if (!LoggedInUser.Role.Privilleges.CanBeep)
            {
                Client.ClientCallback.TellMessage("You do not have promission to use the beep function.", OutputLevel.Info);
            }
            else
            {
                Console.Beep(Hertz, Duration);
                Client.ClientCallback.TellMessage($"Console beeped. Hertz: {Hertz}, Duration: {Duration}", OutputLevel.Info);
            }
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
        }

        public void PlaySound(string FileName)
        {
            CheckRegisteration("PlaySound");
            if (!LoggedInUser.Role.Privilleges.CanPlaySound)
            {
                Client.ClientCallback.TellMessage("You do not have promission to use the PlaySound function.", OutputLevel.Info);
            }
            else
            {
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer(FileName);
                sp.Play();
            }
        }

        public void PlaySoundLoop(string FileName)
        {
            CheckRegisteration("PlaySoundLoop");
            if (!LoggedInUser.Role.Privilleges.CanPlaySoundLoop)
            {
                Client.ClientCallback.TellMessage("You do not have promission to use the CanPlaySoundLoop function.", OutputLevel.Info);
            }
            else
            {
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer(FileName);
                sp.PlayLooping();
            }
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
        }

        public void PlaySoundSync(string FileName)
        {
            CheckRegisteration("PlaySoundSync");
            if (!LoggedInUser.Role.Privilleges.CanPlaySoundSync)
            {
                Client.ClientCallback.TellMessage("You do not have promission to use the CanPlaySoundSync function.", OutputLevel.Info);
            }
            else
            {
                System.Media.SoundPlayer sp = new System.Media.SoundPlayer(FileName);
                sp.PlaySync();
            }
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
        }

        public void Register(RegistirationObject Settings)
        {
            ServerManager.Logger.AddOutput("A new client is awaiting registiration.", OutputLevel.Info);
            ServerManager.Logger.AddOutput("Instanitiating callback object.", OutputLevel.Debug);
            ServerManager.Logger.AddOutput("Getting ClientBuilder from client.", OutputLevel.Debug);
            var callback = OperationContext.Current.GetCallbackChannel<IRemoteClient>();
            Client = Client<IRemoteClient>.Build(callback.RegisterClient(), callback);
            ServerManager.Logger.AddOutput("Received registiration object from client.", OutputLevel.Info);
            this.Settings = Settings;
            var l = ServerManager.Logger.AddOutput("Processing registiration object.", OutputLevel.Debug);
            Client.ClientCallback.TellMessage(new UILogItem(l.Level, l.Message, l.From));
            if (Settings.LoginRightAway)
            {
                LogIn(Settings.Credentials);
            }
            else
            {
                var l3 = ServerManager.Logger.AddOutput("Awaiting credentials from the client.", OutputLevel.Info);
                Client.ClientCallback.TellMessage(new UILogItem(l3.Level, l3.Message, l3.From));
                UserCredentials upp = Client.ClientCallback.RequestAuthentication(new AuthenticationRequest(AutehnticationSeverity.Normal) { Reason = "The server requires credentials to register."});
                LogIn(upp);
            }
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
        }
        private void RegisterComplete()
        {
            ServerManager.Logger.AddOutput($"Client \"{Client.FriendlyName}\" [{Client.UniqueID}] Type: {Client.ClientType} registired.", Logging.OutputLevel.Info);
            Registered = true;
            Client.ClientCallback.TellMessage("Registiration complete.", Logging.OutputLevel.Info);
            Client.ClientCallback.RegistirationComplete();
        }

        public void RunProgram(string Program, string Argument)
        {
            CheckRegisteration("RunProgram");
            if (!LoggedInUser.Role.Privilleges.CanRunProgram)
            {
                Client.ClientCallback.TellMessage("You do not have promission to use the CanRunProgram function.", OutputLevel.Info);
            }
            else
            {
                ServerManager.Logger.AddOutput("Creating process component.", OutputLevel.Debug);
                Process p = new Process();
                ServerManager.Logger.AddOutput($"File to execute: {Program}", OutputLevel.Debug);
                p.StartInfo.FileName = Program;
                ServerManager.Logger.AddOutput($"File arguments: {Argument}", OutputLevel.Debug);
                p.StartInfo.Arguments = Argument;
                ServerManager.Logger.AddOutput($"Shell execution is disabled.", OutputLevel.Debug);
                p.StartInfo.UseShellExecute = false;
                ServerManager.Logger.AddOutput($"Error stream will be redirected.", OutputLevel.Debug);
                p.StartInfo.RedirectStandardError = true;
                ServerManager.Logger.AddOutput($"Standord stream will be redirected.", OutputLevel.Debug);
                p.StartInfo.RedirectStandardOutput = true;
                p.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        if (Client.ExtraData.TryGetValue("ps_appendNewLine", out string val))
                        {
                            if (val == "false")
                            {
                                Client.ClientCallback.TellMessageToServerConsole(e.Data);
                            }
                            else
                            {
                                Client.ClientCallback.TellMessageToServerConsole(new UILogItem(OutputLevel.Error, $"Extra data for appendText is invalid. Value: {val}", "Server Host"));
                            }
                        }
                        else
                        {
                            Client.ClientCallback.TellMessageToServerConsole(e.Data + "\n");
                        }
                    }
                };
                p.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data != null)
                    {
                        if (Client.ExtraData.TryGetValue("ps_appendNewLine", out string val))
                        {
                            if (val == "false")
                            {
                                Client.ClientCallback.TellMessageToServerConsole(e.Data);
                            }
                            else
                            {
                                Client.ClientCallback.TellMessageToServerConsole(new UILogItem(OutputLevel.Error, $"Extra data for appendText is invalid. Value: {val}", "Server Host"));
                            }
                        }
                        else
                        {
                            Client.ClientCallback.TellMessageToServerConsole(e.Data + "\n");
                        }
                    }
                };
                ServerManager.Logger.AddOutput("Starting process component.", OutputLevel.Info);
                p.Start();
                ServerManager.Logger.AddOutput("Beginning error stream read line.", OutputLevel.Debug);
                p.BeginErrorReadLine();
                ServerManager.Logger.AddOutput("Beginning standord stream reade line.", OutputLevel.Debug);
                p.BeginOutputReadLine();
            }
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
        }

        public void ShowMessageBox(string Message, string Caption, System.Windows.Forms.MessageBoxIcon Icon, System.Windows.Forms.MessageBoxButtons Buttons)
        {
            CheckRegisteration("ShowMessageBox");
            if (!LoggedInUser.Role.Privilleges.CanShowMessageBox)
            {
                Client.ClientCallback.TellMessage("You do not have promission to use the CanShowMessageBox function.", OutputLevel.Info);
            }
            else
            {
                var dr = MessageBox.Show(Message, Caption, Buttons, Icon);
                Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Info, $"The user responded to the message box. Response: {dr.ToString()}", "Server Host"));
            }
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
        }

        public void Speak(string Message, VoiceGender Gender, VoiceAge Age)
        {
            CheckRegisteration("Speak");
            if (!LoggedInUser.Role.Privilleges.CanSpeak)
            {
                Client.ClientCallback.TellMessage("You do not have promission to use the Speak function.", OutputLevel.Info);
            }
            else
            {
                System.Speech.Synthesis.SpeechSynthesizer ss = new System.Speech.Synthesis.SpeechSynthesizer();
                ss.SelectVoiceByHints(Gender, Age);
                ss.Speak(Message);
                Client.ClientCallback.TellMessage($"Server spoke. Message: {Message}, gender: {Gender.ToString()}, age: {Age.ToString()}", OutputLevel.Info);
            }
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
        }

        public CommandPipeline RunServerCommand(string Command, CommandExecutionMode commandMode)
        {
            CommandPipeline pipe = new CommandPipeline();
            int pos = 0;
            CheckRegisteration("RunServerCommand");
            if (!LoggedInUser.Role.Privilleges.CanAccessConsole)
            {
                Client.ClientCallback.TellMessage("You do not have promission to use the Console function.", OutputLevel.Info);
                return null;
            }
            else
            {
                string[] cs = Command.Split('&');
                if (cs.Length == 1)
                {
                    var request = new CommandRequest(cs[0].Split(' '));
                    pipe.Add(0, new CommandRoutine(request, ServerManager.Execute(request, commandMode, pipe)));
                }
                else
                {
                    foreach(string c in cs)
                    {
                        CommandRequest req = new CommandRequest(c.Split(' '));
                        pipe.Add(pos++, new CommandRoutine(req, ServerManager.Execute(req, CommandExecutionMode.Client, pipe)));
                    }
                }
            }
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            return pipe;
        }

        public void UpdateServerSettings(ServerSettings Settings)
        {
            CheckRegisteration("UpdateServerSettings");
            if (!LoggedInUser.Role.Privilleges.CanAccessSettings)
            {
                Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Error, "You do not have permission to change server settings.", "Server Host"));
            }
            else
            {
                ServerManager.Logger.AddOutput("Updating server settings.", OutputLevel.Info);
                ServerManager.DefaultSettings = Settings;
                Client.ClientCallback.TellMessage("Saving settings.", OutputLevel.Info);
                ServerManager.DefaultSettings.Save();
                Client.ClientCallback.TellMessage("Settings saved.", OutputLevel.Info);
                ServerManager.Logger.AddOutput("Settings saved.", OutputLevel.Info);
            }
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
        }

        public ServerSettings GetServerSettings()
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            CheckRegisteration("GetServerSettings");
            if (!LoggedInUser.Role.Privilleges.CanAccessSettings)
            {
                Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Error, "You do not have permission to change server settings.", "Server Host"));
                return null;
            }
            else
            {
                ServerManager.Logger.AddOutput("Retreiving server settings.", OutputLevel.Info);
                return ServerManager.DefaultSettings;
            }
        }

        public void Restart()
        {
            Application.Restart();
        }
        public UserAccount GetLoggedInUser()
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            CheckRegisteration("GetLoggedInUser");
            return LoggedInUser;
        }

        public ExtensionReturn RunExtension(string ExtensionName, ExtensionExecutionContext Context, string[] args)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            CheckRegisteration("RunExtension");
            if (!LoggedInUser.Role.Privilleges.CanRunExtension)
            {
                Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Error, "You do not have permission to run an extension.", "Server Host"));
                return new ExtensionReturn(ExtensionStatusCodes.ACCESS_DENIED);
            }
            else
            {
                ServerManager.Logger.AddOutput($"Executing extension. Name: {ExtensionName}, CallType: {Context.Mode.ToString()}", OutputLevel.Info);
                try
                {
                    ServerExtension e = _allExtensions[ExtensionName];
                    if (e.SupportClientTypes != ClientSupportedTypes.Both && ((Client.ClientType == ClientType.CommandLine && e.SupportClientTypes == ClientSupportedTypes.GUI) || (Client.ClientType == ClientType.GUI && e.SupportClientTypes == ClientSupportedTypes.CommandLine)))
                    {
                        ServerManager.Logger.AddOutput($"Unsupported client type. Please use a {e.SupportClientTypes} client instead.", OutputLevel.Info);
                        Client.ClientCallback.TellMessageToServerConsole(new UILogItem(OutputLevel.Error, $"Unsupported client type. Please use a {e.SupportClientTypes} client instead.", "Server Host"));
                    }
                    var s = e.Execute(Context, args);
                    ServerManager.Logger.AddOutput($"Returnaing extension response. Success: {(s.ReturnCode == 0 ? true : false)}", OutputLevel.Debug);
                    return s;
                }
                catch (KeyNotFoundException)
                {
                    ServerManager.Logger.AddOutput($"Extension {ExtensionName} does not exist.", OutputLevel.Error);
                    Client.ClientCallback.TellMessageToServerConsole(new UILogItem(OutputLevel.Error, $"Extension {ExtensionName} does not exist.", "Server Host"));
                    return new ExtensionReturn(ExtensionStatusCodes.EXTENSION_NOT_FOUND);
                }
            }
        }

        public List<ExtensionDetails> GetExtensionNames()
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            CheckRegisteration("GetExtensionNames");
            List<ExtensionDetails> l = new List<ExtensionDetails>();
            foreach (KeyValuePair<string, ServerExtension> s in _allExtensions)
            {
                l.Add(s.Value.GeneralDetails);
            }
            return l;
        }
        public IEnumerable<CommandDescription> GetCommands()
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            List<CommandDescription> rc = new List<CommandDescription>();
            CheckRegisteration("GetCommands");
            ServerManager.Logger.AddOutput("Requesting commands list.", OutputLevel.Info);
            Client.ClientCallback.TellMessage("Returning commands list.", OutputLevel.Info);
            foreach(KeyValuePair<string, CommandDelegate> currentCommand in ServerManager.DefaultService.Commands)
            {
                rc.Add(new CommandDescription() { Help = RemotePlusConsole.GetCommandHelp(currentCommand.Value), Behavior = RemotePlusConsole.GetCommandBehavior(currentCommand.Value), HelpPage = RemotePlusConsole.GetCommandHelpPage(currentCommand.Value), CommandName = currentCommand.Key });
            }
            return rc;
        }
        public IEnumerable<string> GetCommandsAsStrings()
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            CheckRegisteration("GetCommandsAsStrings");
            Client.ClientCallback.SendSignal(new SignalMessage("operation_completed", ""));
            return ServerManager.DefaultService.Commands.Keys;
        }

        public ServerExtensionCollectionProgrammer GetCollectionProgrammer()
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            ServerExtensionCollectionProgrammer cprog = new ServerExtensionCollectionProgrammer();
            return cprog;
        }

        public ServerExtensionLibraryProgrammer GetServerLibraryProgrammer(string LibraryName)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            ServerExtensionLibrary lib = ServerManager.DefaultCollection.Libraries[LibraryName];
            ServerExtensionLibraryProgrammer slprog = new ServerExtensionLibraryProgrammer(lib.FriendlyName, lib.Name, lib.LibraryType);
            return slprog;
        }

        public ServerExtensionProgrammer GetServerExtensionProgrammer(string ExtensionName)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            ServerExtensionProgrammer seprog = new ServerExtensionProgrammer()
            {
                ExtensionDetails = ServerManager.DefaultCollection.GetAllExtensions()[ExtensionName].GeneralDetails
            };
            return seprog;
        }

        public ServerExtensionProgrammer GetServerExtensionProgrammer(string LibraryName, string ExtensionName)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            ServerExtensionProgrammer seprog = new ServerExtensionProgrammer()
            {
                ExtensionDetails = ServerManager.DefaultCollection.Libraries[LibraryName].Extensions[ExtensionName].GeneralDetails
            };
            return seprog;
        }

        public void ProgramServerEstensionCollection(ServerExtensionCollectionProgrammer collectProgrammer)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
        }

        public void ProgramServerExtesnionLibrary(ServerExtensionLibraryProgrammer libProgrammer)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
        }

        public void ProgramServerExtension(string LibraryName, ServerExtensionProgrammer seProgrammer)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            ServerExtensionProgrammerUpdateEvent programmerEvent = new ServerExtensionProgrammerUpdateEvent(seProgrammer);
            ServerManager.DefaultCollection.Libraries[LibraryName].Extensions[seProgrammer.ExtensionDetails.Name].ProgramRequested(programmerEvent);
            if(!programmerEvent.Cancel)
            {
                //TODO: Add modifications to server extension here.
            }
        }

        public void SwitchUser()
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            LogOff();
            ServerManager.Logger.AddOutput("Logging in.", OutputLevel.Info ,"Server Host");
            Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Info, "Logging in.", "Server Host"));
            var cred = Client.ClientCallback.RequestAuthentication(new AuthenticationRequest(AutehnticationSeverity.Normal) { Reason = "Please provide a username and password to switch to." });
            LogIn(cred);
        }
        private void LogOff()
        {
            Registered = false;
            string username = LoggedInUser.Credentials.Username;
            LoggedInUser = null;
            ServerManager.Logger.AddOutput($"User {username} logged off.", OutputLevel.Info, "Server Host");
            Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Info, $"user {username} logged off.", "Server Host"));
        }
        private void LogIn(UserCredentials cred)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            if (cred == null)
            {
                ServerManager.Logger.AddOutput("The user did not pass in any credentials. Authentication failed.", OutputLevel.Info);
                Client.ClientCallback.TellMessage("Can't you at least provide a username and password?", OutputLevel.Info);
                Client.ClientCallback.Disconnect("Authentication failed.");
                return;
            }
            var l4 = ServerManager.Logger.AddOutput("Authenticating your user credentials.", OutputLevel.Info);
            Client.ClientCallback.TellMessage(new UILogItem(l4.Level, l4.Message, l4.From));
            foreach (UserAccount Account in ServerManager.DefaultSettings.Accounts)
            {
                if (Account.Verify(cred))
                {
                    LoggedInUser = Account;
                    RegisterComplete();
                    break;
                }
            }
            if (Registered != true)
            {
                Client.ClientCallback.TellMessage("Registiration failed. Authentication failed.", OutputLevel.Info);
                Client.ClientCallback.Disconnect("Registiration failed.");
                ServerManager.Logger.AddOutput($"Client {Client.FriendlyName} [{Client.UniqueID.ToString()}] disconnected. Failed to register to the server. Authentication failed.", OutputLevel.Info);
            }
        }

        public void Disconnect()
        {
            ServerManager.Logger.AddOutput($"Client \"{Client.FriendlyName}\" [{Client.UniqueID}] disconectted.", OutputLevel.Info);
        }

        public void EncryptFile(string fileName, string password)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            ServerManager.Logger.AddOutput($"Encrypting file. file name: {fileName}", OutputLevel.Info);
            GameclubCryptoServices.CryptoService.EncryptFile(password, fileName, Path.ChangeExtension(fileName, ".ec"));
            ServerManager.Logger.AddOutput("File encrypted.", OutputLevel.Info);
            Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Info, $"File encrypted. File: {fileName}", "Server Host"));
        }

        public void DecryptFile(string fileName, string password)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            ServerManager.Logger.AddOutput($"Decrypting file. file name: {fileName}", OutputLevel.Info);
            GameclubCryptoServices.CryptoService.DecrypttFile(password, fileName, Path.ChangeExtension(fileName, ".uc"));
            ServerManager.Logger.AddOutput("File decrypted.", OutputLevel.Info);
            Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Info, $"File decrypted. File: {fileName}", "Server Host"));
        }

        public string GetCommandHelpPage(string command)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            return RemotePlusConsole.ShowHelpPage(ServerManager.DefaultService.Commands, command);
        }

        public string GetCommandHelpDescription(string command)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            return RemotePlusConsole.ShowCommandHelpDescription(ServerManager.DefaultService.Commands, command);
        }
        int num = 0;
        public RemoteDirectory GetRemoteFiles(string path, bool usingRequest)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            DirectoryInfo subDir = new DirectoryInfo(path);
            RemoteDirectory r = new RemoteDirectory(subDir.FullName, subDir.LastAccessTime);
            //Get files
            foreach(FileInfo files in subDir.EnumerateFiles())
            {
                r.Files.Add(new RemoteFile(files.FullName, files.CreationTime, files.LastAccessTime));
            }
            //Get Folders
            foreach(DirectoryInfo folders in subDir.EnumerateDirectories())
            {
                r.Directories.Add(new RemoteDirectory(folders.FullName, folders.LastAccessTime));
            }
            return r;
        }
        public EmailSettings GetServerEmailSettings()
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            CheckRegisteration("GetServerEmailSettings");
            if (!LoggedInUser.Role.Privilleges.CanAccessSettings)
            {
                Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Error, "You do not have permission to change email server settings.", "Server Host"));
                return null;
            }
            else
            {
                ServerManager.Logger.AddOutput("Retreiving email server settings.", OutputLevel.Info);
                return ServerManager.DefaultEmailSettings;
            }
        }

        public void UpdateServerEmailSettings(EmailSettings emailSetting)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            CheckRegisteration("UpdateServerEmailSettings");
            if (!LoggedInUser.Role.Privilleges.CanAccessSettings)
            {
                Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Error, "You do not have permission to change server email settings.", "Server Host"));
            }
            else
            {
                ServerManager.Logger.AddOutput("Updating server email settings.", OutputLevel.Info);
                ServerManager.DefaultEmailSettings = emailSetting;
                Client.ClientCallback.TellMessage("Saving settings.", OutputLevel.Info);
                ServerManager.DefaultEmailSettings.Save();
                Client.ClientCallback.TellMessage("Settings saved.", OutputLevel.Info);
                ServerManager.Logger.AddOutput("Settings saved.", OutputLevel.Info);
            }
        }

        public bool SendEmail(string To, string Subject, string Message)
        {
            // OperationContext.Current.OperationCompleted += (sender, e) => Client.ClientCallback.SendSignal(new SignalMessage(OPERATION_COMPLETED, ""));
            EmailClient client = new EmailClient(ServerManager.DefaultEmailSettings);
            if(client.SendEmail(To, Subject, Message, out Exception err))
            {
                Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Info, "Email message sent.", "Server Host"));
                return true;
            }
            else
            {
                Client.ClientCallback.TellMessage(new UILogItem(OutputLevel.Error, $"Email message failed: {err.ToString()}", "Server Host"));
                return false;
            }
        }
    }
}