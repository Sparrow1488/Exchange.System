namespace Exchange.System.Senders
{
    public class ConnectionSettings
    {
        public ConnectionSettings(string host, int port)
        {
            HostName = host;
            Port = port;
        }
        public string HostName { get; }
        public int Port { get; }
    }
}
