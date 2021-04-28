using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Sendlers;
using ExchangeSystem.Requests.Sendlers.Close;

namespace TestAPIProject
{
    public class Program
    {
        private static ConnectionSettings connectionSettings = new ConnectionSettings("127.0.0.1", 80);
        static void Main(string[] args)
        {
            //var info = new UserPassport("Valentin", "1488");
            //Package auth = new Authorization(info);
            
            //var aesRsa = new AesRsaSendler(connectionInfo);
            //string jsonSecretResponse = aesRsa.SendRequest(auth);

            var message = new Message("Чеб такого написать чтоп пш пш по приколу");
            var pack = new NewMessage(message);
            var aesRsaSender = new AesRsaSendler(connectionSettings);
            string response = aesRsaSender.SendRequest(pack);
        }
    }
}
