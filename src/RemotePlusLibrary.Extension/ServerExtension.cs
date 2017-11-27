﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using RemotePlusLibrary.Extension.Programmer;
using RemotePlusLibrary.Extension;

namespace RemotePlusLibrary.Extension
{
    public abstract class ServerExtension : RemotePlusLibrary.Extension.IExtension<ExtensionDetails>
    {
        public ClientSupportedTypes SupportClientTypes { get; set; }
        public virtual void ProgramRequested(ServerExtensionProgrammerUpdateEvent requestProgrammer)
        {

        }
        protected ServerExtension(ExtensionDetails d)
        {
            GeneralDetails = d;
        }
        public ExtensionDetails GeneralDetails { get; private set; }

        public abstract ExtensionReturn Execute(ExtensionExecutionContext Context, string[] arguments);
    }
}