using ExchangeServer.MVC.Exceptions.NetworkExceptions;
using ExchangeServer.Protocols;
using ExchangeServer.Protocols.Selectors;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeServer.MVC.Routers
{
    public class Router : IRouter
    {
        private TcpClient _client;
        private IProtocolSelector _selector;
        private IProtocol _selectedProtocol;
        private Package _receivedPackage;
        private EncryptType _encryptType = EncryptType.None;
        private NetworkHelper _networkHelper = new NetworkHelper();

        /// <summary>
        /// Получает запрос от подключенного пользователя, выбирая необходимый протокол и декодера
        /// </summary>
        /// <param name="client">Подключенный клиент</param>
        /// <returns>Пакет-запрос пользователя типа Package</returns>
        public async Task<IPackage> IssueRequestAsync(TcpClient client)
        {
            if (!client.Connected)
                throw new ConnectionException();
            if (client == null)
                throw new NullReferenceException($"Переданный клиент '{nameof(client)}' не может быть равен null") ;

            _client = client;
            var stream = _client.GetStream();

            byte[] receivedData = await _networkHelper.ReadDataAsync(stream, 1024);
            string _requestInfoJson = _networkHelper.Encoding.GetString(receivedData);
            var _requestInfo = (RequestInformator)JsonConvert.DeserializeObject(_requestInfoJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
            _encryptType = _requestInfo.EncryptType;

            _selectedProtocol = LookForProtocol(_requestInfo.EncryptType);
            _receivedPackage = await _selectedProtocol.ReceivePackage(client) as Package;
            _encryptType = _selectedProtocol.GetProtocolEncryptType();

            return _receivedPackage;
        }
        /// <summary>
        /// Используйте этот метод после метода "IssueRequest()".
        /// </summary>
        public EncryptType GetPackageEncryptType()
        {
            return _encryptType;
        }
        private IProtocol LookForProtocol(EncryptType encryptType)
        {
            _selector = new ProtocolSelector();
            return _selector.SelectProtocol(encryptType);
        }
        
    }
}
