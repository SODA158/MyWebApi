using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using MyWebApi;

public class AppDBContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }

    public AppDBContext(DbContextOptions<AppDBContext> options)
    : base(options)
    {
        //Accounts.Add(new Account { Id = 1, Name = "Artem", Surname = "An", Email = "Artem@mai.ru", Blocked = false, Balance = 100.5M });
        //Accounts.Add(new Account { Id = 2, Name = "Ivan", Surname = "Ivanov", Email = "Ivan@mai.ru", Blocked = false, Balance = 10.9M });
        //Accounts.Add(new Account { Id = 3, Name = "Peter", Surname = "Petrov", Email = "Peter@mai.ru", Blocked = false, Balance = 27.1M });
    }


    

}