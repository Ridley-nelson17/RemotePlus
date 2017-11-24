﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotePlusLibrary.Extension
{
    public interface ILibraryStartup
    {
        void Init(ILibraryBuilder builder, IInitEnvironment env);
    }
}