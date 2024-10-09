using MyWebApi;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAllAsync();
    Task<Account> GetByIdAsync(int accountId);
    Task<Account> AddAsync(Account account);
    Task<Account> UpdateAsync(Account account);
    void DeleteByIdAsync(int accountId);
}