using System;
using System.Collections.Generic;
using System.Text;

namespace ExchangeServer.MVC.Exceptions.NetworkExceptions
{
    public class ConnectionException : Exception
    {
        public override string Message => "Возникла ошибка подключения";
    }
}
