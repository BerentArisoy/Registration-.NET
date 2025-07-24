using Microsoft.EntityFrameworkCore;
using Registration_Test.Entities;

namespace Registration_Test.DataAccess;

public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        base.OnModelCreating(modelBuilder);
    }
}