using AccountMicro.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AccountMicro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private AccountContext _context;
        public AccountController(AccountContext context) 
        {
            _context = context;
        }
        [HttpPost(Name = "CreateAccount")]
        public Account Create()
        {
            _context.Accounts.Add(new Account() { Funds = 999, Name = "Cuenta 1" });
            _context.SaveChanges();
            return new Account();
        }

        [HttpGet(Name = "GetAccount")]
        public string GetAccount(int idAccount) 
        {
            return JsonConvert.SerializeObject(_context.Accounts.Where(x => x.Id == idAccount).FirstOrDefault()); 
        }
    }
}
