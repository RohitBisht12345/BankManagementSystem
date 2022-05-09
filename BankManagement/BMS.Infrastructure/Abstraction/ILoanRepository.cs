using BMS.Models.Entities;
using System;
using System.Threading.Tasks;

namespace BMS.Infrastructure.Abstraction
{
    public interface ILoanRepository
    {
        Task<Loans> RegisterLoan(Loans loan);
        Loans GetLoanById(Guid accountId);
    }
}
