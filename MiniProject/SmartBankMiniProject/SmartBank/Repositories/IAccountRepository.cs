using SmartBank.Models;

namespace SmartBank.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> CreateAccount(Account account);
        Task<List<Account>> GetAllAccounts();
        Task<Account?> GetAccountById(int id);
        Task UpdateAccount(Account account);
        Task DeleteAccount(int id);
    }
}
