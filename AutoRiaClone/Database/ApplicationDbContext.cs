using AutoRiaClone.Database.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AutoRiaClone.Database;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }
    public DbSet<CarModel> Models { get; set; }
    public DbSet<CarMake> Makes { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<CarAdvertisement> Advertisements { get; set; }
}