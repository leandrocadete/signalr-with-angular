
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Api.Controller;

[Route("api/[Controller]")]
[ApiController]
public class LoginController : ControllerBase {
    private readonly IHubContext<RealTimeHub> _hubContext;

    public LoginController(IHubContext<RealTimeHub> hubContext) {
        _hubContext = hubContext;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] Model.User user) {
        try {
            user.Id = user.Id +1;
            user.Validate(user.Id - 1);
            return Ok(user);
        } catch (Exception ex) {
            System.Console.WriteLine(ex);
            return BadRequest(ex);
        }
    }
    [HttpGet]
    public async Task<IActionResult> NotifyClients() {
        try
        {
            //RealTimeHub hub = RealTimeHub
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "This is a notification");
            return Ok(await Task.FromResult("Ok"));
        }
        catch (System.Exception)
        {
            throw;
        }
    }

}