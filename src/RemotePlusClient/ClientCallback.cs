﻿using RemotePlusLibrary;
using System.ServiceModel;
using System.Windows.Forms;
using Logging;
using System;
using System.Net;
using System.Drawing;
using RemotePlusClient.CommonUI;
using RemotePlusLibrary.Extension.CommandSystem;
using System.Media;
using System.Diagnostics;
using System.Speech.Synthesis;
using System.Threading;
using RemotePlusLibrary.Security.AccountSystem;

namespace RemotePlusClient
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
        IncludeExceptionDetailInFaults = true,
        UseSynchronizationContext = false)]
    public class ClientCallback : IRemoteClient
    {
        public int ServerPosition { get; private set; }
        public bool SwapFlag { get; private set; }
        public bool ConsoleStreamEnabled { get; private set; } = true;
        Logger consoleStream = null;
        /// <summary>
        /// Tells the client which textbox to use for the server output
        /// </summary>
        /// <param name="logger"></param>
        public void SwapConsoleStream(Logger logger)
        {
            consoleStream = logger;
            SwapFlag = true;
        }
        public void DisableConsoleStream()
        {
            ConsoleStreamEnabled = false;
        }
        public void EnableConsoleStream()
        {
            ConsoleStreamEnabled = true;
        }

        #region Callback Methods
        public ClientCallback(int serverPos)
        {
            ServerPosition = serverPos;
        }
        public void Beep(int Hertz, int Duration)
        {
            Console.Beep(Hertz, Duration);
        }

        public void PlaySound(string FileName)
        {
            SoundPlayer player = new SoundPlayer(FileName);
            player.Play();
        }

        public void PlaySoundLoop(string FileName)
        {
            SoundPlayer player = new SoundPlayer(FileName);
            player.PlaySync();
        }

        public void PlaySoundSync(string FileName)
        {
            SoundPlayer player = new SoundPlayer(FileName);
            player.PlayLooping();
        }
        public void RunProgram(string Program, string Argument)
        {
            Process.Start(Program, Argument);
        }
        public DialogResult ShowMessageBox(string Message, string Caption, MessageBoxIcon Icon, MessageBoxButtons Buttons)
        {
            return MessageBox.Show(Message, Caption, Buttons, Icon);
        }

        public void Speak(string Message, VoiceGender Gender, VoiceAge Age)
        {
            SpeechSynthesizer ss = new SpeechSynthesizer();
            ss.SelectVoiceByHints(Gender, Age);
            ss.Speak(Message);
        }
        public void Disconnect(Guid serverGuid, string Reason)
        {
            LogItem l = new LogItem(Logging.OutputLevel.Error, $"The server {ServerPosition} disconnected from the client. Reason: " + Reason, $"Server Host ({serverGuid})");
            MainF.ConsoleObj.Logger.AddOutput(l);
            MainF.Disconnect();
        }

        public UserCredentials RequestAuthentication(Guid serverGuid, AuthenticationRequest Request)
        {
            AuthenticationDialog ad = new AuthenticationDialog(Request);
            if(ad.ShowDialog() == DialogResult.OK)
            {
                return ad.UserInfo;
            }
            else
            {
                return null;
            }
        }

        public void TellMessage(Guid serverGuid, string Message, Logging.OutputLevel o)
        {
            if (ConsoleStreamEnabled)
            {
                if (!SwapFlag)
                {
                    ClientApp.Logger.AddOutput(Message, o, $"Server Host ({serverGuid}");
                }
                else
                {
                    consoleStream.AddOutput(Message, o, $"Server Host ({serverGuid})");
                }
                MainF.ConsoleObj.Logger.AddOutput(Message, o, $"Server Host ({serverGuid})");
            }
        }

        public void TellMessage(Guid serverGuid, UILogItem li)
        {
            li.From += $" ({serverGuid})";
            if (ConsoleStreamEnabled)
            {
                if (!SwapFlag)
                {
                    ClientApp.Logger.AddOutput(new LogItem(li.Level, li.Message, li.From));
                }
                else
                {
                    consoleStream.AddOutput(new LogItem(li.Level, li.Message, li.From));
                }
                MainF.ConsoleObj.Logger.AddOutput(li);
            }
        }

        public void TellMessage(Guid serverGuid, UILogItem[] Logs)
        {
            if (ConsoleStreamEnabled)
            {
                foreach (LogItem li in Logs)
                {
                    li.From += $" ({serverGuid})";
                    if (!SwapFlag)
                    {
                        ClientApp.Logger.AddOutput(new LogItem(li.Level, li.Message, li.From));
                    }
                    else
                    {
                        consoleStream.AddOutput(new LogItem(li.Level, li.Message, li.From));
                    }
                    MainF.ConsoleObj.Logger.AddOutput(li);
                }
            }
        }

        public void TellMessageToServerConsole(Guid serverGuid, UILogItem li)
        {
            if (MainF.ServerConsoleObj == null)
            {
                li.From = $"Server Console ({serverGuid}";
                MainF.ConsoleObj.Logger.AddOutput(li);
            }
            else
            {
                MainF.ServerConsoleObj.Logger.AddOutput(li);
            }
        }

        public ClientBuilder RegisterClient()
        {
            ClientBuilder builder = new ClientBuilder(ClientType.GUI)
            {
                FriendlyName = "Default GUI Client"
            };
            builder.ExtraData.Add("global_newLine", "true");
            builder.ExtraData.Add("ps_appendNewLine", "false");
            return builder;
        }

        public void TellMessageToServerConsole(Guid serverGuid, string Message)
        {
            if (MainF.ServerConsoleObj == null)
            {
                MainF.ConsoleObj.AppendText(Message);
            }
            else
            {
                MainF.ServerConsoleObj.AppendText(Message);
            }
        }

        public ReturnData RequestInformation(Guid serverGuid, RequestBuilder builder)
        {
            ReturnData data = null;
            Thread t = new Thread((p) => data = RequestStore.Show((RequestBuilder)p));
            t.SetApartmentState(ApartmentState.STA);
            t.Start(builder);
            t.Join();
            return data;
        }

        public void RegistirationComplete(Guid serverGuid)
        {
            //Role.RoleNames = MainF.Remote.GetServerRoleNames().ToArray();
        }

        public void SendSignal(Guid serverGuid, SignalMessage message)
        {
            switch(message.Message)
            {
                case "fileTransfer":
                    ((RemoteFileBrowser)((MainF)Form.ActiveForm).TopPages["Remote File Browser"]).Counter = int.Parse(message.Value);
                    break;
                case "r_fileTransfer":
                    RequestStore.GetCurrent().Update(message.Value);
                    break;
            }
        }

        public void ChangePrompt(Guid serverGuid, RemotePlusLibrary.Extension.CommandSystem.PromptBuilder newPrompt)
        {
            //TODO: Implement Prompt
        }

        public RemotePlusLibrary.Extension.CommandSystem.PromptBuilder GetCurrentPrompt()
        {
            return null;
        }

        public void TellMessageToServerConsole(Guid serverGuid, ConsoleText text)
        {
            Color originalColor = MainF.ConsoleObj.ForeColor;
            MainF.ConsoleObj.ForeColor = text.TextColor;
            MainF.ConsoleObj.AppendText(text.Text);
            MainF.ConsoleObj.ForeColor = originalColor;
        }
        #endregion

    }
}