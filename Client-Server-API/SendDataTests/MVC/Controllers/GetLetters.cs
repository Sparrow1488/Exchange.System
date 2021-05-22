using ExchangeServer.MVC.Models;
using ExchangeServer.Protocols.Responders;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public class GetLetters : Controller
    {
        public override RequestTypes RequestType => RequestTypes.GetMessages;

        protected override Responder Responder { get; set; }
        protected override IResponderSelector ResponderSelector { get; set; } = new ResponderSelector();
        private ResponsePackage _response;

        public override void ProcessRequest(TcpClient connectedClient, IPackage package, EncryptType encryptType)
        {
            var userPackage = (Package)package;
            var token = userPackage.UserToken;
            if (string.IsNullOrWhiteSpace(token))
                PrepareReponse(false, new Letter[0], "Для вызова данной команды требуется токен авторизованного пользователя!");
            else
            {
                LetterModel letterModel = new LetterModel();
                var allLetters = letterModel.GetAllOrDefault();
                if (allLetters == null)
                    PrepareReponse(false, new Letter[0], "Возникла ошибка при получении писем");
                else if(allLetters.Length == 0)
                    PrepareReponse(false, new Letter[0], "Список писем пока пуст");
                else
                    PrepareReponse(true, allLetters, "");
            }
            Responder = ResponderSelector.SelectResponder(encryptType);
            Responder.SendResponse(connectedClient, _response);
        }
        private void PrepareReponse(bool success, Letter[] letters, string errorMessage)
        {
            if (success)
                _response = new ResponsePackage(letters, ResponseStatus.Ok);
            else
                _response = new ResponsePackage(string.Empty, ResponseStatus.Exception, errorMessage);
        }
    }
}
