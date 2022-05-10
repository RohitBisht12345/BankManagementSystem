using BMS.API.Controllers;
using BMS.API.Models;
using BMS.Tests.Autofixture;
using Microsoft.Extensions.Configuration;
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
            IConfiguration config)
        {
            //Arrange
            config = null;

            //Act && Assert
            Assert.Throws<ArgumentNullException>(() => new AuthController(config));
        }

        [Test]
        [UseFakeDependencies]
        public void Authenticate_WithValidRequest_ReturnSuccessToken(
            UserModel request,
            IConfiguration config)
        {
            //Act
            var subject = new AuthController(config);
            var response = subject.Authenticate(request);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.OK), ((Microsoft.AspNetCore.Mvc.ObjectResult)response).StatusCode);
        }

        [Test]
        [UseFakeDependencies]
        public void Authenticate_WithInValidRequest_ReturnUnauthorizedResponse(
           UserModel request,
           IConfiguration config)
        {
            //Arrange
            request.Username = "adm";
            request.Password = "adm";

            //Act
            var subject = new AuthController(config);
            var response = subject.Authenticate(request);

            //Assert
            Assert.AreEqual(((int)HttpStatusCode.Unauthorized), ((Microsoft.AspNetCore.Mvc.StatusCodeResult)response).StatusCode);
        }
    }
}
