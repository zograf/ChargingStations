using System;
using Xunit;
using Microsoft.AspNet.SignalR.Hubs;
using Moq;
using System.Dynamic;
using ChargingStation.Service.RealtimeReport;

namespace TestLibrary
{
    public class Tests
    {
        [Fact]
        public void HubsAreMockableViaDynamic()
        {
            bool sendCalled = false;
            var hub = new MainHub();
            var mockClients = new Mock<IHubCallerConnectionContext<dynamic>>();
            hub.Clients = (Microsoft.AspNetCore.SignalR.IHubCallerClients)mockClients.Object;
            dynamic all = new ExpandoObject();
            all.broadcastMessage = new Action<string, string>((name, message) =>
            {
                sendCalled = true;
            });
            mockClients.Setup(m => m.All).Returns((ExpandoObject)all);
            hub.Send(1, 1);
            Assert.True(sendCalled);
        }
    }
}