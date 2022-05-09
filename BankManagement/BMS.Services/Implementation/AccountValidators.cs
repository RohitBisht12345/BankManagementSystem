using BMS.Models.Entities;
using BMS.Services.Abstraction;
using System.Collections.Generic;

namespace BMS.Services.Implementation
{
    public class AccountValidators : IValidators<Accounts>
    {
        private LinkedList<string> ValidationErrors { get; }

        public AccountValidators()
        {
            ValidationErrors = new LinkedList<string>();
        }
        public IEnumerable<string> Validate(Accounts request)
        {
            if (request == null)
            {
                ValidationErrors.AddLast($"{nameof(request)} must have a value");
                return ValidationErrors;
            }
            if (string.IsNullOrEmpty(request.UserName))
            {
                ValidationErrors.AddLast($"{nameof(request.UserName)} must have a value");
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                ValidationErrors.AddLast($"{nameof(request.Password)} must have a value");
            }
            if (string.IsNullOrEmpty(request.AccountType))
            {
                ValidationErrors.AddLast($"{nameof(request.AccountType)} must have a value");
            }

            return ValidationErrors;
        }
    }
}
