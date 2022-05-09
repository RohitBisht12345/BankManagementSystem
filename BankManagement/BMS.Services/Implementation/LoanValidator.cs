using BMS.Models.Entities;
using BMS.Services.Abstraction;
using System.Collections.Generic;

namespace BMS.Services.Implementation
{
    public class LoanValidators : IValidators<Loans>
    {
        private LinkedList<string> ValidationErrors { get; }

        public LoanValidators()
        {
            ValidationErrors = new LinkedList<string>();
        }
        public IEnumerable<string> Validate(Loans request)
        {
            if (request == null)
            {
                ValidationErrors.AddLast($"{nameof(request)} must have a value");
                return ValidationErrors;
            }
            if (string.IsNullOrEmpty(request.LoanAmount))
            {
                ValidationErrors.AddLast($"{nameof(request.LoanAmount)} must have a value");
            }
            if (string.IsNullOrEmpty(request.LoanType))
            {
                ValidationErrors.AddLast($"{nameof(request.LoanType)} must have a value");
            }
            return ValidationErrors;
        }
    }
}
