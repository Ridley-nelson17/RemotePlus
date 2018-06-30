﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotePlusLibrary
{
    public static class IOCContainer
    {
        public static IKernel Provider { get; set; } = new StandardKernel();
    }
}
