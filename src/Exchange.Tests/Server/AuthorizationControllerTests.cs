using Exchange.Server.Primitives;
using Exchange.System.Entities;
using Exchange.System.Packages;
using Exchange.Tests.Server.Mocks.Controllers;
using ExchangeSystem.Packages;
using NUnit.Framework;
using System;

namespace Exchange.Tests.Server
{
    public class AuthorizationControllerTests
    {
        private Request<UserPassport> _request;
        private RequestContext _requestContext;
        private ResponseReport _responseReport;

        [SetUp]
        public void Setup() 
        { 
            _request = new Request<UserPassport>("Authorization");
            _request.Body = new Body<UserPassport>(new UserPassport("asd", "1234"));
            _requestContext = RequestContext.ConfigureContext(context => context.SetRequest(_request));
        }

        [Test]
        public void Authorize_ValidUserPassport_AuthToken()
        {
            var expected = new Response<Guid>(_responseReport, Guid.NewGuid());
            var controllerUnderTests = AuthorizationControllerMoqFactory.Order(expected, _requestContext);

            var authResponse = controllerUnderTests.Authorization();
            Assert.IsAssignableFrom<Response<Guid>>(authResponse);
        }
    }
}