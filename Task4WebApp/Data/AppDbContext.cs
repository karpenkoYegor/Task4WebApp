using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Task4WebApp.Data.Entities;

namespace Task4WebApp.Data;

public class AppDbContext : IdentityDbContext<UserAccount>
{
    public DbSet<UserAccount> UserAccounts => Set<UserAccount>();
    public DbSet<UserData> UserData => Set<UserData>();
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
}