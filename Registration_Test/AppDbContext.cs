using Microsoft.EntityFrameworkCore;
using Registration_Test.Entities;

namespace Registration_Test;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=registration_db;Username=postgres;Password=sDNsku6tdm.");
    }
}