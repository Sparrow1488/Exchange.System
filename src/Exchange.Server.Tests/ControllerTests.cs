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

        [Ignore("Изменилась логика, в данный момент тест не может быть пройдет, нужно переписать")]
        [Test]
        public void ProcessRequest_RequestContextWithDisconnectedClient_ResponsePackage()
        {
            //Assert.NotNull(response);
            //Assert.NotNull(responseReport);
            //Assert.AreSame(System.Enums.AuthorizationStatus.Success, responseReport.Status);
        }

        [Ignore("Изменилась логика, в данный момент тест не может быть пройдет, нужно переписать")]
        [Test]
        public async Task ProcessRequest_RequestContextWithDisconnectedClientAndInvalidPassport_ResponsePackage()
        {
            //Assert.NotNull(response);
            //Assert.NotNull(responseReport);
            //Assert.AreSame(System.Enums.ResponseStatus.Bad, responseReport.Status);
        }
    }
}