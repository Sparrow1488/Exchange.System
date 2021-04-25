using Encryptors.Aes;
using ExchangeSystem.Requests.Packages;
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
        public ConnectionSettings ConnectionSettings { get; }
        public ProtectedPackage SecretPackage { get; protected set; }
        private RequestInformation _requestInfo;

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
            
            var stream = client.GetStream();
            byte[] buffer = Encoding.UTF32.GetBytes(secretJsonPackage);

            _requestInfo = new RequestInformation(EncryptTypes.AesRsa, buffer.Length);
            string requestJson = JsonConvert.SerializeObject(_requestInfo, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            byte[] requestInfoBuffer = Encoding.UTF32.GetBytes(requestJson);


            WriteData(ref stream, requestInfoBuffer);
            byte[] publicServerRsa = ReadData(ref stream, 256);
            string _key = Encoding.UTF32.GetString(publicServerRsa);
            WriteData(ref stream, buffer);

            stream.Close();

            string jsonResponse = Encoding.UTF32.GetString(publicServerRsa);
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
