using Microsoft.AspNetCore.SignalR;

namespace BlazorApp5.Server.Hubs
{
    public class StronglyTypeChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
        }



        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.ReceiveMessage(message);
        }
    }
}
