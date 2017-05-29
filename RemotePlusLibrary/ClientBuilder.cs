﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RemotePlusLibrary
{
    [DataContract]
    public class ClientBuilder
    {
        [DataMember]
        public string FriendlyName { get; set; }
        [DataMember]
        public Dictionary<string, string> ExtraData { get; set; } = new Dictionary<string, string>();
    }
}
