using Exchange.Server.Controllers;
using Exchange.Server.Primitives;
using Exchange.System.Entities;
using Exchange.System.Packages.Primitives;
using Exchange.System.Protection;
using ExchangeSystem.Packages;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Exchange.Server.Tests
{
    public class ControllerTests
    {
        public UserPassport ValidPassport { get; private set; }
        public UserPassport InvalidPassport { get; private set; }
        public ControllerSelector ControllerSelector { get; private set; }

        [SetUp]
        public void Setup() 
        {
            ValidPassport = new UserPassport("asd", "1234");
            InvalidPassport = new UserPassport("", null);
            ControllerSelector = new ControllerSelector();
        }

        [Test]
        public async Task ProcessRequest_RequestContextWithDisconnectedClient_ResponsePackage()
        {
            var controller = ControllerSelector.SelectController("Authorization");
            var requestContent = new Authorization(ValidPassport);

            var context = RequestContext.ConfigureContext(context =>
                                            context.SetContent(requestContent)
                                                .SetEncription(EncryptType.None));
            await controller.ProcessRequestAsync(context);
            var response = controller.Response;
            var responseReport = response.ResponseData as ResponseReport;
            Assert.NotNull(response);
            Assert.NotNull(responseReport);
            Assert.AreSame(System.Enums.AuthorizationStatus.Success, responseReport.Status);
        }

        [Test]
        public async Task ProcessRequest_RequestContextWithDisconnectedClientAndInvalidPassport_ResponsePackage()
        {
            var controller = ControllerSelector.SelectController("Authorization");
            var requestContent = new Authorization(InvalidPassport);

            var context = RequestContext.ConfigureContext(context =>
                                            context.SetContent(requestContent)
                                                .SetEncription(EncryptType.None));
            await controller.ProcessRequestAsync(context);
            var response = controller.Response;
            var responseReport = response.ResponseData as ResponseReport;
            Assert.NotNull(response);
            Assert.NotNull(responseReport);
            Assert.AreSame(System.Enums.ResponseStatus.Bad, responseReport.Status);
        }
    }
}