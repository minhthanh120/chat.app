using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Notification.Commons.Interfaces;

namespace Notification.Hubs
{
    public class SignalRHub: Hub<INotification>
    {
        [Authorize]
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"User {Context.UserIdentifier} had connected!");
            await base.OnConnectedAsync();
        }
        public async Task SendMessageToClients(DateTime dateTime)
        {
            await Clients.All.SendMessage();
        }
    }
}
