﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RemotePlusLibrary
{
    [DataContract]
    public class AuthenticationRequest
    {
        [DataMember]
        public string Reason { get; set; }
    }
}
