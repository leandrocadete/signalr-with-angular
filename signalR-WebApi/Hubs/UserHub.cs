using Microsoft.AspNetCore.SignalR;
using Model;

namespace SignalR;

public class UserHub : Hub
{
    public static List<User> Users= new List<User>();
    public static List<string> GroupJoined { get; set;} = new List<string>();
    public async Task AddNewuser(string group){
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
    }

    public async Task RemoveNewuser(string group) {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
    }
}
