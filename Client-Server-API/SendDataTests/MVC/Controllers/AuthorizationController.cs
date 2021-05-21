using ExchangeServer.MVC.Exceptions.NetworkExceptions;
using ExchangeServer.MVC.Models;
using ExchangeServer.Protocols.Responders;
using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public class AuthorizationController : Controller
    {
        private TcpClient _client;
        public override RequestTypes RequestType => RequestTypes.Authorization;
        protected override Responder Responder { get; set; }
        protected override IResponderSelector ResponderSelector { get; set; }
        private ResponsePackage _responsePackage;

        public override void ProcessRequest(TcpClient connectedClient, IPackage package, EncryptType encryptType)
        {
            _client = connectedClient;
            var receivedPack = package as Package;
            var userPassport = receivedPack.RequestObject as UserPassport;
            Console.WriteLine("Received user passport. Pas: {0}, Log: {1}", userPassport.Password, 
                                                                                                                                                userPassport.Login);
            UserModel userModel = new UserModel();
            var findUser = userModel.ReceiveUserBy(userPassport);
            var validUser = new User(findUser); // сделал такую дикость, потому что не понял как изменить автосгенерированный тип EF.User на мой, нормальный
            validUser.Passport = userPassport;
            if (findUser != null)
                PrepareResponsePackage(true, validUser);
            else
                PrepareResponsePackage(false, validUser);
            ResponderSelector responderSelector = new ResponderSelector();
            Responder = responderSelector.SelectResponder(encryptType);
            if (connectedClient.Connected)
                Responder.SendResponse(connectedClient, _responsePackage);
            else
                throw new ConnectionException("Клиент не был подключен");
        }
        public void PrepareResponsePackage(bool authSuccess, User authUser)
        {
            if(authSuccess)
                _responsePackage = new ResponsePackage(authUser, ResponseStatus.Ok);
            else
                _responsePackage = new ResponsePackage(string.Empty, ResponseStatus.Exception, "Ошибка авторизации");
        }
    }
}
