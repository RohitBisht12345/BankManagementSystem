using AutoFixture.NUnit3;
using BMS.API.Controllers;
using BMS.Infrastructure.Abstraction;
using BMS.Models.Entities;
using BMS.Services.Abstraction;
using BMS.Services.Implementation;
using BMS.Tests.Autofixture;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace BMS.Tests.UnitTest.Controller
{
    [TestFixture]
    public class BMSControllerTests
    {
        [Test]
        [UseFakeDependencies]
        public void BmsController_WithNullRequestProcessor_ThrowArgumentNullException(
          RequestProcessor requestProcessor)
        {
            //Arrange
            requestProcessor = null;

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => new BmsController(requestProcessor));
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetAccount_ReturnSuccessResponse(
            Accounts account,
            RequestProcessor requestProcessor)
        {
            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.GetAccount(account.AccountID);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.OK), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetAccount_ReturnNotFoundResponse(
            Accounts account,
            RequestProcessor requestProcessor,
            [Frozen] Mock<IAccountRepository> mockAccountRepository)
        {
            //Arrange
            account = null;

            mockAccountRepository.Setup(x => x.GetAccountById(It.IsAny<Guid>()))
                .Returns(account);

            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.GetAccount(Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa2"));

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.NotFound), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetAccount_ReturnBadRequestResponse(
            RequestProcessor requestProcessor)
        {
            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.GetAccount(Guid.Empty);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.BadRequest), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetLoan_ReturnSuccessResponse(
            Accounts account,
            RequestProcessor requestProcessor)
        {
            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.GetLoan(account.AccountID);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.OK), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetLoan_ReturnNotFoundResponse(
            Accounts account,
            RequestProcessor requestProcessor,
            [Frozen] Mock<ILoanRepository> mockLoanRepository)
        {
            //Arrange
            List<Loans> lstLoans = null;

            mockLoanRepository.Setup(x => x.GetLoanById(It.IsAny<Guid>()))
                .Returns(lstLoans);

            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.GetLoan(account.AccountID);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.NotFound), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task GetLoan_ReturnBadRequestResponse(
            Accounts account,
            RequestProcessor requestProcessor)
        {
            //Arrange
            account.AccountID = Guid.Empty;

            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.GetLoan(account.AccountID);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.BadRequest), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PostAccount_ReturnSuccessResponse(
           Accounts account,
           RequestProcessor requestProcessor)
        {
            account = null;
            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.PostAccount(account);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.OK), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PostAccount_ReturnBadRequestResponse(
            [Frozen] Mock<IValidators<Accounts>> mockAccountValidator,
            Accounts account,
            RequestProcessor requestProcessor)
        {
            //Arrange
            account.UserName = null;
            mockAccountValidator.Setup(x => x.Validate(It.IsAny<Accounts>())).Returns(new string[] { "Username must have a value" });

            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.PostAccount(account);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.BadRequest), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PostLoan_ReturnSuccessResponse(
           Loans loan,
           RequestProcessor requestProcessor)
        {
            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.PostLoan(loan);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.OK), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PostLoan_ReturnBadRequestResponse(
          [Frozen] Mock<IValidators<Loans>> mockLoanValidator,
          Loans loan,
          RequestProcessor requestProcessor)
        {
            //Arrange
            loan.LoanAmount = null;
            mockLoanValidator.Setup(x => x.Validate(It.IsAny<Loans>())).Returns(new string[] { "LoanAmount must have a value" });


            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.PostLoan(loan);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.BadRequest), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PutAccount_ReturnSuccessResponse(
          Accounts account,
          RequestProcessor requestProcessor)
        {
            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.PutAccount(account);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.OK), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PutAccount_ReturnNotFoundResponse(
        [Frozen] Mock<IAccountRepository> mockAccountRepository,
        Accounts account,
        RequestProcessor requestProcessor)
        {
            //Arrange
            Accounts accountResult = null;
            mockAccountRepository.Setup(x => x.GetAccountById(It.IsAny<Guid>()))
            .Returns(accountResult);

            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.PutAccount(account);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.NotFound), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public async Task PutAccount_ReturnBadRequestResponse(
        [Frozen] Mock<IValidators<Accounts>> mockAccountValidator,
        Accounts account,
        RequestProcessor requestProcessor)
        {
            //Arrange
            account.UserName = null;
            mockAccountValidator.Setup(x => x.Validate(It.IsAny<Accounts>())).Returns(new string[] { "Username must have a value" });

            //Act
            var subject = new BmsController(requestProcessor);
            var response = await subject.PutAccount(account);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.BadRequest), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }
    }
}
