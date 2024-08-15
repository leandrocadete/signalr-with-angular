
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Model;
using Model.Services;
using SignalR;

namespace Api.Controller;

[Route("api/[Controller]")]
[ApiController]
public class LoginController : ControllerBase {
    private readonly IHubContext<RealTimeHub> _hubContext;
    private readonly IHubContext<UserHub> _userHubContext;
    private readonly ILogin _login;

    public LoginController(
        IHubContext<RealTimeHub> hubContext, 
        IHubContext<UserHub>  userHub,
        ILogin login
        ) {
        _hubContext = hubContext;
        _userHubContext = userHub;
        _login = login;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] Model.User user) {
        try {
            System.Console.WriteLine("Login controller");
            user.Validate();
            Result result = _login.Login(user);

            
            return await Task.FromResult(Ok(result));
        } catch (Exception ex) {
            System.Console.WriteLine(ex);
            return await Task.FromResult(StatusCode(500, new Result() { Success = false, Messages = new [] {"Teste"} }));
        }
    }
    [HttpGet]
    public async Task<IActionResult> NotifyClients() {
        try
        {
            //RealTimeHub hub = RealTimeHub
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "This is a notification");
            return Ok(await Task.FromResult(new { message = "Ok" }));
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    [HttpPost("[action]/{group}")]
    public async Task<IActionResult> NotifyClientsByGroup([FromRoute] string group, [FromBody] string message) {

        try {
            await _userHubContext.Clients.Group(group).SendAsync("fromGroupAdm", $"Notification to the group: {group} - {message}");
            System.Console.WriteLine("NotifyClientsByGroup - Group name {0} - message {1}", group, message);
            return Ok(await Task.FromResult(new {message ="Ok"}));
        } catch (Exception ex) {
            return await Task.FromResult(BadRequest(ex.Message));
        }
    }

    [HttpPost("[action]/{group}")]
    public async Task<IActionResult> NotifyAllClients([FromBody] string message) {

        try {
            await _userHubContext.Clients.All.SendAsync("fromGroupAdm", $"Notification to All  clients: - {message}");
            System.Console.WriteLine("NotifyClientsByGroup - message {0}", message);
            return Ok(await Task.FromResult(new {message ="Ok"}));
        } catch (Exception ex) {
            return await Task.FromResult(BadRequest(ex.Message));
        }
    }
}