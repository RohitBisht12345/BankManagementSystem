using BMS.Models.Entities;
using BMS.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
            if (string.IsNullOrEmpty(request.Address))
            {
                ValidationErrors.AddLast($"{nameof(request.Address)} must have a value");
            }
            if (string.IsNullOrEmpty(request.Country))
            {
                ValidationErrors.AddLast($"{nameof(request.Country)} must have a value");
            }
            if (string.IsNullOrEmpty(request.EmailAddress) || !(Regex.IsMatch(request.EmailAddress, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase)))
            {
                ValidationErrors.AddLast($"{nameof(request.EmailAddress)} must have a valid email");
            }
            if (string.IsNullOrEmpty(request.Name))
            {
                ValidationErrors.AddLast($"{nameof(request.Name)} must have a value");
            }
            if (string.IsNullOrEmpty(request.PAN) || !(Regex.IsMatch(request.PAN, @"([A-Z]){5}([0-9]){4}([A-Z]){1}$", RegexOptions.IgnoreCase)) )
            {
                ValidationErrors.AddLast($"{nameof(request.PAN)} must have a alphanumeric value");
            }
            if (string.IsNullOrEmpty(request.State))
            {
                ValidationErrors.AddLast($"{nameof(request.State)} must have a value");
            }
            if (request.DOB == DateTime.MinValue)
            {
                ValidationErrors.AddLast($"{nameof(request.DOB)} must have a value");
            }
            if (!Regex.IsMatch(request.ContactNo, @"^[6-9]\d{9}$", RegexOptions.IgnoreCase))
            {
                ValidationErrors.AddLast($"{nameof(request.ContactNo)} must not exceed 10 digit");
            }
            return ValidationErrors;
        }
    }
}
