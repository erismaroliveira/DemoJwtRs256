using DemoJwtRs256.Exceptions;
using DemoJwtRs256.Models;
using DemoJwtRs256.Repositories.Interfaces;
using DemoJwtRs256.Services.Interfaces;

namespace DemoJwtRs256.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task CreateUser(RegisterUserRequest request)
    {
        if (await _userRepository.GetUserByEmail(request.Email) != null)
        {
            throw new UserAlreadyExistsException("Usuário já existe.");
        }

        ValidatePassword(request.Password);

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = passwordHash,
            CreatedAt = DateTime.UtcNow
        };

        await _userRepository.AddUser(user);
    }

    public async Task<string> Login(LoginUserRequest request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email) ?? throw new UserNotFoundException("Usuário não encontrado.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Senha incorreta.");
        }

        return _tokenService.GenerateToken(user.Email);
    }

    private static void ValidatePassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
        {
            throw new ArgumentException("A senha deve ter pelo menos 6 caracteres.");
        }
    }
}
