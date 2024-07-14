
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[Route("api/[Controller]")]
[ApiController]
public class LoginController : ControllerBase {


    public LoginController() {}

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



}