using BMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMS.Infrastructure.Abstraction
{
    public interface ILoanRepository
    {
        Task<Loans> RegisterLoan(Loans loan);
        IEnumerable<Loans> GetLoanById(Guid accountId);
    }
}
