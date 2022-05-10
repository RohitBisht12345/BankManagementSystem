using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.NUnit3;
using BMS.API.Models;
using BMS.Infrastructure.Abstraction;
using BMS.Models.Entities;
using BMS.Services.Abstraction;
using BMS.Services.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.Tests.Autofixture
{
    public class UseFakeDependencies : AutoDataAttribute
    {
        public UseFakeDependencies():base(CreateFixture)
        {

        }

        private static IFixture CreateFixture()
        {

            var fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });

            fixture.Customize<Accounts>(option =>
            {
                return option
                .With(x => x.AccountID, Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa2"))
                .With(x => x.Name, "RohitBisht")
                .With(x => x.UserName, "admin1")
                .With(x => x.Password, "admin1")
                .With(x => x.Address, "India")
                .With(x => x.State, "Haryana")
                .With(x => x.Country, "India")
                .With(x => x.EmailAddress, "admin1@gmail.com")
                .With(x => x.PAN, "CFDH1234SS")
                .With(x => x.ContactNo, "+91837383920")
                .With(x => x.DOB, DateTime.Parse("2022-05-06T13:01:09.307"))
                .With(x => x.AccountType, "Saving");
            });

            fixture.Customize<UserModel>(option =>
            {
                return option
                .With(x => x.Username, "admin")
                .With(x => x.Password, "admin");
            });

            fixture.Customize<Loans>(option =>
            {
                return option
                .With(x => x.AccountID, Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa2"))
                .With(x => x.LoanAmount, "RohitBisht")
                .With(x => x.LoanDuration, "admin1")
                .With(x => x.LoanType, "admin1")
                .With(x => x.InterestRate, "India")
                .With(x => x.LoanDate, DateTime.Parse("2022-05-06T13:01:09.307"));
            });

            var keys = new Dictionary<string, string>
            {
                {"Jwt:Key", "ThisismySecretKey" },
                {"Jwt:Issuer", "Test.com"}
            };

            var configuration = fixture.Freeze<IConfiguration>();
            var mockConfiguration = Mock.Get(configuration);
            mockConfiguration.Setup(_ => _[It.IsAny<string>()]).Returns((string key) => keys[key]);

            var mockAccountValidator = fixture.Freeze<Mock<IValidators<Accounts>>>();
            mockAccountValidator.Setup(x => x.Validate(It.IsAny<Accounts>()))
                .Returns(Array.Empty<string>());

            var mockLoanValidator = fixture.Freeze<Mock<IValidators<Loans>>>();
            mockLoanValidator.Setup(x => x.Validate(It.IsAny<Loans>()))
                .Returns(Array.Empty<string>());

            var mockAccountRepository = fixture.Freeze<Mock<IAccountRepository>>();
            mockAccountRepository.Setup(x => x.GetAccount(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Accounts() { AccountID= Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa2") });

            mockAccountRepository.Setup(x => x.RegisterAccount(It.IsAny<Accounts>()))
              .Returns( Task.FromResult(new Accounts() { AccountID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa2") }));

            mockAccountRepository.Setup(x => x.GetAccountById(It.IsAny<Guid>()))
             .Returns(new Accounts() { AccountID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa2") });

            mockAccountRepository.Setup(x => x.UpdateAccount(It.IsAny<Accounts>()))
           .Returns(Task.FromResult(new Accounts() { AccountID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa2") }));

            var mockLoanRepository = fixture.Freeze<Mock<ILoanRepository>>();
            mockLoanRepository.Setup(x => x.GetLoanById(It.IsAny<Guid>()))
                .Returns(new Loans() { LoanID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa3") });

            mockLoanRepository.Setup(x => x.RegisterLoan(It.IsAny<Loans>()))
             .Returns(Task.FromResult(new Loans() { LoanID = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa3") }));

            return fixture;
        }

    }
}
