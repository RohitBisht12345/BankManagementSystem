using BMS.Models.Entities;
using BMS.Services.Implementation;
using BMS.Tests.Autofixture;
using NUnit.Framework;
using System;
using System.Linq;

namespace BMS.Tests.UnitTest.Services.Implementation
{
    [TestFixture]
    public class AccountValidatorTests
    {
        [Test]
        [UseFakeDependencies]
        public void AccountValidatewithNullRequest(
            AccountValidators subject)
        {
            //Arrange
            Accounts request = null;

            //Act
            var validationError = subject.Validate(request);
            bool response = validationError.Contains("request must have a value");

            //Assert
            Assert.IsTrue(response);
        }

        [Test]
        [UseFakeDependencies]
        public void AccountValidatewithInvalidRequest(
            AccountValidators subject)
        {
            //Arrange
            Accounts request = new();

            //Act
            var validationErrors = subject.Validate(request);

            //Assert
            Assert.IsNotEmpty(validationErrors);
        }

        [Test]
        [UseFakeDependencies]
        public void AccountValidatewithValidRequest(
            Accounts request,
            AccountValidators subject)
        {
            //Act
            var validationErrors = subject.Validate(request);

            //Assert
            Assert.IsEmpty(validationErrors);
        }
    }
}
