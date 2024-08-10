using Microsoft.AspNetCore.SignalR;
using Model;

namespace SignalR;

public class UserHub : Hub
{
    public static List<User> Users = new List<User>();
    public static List<string> GroupJoined { get; set; } = new List<string>();
    public async Task Subscribe(string group)
    {
        System.Console.WriteLine("Subscribe method: {0}", group);
        if (string.IsNullOrEmpty(group)) return;
        if (GroupJoined.Any(g => g == group))
        {
            System.Console.WriteLine("Already joinned in the group array!");
            return;
        }
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
    }

    public async Task Unsubscribe(string group)
    {
        System.Console.WriteLine("Unsubscribe method: {0}", group);
        System.Console.WriteLine("Removed from group array!");
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
    }
}
