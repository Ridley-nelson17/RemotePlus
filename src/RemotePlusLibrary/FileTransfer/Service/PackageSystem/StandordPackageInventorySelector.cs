﻿using RemotePlusLibrary.Core.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace RemotePlusLibrary.FileTransfer.Service.PackageSystem
{
    public class StandordPackageInventorySelector : IPackageInventorySelector
    {

        List<Action<Package>> _routes = new List<Action<Package>>();

        public void AddPackageInventory<TPackage, TInventoryImpl>(string name)
            where TPackage : Package
            where TInventoryImpl : IPackageInventory<TPackage>
        {
            GlobalServices.Logger.Log($"Adding package inventory of type: {nameof(TPackage)}, name: {name}", BetterLogger.LogLevel.Info);
            IOCContainer.Provider.Bind<IPackageInventory<TPackage>>().To(typeof(TInventoryImpl)).InSingletonScope().Named(name);
        }

        public void AddRoute(Action<Package> router)
        {
            _routes.Add(router);
        }

        public IPackageInventory<TPackage> GetInventory<TPackage>(string name) where TPackage : Package
        {
            var inv = IOCContainer.Provider.Get<IPackageInventory<TPackage>>(name);
            return inv;
        }

        public void Route(Package package)
        {
            for(int i = 0; i < _routes.Count; i++)
            {
                _routes[i].Invoke(package);
            }
        }
    }
}