using BMS.API.Models;
using BMS.Infrastructure.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BMS.API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        protected readonly IAccountRepository _accountRepository;
        public AuthController(IConfiguration config, IAccountRepository accountRepository)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] UserModel login)
        {
            IActionResult response = Unauthorized();
            login ??= new UserModel();

            bool validUser = AuthenticateUser(login);

            if (validUser)
            {
                var tokenString = GenerateJSONWebToken();
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool AuthenticateUser(UserModel login)
        {
            var IsValid = _accountRepository.GetAccount(login.Username.Trim(), login.Password.Trim());

            return IsValid;
        }
    }
}
