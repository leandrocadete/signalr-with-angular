
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

}