
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[Route("api/[Controller]")]
[ApiController]
public class AcoesController : ControllerBase {


    [HttpGet("[action]")]
    public async Task<IActionResult> ListAcoes01() {

        var acoes = new [] {
            "VALE3",
            "SAPR4",
            "KLBN4"
        };

        
        var okResult = Ok(acoes);

        return await Task.FromResult(okResult);
    }
    
    [Authorize]
    [HttpGet("[action]")]
    public async Task<IActionResult> ListAcoes02() {

        var acoes = new [] {
            "VALE3",
            "SAPR4",
            "KLBN4"
        };

        
        var okResult = Ok(acoes);

        return await Task.FromResult(okResult);
    }

}