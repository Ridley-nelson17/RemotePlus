﻿using RemotePlusLibrary.Core;
using RemotePlusLibrary.Extension.CommandSystem;
using RemotePlusServer.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using static RemotePlusServer.Core.ServerManager;

namespace RSPM
{
    public class DefaultPackageManager : IPackageManager
    {
        public List<Uri> Sources { get; private set; } = new List<Uri>();
        private IPackageDownloader _mainDownloader;
        private ISourceReader _reader;
        public DefaultPackageManager(IPackageDownloader downloader, ISourceReader reader)
        {
            _mainDownloader = downloader;
            _reader = reader;
        }
        public void InstallPackage(string packageName)
        {
            ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole($"Beginning installation of {packageName}");
            if (_mainDownloader.DownlaodPackage(packageName, Sources.ToArray()))
            {
                IPackageReader reader = new DefaultPackageReader();
                ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole("Reading package file (pkg)");
                var package = reader.BuildPackage($"{packageName}.pkg");
                if (package != null)
                {
                    if (package.Description != null)
                    {
                        if (confirmInstallation(package.Description))
                        {
                            var clientLogger = new ClientLogger(ServerRemoteService.RemoteInterface.Client);
                            ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole("Extracting package.");
                            package.ExtractWithoutManifest("extensions");
                            ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole("Loading extensions.");
                            GlobalServices.Logger.AddLogger(clientLogger);
                            package.LoadPackageExtensions("extensions", DefaultCollection);
                            GlobalServices.Logger.RemoveLogger(clientLogger);
                            ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole("Finished extracting package. Deleting downloaded package.");
                        }
                        else
                        {
                            ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole("Operation canceled. Deleting downloaded package.");
                        }
                    }
                    else
                    {
                        ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole(new ConsoleText("Invalid package: missing manifest. Deleting downloaded package.") { TextColor = Color.Red });
                    }
                    package.Zip.Dispose();
                    File.Delete($"{packageName}.pkg");
                    ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole("Finished installing package.");
                }
            }
        }

        private bool confirmInstallation(PackageDescription description)
        {
            if(ServerRemoteService.RemoteInterface.Client.ClientType == RemotePlusLibrary.Client.ClientType.CommandLine)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Package {description.Name} is ready for installation.");
                sb.AppendLine();
                sb.AppendLine($"Package Description: {description.Description}");
                sb.AppendLine();
                sb.AppendLine("The package will be extracted to the extensions folder of the server.");
                sb.AppendLine("Once completed, the extension folder will be rescanned for new extensions.");
                sb.AppendLine("WARNING: The package may contain code that may harm or damage the server/client.");
                sb.AppendLine("If downloaded from a third-party source, it is YOUR responsibility to make sure that");
                sb.AppendLine("the package is legit. We are not responsible for any malicious activities.");
                sb.AppendLine("If the package was downloaded from our package source, please notify us immediately if you suspect that a package is malicious.");
                sb.AppendLine();
                ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole(sb.ToString());
                string response = ServerRemoteService.RemoteInterface.Client.ClientCallback.RequestInformation(new RemotePlusLibrary.RequestSystem.RequestBuilder("rcmd_textBox", "I acknowledge the warning and are ready to extract and install package [Y/N]", null)).Data.ToString();
                if(string.Equals(response, "Y", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void LoadPackageSources()
        {
            ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole("Reading sources.");
            try
            {
                _reader.ParsedSource += (sender, e) =>
                {
                    ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole($"Source: {e.ParsedUri.ToString()}");
                };
                var urls = _reader.ReadSources("Configurations\\Server\\sources.list");
                Sources.AddRange(urls);
            }
            catch (FileNotFoundException)
            {
                ServerRemoteService.RemoteInterface.Client.ClientCallback.TellMessageToServerConsole("The sources list does not exist. Creating new list.");
                File.Create("Configurations\\Server\\sources.list");
            }
        }
    }
}