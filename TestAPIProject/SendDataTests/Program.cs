using Encryptors.Aes;
using ExchangeSystem.Requests;
using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Packages.Protected;
using ExchangeSystem.SecurityData;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace SendDataTests
{
    class Program
    {

        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(80);
            listener.Start();
            Console.WriteLine("Listen...");

            var client = listener.AcceptTcpClient();
            Console.WriteLine("Connected");
            var stream = client.GetStream();

            byte[] receiveArray = new byte[1024];
            StringBuilder builder = new StringBuilder();
            do
            {
                int bytes = stream.Read(receiveArray, 0, receiveArray.Length);
                builder.Append(Encoding.UTF32.GetString(receiveArray, 0, bytes));
            }
            while (stream.DataAvailable);

            ProtectedPackage pack = (ProtectedPackage)JsonConvert.DeserializeObject(builder.ToString(), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });
            Console.WriteLine(pack);
            AesRsaReceiver(pack);

        }
        private static void AesRsaReceiver(ProtectedPackage protectedPackage)
        {
            AesEncryptor aes = new AesEncryptor();
            if (protectedPackage.Security.EncryptType == EncryptTypes.AesRsa)
            {
                AesRsaSecurity aesRsa = protectedPackage.Security as AesRsaSecurity;
                byte[] KEY = aes.FromBase64ToKey(aesRsa.AesKey);
                byte[] IV = aes.FromBase64ToKey(aesRsa.AesIV);

                AesEncryptor newAesRsa = new AesEncryptor(KEY, IV);

                string jsonPack = newAesRsa.DecryptString(Convert.FromBase64String(protectedPackage.SecretPackage));
                Package deryptPack = (Package)JsonConvert.DeserializeObject(jsonPack, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                });
                Console.WriteLine(deryptPack.RequestType);
            }
        }
    }
}
