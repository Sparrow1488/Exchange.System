using ExchangeServer.LocalDataBase;
using ExchangeServer.MVC.Models;
using ExchangeServer.Protocols;
using ExchangeServer.Protocols.Selectors;
using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public class GetLetters : Controller
    {
        public override RequestType RequestType => RequestType.GetMessages;
        protected override Protocol Protocol { get; set; }
        protected override IProtocolSelector ProtocolSelector { get; set; } = new ProtocolSelector();


        public override void ProcessRequest(TcpClient connectedClient, Package package, EncryptType encryptType)
        {
            Client = connectedClient;
            EncryptType = encryptType;
            var userPackage = (Package)package;
            var token = userPackage.UserToken;
            if (string.IsNullOrWhiteSpace(token))
                PrepareReponse(false, new Letter[0], "Для вызова данной команды требуется токен авторизованного пользователя!");
            else
            {
                var isAdmin = CheckUserForAdminStatus(token);
                if (isAdmin)
                {
                    LetterModel letterModel = new LetterModel();
                    var allLetters = letterModel.GetAllOrDefault();
                    if (allLetters == null)
                        PrepareReponse(false, new Letter[0], "Возникла ошибка при получении писем");
                    else if (allLetters.Length == 0)
                        PrepareReponse(false, new Letter[0], "Список писем пока пуст");
                    else
                        PrepareReponse(true, allLetters, "");
                }
                else
                    PrepareReponse(false, new Letter[0], "Недостаточно прав");
            }
            SendResponse();
        }
        private void PrepareReponse(bool success, Letter[] letters, string errorMessage)
        {
            if (success)
                Response = new ResponsePackage(letters, ResponseStatus.Ok);
            else
                Response = new ResponsePackage(string.Empty, ResponseStatus.Exception, errorMessage);
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
