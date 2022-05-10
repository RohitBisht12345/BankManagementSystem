using AutoFixture.NUnit3;
using BMS.Infrastructure.Abstraction;
using BMS.Models.Entities;
using BMS.Services.Abstraction;
using BMS.Services.Implementation;
using BMS.Services.Models;
using BMS.Tests.Autofixture;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Tests.UnitTest.Services.Implementation
{
    [TestFixture]
    public class RequestProcessorTests
    {
        [Test]
        [UseFakeDependencies]
        public void RequestProcessor_WithNullAccountRepo_ThrowArgumentNullException(
            IAccountRepository accountRepository,
            ILoanRepository loanRepository,
            IValidators<Accounts> accountValidator,
            IValidators<Loans> loanValidator,
            ILogger<RequestProcessor> logger)
        {
            //Arrange
            accountRepository = null;

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => new RequestProcessor(
            accountRepository,
            loanRepository,
            accountValidator,
            loanValidator,
            logger));
        }

        [Test]
        [UseFakeDependencies]
        public void RequestProcessor_WithNullloanRepo_ThrowArgumentNullException(
          IAccountRepository accountRepository,
          ILoanRepository loanRepository,
          IValidators<Accounts> accountValidator,
          IValidators<Loans> loanValidator,
          ILogger<RequestProcessor> logger)
        {
            //Arrange
            loanRepository = null;

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => new RequestProcessor(
            accountRepository,
            loanRepository,
            accountValidator,
            loanValidator,
            logger));
        }

        [Test]
        [UseFakeDependencies]
        public void RequestProcessor_WithNullAccountValidator_ThrowArgumentNullException(
          IAccountRepository accountRepository,
          ILoanRepository loanRepository,
          IValidators<Accounts> accountValidator,
          IValidators<Loans> loanValidator,
          ILogger<RequestProcessor> logger)
        {
            //Arrange
            accountValidator = null;

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => new RequestProcessor(
            accountRepository,
            loanRepository,
            accountValidator,
            loanValidator,
            logger));
        }

        [Test]
        [UseFakeDependencies]
        public void RequestProcessor_WithNullLoanValidator_ThrowArgumentNullException(
          IAccountRepository accountRepository,
          ILoanRepository loanRepository,
          IValidators<Accounts> accountValidator,
          IValidators<Loans> loanValidator,
          ILogger<RequestProcessor> logger)
        {
            //Arrange
            loanValidator = null;

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => new RequestProcessor(
            accountRepository,
            loanRepository,
            accountValidator,
            loanValidator,
            logger));
        }

        [Test]
        [UseFakeDependencies]
        public void RequestProcessor_WithNulllLogger_ThrowArgumentNullException(
          IAccountRepository accountRepository,
          ILoanRepository loanRepository,
          IValidators<Accounts> accountValidator,
          IValidators<Loans> loanValidator,
          ILogger<RequestProcessor> logger)
        {
            //Arrange
            logger = null;

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => new RequestProcessor(
            accountRepository,
            loanRepository,
            accountValidator,
            loanValidator,
            logger));
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetAccount_WithValidRequest_ReturnSuccessResponse(
            RequestProcessor subject)
        {
            //Arrange
            string username = "admin1";
            string password = "admin1";

            //Act
            var response = await subject.GetAccount(username, password);

            //Assert
            response.Errors.ShouldBe(null);
            response.Data.ShouldNotBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.Success);
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetAccount_WithValidRequest_ReturnErrorResponse(
            Accounts account,
            RequestProcessor subject,
            [Frozen] Mock<IAccountRepository> mockAccountRepository)
        {
            //Arrange
            account = null;
            string username = "admin";
            string password = "admin";

            mockAccountRepository.Setup(x => x.GetAccount(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(account);

            //Act
            var response = await subject.GetAccount(username, password);

            //Assert
            response.Errors.ShouldBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.NotFound);
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetAccount_WithNullRequest_ReturnErrorResponse(RequestProcessor subject)
        {
            //Arrange
            string username = null;
            string password = null;

            //Act
            var response = await subject.GetAccount(username, password);

            //Assert
            response.Errors.ShouldNotBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.ValidationFailed);
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetLoan_WithValidRequest_ReturnSuccessResponse(
            Accounts account,
            RequestProcessor subject)
        {
            //Arrange
            

            //Act
            var response = await subject.GetLoan(account.AccountID);

            //Assert
            response.Errors.ShouldBe(null);
            response.Data.ShouldNotBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.Success);
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetLoan_WithValidRequest_ReturnErrorResponse(
            Accounts account,
            Loans loan,
            RequestProcessor subject,
            [Frozen] Mock<ILoanRepository> mockLoanRepository)
        {
            //Arrange
            loan = null;

            mockLoanRepository.Setup(x => x.GetLoanById(It.IsAny<Guid>()))
                .Returns(loan);

            //Act
            var response = await subject.GetLoan(account.AccountID);

            //Assert
            response.Errors.ShouldBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.NotFound);
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetLoan_WithNullRequest_ReturnErrorResponse(
            Accounts account,
            RequestProcessor subject)
        {
            //Arrange
            account.AccountID = Guid.Empty;

            //Act
            var response = await subject.GetLoan(account.AccountID);

            //Assert
            response.Errors.ShouldNotBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.ValidationFailed);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PostAccount_WithValidRequest_ReturnSuccessResponse(
            Accounts account,
           RequestProcessor subject)
        {

            //Act
            var response = await subject.PostAccount(account);

            //Assert
            response.Errors.ShouldBe(null);
            response.Data.ShouldNotBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.Success);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PostAccount_WithNullRequest_ReturnErrorResponse(
            [Frozen] Mock<IValidators<Accounts>> mockAccountValidator,
            Accounts account,
          RequestProcessor subject)
        {
            //Arrange
            account.UserName = null;
            mockAccountValidator.Setup(x => x.Validate(It.IsAny<Accounts>())).Returns(new string[] { "Username must have a value" });

            //Act
            var response = await subject.PostAccount(account);

            //Assert
            response.Errors.ShouldNotBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.ValidationFailed);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PostLoan_WithValidRequest_ReturnSuccessResponse(
           Loans loan,
           RequestProcessor subject)
        {
            //Act
            var response = await subject.PostLoan(loan);

            //Assert
            response.Errors.ShouldBe(null);
            response.Data.ShouldNotBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.Success);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PostLoan_WithNullRequest_ReturnErrorResponse(
          [Frozen] Mock<IValidators<Loans>> mockLoanValidator,
          Loans loan,
          RequestProcessor subject)
        {
            //Arrange
            loan.LoanAmount = null;
            mockLoanValidator.Setup(x => x.Validate(It.IsAny<Loans>())).Returns(new string[] { "LoanAmount must have a value"});


            //Act
            var response = await subject.PostLoan(loan);

            //Assert
            response.Errors.ShouldNotBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.ValidationFailed);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PutAccount_WithValidRequest_ReturnSuccessResponse(
          Accounts account,
          RequestProcessor subject)
        {
            //Act
            var response = await subject.PutAccount(account);

            //Assert
            response.Errors.ShouldBe(null);
            response.Data.ShouldNotBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.Success);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PutAccount_WithValidRequest_ReturnErrorResponse(
        [Frozen] Mock<IAccountRepository> mockAccountRepository,
        Accounts account,
        RequestProcessor subject)
        {
            //Arrange
            Accounts accountResult = null;
            mockAccountRepository.Setup(x => x.GetAccountById(It.IsAny<Guid>()))
            .Returns(accountResult);

            //Act
            var response = await subject.PutAccount(account);

            //Assert
            response.Errors.ShouldBe(null);
            response.Data.ShouldBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.NotFound);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PutAccount_WithNullRequest_ReturnErrorResponse(
        [Frozen] Mock<IValidators<Accounts>> mockAccountValidator,
        Accounts account,
        RequestProcessor subject)
        {
            //Arrange
            account.UserName = null;
            mockAccountValidator.Setup(x => x.Validate(It.IsAny<Accounts>())).Returns(new string[] { "Username must have a value" });

            //Act
            var response = await subject.PutAccount(account);

            //Assert
            response.Errors.ShouldNotBe(null);
            response.ResponseCode.ShouldBe(BMS.Services.Models.ResponseCode.ValidationFailed);
        }
    }
}

