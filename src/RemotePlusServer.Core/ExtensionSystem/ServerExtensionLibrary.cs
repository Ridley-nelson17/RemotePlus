﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using RemotePlusLibrary.Extension;
using RemotePlusLibrary.Extension.ExtensionLoader;
using RemotePlusLibrary.Extension.ExtensionLoader.Initialization;
using BetterLogger;

namespace RemotePlusServer.Core.ExtensionSystem
{
    /// <summary>
    /// Represents a server extension library.
    /// </summary>
    public class ServerExtensionLibrary : ExtensionLibraryBase<string>
    {
        protected ServerExtensionLibrary(string friendlyName, string name, ExtensionLibraryType type, Guid g, RequiresDependencyAttribute[] deps, Version v) : base(friendlyName, name, type, g, deps, v)
        {
        }

        public static ServerExtensionLibrary LoadServerLibrary(string FileName, Action<string, LogLevel> callback, IInitEnvironment env)
        {
            ServerExtensionLibrary lib;
            Assembly a = Assembly.LoadFrom(FileName);
            ExtensionLibraryAttribute ea = a.GetCustomAttribute<ExtensionLibraryAttribute>();
            if (ea != null)
            {
                if (ea.LibraryType == ExtensionLibraryType.Server || ea.LibraryType == ExtensionLibraryType.Both)
                {
                    Guid guid = Guid.Empty;
                    try
                    {
                        guid = ParseGuid(ea.Guid);
                    }
                    catch (FormatException)
                    {
                        guid = Guid.NewGuid();
                        callback($"Unable to parse GUID. Using random generated GUID. GUID: [{guid.ToString()}]", LogLevel.Warning);
                    }
                    Version version = ParseVersion(ea.Version);
                    var deps = LoadDependencies(a, callback, env);
                    if (!typeof(ILibraryStartup).IsAssignableFrom(ea.Startup))
                    {
                        throw new ArgumentException("The startup type does not implement ILibraryStartup.");
                    }
                    else
                    {
                        var st = (ILibraryStartup)Activator.CreateInstance(ea.Startup);
                        callback("Beginning initialization.", LogLevel.Info);
                        ServerLibraryBuilder builder = new ServerLibraryBuilder(ea.Name, ea.FriendlyName, ea.Version, ea.LibraryType);
                        st.Init(builder, env);
                        callback("finished initalization.", LogLevel.Info);
                        lib = new ServerExtensionLibrary(ea.FriendlyName, ea.Name, ea.LibraryType, guid, deps, version);
                        callback("Registiring server hooks.", LogLevel.Info);
                        lib.Hooks = builder.Hooks;
                    }
                }
                else
                {
                    throw new InvalidExtensionLibraryException($"The extension library {ea.FriendlyName} must be of type server.");
                }
            }
            else
            {
                throw new InvalidExtensionLibraryException("The library does not have an ExtensionLibraryAttrubte.");
            }
            return lib;
        }
        private static RequiresDependencyAttribute[] LoadDependencies(Assembly a, Action<string, LogLevel> callback, IInitEnvironment env)
        {
            callback($"Searching dependencies for {a.GetName().Name}", LogLevel.Info);
            RequiresDependencyAttribute[] deps = ExtensionLibraryBase<ServerExtensionLibrary>.FindDependencies(a);
            foreach (RequiresDependencyAttribute d in deps)
            {
                if(File.Exists(d.DependencyName))
                {
                    callback($"Found dependency {d.DependencyName}", LogLevel.Info);
                    if (d.DependencyType != DependencyType.Resource)
                    {
                        try
                        {
                            Assembly da = Assembly.LoadFrom(d.DependencyName);
                            if(da.GetName().Version != d.Version)
                            {
                                throw new DependencyException($"Library {d.DependencyName}, version {da.GetName().Version} does not match requred version of {d.Version}");
                            }
                            else
                            {
                                if(d.LoadIfNotLoaded && d.DependencyType == DependencyType.RemotePlusLib)
                                {
                                    callback($"Loading dependency {d.DependencyName}", LogLevel.Info);
                                    ServerExtensionLibrary.LoadServerLibrary(d.DependencyName, callback, env);
                                }
                            }
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
                else
                {
                    if (d.DependencyType == DependencyType.Resource)
                    {
                        throw new DependencyException($"Library {a.GetName().Name} is dependent on {d.DependencyName}");
                    }
                    else
                    {
                        throw new DependencyException($"Library {a.GetName().Name} is dependent on {d.DependencyName}, version {d.Version.ToString()}");
                    }
                }
            }
            return deps;
        }
    }
}