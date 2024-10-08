using MyWebApi;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAllAsync();
    Task<Account> GetByIdAsync(int accountId);
    Task<Account> AddAsync(Account account);    
    Task<Account> UpdateAsync(Account account);
    Task<Account> TopUpBalance(int id, decimal diposit);
    Task<Account> WithdrawMoney(int id, decimal diposit);
    Task<Account> TransferMoney(int senderId, int recipientId, decimal diposit);
    Task<Account> BlockById(int accountId);
    Task<Account> UnblockById(int accountId);
    Task<Account> DeleteByIdAsync(int accountId);
}