using Exchange.Server.Controllers;
using Exchange.Server.Primitives;
using Exchange.System.Packages;
using Moq;

namespace Exchange.Tests.Server.Mocks.Controllers
{
    internal static class AuthorizationControllerMoqFactory
    {
        public static AuthorizationController Order(Response expected, RequestContext context)
        {
            var moq = new Mock<AuthorizationController>(context);
            moq.Setup(auth => auth.Authorization()).Returns(expected);
            return moq.Object;
        }
    }
}
