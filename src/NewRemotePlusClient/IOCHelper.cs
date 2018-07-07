﻿ using NewRemotePlusClient.ViewModels;
using RemotePlusLibrary.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using NewRemotePlusClient.IOC;
using RemotePlusClient.CommonUI.ConnectionClients;

namespace NewRemotePlusClient
{
    public static class IOCHelper
    {
        public static MainWindowViewModel MainWindow => IOCContainer.Provider.Get<MainWindowViewModel>();
        public static IUIManager UI => IOCContainer.Provider.Get<IUIManager>();
        public static ServiceClient Client => IOCContainer.Provider.Get<ServiceClient>();
        public static ILoginManager LoginManager => IOCContainer.Provider.Get<ILoginManager>();
    }
}
