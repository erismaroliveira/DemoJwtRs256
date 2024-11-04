using DemoJwtRs256.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoJwtRs256.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public DbSet<User> Users { get; set; }
}