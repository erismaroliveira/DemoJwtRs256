using DemoJwtRs256.Models;

namespace DemoJwtRs256.Repositories.Interfaces;

public interface IUserRepository
{
    Task AddUser(User user);
    Task<User> GetUserByEmail(string email);
}
