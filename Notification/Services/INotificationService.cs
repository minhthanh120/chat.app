using System.Net.WebSockets;

namespace Notification.Services
{
    public interface INotificationService
    {
        Task Echo(WebSocket webSocket);
    }
}
