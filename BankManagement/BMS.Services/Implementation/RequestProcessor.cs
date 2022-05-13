using BMS.Infrastructure.Abstraction;
using BMS.Models.Entities;
using BMS.Services.Abstraction;
using BMS.Services.Helpers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMS.Services.Implementation
{
    public class RequestProcessor : IRequestProcessor
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IValidators<Accounts> _accountvalidators;
        private readonly IValidators<Loans> _loanvalidators;
        private readonly ILogger<RequestProcessor> _logger;

        public RequestProcessor(
            IAccountRepository accountRepository,
            ILoanRepository loanRepository,
            IValidators<Accounts> accountvalidators,
            IValidators<Loans> loanvalidators,
            ILogger<RequestProcessor> logger)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _loanRepository = loanRepository ?? throw new ArgumentNullException(nameof(loanRepository));
            _accountvalidators = accountvalidators ?? throw new ArgumentNullException(nameof(accountvalidators));
            _loanvalidators = loanvalidators ?? throw new ArgumentNullException(nameof(loanvalidators));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<BmsResponse<Accounts>> GetAccountById(Guid accountId)
        {
            _logger.LogInformation("GetAccount processing...");
            if (accountId.Equals(Guid.Empty))
            {
                IEnumerable<string> error = new[] { "AccountId must not be empty" };
                var errorResponse = new BmsResponse<Accounts>()
                {
                    IsSuccess = false,
                    ResponseCode = ResponseCode.ValidationFailed,
                    Errors = error,
                    Data = null,
                    Message = Constants.ValidationFailed
                };
                _logger.LogError("Validation failed");
                return Task.FromResult(errorResponse);
            }
            else
            {
                var loanResponse = _accountRepository.GetAccountById(accountId);
                var successResponse = new BmsResponse<Accounts>()
                {
                    IsSuccess = loanResponse != null,
                    ResponseCode = loanResponse != null ? ResponseCode.Success : ResponseCode.NotFound,
                    Errors = null,
                    Data = loanResponse,
                    Message = loanResponse != null ? Constants.Success : Constants.RecordNotFound
                };
                _logger.LogInformation("GetAccount completed");
                return Task.FromResult(successResponse);
            }
        }

        public Task<BmsResponse<IEnumerable<Loans>>> GetLoan(Guid accountId)
        {
            _logger.LogInformation("GetLoan processing...");
            if (accountId.Equals(Guid.Empty))
            {
                IEnumerable<string> error = new[] { "AccountId must not be empty" };
                var errorResponse = new BmsResponse<IEnumerable<Loans>>()
                {
                    IsSuccess = false,
                    ResponseCode = ResponseCode.ValidationFailed,
                    Errors = error,
                    Data = null,
                    Message = Constants.ValidationFailed
                };
                _logger.LogError("Validation failed");
                return Task.FromResult(errorResponse);
            }
            else
            {
                var loanResponse = _loanRepository.GetLoanById(accountId);
                var successResponse = new BmsResponse<IEnumerable<Loans>>()
                {
                    IsSuccess = loanResponse.Any(),
                    ResponseCode = loanResponse.Any() ? ResponseCode.Success : ResponseCode.NotFound,
                    Errors = null,
                    Data = loanResponse,
                    Message = loanResponse.Any() ? Constants.Success : Constants.RecordNotFound
                };
                _logger.LogInformation("GetLoan completed");
                return Task.FromResult(successResponse);
            }
        }

        public async Task<BmsResponse<string>> PostAccount(Accounts account)
        {
            _logger.LogInformation("PostAccount processing...");
            var validationErrors = _accountvalidators.Validate(account);
            if (validationErrors.Any())
            {
                var errorResponse = new BmsResponse<string>()
                {
                    IsSuccess = false,
                    ResponseCode = ResponseCode.ValidationFailed,
                    Errors = validationErrors,
                    Data = null,
                    Message = Constants.ValidationFailed
                };
                _logger.LogError("Validation failed");
                return errorResponse;
            }
            else
            {
                var accountResponse = await _accountRepository.RegisterAccount(account);
                var successResponse = new BmsResponse<string>()
                {
                    IsSuccess = true,
                    ResponseCode = ResponseCode.Success,
                    Errors = null,
                    Data = accountResponse.AccountID.ToString(),
                    Message = Constants.Success
                };
                _logger.LogInformation("PostAccount completed");
                return successResponse;
            }
        }

        public async Task<BmsResponse<string>> PostLoan(Loans loan)
        {
            _logger.LogInformation("PostLoan processing...");
            var validationErrors = _loanvalidators.Validate(loan);
            if (validationErrors.Any())
            {
                var errorResponse = new BmsResponse<string>()
                {
                    IsSuccess = false,
                    ResponseCode = ResponseCode.ValidationFailed,
                    Errors = validationErrors,
                    Data = null,
                    Message = Constants.ValidationFailed
                };
                _logger.LogError("Validatin failed");
                return errorResponse;
            }
            else
            {
                var loanResponse = await _loanRepository.RegisterLoan(loan);
                var successResponse = new BmsResponse<string>()
                {
                    IsSuccess = true,
                    ResponseCode = ResponseCode.Success,
                    Errors = null,
                    Data = loanResponse.LoanID.ToString(),
                    Message = Constants.Success
                };
                _logger.LogInformation("PostLoan completed");
                return successResponse;
            }
        }

        public async Task<BmsResponse<string>> PutAccount(Accounts account)
        {
            _logger.LogInformation("PutAccount processing...");
            var validationErrors = _accountvalidators.Validate(account);
            if (validationErrors.Any())
            {
                var errorResponse = new BmsResponse<string>()
                {
                    IsSuccess = false,
                    ResponseCode = ResponseCode.ValidationFailed,
                    Errors = validationErrors,
                    Data = null,
                    Message = Constants.ValidationFailed
                };
                _logger.LogError("Validation failed");
                return errorResponse;
            }
            else
            {
                var accountResponse = _accountRepository.GetAccountById(account.AccountID);
                if (accountResponse != null)
                {
                    await _accountRepository.UpdateAccount(account);
                    var successResponse = new BmsResponse<string>()
                    {
                        IsSuccess = true,
                        ResponseCode = ResponseCode.Success,
                        Errors = null,
                        Data = accountResponse.AccountID.ToString(),
                        Message = Constants.Success
                    };
                    _logger.LogInformation("PutLoan completed");
                    return successResponse;
                }
                else
                {
                    var errorResponse = new BmsResponse<string>()
                    {
                        IsSuccess = false,
                        ResponseCode = ResponseCode.NotFound,
                        Errors = null,
                        Data = null,
                        Message = Constants.RecordNotFound
                    };
                    _logger.LogError("PutLoan rejected");
                    return errorResponse;
                }


            }
        }
    }
}
