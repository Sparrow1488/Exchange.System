using Exchange.Server.Protocols;
using Exchange.System.Enums;
using Exchange.System.Packages;
using Exchange.System.Protection;
using System;
using System.Net.Sockets;

namespace Exchange.Server.Primitives
{
    public class RequestContext
    {
        private RequestContext() { }

        public Package Content { get; private set; }
        public Request Request { get; private set; }
        public EncryptType EncryptType { get; private set; }
        public ProtectionType Protection { get; private set; }
        public TcpClient Client { get; private set; }
        public NetworkProtocol Protocol { get; private set; }

        public static RequestContext ConfigureContext(Action<RequestContext> config)
        {
            var context = new RequestContext();
            config?.Invoke(context);
            return context;
        }

        public RequestContext SetContent(Package package)
        {
            Content = package;
            return this;
        }

        public RequestContext SetEncription(EncryptType encryptType)
        {
            EncryptType = encryptType;
            return this;
        }

        public RequestContext SetProtection(ProtectionType protection)
        {
            Protection = protection;
            return this;
        }

        public RequestContext SetClient(TcpClient client)
        {
            Client = client;
            return this;
        }

        public RequestContext SetRequest(Request request)
        {
            Request = request;
            return this;
        }

        public RequestContext SetProtocol(NetworkProtocol protocol)
        {
            Protocol = protocol;
            return this;
        }
    }
}
