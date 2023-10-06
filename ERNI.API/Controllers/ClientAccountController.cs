using ERNI.Data.Model;
using ERNI.Reporsitory.Application.Account.Commands;
using ERNI.Reporsitory.Application.Account.Commands.Handlers;
using ERNI.Reporsitory.Application.Account.Queries;
using ERNI.Reporsitory.Application.Account.Queries.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ERNI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAccountController : ControllerBase
    {
        private readonly RegisterCommandHandler _clientAccountHandler;
        private readonly ClientLoginQueryHandler _clientLoginHandler;

        public ClientAccountController(RegisterCommandHandler clientAccountHandler, 
                                       ClientLoginQueryHandler clientLoginHandler)
        {
            _clientAccountHandler = clientAccountHandler;
            _clientLoginHandler = clientLoginHandler;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create-client")]
        //ENDPOINT: Register new Client 
        public async Task<IActionResult> CreateClient([FromBody]RegisterCommand register)
        {
            var result = await _clientAccountHandler.Handle(register, new CancellationToken());

            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("get-token")]
        //ENDPOINT: Get CLient token 
        public async Task<IActionResult> GetToken([FromBody] ClientLoginQuery login)
        {
            var token = await _clientLoginHandler.Handle(login, new CancellationToken());
            return Ok(token);
        }
    }
}
