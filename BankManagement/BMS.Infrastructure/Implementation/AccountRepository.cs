using BMS.Infrastructure.Abstraction;
using BMS.Infrastructure.Persistance;
using BMS.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMS.Infrastructure.Implementation
{
    public class AccountRepository : IAccountRepository
    {
        protected readonly BmsContext _dbContext;

        public AccountRepository(BmsContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Accounts GetAccount(string username, string password)
        {

            return _dbContext.Set<Accounts>().Where(x => x.UserName == username && x.Password == password).FirstOrDefault();
        }

        public Accounts GetAccountById(Guid accountId)
        {
            return _dbContext.Set<Accounts>().Where(x => x.AccountID == accountId).FirstOrDefault();

        }

        public async Task<Accounts> RegisterAccount(Accounts account)
        {
            _dbContext.Set<Accounts>().Add(account);
            await _dbContext.SaveChangesAsync();
            return account;
        }

        public async Task UpdateAccount(Accounts account)
        {
            _dbContext.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}