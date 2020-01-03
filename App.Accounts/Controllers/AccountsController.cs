using App.Accounts.Exceptions;
using App.Accounts.Filters;
using App.Models.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace App.Accounts.Controllers
{
    [Route("api/accounts/")]
    [ApiController]
    [TypeFilter(typeof(AccountsExceptionFilter), Arguments = new object[] { nameof(AccountsController) })]
    public class AccountsController : Controller
    {
        readonly IAccountManager _accountManager;
        readonly ILogger<AccountsController> _logger;

        public AccountsController(IAccountManager accountManager, ILogger<AccountsController> logger)
        {
            _accountManager = accountManager;
            _logger = logger;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<Account>> GetAllAccounts()
        {
            _logger.LogInformation($"Call GetAllAccounts action method.");

            var accounts = _accountManager.GetAccounts();

            return Ok(accounts);
        }

        [HttpGet("{countryCode}/{checkDigits}/{bankCode}/{accountNumber}")]
        public ActionResult<Account> GetAccount(string countryCode, string checkDigits, string bankCode, string accountNumber)
        {
            _logger.LogInformation($"Call GetAllAccounts action method with parameters: " +
                $"Country Code {countryCode}, Check Digits {checkDigits}, " +
                $"Bank Code {bankCode}, Account Number {accountNumber}.");

            var account = _accountManager.GetAccount(countryCode, checkDigits, bankCode, accountNumber);

            if (account == null)
                throw new ArticleNotFoundException(countryCode, checkDigits, bankCode, accountNumber);

            return Ok(account);
        }

        [HttpPut("block/{id}")]
        public ActionResult BlockAccount(int id)
        {
            _logger.LogInformation($"Call BlockAccount action method.");
            _accountManager.BlockAccount(id);

            return Ok();
        }

        [HttpPut("unblock/{id}")]
        public ActionResult UnblockAccount(int id)
        {
            _logger.LogInformation($"Call UnblockAccount action method.");
            _accountManager.UnblockAccount(id);

            return Ok();
        }
    }
}
