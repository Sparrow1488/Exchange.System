using ExchangeSystem.SecurityData;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace ExchangeServer.Protocols.Responders
{
    public class AesRsaResponder : Responder
    {
        public override EncryptTypes EncryptType => EncryptTypes.AesRsa;
        private NetworkHelper _networkHelper = new NetworkHelper();

        public override void SendResponse(TcpClient toClient, object response)
        {
            if (response == null)
                throw new NullReferenceException("Похоже, вы передали null при отправке ответа");

            var clientStream = toClient.GetStream();
            byte[] responseData = Encoding.UTF32.GetBytes(ToJson(response));
            byte[] responseSize = Encoding.UTF32.GetBytes(Convert.ToString(responseData.Length));
            _networkHelper.WriteData(ref clientStream, responseSize);
            _networkHelper.WriteData(ref clientStream, responseData);
        }
        private string ToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}
