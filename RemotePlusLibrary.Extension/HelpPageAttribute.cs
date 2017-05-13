﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotePlusLibrary.Extension
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HelpPageAttribute : Attribute
    {
        public string Details { get; private set; }
        public HelpSourceType Source { get; set; } = HelpSourceType.Text;
        public HelpPageAttribute(string details)
        {
            Details = details;
        }
    }
}
