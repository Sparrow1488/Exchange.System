using Encryptors.Aes;
using Encryptors.Rsa;
using ExchangeSystem.Requests;
using ExchangeSystem.Requests.Objects;
using ExchangeSystem.Requests.Objects.Entities;
using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Packages.Protected;
using ExchangeSystem.SecurityData;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace SendDataTests
{
    class Program
    {
        private static RSAParameters _privateKey;
        private static RSAParameters _publicKey;
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(80);
            listener.Start();
            Console.WriteLine("Listen...");

            var client = listener.AcceptTcpClient();
            Console.WriteLine("Connected");
            var stream = client.GetStream();

            byte[] receiveArray = ReadData(ref stream, 1024);
            Console.WriteLine(receiveArray);
            string _infoJson = Encoding.UTF32.GetString(receiveArray);
            var _requestInfo = (RequestInformator)JsonConvert.DeserializeObject(_infoJson, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
            });

            /*
             *            НАЧИНАЕТСЯ РАБОТА РОУТЕРА
             */

            if(_requestInfo.EncryptType == EncryptTypes.AesRsa)
            {
                RsaEncryptor rsa = new RsaEncryptor();
                _publicKey = rsa.PublicKey;
                _privateKey = rsa.PrivateKey;
                RsaConverter converter = new RsaConverter();
                var publicXmlKey = converter.AsXML(rsa.PublicKey);
                byte[] publicRsa = Encoding.UTF32.GetBytes(publicXmlKey);
                WriteData(ref stream, publicRsa);
                byte[] _futureSecretPackageSize = ReadData(ref stream, 128);
                string secretPackageSize = Encoding.UTF32.GetString(_futureSecretPackageSize);
                byte[] bufferForSecretPackage = ReadData(ref stream, Convert.ToInt32(secretPackageSize));
                string _protectedJsonPackage = Encoding.UTF32.GetString(bufferForSecretPackage);
                ProtectedPackage pack = (ProtectedPackage)JsonConvert.DeserializeObject(_protectedJsonPackage, new JsonSerializerSettings
                 {
                     TypeNameHandling = TypeNameHandling.All,
                 });
                Console.WriteLine(pack);
                AesRsaReceiver(pack);
            }

        }
        private static byte[] ReadData(ref NetworkStream stream, int bufferSize)
        {
            byte[] receivedBuffer = new byte[bufferSize];
            do
            {
                stream.Read(receivedBuffer, 0, receivedBuffer.Length);
            }
            while (stream.DataAvailable);
            return receivedBuffer;
        }
        private static void WriteData(ref NetworkStream stream, byte[] buffer)
        {
            do
            {
                stream.Write(buffer, 0, buffer.Length);
            }
            while (stream.DataAvailable);
        }
        private static void AesRsaReceiver(ProtectedPackage protectedPackage)
        {
            if (protectedPackage.Security.EncryptType == EncryptTypes.AesRsa)
            {
                AesRsaSecurity aesRsa = protectedPackage.Security as AesRsaSecurity;
                RsaEncryptor rsa = new RsaEncryptor();
                byte[] encAesKey = Convert.FromBase64String(aesRsa.AesKey);
                byte[] encAesIV = Convert.FromBase64String(aesRsa.AesIV);

                RsaConverter converter = new RsaConverter();
                RSAParameters clientPublicKey = converter.AsParameters(aesRsa.RsaPublicKey);
                byte[] decryptAesKey = rsa.Decrypt(encAesKey, _privateKey);
                byte[] decryptAesIV = rsa.Decrypt(encAesIV, _privateKey);
                AesEncryptor newAesRsa = new AesEncryptor(decryptAesKey, decryptAesIV);
                Console.WriteLine("повезло-повезло");


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
