using BMS.Infrastructure.Abstraction;
using BMS.Infrastructure.Persistance;
using BMS.Models.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BMS.Infrastructure.Implementation
{
    public class LoanRepository : ILoanRepository
    {
        protected readonly BmsContext _dbContext;

        public LoanRepository(BmsContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public Loans GetLoanById(Guid accountId)
        {
            return _dbContext.Set<Loans>().Where(x => x.AccountID == accountId).FirstOrDefault();
        }

        public async Task<Loans> RegisterLoan(Loans loan)
        {
            _dbContext.Set<Loans>().Add(loan);
            await _dbContext.SaveChangesAsync();
            return loan;
        }
    }
}
