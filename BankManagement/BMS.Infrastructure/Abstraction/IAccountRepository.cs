using BMS.Models.Entities;
using System;
using System.Threading.Tasks;

namespace BMS.Infrastructure.Abstraction
{
    public interface IAccountRepository
    {
        bool GetAccount(string username, string password);
        Accounts GetAccountById(Guid accountId);
        Task<Accounts> RegisterAccount(Accounts account);
        Task UpdateAccount(Accounts account);
    }
}
