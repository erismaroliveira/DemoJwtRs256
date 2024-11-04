using DemoJwtRs256.Data;
using DemoJwtRs256.Models;
using DemoJwtRs256.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DemoJwtRs256.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
    }
}
