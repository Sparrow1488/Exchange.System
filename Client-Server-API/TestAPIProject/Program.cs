using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Sendlers;
using ExchangeSystem.Requests.Sendlers.Close;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TestAPIProject
{
    public class Program
    {
        private static ConnectionSettings connectionSettings = new ConnectionSettings("127.0.0.1", 80);
        static void Main(string[] args)
        {
            StartTen();

            Console.ReadKey();
        }
        private static int exceptions = 0;
        private static int processed = 0;
        static void StartTen()
        {
            Task.Factory.StartNew(() => SendRequest());
            Task.Delay(500).Wait();
            Task.Factory.StartNew(() => SendRequest());
            Task.Delay(500).Wait();
            Task.Factory.StartNew(() => SendRequest());
            Task.Delay(500).Wait();
            Task.Factory.StartNew(() => SendRequest());
            Task.Delay(500).Wait();
            Task.Factory.StartNew(() => SendRequest());
            Task.Delay(500).Wait();
            Task.Factory.StartNew(() => SendRequest());
            Task.Delay(500).Wait();
            Task.Factory.StartNew(() => SendRequest());
            Task.Delay(500).Wait();
            Task.Factory.StartNew(() => SendRequest());
            Task.Delay(500).Wait();
            Task.Factory.StartNew(() => SendRequest());
            Task.Delay(500).Wait();
            Task.Factory.StartNew(() => SendRequest());
            Task.Delay(500).Wait();
        }
        static void SendRequest()
        {
            try
            {
                var message = new Message("Чеб такого написать чтоп пш пш по приколу");
                var pack = new NewMessage(message);
                var aesRsaSender = new AesRsaSendler(connectionSettings);
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var response = aesRsaSender.SendRequest(pack);
                stopwatch.Stop();
                Console.WriteLine("Response status: {0}", response.Status);
                Console.WriteLine("Server error message: {0}", response.ErrorMessage);
                Console.WriteLine("Response as string {0}", (string)response.ResponseData);
                Console.WriteLine("Response received in {0} millisecond", stopwatch.ElapsedMilliseconds);
                processed++;
                Console.Write("Exceptions: {0}; Processed: {1}", exceptions, processed);
            }
            catch { exceptions++; processed++; Console.WriteLine("==========Exception==========="); }
        }
    }
}
