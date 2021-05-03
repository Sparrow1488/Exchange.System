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
        private EncryptTypes _encryptType = EncryptTypes.None;
        private Security _security;
        private ResponsePackage _responsePackage;

        public override void ProcessRequest(TcpClient connectedClient, IPackage package, Security packageSecurity)
        {
            CheckValidation(connectedClient, package, packageSecurity);

            MessageModel model = new MessageModel();
            bool wasAddSuccess = model.AddNew(); // ЭТО ВСЕ ВРЕМЕННЫЕ УСЛОВНОСТИ
            PrepareResponsePackage(wasAddSuccess);

            ResponderSelector responderSelector = new ResponderSelector();
            Responder = responderSelector.SelectResponder(_encryptType);
            if (connectedClient.Connected)
                Responder.SendResponse(connectedClient, _responsePackage); //TODO: подумать над форматом ответов
            else
                throw new ConnectionException("Клиент был не подключен");

        }
        private void CheckValidation(TcpClient client, IPackage package, Security packageSecurity)
        {
            _client = client;
            _clientRequestObject = (Package)package;
            if (packageSecurity != null)
            {
                _encryptType = packageSecurity.EncryptType;
                _security = packageSecurity;
            }
        }
        private void PrepareResponsePackage(bool addMessageSuccess)
        {
            string errorMessage = string.Empty;
            object responseObject;
            if (addMessageSuccess)
            {
                responseObject = "Сообщение доставлено";
                _responsePackage = new ResponsePackage(responseObject, ResponseStatus.Ok);
            }
            else
            {
                errorMessage = "Неизвестная ошибка";
                _responsePackage = new ResponsePackage(null, ResponseStatus.Exception, errorMessage);
            }
        }
    }
}
