using Exchange.Server .Models;
using Exchange.Server.Protocols;
using Exchange.Server.Protocols.Selectors;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages.Default;
using Exchange.System.Protection;
using System.Net.Sockets;

namespace Exchange.Server .Controllers
{
    public class GetSource : Controller
    {
        public override RequestType RequestType => RequestType.GetSource;

        protected override Protocol Protocol { get; set; }
        protected override IProtocolSelector ProtocolSelector { get; set; } = new ProtocolSelector();

        public override void ProcessRequest(TcpClient connectedClient, Package package, EncryptType encryptType)
        {
            Client = connectedClient;
            EncryptType = encryptType;
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
        private void PrepareResponse(bool success, Source[] sources, string error)
        {
            if (success)
                Response = new ResponsePackage(sources, ResponseStatus.Ok);
            else
                Response = new ResponsePackage(string.Empty, ResponseStatus.Exception, error);
        }
    }
}
