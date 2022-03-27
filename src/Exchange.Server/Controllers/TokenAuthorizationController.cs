using Exchange.Server.Models;
using Exchange.Server.Protocols;
using Exchange.Server.Protocols.Selectors;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages.Default;
using Exchange.System.Protection;
using System.Net.Sockets;

namespace Exchange.Server.Controllers
{
    public class TokenAuthorizationController : Controller
    {
        public override RequestType RequestType => RequestType.TokenAuthorization;
        protected override Protocol Protocol { get; set; }
        protected override IProtocolSelector ProtocolSelector { get; set; } = new ProtocolSelector();

        public override void ProcessRequest(TcpClient connectedClient, Package package, EncryptType encryptType)
        {
            Client = connectedClient;
            EncryptType = encryptType;
            var userPack = package as Package;
            var userPassport = userPack.RequestObject as UserPassport;
            if (string.IsNullOrWhiteSpace(userPassport.Token))
                PrepareResponse(false, "Не верный токен", string.Empty);
            else
            {
                UserModel userModel = new UserModel();
                var findUser = userModel.ReceiveUserBy(userPassport.Token);
                if (findUser == null)
                    PrepareResponse(false, "Ошибка авторизации", string.Empty);
                else
                    PrepareResponse(true, string.Empty, findUser);
            }
            SendResponse();
        }
        private void PrepareResponse(bool success, string error, object response)
        {
            if (success)
                Response = new ResponsePackage(response, ResponseStatus.Ok);
            else
                Response = new ResponsePackage(string.Empty, ResponseStatus.Exception, error);
        }
    }
}
