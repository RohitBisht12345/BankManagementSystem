using AutoFixture.NUnit3;
using BMS.API.Controllers;
using BMS.API.Models;
using BMS.Infrastructure.Abstraction;
using BMS.Tests.Autofixture;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Net;

namespace BMS.Tests.UnitTest.Controller
{
    [TestFixture]
    public class AuthControllerTests
    {
        [Test]
        [UseFakeDependencies]
        public void AuthController_WithNullConfiguration_ThrowArgumentNullException(
            IConfiguration config,
            IAccountRepository accountRepository)
        {
            //Arrange
            config = null;

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => new AuthController(config, accountRepository));
        }

        [Test]
        [UseFakeDependencies]
        public void AuthController_WithNullAcccountRepository_ThrowArgumentNullException(
            IConfiguration config,
            IAccountRepository accountRepository)
        {
            //Arrange
            accountRepository = null;

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => new AuthController(config, accountRepository));
        }

        [Test]
        [UseFakeDependencies]
        public void Authenticate_WithValidRequest_ReturnSuccessToken(
            UserModel request,
            IConfiguration config,
            IAccountRepository accountRepository)
        {
            //Act
            var subject = new AuthController(config, accountRepository);
            var response = subject.Authenticate(request);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.OK), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public void Authenticate_WithInValidRequest_ReturnUnauthorizedResponse(
           UserModel request,
           IConfiguration config,
           IAccountRepository accountRepository,
           [Frozen] Mock<IAccountRepository> mockAccountRepository)
        {
            //Arrange
            request.Username = "adm";
            request.Password = "adm";

            //Arrange
            mockAccountRepository.Setup(x => x.GetAccount(It.IsAny<string>(), It.IsAny<string>()))
               .Returns(false);

            //Act
            var subject = new AuthController(config, accountRepository);
            var response = subject.Authenticate(request);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.Unauthorized), ((Microsoft.AspNetCore.Mvc.StatusCodeResult)response).StatusCode);
        }
    }
}
