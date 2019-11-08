using App.Accounts.Exceptions;
using App.Models.Accounts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace App.Accounts.Controllers
{
    [Route("api/accounts/")]
    [ApiController]
    public class AccountsController : Controller
    {
        readonly IAccountManager _accountManager;

        public AccountsController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Account>> GetAllAccounts()
        {
            var accounts = _accountManager.GetAccounts();

            return Ok(accounts);
        }

        [HttpGet("{countryCode}/{checkDigits}/{bankCode}/{accountNumber}")]
        public ActionResult<Account> GetAccount(string countryCode, string checkDigits, string bankCode, string accountNumber)
        {
            var account = _accountManager.GetAccount(countryCode, checkDigits, bankCode, accountNumber);

            if (account == null)
                throw new ArticleNotFoundException(countryCode, checkDigits, bankCode, accountNumber);

            return Ok(account);
        }

        [HttpPut("block/{id}")]
        public ActionResult BlockAccount(int id)
        {
            _accountManager.BlockAccount(id);

            return Ok();
        }

        [HttpPut("unblock/{id}")]
        public ActionResult UnblockAccount(int id)
        {
            _accountManager.UnblockAccount(id);

            return Ok();
        }
    }
}
