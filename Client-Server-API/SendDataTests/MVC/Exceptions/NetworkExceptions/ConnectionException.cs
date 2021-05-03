﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeServer.MVC.Exceptions.NetworkExceptions
{
    public class ConnectionException : Exception
    {
        public ConnectionException()
        {
        }
        public ConnectionException(string message) : base(message)
        {
        }

        public override string Message => "Возникла ошибка подключения";
    }
}
