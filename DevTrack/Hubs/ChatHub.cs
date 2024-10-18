// ChatHub.cs
namespace DevTrack.Hubs
{
    public class ChatHub : Hub
    {
        public ChatHub(IHubCallerClients clients)
        {
            Clients = clients ?? throw new ArgumentNullException(nameof(clients));
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }

    public class Hub
    {
        public required IHubCallerClients Clients { get; set; }
    }

    public interface IHubCallerClients
    {
        IClientProxy All { get; }
    }

    public interface IClientProxy
    {
        Task SendAsync(string method, params object[] args);
    }
}