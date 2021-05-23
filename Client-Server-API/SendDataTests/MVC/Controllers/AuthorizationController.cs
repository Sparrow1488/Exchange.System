using ExchangeServer.LocalDataBase;
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
        private string _authToken = string.Empty;
        private EncryptType _encrypt;

        public override void ProcessRequest(TcpClient connectedClient, IPackage package, EncryptType encryptType)
        {
            _encrypt = encryptType;
            _client = connectedClient;
            var receivedPack = package as Package;
            var userPassport = receivedPack.RequestObject as UserPassport;
            Console.WriteLine("Received user passport. Pas: {0}, Log: {1}", userPassport.Password, 
                                                                                                                                                userPassport.Login);
            UserModel userModel = new UserModel();
            var findUser = userModel.ReceiveUserBy(userPassport);
            if (findUser != null)
            {
                var findPassport = userModel.ReceivePassportBy(userPassport.Login, userPassport.Password);
                var validUser = new User(findUser); // сделал такую дикость, потому что не понял как изменить автосгенерированный тип EF.User на мой, нормальный
                validUser.Passport = findPassport;
                if (validUser != null)
                    PrepareResponsePackage(true, validUser);
                else
                    PrepareResponsePackage(false, validUser);
                SendResponse();
            }
            else
                PrepareResponsePackage(false, null);
        }
        public void PrepareResponsePackage(bool authSuccess, User authUser)
        {
            if (authSuccess)
            {
                _authToken = ServerLocalDb.AddNew(authUser.Passport);
                authUser.Passport.Token = _authToken;
                _responsePackage = new ResponsePackage(authUser, ResponseStatus.Ok);
            }
            else
                _responsePackage = new ResponsePackage(string.Empty, ResponseStatus.Exception, "Ошибка авторизации");
        }
        private void SendResponse()
        {
            ResponderSelector responderSelector = new ResponderSelector();
            Responder = responderSelector.SelectResponder(_encrypt);
            if (_client.Connected)
                Responder.SendResponse(_client, _responsePackage);
            else
                throw new ConnectionException("Клиент не был подключен");
        }
    }
}
