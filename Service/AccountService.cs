using MyWebApi;

public class AccountService: IAccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository){
        _accountRepository = accountRepository;
    }

    public async Task<IEnumerable<Account>> GetAllAccountAsync(){
        return await _accountRepository.GetAllAsync();
    }
    public async  Task<Account> GetAccountByIdAsync(int accountId){
        return await _accountRepository.GetByIdAsync(accountId);
    }

    public async Task<Account> AddAccountAsync(Account account){
        return await _accountRepository.AddAsync(account);
    }

    public async Task<Account> UpdateAccountAsync(Account account){
        return await _accountRepository.UpdateAsync(account);
    }

    public async Task<Account> TopUpBalanceAccount(int id, decimal diposit){
        if(diposit>0) return await _accountRepository.TopUpBalance(id,Decimal.Round(diposit,2));
        else return null;
    }

    public async Task<Account> WithdrawMoneyAccount(int id, decimal diposit){
        if(diposit>0) return await _accountRepository.WithdrawMoney(id,Decimal.Round(diposit,2));
        else return null;
    }

    public async Task<Account> TransferMoneyAccount(int senderId, int recipientId, decimal diposit){
        if(diposit>0) return await _accountRepository.TransferMoney(senderId, recipientId, Decimal.Round(diposit,2));
        else return null;
    }

    public async Task<Account> BlockAccountById(int accountId){
        return await _accountRepository.BlockById(accountId);
            
    }

    public async Task<Account> UnblockAccountById(int accountId){
        return await _accountRepository.UnblockById(accountId);
    }

    public async Task<Account> DeleteAccountByIdAsync(int accountId){
        var result = await _accountRepository.DeleteByIdAsync(accountId);
        return result;
    }
}