﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotePlusLibrary.Core.IOC
{
    /// <summary>
    /// Provides a way to plug in initialization steps when the server starts.
    /// </summary>
    public interface IServerBuilder
    {
        IServerBuilder AddTask(Action task);
        IServerInitilizer Build();
    }
}
