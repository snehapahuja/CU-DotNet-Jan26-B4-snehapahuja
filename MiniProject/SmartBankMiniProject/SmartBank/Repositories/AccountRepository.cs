using SmartBank.Models;
using SmartBank.Data;
using Microsoft.EntityFrameworkCore;

namespace SmartBank.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly SmartBankContext _context;

        public AccountRepository(SmartBankContext context)
        {
            _context = context;
        }

        public async Task<Account> CreateAccount(Account account)
        {
            _context.Account.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<List<Account>> GetAllAccounts()
        {
            return await _context.Account.ToListAsync();
        }

        public async Task<Account?> GetAccountById(int id)
        {
            return await _context.Account.FindAsync(id);
        }

        public async Task UpdateAccount(Account account)
        {
            _context.Account.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccount(int id)
        {
            var acc = await _context.Account.FindAsync(id);
            if (acc != null)
            {
                _context.Account.Remove(acc);
                await _context.SaveChangesAsync();
            }
        }
    }
}
