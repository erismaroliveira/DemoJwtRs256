using DemoJwtRs256.Models;

namespace DemoJwtRs256.Services.Interfaces;

public interface IUserService
{
    Task CreateUser(RegisterUserRequest request);
    Task<string> Login(LoginUserRequest request);
}
