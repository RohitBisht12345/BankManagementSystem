using BMS.Models.Entities;
using BMS.Services.Abstraction;
using BMS.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BMS.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BmsController : ControllerBase
    {
        private readonly IRequestProcessor _requestProcessor;

        public BmsController(IRequestProcessor requestProcessor)
        {
            _requestProcessor = requestProcessor ?? throw new ArgumentNullException(nameof(requestProcessor));
        }

        [HttpGet]
        [Route("GetAccount")]
        public async Task<ActionResult> GetAccount(string userName, string password)
        {
            var response = await _requestProcessor.GetAccount(userName, password);

            return response.ResponseCode switch
            {
                ResponseCode.Success => Ok(response),
                ResponseCode.ValidationFailed => BadRequest(response),
                ResponseCode.NotFound => StatusCode((int)HttpStatusCode.NotFound, response),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, response.Errors),
            };
        }

        [HttpGet]
        [Route("GetLoan")]
        public async Task<ActionResult> GetLoan(Guid accountId)
        {
            var response = await _requestProcessor.GetLoan(accountId);
            return response.ResponseCode switch
            {
                ResponseCode.Success => Ok(response),
                ResponseCode.ValidationFailed => BadRequest(response),
                ResponseCode.NotFound => StatusCode((int)HttpStatusCode.NotFound, response),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, response.Errors),
            };
        }

        [HttpPost]
        [Route("RegisterAccount")]
        public async Task<ActionResult> PostAccount([FromBody] Accounts accounts)
        {
            var response = await _requestProcessor.PostAccount(accounts);
            return response.ResponseCode switch
            {
                ResponseCode.Success => Ok(response),
                ResponseCode.ValidationFailed or ResponseCode.NotFound => BadRequest(response),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, response.Errors),
            };
        }

        [HttpPost]
        [Route("RegisterLoan")]
        public async Task<ActionResult> PostLoan([FromBody] Loans loans)
        {
            var response = await _requestProcessor.PostLoan(loans);
            return response.ResponseCode switch
            {
                ResponseCode.Success => Ok(response),
                ResponseCode.ValidationFailed or ResponseCode.NotFound => BadRequest(response),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, response.Errors),
            };
        }

        [HttpPut]
        [Route("UpdateAccount")]
        public async Task<ActionResult> PutAccount([FromBody] Accounts accounts)
        {
            var response = await _requestProcessor.PutAccount(accounts);
            return response.ResponseCode switch
            {
                ResponseCode.Success => Ok(response),
                ResponseCode.ValidationFailed => BadRequest(response),
                ResponseCode.NotFound => StatusCode((int)HttpStatusCode.NotFound, response),
                _ => StatusCode((int)HttpStatusCode.InternalServerError, response.Errors),
            };
        }
    }
}
