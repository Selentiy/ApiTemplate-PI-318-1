using App.Models.Accounts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            if (accounts.Count() == 0)
                return NoContent();

            return Ok(accounts);
        }

        [HttpGet("{countryCode}/{checkDigits}/{bankCode}/{accountNumber}")]
        public ActionResult<Account> GetAccount(string countryCode, string checkDigits, string bankCode, string accountNumber)
        {
            var account = _accountManager.GetAccount(countryCode, checkDigits, bankCode, accountNumber);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        [HttpPut("block/{id}")]
        public ActionResult BlockAccount(int accountId)
        {
            bool result = _accountManager.BlockAccount(accountId);

            if (!result)
                return BadRequest();

            return Ok();
        }

        [HttpPut("unblock/{id}")]
        public ActionResult UnblockAccount(int accountId)
        {
            bool result = _accountManager.UnblockAccount(accountId);

            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}
