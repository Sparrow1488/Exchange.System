using Exchange.Server.LocalDataBase;
using Exchange.Server .Exceptions.NetworkExceptions;
using Exchange.Server .Models;
using Exchange.Server.Protocols;
using Exchange.Server.Protocols.Selectors;
using Exchange.System.Requests.Objects;
using Exchange.System.Requests.Objects.Entities;
using Exchange.System.Requests.Packages;
using Exchange.System.Requests.Packages.Default;
using Exchange.System.Protection;
using System;
using System.Net.Sockets;

namespace Exchange.Server .Controllers
{
    public class AuthorizationController : Controller
    {
        public override RequestType RequestType => RequestType.Authorization;
        protected override Protocol Protocol { get; set; }
        protected override IProtocolSelector ProtocolSelector { get; set; } = new ProtocolSelector();

        private string _authToken = string.Empty;

        public override void ProcessRequest(TcpClient connectedClient, Package package, EncryptType encryptType)
        {
            EncryptType = encryptType;
            Client = connectedClient;
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
                validUser.UserPassport = findPassport;
                if (validUser != null)
                    PrepareResponsePackage(true, validUser);
                else
                    PrepareResponsePackage(false, validUser);
            }
            else
                PrepareResponsePackage(false, null);
            SendResponse();
        }
        public void PrepareResponsePackage(bool authSuccess, User authUser)
        {
            if (authSuccess)
            {
                _authToken = ServerLocalDb.AddNew(authUser.UserPassport);
                authUser.UserPassport.Token = _authToken;
                Response = new ResponsePackage(authUser, ResponseStatus.Ok);
            }
            else
                Response = new ResponsePackage(string.Empty, ResponseStatus.Exception, "Ошибка авторизации");
        }
    }
}
