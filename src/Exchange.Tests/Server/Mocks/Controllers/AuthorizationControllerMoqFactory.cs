using AutoFixture;
using Exchange.Server.Controllers;
using Exchange.Server.Primitives;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages;
using ExchangeSystem.Packages;
using Moq;
using System;

namespace Exchange.Tests.Server.Mocks.Controllers
{
    internal static class AuthorizationControllerMoqFactory
    {
        public static AuthorizationController Order(Response expected, RequestContext context)
        {
            var passport = (UserPassport)context.Request.GetBodyContent();
            var moq = new Mock<AuthorizationController>(context);
            moq.Setup(auth => auth.Authorization(passport)).Returns(expected);
            return moq.Object;
        }

        public static AuthorizationController Order()
        {
            var moq = new Mock<AuthorizationController>();
            moq.Setup(auth => auth.Authorization(null)).Throws<ArgumentNullException>();
            moq.Setup(auth => auth.Authorization(new UserPassport(It.IsAny<string>(), It.IsAny<string>()))).Returns(SuccessAuthResponse());
            return moq.Object;
        }

        private static Response SuccessAuthResponse() =>
            new Response<Guid>(
                new ResponseReport(AuthorizationStatus.Success.Message, 
                    AuthorizationStatus.Success), Guid.NewGuid());
    }
}
