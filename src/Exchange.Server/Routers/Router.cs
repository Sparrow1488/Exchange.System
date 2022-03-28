using Exchange.Server.Exceptions.NetworkExceptions;
using Exchange.Server.Protocols;
using Exchange.Server.Protocols.Selectors;
using Exchange.System.Packages;
using Exchange.System.Packages.Default;
using Exchange.System.Protection;
using ExchangeSystem.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Exchange.Server.Routers
{
    public sealed class Router : IRouter
    {
        private Queue<TcpClient> _queue = new Queue<TcpClient>();

        private IProtocolSelector _protocolSelector = new ProtocolSelector();
        private NetworkChannel _networkChannel = new NetworkChannel();
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings() {
            TypeNameHandling = TypeNameHandling.All
        };
        private EncryptType _encryptType = EncryptType.None;
        private IProtocol _selectedProtocol;
        private TcpClient _client = default;
        private Package _receivedPackage;

        public void AddInQueue(TcpClient clientToProccess)
        {
            Ex.ThrowIfNull(clientToProccess);
            _queue.Enqueue(clientToProccess);
        }

        /// <summary> Получает запрос от подключенного пользователя, выбирая необходимый протокол и декодер </summary>
        /// <returns>Пакет-запрос пользователя типа <see cref="IPackage"/></returns>
        public async Task<IPackage> ExtractRequestPackageAsync()
        {
            Ex.ThrowIfTrue<ConnectionException>(!_client.Connected, "Client is not connected");

            using (var stream = _client.GetStream())
            {
                string _requestInfoJson = await _networkChannel.ReadAsync(stream);
                var _requestInfo = JsonConvert.DeserializeObject<RequestInformator>(_requestInfoJson, _jsonSettings);
                _encryptType = _requestInfo.EncryptType;

                _selectedProtocol = LookForProtocol(_requestInfo.EncryptType);
                _receivedPackage = await _selectedProtocol.ReceivePackageAsync(_client) as Package;
                _encryptType = _selectedProtocol.GetProtocolEncryptType();
            }
            return _receivedPackage;
        }

        /// <summary>
        /// Используйте этот метод после метода <see cref="ExtractRequestPackageAsync()"/>".
        /// </summary>
        public EncryptType GetPackageEncryptType() => _encryptType;

        public int GetQueueLength() => _queue.Count;

        private IProtocol LookForProtocol(EncryptType encryptType) =>
            _protocolSelector.SelectProtocol(encryptType);
    }
}
