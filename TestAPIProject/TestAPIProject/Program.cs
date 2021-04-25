using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Sendlers;
using ExchangeSystem.Requests.Sendlers.Close;
using ExchangeSystem.Requests.Sendlers.Open;
using System;

namespace TestAPIProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            var info = new UserPassport("Valentin", "1488");
            Package auth = new Authorization(info);
            var connectionInfo = new ConnectionSettings("127.0.0.1", 80);
            //var sendler = new RequestSendler(connectionInfo);
            //string jsonResponse = sendler.SendRequest(auth);
            //Console.WriteLine(jsonResponse);

            var aesRsa = new AesRsaSendler(connectionInfo);
            string jsonSecretResponse = aesRsa.SendRequest(auth);
        }
    }
}
