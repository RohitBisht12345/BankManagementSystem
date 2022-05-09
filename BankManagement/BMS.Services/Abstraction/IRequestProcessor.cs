using BMS.Models.Entities;
using BMS.Services.Models;
using System;
using System.Threading.Tasks;

namespace BMS.Services.Abstraction
{
    public interface IRequestProcessor
    {
        Task<BmsResponse<string>> PostAccount(Accounts account);

        Task<BmsResponse<string>> PutAccount(Accounts account);

        Task<BmsResponse<Accounts>> GetAccount(string username, string password);

        Task<BmsResponse<string>> PostLoan(Loans loan);

        Task<BmsResponse<Loans>> GetLoan(Guid accountId);

    }
}
