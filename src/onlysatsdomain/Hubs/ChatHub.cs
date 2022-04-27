using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace onlysats.domain.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, long origin_server_ts, string event_id)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message, origin_server_ts, event_id);
        }
    }
}
