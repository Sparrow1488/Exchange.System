using ExchangeServer.LocalDataBase;
using ExchangeServer.MVC.Models;
using ExchangeServer.Protocols;
using ExchangeServer.Protocols.Selectors;
using ExchangeSystem.Requests.Objects;
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
        public override RequestType RequestType => RequestType.NewPublication;
        protected override Protocol Protocol { get; set; }
        protected override IProtocolSelector ProtocolSelector { get; set; } = new ProtocolSelector();

        public override void ProcessRequest(TcpClient connectedClient, Package package, EncryptType encryptType)
        {
            var userPack = package as Package;
            var token = userPack.UserToken;
            EncryptType = encryptType;
            Client = connectedClient;
            if (string.IsNullOrWhiteSpace(token))
                PrepareResponse(false, "Вы не можете вызывать данную команду без токена авторизации");
            else
            {
                var isAdmin = CheckUserForAdminStatus(token);
                if (isAdmin)
                {
                    PublicationModel publicationModel = new PublicationModel();
                    var userPublication = userPack.RequestObject as Publication;
                    var success = publicationModel.Add(Validation(userPublication));
                    if (success)
                        PrepareResponse(true, "");
                    else
                        PrepareResponse(false, "Ошибка размещения публикации. Обратитесь к системному администратору или повторите свою попытку");
                }
                else
                    PrepareResponse(false, "Ваш статус не позволяет Вам размещать новые публикации в системе");
            }
            SendResponse();
         }
        private void PrepareResponse(bool success, string errorMessage)
        {
            if (success)
                Response = new ResponsePackage("Публикация успешно размещена", ResponseStatus.Ok);
            else
                Response = new ResponsePackage("", ResponseStatus.Exception, errorMessage);
        }
        private Publication Validation(Publication post)
        {
            post.DateCreate = DateTime.Now;
            return post;
        }
        private bool CheckUserForAdminStatus(string token)
        {
            var status = ServerLocalDb.CheckStatus(token);
            if (status == AdminStatus.Admin)
                return true;
            else
                return false;
        }
    }
}
