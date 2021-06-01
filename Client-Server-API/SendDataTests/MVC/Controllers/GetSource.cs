using ExchangeServer.MVC.Models;
using ExchangeServer.Protocols.Responders;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Collections.ObjectModel;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public class GetSource : Controller
    {
        public override RequestType RequestType => RequestType.GetSource;

        protected override Responder Responder { get; set; }
        protected override IResponderSelector ResponderSelector { get; set; } = new ResponderSelector();
        private TcpClient _client;
        private ResponsePackage _response;
        private EncryptType _encrypt;

        public override void ProcessRequest(TcpClient connectedClient, Package package, EncryptType encryptType)
        {
            _client = connectedClient;
            _encrypt = encryptType;
            var userPack = package as Package;
            var testPubl = userPack.RequestObject as Publication;
            var collectionIds = testPubl.sourcesId;
            if (collectionIds != null && collectionIds.Length > 0)
            {
                SourceModel sourceModel = new SourceModel();
                var findAll = sourceModel.GetAllOrDefault(collectionIds);
                if (findAll != null && findAll.Length > 0)
                    PrepareResponse(true, findAll, "");
                else
                    PrepareResponse(false, new Source[0], "Публикации не найдены или, возможно, были удалены");
            }
            else
                PrepareResponse(false, new Source[0], "Переданы нулевые идентификационные номеру");
            SendResponse();
        }
        private void SendResponse()
        {
            Responder = ResponderSelector.SelectResponder(_encrypt);
            Responder.SendResponse(_client, _response);
        }
        private void PrepareResponse(bool success, Source[] sources, string error)
        {
            if (success)
                _response = new ResponsePackage(sources, ResponseStatus.Ok);
            else
                _response = new ResponsePackage(string.Empty, ResponseStatus.Exception, error);
        }
    }
}
