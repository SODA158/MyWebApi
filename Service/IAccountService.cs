using MyWebApi;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAccountAsync();
    Task<Account> GetAccountByIdAsync(int accountId);
    Task<Account> AddAccountAsync(Account account);
    Task<Account> UpdateAccountAsync(Account account);
    Task<Account> TopUpBalanceAccount(int id, decimal diposit);
    Task<Account> WithdrawMoneyAccount(int id, decimal diposit);
    Task<Account> TransferMoneyAccount(int senderId, int recipientId, decimal diposit);
    Task<Account> BlockAccountById(int accountId);
    Task<Account> UnblockAccountById(int accountId);
    Task<Account> DeleteAccountByIdAsync(int accountId);
}