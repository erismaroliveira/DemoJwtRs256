using DemoJwtRs256.Exceptions;
using DemoJwtRs256.Models;
using DemoJwtRs256.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DemoJwtRs256.Controllers;

[Route("api")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("create-account")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        if (request == null)
        {
            return BadRequest("Dados inválidos.");
        }

        try
        {
            await _userService.CreateUser(request);
            return Ok("Usuário criado com sucesso!");
        }
        catch (UserAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao criar o usuário.");
        }
    }

    [HttpPost("sessions")]
    public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
    {
        try
        {
            var token = await _userService.Login(request);
            return Ok(new { token });
        }
        catch (UserNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao realizar o login.");
        }
    }
}

