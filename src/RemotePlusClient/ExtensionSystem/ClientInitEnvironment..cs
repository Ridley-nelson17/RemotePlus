﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemotePlusLibrary.Extension;
using RemotePlusLibrary.Extension.ExtensionLoader.Initialization;

namespace RemotePlusClient.ExtensionSystem
{
    public class ClientInitEnvironment : IInitEnvironment
    {
        public bool PreviousError { get; set; }

        public int InitPosition { get; set; }

        public ClientInitEnvironment(bool pe)
        {
            InitPosition = 1;
            PreviousError = pe;
        }
    }
}
