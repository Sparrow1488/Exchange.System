using Exchange.Server.Controllers;
using Exchange.Server.Primitives;
using Exchange.System.Entities;
using Exchange.System.Enums;
using Exchange.System.Packages.Default;
using Exchange.System.Protection;
using NUnit.Framework;

namespace Exchange.Server.Tests
{
    public class ControllerTests
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void ProcessRequest_RequestContextWithDisconnectedClient_ResponsePackage()
        {
            var selector = new ControllerSelector();
            var controller = selector.SelectController("Authorization");
            var requestContent = new Authorization(new UserPassport("asd", "1234"));

            var context = RequestContext.ConfigureContext(context =>
                                            context.SetContent(requestContent)
                                                .SetEncription(EncryptType.None));
            controller.ProcessRequestAsync(context);
            var response = controller.Response;
            Assert.NotNull(response);
        }
    }
}