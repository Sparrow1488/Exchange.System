using ExchangeServer.MVC.Exceptions.NetworkExceptions;
using ExchangeServer.MVC.Models;
using ExchangeServer.Protocols.Responders;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public class NewMessage : Message
    {
        public override RequestTypes RequestType => RequestTypes.NewMessage;
        protected override Responder Responder { get; set; }
        protected override IResponderSelector ResponderSelector { get; set; }
        private Package _clientRequestObject;
        private TcpClient _client;

        public override void ProcessRequest(TcpClient connectedClient, IPackage package, EncryptTypes encryptType)
        {
            _client = connectedClient;
            _clientRequestObject = (Package)package;

            MessageModel model = new MessageModel();
            bool wasAddSuccess = model.AddNew(); // ЭТО ВСЕ ВРЕМЕННЫЕ УСЛОВНОСТИ
            ResponderSelector responderSelector = new ResponderSelector();
            Responder = responderSelector.SelectResponder(encryptType);
            if (connectedClient.Connected)
                Responder.SendResponse(connectedClient, null); //TODO: подумать над форматом ответов
            else
                throw new ConnectionException("Клиент был не подключен");
        }
    }
}
