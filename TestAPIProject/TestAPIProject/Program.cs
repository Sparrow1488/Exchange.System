using ExchangeSystem.Requests;
using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Sendlers;
using ExchangeSystem.Requests.Sendlers.Open;
using Newtonsoft.Json;
using System;

namespace TestAPIProject
{
    class Program
    {
        public enum Types
        {
            Auth = 1
        }
        static void Main(string[] args)
        {
            var info = new UserInfo("Valentin", "1488");
            Package auth = new Authorization(info);
            var connectionInfo = new ConnectionSettings("127.0.0.1", 80);
            var sendler = new RequestSendler(auth, connectionInfo);
            string jsonResponse = sendler.SendRequest();
            Console.WriteLine(jsonResponse);
        }
    }
}
