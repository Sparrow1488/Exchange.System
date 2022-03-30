using Exchange.System.Packages.Default;
using Exchange.System.Protection;
using System;
using System.Net.Sockets;

namespace Exchange.Server.Primitives
{
    public class RequestContext
    {
        private RequestContext() { }

        public IPackage Content { get; private set; }
        public EncryptType EncryptType { get; private set; }
        public TcpClient Client { get; private set; }


        public static RequestContext ConfigureContext(Action<RequestContext> config)
        {
            var context = new RequestContext();
            config?.Invoke(context);
            return context;
        }

        public RequestContext SetContent(IPackage package)
        {
            Content = package;
            return this;
        }

        public RequestContext SetEncription(EncryptType encryptType)
        {
            EncryptType = encryptType;
            return this;
        }

        public RequestContext SetClient(TcpClient client)
        {
            Client = client;
            return this;
        }
    }
}
