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
        public void Setup() { }

        [Test]
        [TestCase("asd", "1234")]
        public void Authorize_ValidUserPassport_AuthTokenAndSuccessStatus(
            string login, string password)
        {
            var report = CreateResponseReport(AuthorizationStatus.Success);
            var expected = new Response<Guid>(report, Guid.NewGuid());
            var context = CreateRequestContextWithUserPassport(login, password);
            _controllerUnderTests = AuthorizationControllerMoqFactory.Order(expected, context);

            var authResult = _controllerUnderTests.Authorization();
            Assert.IsAssignableFrom<Response<Guid>>(authResult);
            Assert.AreEqual(AuthorizationStatus.Success , authResult.Report.Status);
        }

        [Test]
        [TestCase(null, "")]
        public void Authorize_InvalidUserPassport_EmptyEntityAndFailedStatus(
            string login, string password)
        {
            var report = CreateResponseReport(AuthorizationStatus.Failed);
            var expected = new Response<EmptyEntity>(report, new EmptyEntity());
            var requestContext = CreateRequestContextWithUserPassport(login, password);
            _controllerUnderTests = AuthorizationControllerMoqFactory.Order(expected, requestContext);

            var authResult = _controllerUnderTests.Authorization();
            Assert.IsAssignableFrom<Response<EmptyEntity>>(authResult);
            Assert.AreEqual(AuthorizationStatus.Failed, authResult.Report.Status);
        }

        private RequestContext CreateRequestContextWithUserPassport(string login, string passport)
        {
            var request = new Request<UserPassport>("Authorization", ProtectionType.Default);
            request.Body = new Body<UserPassport>(new UserPassport(login, passport));
            return RequestContext.ConfigureContext(context => context.SetRequest(request));
        }

        private ResponseReport CreateResponseReport(AuthorizationStatus status, string message = null)
        {
            ResponseReport report = default;
            if (status.Equals(AuthorizationStatus.Failed))
                report = new ResponseReport(message ?? AuthorizationStatus.Failed.Message, AuthorizationStatus.Failed);
            else if (status.Equals(AuthorizationStatus.Success))
                report = new ResponseReport(message ?? AuthorizationStatus.Failed.Message, AuthorizationStatus.Success);
            return report;
        }
    }
}