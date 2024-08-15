using System.Text.RegularExpressions;
using Microsoft.AspNetCore.SignalR;
using Model;

namespace SignalR;

public class UserHub : Hub
{
    public static List<User> Users = new List<User>();
    public static List<string> GroupJoined { get; set; } = new List<string>();
    public async Task Subscribe(string group)
    {
        System.Console.WriteLine("Subscribe method - group: {0} - connectionId {1}", group, Context.ConnectionId);
        if (string.IsNullOrEmpty(group)) return;
        // if (GroupJoined.Any(g => g == group))
        // {
        //     System.Console.WriteLine("Already joinned in the group array!");
        //     return;
        // }
        await Groups.AddToGroupAsync(Context.ConnectionId, group);
        System.Console.WriteLine("Addo to groups {0}", group.Length);
        
    }

    public async Task Unsubscribe(string group)
    {
        System.Console.WriteLine("Unsubscribe method: - group {0}", group);
        System.Console.WriteLine("Removed from group array!");
        var g = GroupJoined.Find(g => g == group);
        if (g is null) {
            System.Console.WriteLine("Group not found!");
        }
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
    }
}
