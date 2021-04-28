using ExchangeSystem.Requests.Packages;
using ExchangeSystem.Requests.Packages.Default;
using ExchangeSystem.Requests.Packages.Protected;
using System;
using System.Net.Sockets;

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
        public abstract string SendRequest(IPackage package);

        protected byte[] ReadData(ref NetworkStream stream, int bufferSize)
        {
            byte[] receivedBuffer = new byte[bufferSize];
            do
            {
                stream.Read(receivedBuffer, 0, receivedBuffer.Length);
            }
            while (stream.DataAvailable);
            return receivedBuffer;
        }
        protected void WriteData(ref NetworkStream stream, byte[] buffer)
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
