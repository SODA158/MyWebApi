using Microsoft.EntityFrameworkCore;
using MyWebApi;

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
        var newAccounts = await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
        return newAccounts.Entity;
    }

    public async Task<Account> UpdateAsync(Account account){
        var newAccounts = await _context.Accounts.FirstOrDefaultAsync(x => x.Id == account.Id);
        if(newAccounts!=null){
            newAccounts = account;
            await _context.SaveChangesAsync();
            return newAccounts;
        }
        return null;
    }
    public async Task<Account> TopUpBalance(Account Account){
        var newAccounts = await _context.Accounts.FirstOrDefaultAsync(x=>x.Id == Account.Id);
        if(newAccounts!=null){
            newAccounts = Account;
            await _context.SaveChangesAsync();
            return newAccounts;
        }
        else return null;
    }

    public async Task<Account> TopUpBalance(int id, decimal diposit){
        var newAccounts = await _context.Accounts.FirstOrDefaultAsync(x=>x.Id == id);
        if(newAccounts!=null && newAccounts.Blocked==false){
            newAccounts.Balance += diposit;
            await _context.SaveChangesAsync();
            return newAccounts;
        }
        else return null;
    }

    public async Task<Account> WithdrawMoney(int id, decimal diposit){
        var newAccounts = await _context.Accounts.FirstOrDefaultAsync(x=>x.Id == id);
        if(newAccounts!=null  && diposit<=newAccounts.Balance && newAccounts.Blocked==false){
            newAccounts.Balance -= diposit;
            await _context.SaveChangesAsync();
            return newAccounts;
        }
        else return null;
    }
    public async Task<Account> TransferMoney(int senderId, int recipientId, decimal diposit){
        var senderAccounts = await _context.Accounts.FirstOrDefaultAsync(x=>x.Id == senderId);
        var recipientAccounts = await _context.Accounts.FirstOrDefaultAsync(x=>x.Id == recipientId);

        if(senderAccounts!=null && recipientAccounts!=null 
        && diposit<=senderAccounts.Balance 
        && senderAccounts.Blocked==false && recipientAccounts.Blocked==false)
        {
            senderAccounts.Balance -= diposit;
            recipientAccounts.Balance += diposit;
            await _context.SaveChangesAsync();
            return recipientAccounts;
        }
        else return null;
    }

    public async Task<Account> BlockById(int accountId){
        var _account = await _context.Accounts.FirstOrDefaultAsync(x=>x.Id == accountId);
        if (_account != null)
        {
            _account.Blocked = true;
            await _context.SaveChangesAsync();
            return _account;
        }
        else return null;

    }

    public async Task<Account> UnblockById(int accountId){
        var _account = await _context.Accounts.FirstOrDefaultAsync(x=>x.Id == accountId);
        if(_account!=null)
        { 
            _account.Blocked = false;
            await _context.SaveChangesAsync();
            return _account;
        }
        else return null;
    }
    
    public async Task<Account?> DeleteByIdAsync(int AccountsId){
        var _account = await _context.Accounts
            .FirstOrDefaultAsync(x => x.Id == AccountsId);
        if (_account != null)
        {
            _context.Accounts.Remove(_account);
            await _context.SaveChangesAsync();
            return _account;
        }
        else return null;
    }

    
}