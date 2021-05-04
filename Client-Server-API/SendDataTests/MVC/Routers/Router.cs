﻿using ExchangeServer.MVC.Exceptions.NetworkExceptions;
using ExchangeServer.Protocols;
using ExchangeServer.Protocols.Selectors;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace ExchangeServer.MVC.Routers
{
    public class Router : IRouter
    {
        private TcpClient _client;
        private IProtocolSelector _selector;
        private IProtocol _selectedProtocol;
        private IPackage _receivedPackage;
        private NetworkHelper _networkHelper = new NetworkHelper();
        private EncryptType _encryptType = EncryptType.None;
        public IPackage IssueRequest(TcpClient client)
        {
            if (!client.Connected)
                throw new ConnectionException();
            if (client == null)
                throw new NullReferenceException($"Переданный клиент '{nameof(client)}' не может быть равен null") ;

            _client = client;
            var stream = _client.GetStream();

            byte[] receivedData = _networkHelper.ReadData(ref stream, 1024);
            string _requestInfoJson = Encoding.UTF32.GetString(receivedData);
            var _requestInfo = (RequestInformator)JsonConvert.DeserializeObject(_requestInfoJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
            _encryptType = _requestInfo.EncryptType;

            _selectedProtocol = LookForProtocol(_requestInfo.EncryptType);
            _receivedPackage = _selectedProtocol.ReceivePackage(client);
            _encryptType = _selectedProtocol.GetPackageEncryptType();

            return _receivedPackage;
        }

        private IProtocol LookForProtocol(EncryptType encryptType)
        {
            _selector = new ProtocolSelector();
            return _selector.SelectProtocol(encryptType);
        }
        /// <summary>
        /// Используйте этот метод после метода "IssueRequest()".
        /// </summary>
        /// <returns>Null, если у пакета отсутсвует защита</returns>
        public EncryptType GetPackageEncryptType()
        {
            return _encryptType;
        }

        
    }
}
