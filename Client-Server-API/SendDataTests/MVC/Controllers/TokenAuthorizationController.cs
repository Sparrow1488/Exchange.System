using ExchangeServer.MVC.Models;
using ExchangeServer.Protocols.Responders;
using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public class TokenAuthorizationController : Controller
    {
        public override RequestType RequestType => RequestType.TokenAuthorization;

        protected override Responder Responder { get; set; }
        protected override IResponderSelector ResponderSelector { get; set; } = new ResponderSelector();
        private ResponsePackage _response;
        private TcpClient _client;
        private EncryptType _encrypt;

        public override void ProcessRequest(TcpClient connectedClient, Package package, EncryptType encryptType)
        {
            _client = connectedClient;
            _encrypt = encryptType;
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
        private void SendResponse()
        {
            Responder = ResponderSelector.SelectResponder(_encrypt);
            Responder.SendResponse(_client, _response);
        }
        private void PrepareResponse(bool success, string error, object response)
        {
            if (success)
                _response = new ResponsePackage(response, ResponseStatus.Ok);
            else
                _response = new ResponsePackage(string.Empty, ResponseStatus.Exception, error);
        }
    }
}
