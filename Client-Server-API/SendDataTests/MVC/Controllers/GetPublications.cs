using ExchangeServer.MVC.Models;
using ExchangeServer.Protocols.Responders;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public class GetPublications : Controller
    {
        public override RequestTypes RequestType { get; } = RequestTypes.GetPublication;

        protected override Responder Responder { get; set; }
        protected override IResponderSelector ResponderSelector { get; set; } = new ResponderSelector();
        private ResponsePackage _response;
        private TcpClient _client;
        private EncryptType _encrypt;

        public override void ProcessRequest(TcpClient connectedClient, IPackage package, EncryptType encryptType)
        {
            _client = connectedClient;
            _encrypt = encryptType;
            var userPackage = package as Package;
            PublicationModel publicationModel = new PublicationModel();
            var publications = publicationModel.GetAllOrDefault();
            if (publications == null)
                PrepareResponse(false, new Publication[0], "Ошибка обращения к базе данных");
            else if (publications.Length == 0)
                PrepareResponse(false, new Publication[0], "Список публикаций пока пуст");
            else
                PrepareResponse(true, publications, "");

            SendResponse();
        }
        private void PrepareResponse(bool success, Publication[] posts, string errorMessage)
        {
            if (success)
                _response = new ResponsePackage(posts, ResponseStatus.Ok, "");
            else
                _response = new ResponsePackage(string.Empty, ResponseStatus.Exception, errorMessage);
        }
        private void SendResponse()
        {
            Responder = ResponderSelector.SelectResponder(_encrypt);
            Responder.SendResponse(_client, _response);
        }
    }
}
