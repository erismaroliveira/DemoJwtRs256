using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoJwtRs256.Controllers;

[Authorize]
[Route("api")]
[ApiController]
public class QuestionController : ControllerBase
{
    [HttpPost("questions")]
    public async Task<IActionResult> CreateQuestion()
    {
        return Ok("Pergunta criada com sucesso!");
    }
}
