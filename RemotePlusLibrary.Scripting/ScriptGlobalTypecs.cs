﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RemotePlusLibrary.Scripting
{
    [DataContract]
    public enum ScriptGlobalType
    {
        [EnumMember]
        Table,
        [EnumMember]
        Function
    }
}
