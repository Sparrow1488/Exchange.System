using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Packages.Protected;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ExchangeSystem.Requests.Sendlers.Close
{
    public abstract class EncryptRequestSendler
    {
        public EncryptRequestSendler(ConnectionSettings settings)
        {
            ConnectionSettings = settings;
        }
        protected ConnectionSettings ConnectionSettings { get; }
        protected ProtectedPackage SecretPackage { get; set; }
        protected Informator _requestInfo;
        public abstract ResponsePackage SendRequest(IPackage package);

        protected byte[] ReadData(NetworkStream stream, int bufferSize)
        {
            byte[] receivedBuffer = new byte[bufferSize];
            do
            {
                stream.Read(receivedBuffer, 0, receivedBuffer.Length);
            }
            while (stream.DataAvailable);
            return receivedBuffer;
        }
        protected void WriteData(NetworkStream stream, byte[] buffer)
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
