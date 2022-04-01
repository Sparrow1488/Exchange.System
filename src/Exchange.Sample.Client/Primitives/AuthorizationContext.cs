using System;

namespace Exchange.Sample.Client.Primitives
{
    internal class AuthorizationContext
    {
        public bool IsSuccess { get; set; }
        public Guid Token { get; set; }
    }
}
