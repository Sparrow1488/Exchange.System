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
    public class NewPublication : Controller
    {
        public override RequestTypes RequestType => RequestTypes.NewPublication;

        protected override Responder Responder { get; set; }
        protected override IResponderSelector ResponderSelector { get; set; } = new ResponderSelector();
        private ResponsePackage _response;
        private EncryptType _encrypt;
        private TcpClient _client;

        public override void ProcessRequest(TcpClient connectedClient, IPackage package, EncryptType encryptType)
        {
            var userPack = package as Package;
            var token = userPack.UserToken;
            _encrypt = encryptType;
            _client = connectedClient;
            if (string.IsNullOrWhiteSpace(token))
                PrepareResponse(false, "Вы не можете вызывать данную команду без токена авторизации");
            else
            {
                PublicationModel publicationModel = new PublicationModel();
                var userPublication = userPack.RequestObject as Publication;
                var success = publicationModel.Add(Validation(userPublication));
                if (success)
                    PrepareResponse(true, "");
                else
                    PrepareResponse(false, "Ошибка размещения публикации. Обратитесь к системному администратору или повторите свою попытку");
            }
            SendResponse();
         }
        private void PrepareResponse(bool success, string errorMessage)
        {
            if (success)
                _response = new ResponsePackage("Публикация успешно размещена", ResponseStatus.Ok);
            else
                _response = new ResponsePackage("", ResponseStatus.Exception, errorMessage);
        }
        private void SendResponse()
        {
            Responder = ResponderSelector.SelectResponder(_encrypt);
            Responder.SendResponse(_client, _response);
        }
        private Publication Validation(Publication post)
        {
            post.DateCreate = DateTime.Now;
            return post;
        }
    }
}
