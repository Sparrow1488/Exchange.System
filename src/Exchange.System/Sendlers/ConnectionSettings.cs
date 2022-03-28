﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Exchange.System.Requests.Sendlers
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