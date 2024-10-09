using Microsoft.EntityFrameworkCore;
using MyWebApi;
using System.Security.Principal;

public class AccountRepository : IAccountRepository{

    private AppDBContext _context;
    public AccountRepository(AppDBContext context){
        _context = context;
    }

    public async Task<IEnumerable<Account>> GetAllAsync(){
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Account> GetByIdAsync(int accountId)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x=>x.Id == accountId);
    }
    
    public async Task<Account> AddAsync(Account account){
        var _addedAccounts = await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        return _addedAccounts.Entity;
    }

    public async Task<Account> UpdateAsync(Account account){
        var _updatedAccounts = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == account.Id);
        if (_updatedAccounts != null)
        {
            _updatedAccounts.Name = account.Name;
            _updatedAccounts.Surname = account.Surname;
            _updatedAccounts.Email = account.Email;
            _updatedAccounts.Blocked = account.Blocked;
            _updatedAccounts.Balance =  account.Balance;

            await _context.SaveChangesAsync();
            return _updatedAccounts;
        }
        return _updatedAccounts;

    }

    public async void DeleteByIdAsync(int AccountsId)
    {
        var _deletedAccounts = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == AccountsId);
        if (_deletedAccounts != null) _context.Accounts.Remove(_deletedAccounts);
        await _context.SaveChangesAsync();
    }

    
}