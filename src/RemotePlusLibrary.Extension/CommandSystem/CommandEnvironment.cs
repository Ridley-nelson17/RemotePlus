﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemotePlusLibrary.Extension.CommandSystem.CommandClasses.Parsing;

namespace RemotePlusLibrary.Extension.CommandSystem
{
    public class CommandEnvironment : ICommandEnvironmnet
    {
        public IParser Parser { get;private set; }

        public ITokenProcessor Processor { get; private set; }

        public ICommandExecutor Executor { get; private set; }
        public CommandEnvironment(IParser parser, ITokenProcessor processor, ICommandExecutor executor)
        {
            Parser = parser;
            Processor = processor;
            Executor = executor;
        }
    }
}