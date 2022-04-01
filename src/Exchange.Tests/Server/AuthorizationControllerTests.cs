using Exchange.Server.Controllers;
using Exchange.Server.Primitives;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages;
using Exchange.Tests.Server.Mocks.Controllers;
using ExchangeSystem.Packages;
using NUnit.Framework;
using System;

namespace Exchange.Tests.Server
{
    public class AuthorizationControllerTests
    {
        private AuthorizationController _controllerUnderTests;

        [SetUp]
        public void Setup() 
        {
            _controllerUnderTests = AuthorizationControllerMoqFactory.Order();
        }

        [Test]
        [TestCase("asd", "1234")]
        [TestCase("Sparrow", "12312323")]
        [TestCase("gigachad", "222224444")]
        public void Authorize_ValidUserPassport_AuthTokenAndSuccessStatus(
            string login, string password)
        {
            var report = CreateResponseReport(AuthorizationStatus.Success);
            var expected = new Response<Guid>(report, Guid.NewGuid());
            var context = CreateRequestContextWithUserPassport(login, password);
            var controllerUnderTests = AuthorizationControllerMoqFactory.Order(expected, context);

            var authResult = controllerUnderTests.Authorization((UserPassport)context.Request.GetBodyContent());

            Assert.IsAssignableFrom<Response<Guid>>(authResult);
            Assert.AreEqual(AuthorizationStatus.Success , authResult.Report.Status);
        }

        [Test]
        public void Authorize_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _controllerUnderTests.Authorization(null));
        }

        private RequestContext CreateRequestContextWithUserPassport(string login, string passport) =>
            CreateRequestContext(new UserPassport(login, passport));

        private RequestContext CreateRequestContextWithEmptyEntity() =>
            CreateRequestContext(new EmptyEntity());

        private RequestContext CreateRequestContext<TBody>(TBody bodyContent)
        {
            var request = new Request<TBody>("Authorization", ProtectionType.Default);
            request.Body = new Body<TBody>(bodyContent);
            return RequestContext.ConfigureContext(context => context.SetRequest(request));
        }

        private ResponseReport CreateResponseReport(ResponseStatus status, string message = null) =>
            new ResponseReport(message ?? status.Message, status);
    }
}