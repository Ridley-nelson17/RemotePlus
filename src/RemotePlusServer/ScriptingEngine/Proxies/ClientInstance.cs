﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using MoonSharp.Interpreter;
using RemotePlusLibrary;

namespace RemotePlusServer.ScriptingEngine.Proxies
{
    internal class ClientInstance
    {
        public string requestString(string prompt)
        {
            return ServerManager.DefaultService.Remote.Client.ClientCallback.RequestInformation(RequestBuilder.RequestString(prompt)).Data.ToString();
        }
        public void speak(string message, int voiceGender, int voiceAge)
        {
            VoiceAge va = VoiceAge.Adult;
            VoiceGender vg = VoiceGender.Male;
            switch (voiceGender)
            {
                case 0:
                    vg = VoiceGender.Male;
                    break;
                case 1:
                    vg = VoiceGender.Female;
                    break;
                case 2:
                    vg = VoiceGender.Neutral;
                    break;
                case 3:
                    vg = VoiceGender.NotSet;
                    break;
                default:
                    throw new ScriptRuntimeException("Invalid voice gender option.");
            }
            switch (voiceAge)
            {
                case 0:
                    va = VoiceAge.Adult;
                    break;
                case 1:
                    va = VoiceAge.Child;
                    break;
                case 2:
                    va = VoiceAge.Senior;
                    break;
                case 3:
                    va = VoiceAge.Teen;
                    break;
                case 4:
                    va = VoiceAge.NotSet;
                    break;
                default:
                    throw new ScriptRuntimeException("Invalid voice age option.");
            }
            ServerManager.DefaultService.Remote.Client.ClientCallback.RequestInformation(new RequestBuilder("a_speak", message, null) { Metadata = new Dictionary<string, string>() {
                {"vg", vg.ToString()},
                {"va", va.ToString()}
            } });
        }
    }
}