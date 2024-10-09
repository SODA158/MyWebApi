using Microsoft.AspNetCore.Mvc;
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
        var _updatedAccount = await _accountRepository.GetByIdAsync(account.Id);
        if (_updatedAccount == null || _updatedAccount.Blocked == true) throw new Exception("Аккаунт не найден или заблокирован!");
        return await _accountRepository.UpdateAsync(_updatedAccount);
    }

    public async Task<Account> TopUpBalanceAccount(int id, decimal diposit){
        var _updatedAccount = await _accountRepository.GetByIdAsync(id);
        if (_updatedAccount == null || _updatedAccount.Blocked == true) throw new Exception("Аккаунт не найден или заблокирован!");
        if(diposit < 0) throw new Exception("Сумма не может быть меньше 0!");

        _updatedAccount.Balance += Decimal.Round(diposit, 2);

        return await _accountRepository.UpdateAsync(_updatedAccount);
    }

    public async Task<Account> WithdrawMoneyAccount(int id, decimal diposit){
        var _updatedAccount = await _accountRepository.GetByIdAsync(id);
        if (_updatedAccount == null || _updatedAccount.Blocked==true) throw new Exception("Аккаунт не найден или заблокирован!");
        if (diposit < 0) throw new Exception("Сумма не может быть меньше 0!");
        if (diposit > _updatedAccount.Balance) throw new Exception("Недостаточно средств для снятия");

        _updatedAccount.Balance -= Decimal.Round(diposit, 2);

        return await _accountRepository.UpdateAsync(_updatedAccount);
    }

    public async Task<Account> TransferMoneyAccount(int senderId, int recipientId, decimal diposit){
        var _senderAccount = await _accountRepository.GetByIdAsync(senderId);
        var _recipientAccount = await _accountRepository.GetByIdAsync(recipientId);
        if (_senderAccount == null || _senderAccount.Blocked == true) throw new Exception("Аккаунт отправителя не найден или заблокирован!");
        if (_recipientAccount == null || _recipientAccount.Blocked == true) throw new Exception("Аккаунт получателя не найден или заблокирован!");

        if (diposit > 0) throw new Exception("Сумма не может быть меньше 0!");
        if (diposit > _senderAccount.Balance) throw new Exception("Недостаточно средств для снятия");

        _senderAccount.Balance -= Decimal.Round(diposit, 2);
        _recipientAccount.Balance += Decimal.Round(diposit, 2);

        await _accountRepository.UpdateAsync(_senderAccount);
        return await _accountRepository.UpdateAsync(_recipientAccount);

    }

    public async Task<Account> BlockAccountById(int accountId){
        var _blockedAccount = await _accountRepository.GetByIdAsync(accountId);
        if(_blockedAccount==null) throw new Exception("Аккаунт не найден!");

        _blockedAccount.Blocked = true;

        return await _accountRepository.UpdateAsync(_blockedAccount);
    }

    public async Task<Account> UnblockAccountById(int accountId){
        var _unblockedAccount = await _accountRepository.GetByIdAsync(accountId);
        if (_unblockedAccount == null) throw new Exception("Аккаунт не найден!");

        _unblockedAccount.Blocked = false;

        return await _accountRepository.UpdateAsync(_unblockedAccount);
    }

    public async Task<Account> DeleteAccountByIdAsync(int accountId){
        var _deletedAccount = await _accountRepository.GetByIdAsync(accountId);
        if (_deletedAccount == null) throw new Exception("Аккаунт не найден!");
        _accountRepository.DeleteByIdAsync(accountId);
        return _deletedAccount;
    }
}