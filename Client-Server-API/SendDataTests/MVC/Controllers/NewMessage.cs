using ExchangeServer.MVC.Models;
using ExchangeServer.Protocols.Responders;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.SecurityData;
using System.Net.Sockets;

namespace ExchangeServer.MVC.Controllers
{
    public class NewMessage : Message
    {
        public NewMessage(TcpClient client) : base(client)
        {
            _client = client;
        }
        private TcpClient _client;
        public override RequestTypes RequestType => RequestTypes.NewMessage;
        protected override Responder Responder { get; set; }
        protected override IResponderSelector ResponderSelector { get; set; }

        public override void ProcessRequest(IPackage package, EncryptTypes encryptType)
        {
            MessageModel model = new MessageModel();
            string response = model.Get(); // ЭТО ВСЕ ВРЕМЕННЫЕ УСЛОВНОСТИ
            ResponderSelector responderSelector = new ResponderSelector();
            var pack = (Package)package;
            var responder = responderSelector.SelectResponder(encryptType);
            responder.SendResponse(response); //TODO: подумать над форматом ответов
        }
    }
}
