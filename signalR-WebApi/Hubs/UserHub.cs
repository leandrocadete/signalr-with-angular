using Microsoft.AspNetCore.SignalR;

namespace SignalR;

public class UserHub : Hub
{
    public static int TotalViews {get; set;} = 0;

    public async Task NewWindowLoader(){
        TotalViews++;
        // send upadete to all clients
        await Clients.All.SendAsync("updateTotalViews", TotalViews);
    }
}
