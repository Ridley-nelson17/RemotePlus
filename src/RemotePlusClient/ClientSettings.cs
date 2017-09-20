﻿using RemotePlusLibrary.Configuration;
using RemotePlusLibrary.Extension.Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RemotePlusClient
{
    [DataContract]
    public class ClientSettings : IFileConfig
    {
        public const string CLIENT_SETTING_PATH = "Configurations\\CLient\\GlobalClientSettings.config";
        [DataMember]
        public Theme DefaultTheme { get; set; }
        public void Load()
        {
            var c = ConfigurationHelper.LoadConfig<ClientSettings>(CLIENT_SETTING_PATH, RemotePlusLibrary.Core.DefaultKnownTypeManager.GetKnownTypes(null));
            DefaultTheme = c.DefaultTheme;
        }

        public void Save()
        {
            ConfigurationHelper.SaveConfig(this, CLIENT_SETTING_PATH, RemotePlusLibrary.Core.DefaultKnownTypeManager.GetKnownTypes(null));
        }
    }
}
