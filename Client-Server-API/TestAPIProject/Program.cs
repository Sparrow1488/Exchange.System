using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Sendlers;
using ExchangeSystem.Requests.Sendlers.Close;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestAPIProject
{
    public class Program
    {
        private static ConnectionSettings connectionSettings = new ConnectionSettings("127.0.0.1", 80);
        static async Task Main(string[] args)
        {
            //StartTen();
            while (true)
            {
                try
                {
                    Console.WriteLine("Send...");
                    await SendRequest();
                    Console.ReadKey();
                }
                catch { Console.WriteLine("Ошибка"); };
            }
            
        }
        private static int exceptions = 0;
        private static int processed = 0;
        static void StartTen()
        {
            Task.Factory.StartNew(() => SendRequest());
            //Task.Delay(500).Wait();
            //Task.Factory.StartNew(() => SendRequest());
            //Task.Delay(500).Wait();
            //Task.Factory.StartNew(() => SendRequest());
            //Task.Delay(500).Wait();
            //Task.Factory.StartNew(() => SendRequest());
            //Task.Delay(500).Wait();
            //Task.Factory.StartNew(() => SendRequest());
            //Task.Delay(500).Wait();
            //Task.Factory.StartNew(() => SendRequest());
            //Task.Delay(500).Wait();
            //Task.Factory.StartNew(() => SendRequest());
            //Task.Delay(500).Wait();
            //Task.Factory.StartNew(() => SendRequest());
            //Task.Delay(500).Wait();
            //Task.Factory.StartNew(() => SendRequest());
            //Task.Delay(500).Wait();
            //Task.Factory.StartNew(() => SendRequest());
            //Task.Delay(500).Wait();
        }
        static async Task  SendRequest()
        {
            var message = new Message("Чеб такого написать чтоп пш пш по приколу");
            var pack = new NewMessage(message);
            var aesRsaSender = new AesRsaSendler(connectionSettings);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var response = await aesRsaSender.SendRequest(pack);
            stopwatch.Stop();
            ShowResponseStatus(response, stopwatch.ElapsedMilliseconds);
        }
        static void ShowResponseStatus(ResponsePackage response, long stopwatch)
        {
            Console.WriteLine("Response status: {0}", response.Status);
            Console.WriteLine("Server error message: {0}", response.ErrorMessage);
            Console.WriteLine("Response as string {0}", (string)response.ResponseData);
            Console.WriteLine("Response received in {0} millisecond", stopwatch);
            processed++;
            Console.Write("Exceptions: {0}; Processed: {1}", exceptions, processed);
        }
    }
}
