using BMS.Models.Entities;
using BMS.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BMS.Services.Abstraction
{
    public interface IRequestProcessor
    {
        Task<BmsResponse<string>> PostAccount(Accounts account);

        Task<BmsResponse<string>> PutAccount(Accounts account);

        Task<BmsResponse<Accounts>> GetAccountById(Guid accountId);

        Task<BmsResponse<string>> PostLoan(Loans loan);

        Task<BmsResponse<IEnumerable<Loans>>> GetLoan(Guid accountId);

    }
}
