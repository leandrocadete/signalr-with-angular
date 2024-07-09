// Hubs/RealTimeHub.cs

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class RealTimeHub : Hub
{
    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }


    public static int TotalViews {get; set;} = 0;

    public async Task NewWindowLoader(){
        TotalViews++;
        // send upadete to all clients
        await Clients.All.SendAsync("updateTotalViews", TotalViews);
    }
}