using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MyWebApi;

public class AppDBContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public AppDBContext(DbContextOptions<AppDBContext> options)
    : base(options)
    {
    }
}