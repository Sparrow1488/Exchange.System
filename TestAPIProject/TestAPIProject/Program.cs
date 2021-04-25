using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Sendlers;
using ExchangeSystem.Requests.Sendlers.Close;

namespace TestAPIProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            var info = new UserPassport("Valentin", "1488");
            Package auth = new Authorization(info);
            var connectionInfo = new ConnectionSettings("127.0.0.1", 80);
            var aesRsa = new AesRsaSendler(connectionInfo);
            string jsonSecretResponse = aesRsa.SendRequest(auth);
        }
    }
}
