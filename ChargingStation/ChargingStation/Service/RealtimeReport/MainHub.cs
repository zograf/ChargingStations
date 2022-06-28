using Microsoft.AspNetCore.SignalR;

namespace ChargingStation.Service.RealtimeReport
{
    public interface IMainHub
    {
        public void Send(int chargingSpotId, int spotState);
    }

    public class MainHub : Hub, IMainHub, IHubContext
    {
        IHubClients IHubContext.Clients => throw new NotImplementedException();

        public void Send(int chargingSpotId, int spotState)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.SendAsync("ReceiveMessage", chargingSpotId, spotState);
        }
    }
}