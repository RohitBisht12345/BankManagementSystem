using BMS.Models.Entities;
using BMS.Services.Implementation;
using BMS.Tests.Autofixture;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Tests.UnitTest.Services.Implementation
{
    [TestFixture]
    public class AccountValidatorTests
    {   
        [Test]
        [UseFakeDependencies]
        public void AccountValidatewithNullRequest(
            Accounts request, 
            AccountValidators subject)
        {
            //Arrange
            request = null;

            //Act
            var validationError = subject.Validate(request);
            bool response= validationError.Contains("request must have a value");

            //Assert
            Assert.IsTrue(response);
        }

        [Test]
        [UseFakeDependencies]
        public void AccountValidatewithInvalidRequest(
            Accounts request,
            AccountValidators subject)
        {
            //Arrange
            request = new Accounts();

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
