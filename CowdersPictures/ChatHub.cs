using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalR.Mvc
{
    public class ChatHub : Hub
    {
        private IHubContext<ChatHub> _hubContext;

        public ChatHub(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task BroadcastMessage(string name, string message)
        {
            await _hubContext.Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public Task Echo(string name, string message) =>
            Clients.Client(Context.ConnectionId)
                   .SendAsync("echo", name, $"{message} (echo from server)");
    }
}