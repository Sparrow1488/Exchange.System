using Encryptors.Aes;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Packages.Protected;
using ExchangeSystem.SecurityData;
using Newtonsoft.Json;
using System;
using System.Net.Sockets;
using System.Text;

namespace ExchangeSystem.Requests.Sendlers.Close
{
    public abstract class EncryptRequestSendler : IEncryptRequestSendler
    {
        public EncryptRequestSendler(ConnectionSettings settings)
        {
            ConnectionSettings = settings;
        }

        [JsonProperty]
        public ConnectionSettings ConnectionSettings { get; }
        [JsonProperty]
        public ProtectedPackage SecretPackage { get; protected set; }

        public string SendRequest(IPackage package)
        {
            string jsonPackage = package.ToJson();
            AesEncryptor aes = new AesEncryptor();
            string encryptPackage = Convert.ToBase64String(aes.EncryptString(jsonPackage));
            string base64IV = aes.IVToBase64();
            string base64KEY= aes.KeyToBase64();
            Security security = new AesRsaSecurity(string.Empty, string.Empty, base64KEY, base64IV);
            SecretPackage = new ProtectedPackage(encryptPackage, security);
            string secretJsonPackage = SecretPackage.ToJson();

            var client = new TcpClient();
            client.Connect(ConnectionSettings.HostName, ConnectionSettings.Port);
            byte[] buffer = Encoding.UTF32.GetBytes(secretJsonPackage);

            var stream = client.GetStream();

            WriteData(ref stream, buffer);
            byte[] receivedBuffer = ReadData(ref stream, 256);

            stream.Close();

            string jsonResponse = Encoding.UTF32.GetString(receivedBuffer);
            return jsonResponse;
        }

        private byte[] ReadData(ref NetworkStream stream, int bufferSize)
        {
            byte[] receivedBuffer = new byte[bufferSize];
            do
            {
                stream.Read(receivedBuffer, 0, receivedBuffer.Length);
            }
            while (stream.DataAvailable);
            return receivedBuffer;
        }
        private void WriteData(ref NetworkStream stream, byte[] buffer)
        {
            do
            {
                stream.Write(buffer, 0, buffer.Length);
            }
            while (stream.DataAvailable);
        }
        public string ToEncryptJson()
        {
            throw new NotImplementedException();
        }
    }
}
