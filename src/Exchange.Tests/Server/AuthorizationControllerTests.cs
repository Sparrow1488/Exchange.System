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

        [Test]
        public void Authorize_InvalidInputEntity_EmptyEntityAndBadStatus()
        {
            var report = CreateResponseReport(ResponseStatus.Bad);
            var expected = new Response<EmptyEntity>(report, new EmptyEntity());
            var requestContext = CreateRequestContextWithEmptyEntity();
            _controllerUnderTests = AuthorizationControllerMoqFactory.Order(expected, requestContext);

            var authResult = _controllerUnderTests.Authorization();
            Assert.IsAssignableFrom<Response<EmptyEntity>>(authResult);
            Assert.AreEqual(ResponseStatus.Bad, authResult.Report.Status);
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