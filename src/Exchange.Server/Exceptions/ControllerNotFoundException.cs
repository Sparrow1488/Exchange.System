using System;

namespace Exchange.Server.Exceptions
{
    internal class ControllerNotFoundException : Exception
    {
        public ControllerNotFoundException() { }
        public ControllerNotFoundException(string message) : base(message) { }
    }
}
