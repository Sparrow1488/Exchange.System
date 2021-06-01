using ExchangeServer.MVC.Exceptions.NetworkExceptions;
using ExchangeServer.MVC.Models;
using ExchangeServer.Protocols.Responders;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public class NewLetter : Message
    {
        public override RequestType RequestType => RequestType.NewMessage;
        protected override Responder Responder { get; set; }
        protected override IResponderSelector ResponderSelector { get; set; }
        private Package _clientRequestObject;
        private TcpClient _client;
        private EncryptType _encryptType = EncryptType.None;
        private ResponsePackage _responsePackage;

        public override void ProcessRequest(TcpClient connectedClient, Package package, EncryptType encryptType)
        {
            AssignValues(connectedClient, package, encryptType);

            var userLetter = _clientRequestObject.RequestObject as Letter;
            if(userLetter != null)
            {
                userLetter = Validation(userLetter);
                LetterModel letterModel = new LetterModel();
                bool wasAddSuccess = letterModel.Add(userLetter);
                PrepareResponsePackage(wasAddSuccess);
                ResponderSelector responderSelector = new ResponderSelector();
                Responder = responderSelector.SelectResponder(_encryptType);
                if (connectedClient.Connected)
                    Responder.SendResponse(connectedClient, _responsePackage);
                else
                    throw new ConnectionException("Клиент не был подключен");
            }
        }
        private void AssignValues(TcpClient client, IPackage package, EncryptType encryptType)
        {
            _client = client;
            _clientRequestObject = (Package)package;
            _encryptType = encryptType;
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
        private Letter Validation(Letter letter)
        {
            letter.DateCreate = DateTime.Now;
            return letter;
        }
        
    }
}
