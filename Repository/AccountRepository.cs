public interface IAccountRepository
{
    List<Account> GetAll();
}

public class AccountRepository : IAccountRepository{
    private AppDBContext _context;
    public AccountRepository(AppDBContext context){
        _context = context;
    }

    List<Account> GetAll(){
        return _context.Accounts;
    }
}