﻿using Logging;
using RemotePlusLibrary.Extension;
using RemotePlusServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReleaseExtensions
{
    public class CalculatorExtension : ServerExtension
    {
        public CalculatorExtension() : base(new ExtensionDetails("CalculatorExtension", "1.0.0.0"))
        {
        }

        public override OperationStatus Execute(ExtensionExecutionContext Context, params object[] arguments)
        {
            LogItem l;
            OperationStatus Status = new OperationStatus();
            switch(arguments[0])
            {
                case "add":
                    if (Context.Mode == CallType.GUI)
                    {
                        Status.Data = double.Parse((string)arguments[1]) + double.Parse((string)arguments[2]);
                        ServerManager.Logger.AddOutput("Client request addition calculation. Sum: " + Status.Data, OutputLevel.Info, "Calculaotr");
                        Status.Success = true;
                    }
                    else
                    {
                        l = new LogItem(OutputLevel.Info, (double.Parse((string)arguments[1]) + double.Parse((string)arguments[2])).ToString(), "Calculator");
                        ServerManager.Logger.AddOutput("Client request addition calculation. Sum: " + l.Message, l.Level, l.From);
                        RemotePlusServer.ServerManager.Remote.Client.ClientCallback.TellMessageToServerConsole(new UILogItem(l.Level, l.Message, l.From));
                    }
                    return Status;
                case "multiply":
                    if (Context.Mode == CallType.GUI)
                    {
                        Status.Data = double.Parse((string)arguments[1]) * double.Parse((string)arguments[2]);
                        ServerManager.Logger.AddOutput("Client request multiplication calculation. Product: " + Status.Data, OutputLevel.Info, "Calculaotr");
                        Status.Success = true;
                    }
                    else
                    {
                        l = new LogItem(OutputLevel.Info, (double.Parse((string)arguments[1]) * double.Parse((string)arguments[2])).ToString(), "Calculator");
                        ServerManager.Logger.AddOutput("Client request multiplication calculation. Product: " + l.Message, l.Level, l.From);
                        RemotePlusServer.ServerManager.Remote.Client.ClientCallback.TellMessageToServerConsole(new Logging.UILogItem(l.Level, l.Message, l.From));
                    }
                    return Status;
                case "divide":
                    if (Context.Mode == CallType.GUI)
                    {
                        Status.Data = double.Parse((string)arguments[1]) / double.Parse((string)arguments[2]);
                        ServerManager.Logger.AddOutput("Client request division calculation. Quotient: " + Status.Data, OutputLevel.Info, "Calculator");
                        Status.Success = true;
                    }
                    else
                    {
                        l = new LogItem(OutputLevel.Info, (double.Parse((string)arguments[1]) / double.Parse((string)arguments[2])).ToString(), "Calculator");
                        ServerManager.Logger.AddOutput("Client request division calculation. Quotient: " + l.Message, l.Level, l.From);
                        RemotePlusServer.ServerManager.Remote.Client.ClientCallback.TellMessageToServerConsole(new Logging.UILogItem(l.Level, l.Message, l.From));
                    }
                    return Status;
                case "subtract":
                    if (Context.Mode == CallType.GUI)
                    {
                        Status.Data = double.Parse((string)arguments[1]) - double.Parse((string)arguments[2]);
                        ServerManager.Logger.AddOutput("Client request subtraction calculation. Difference: " + Status.Data, OutputLevel.Info, "Calculator");
                        Status.Success = true;
                    }
                    else
                    {
                        l = new LogItem(OutputLevel.Info, (double.Parse((string)arguments[1]) - double.Parse((string)arguments[2])).ToString(), "Calculator");
                        ServerManager.Logger.AddOutput("Client request subtraction calculation. Difference: " + l.Message, l.Level, l.From);
                        RemotePlusServer.ServerManager.Remote.Client.ClientCallback.TellMessageToServerConsole(new Logging.UILogItem(l.Level, l.Message, l.From));
                    }
                    return Status;
                default:
                    if(Context.Mode == CallType.GUI)
                    {
                        Status.Success = false;
                    }
                    else
                    {
                        l = new LogItem(OutputLevel.Error, $"The action {arguments[1]} does not exist.", "Calculator");
                        ServerManager.Logger.AddOutput(l);
                        ServerManager.Remote.Client.ClientCallback.TellMessageToServerConsole(new UILogItem(l.Level, l.Message, l.From));
                    }
                    return Status;
            }
        }

        public override void HaultExtension()
        {
            throw new NotImplementedException();
        }

        public override void ResumeExtension()
        {
            throw new NotImplementedException();
        }
    }
}
