﻿using RemotePlusLibrary.IOC;
using Ninject;

namespace ProxyServer
{
    internal class ServiceCollection : IServiceCollection
    {
        public IServiceCollection AddSingleton<TService>(TService service)
        {
            IOCContainer.Provider.Bind<TService>().ToConstant(service);
            return this;
        }
        public IServiceCollection AddSingleton<TService>()
        {
            IOCContainer.Provider.Bind<TService>().ToSelf().InSingletonScope();
            return this;
        }

        public IServiceCollection AddTransient<TService, TImplementation>()
        {
            IOCContainer.Provider.Bind<TService>().To(typeof(TImplementation)).InTransientScope();
            return this;
        }
        public IServiceCollection AddTransient<TService>()
        {
            IOCContainer.Provider.Bind<TService>().ToSelf().InTransientScope();
            return this;
        }
    }
}