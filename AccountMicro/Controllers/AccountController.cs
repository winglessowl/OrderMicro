using AccountMicro.Accounting.Commands;
using AccountMicro.Accounting.Querys;
using Domain.AccountMicroDomain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccountMicro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ISender _mediatr;
        public AccountController(ISender mediatr)
        {
            _mediatr = mediatr;
        }
        [HttpPost("~/CreateAccount")]
        public async Task<Unit> Create(string name, int Funds)
        {
            var command = new CreateAccountCommand(name, Funds);
            return await _mediatr.Send(command);
        }

        [HttpGet("~/GetAccount")]
        public async Task<Account> GetAccount(int idAccount)
        {
            var query = new AccountQuery(idAccount);
            return await _mediatr.Send(query);
        }

        [HttpGet("~/ListAllAccounts")]
        public async Task<IEnumerable<Account>> ListAllAccounts()
        {
          var query = new AccountsQuery();
          return await _mediatr.Send(query);
        }
    }
}
