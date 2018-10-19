﻿using RemotePlusLibrary.Contracts;
using RemotePlusLibrary.Core;
using RemotePlusLibrary.Discovery;
using RemotePlusLibrary.RequestSystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotePlusClient.CommonUI.Requests
{
    public class SendLocalFileByteStreamRequest : IDataRequest
    {
        IRemote _remote;
        public SendLocalFileByteStreamRequest(IRemote remote)
        {
            _remote = remote;
        }
        public bool ShowProperties => false;

        public string FriendlyName => "Send File Byte Stream Package Request";

        public string Description => "Asks the client for a specific file byte stream package.";

        public RawDataRequest RequestData(RequestBuilder builder)
        {
            try
            {
                byte[] data = File.ReadAllBytes(builder.Message);
                _remote.UploadBytesToPackageSystem(data, data.Length, builder.Message);
                return RawDataRequest.Success(null);
            }
            catch (Exception ex)
            {
                GlobalServices.Logger.Log($"An error occurred in sending byte data to the server. Error: " + ex, BetterLogger.LogLevel.Error);
                return RawDataRequest.Cancel();
            }
        }

        public void Update(string message)
        {
            throw new NotImplementedException();
        }

        public void UpdateProperties()
        {
            throw new NotImplementedException();
        }
    }
}
