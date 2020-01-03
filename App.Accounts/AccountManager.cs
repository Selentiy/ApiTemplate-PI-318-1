using App.Accounts.Exceptions;
using App.Configuration;
using App.Models.Accounts;
using App.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace App.Accounts
{
    public interface IAccountManager
    {
        Account GetAccount(string countryCode, string checkDigits, string bankCode, string accountNumber);
        IEnumerable<Account> GetAccounts();
        void BlockAccount(int accountId);
        void UnblockAccount(int accountId);
    }

    public class AccountManager : IAccountManager, ITransientDependency
    {
        readonly IAccountsRepository _repository;
        readonly ILogger<AccountManager> _logger;

        public AccountManager(IAccountsRepository repo, ILogger<AccountManager> logger)
        {
            _repository = repo;
            _logger = logger;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _repository.GetAccounts();
        }

        public Account GetAccount(string countryCode, string checkDigits, string bankCode, string accountNumber)
        {
            _logger.LogInformation($"Call GetAccount method with parameters: " +
                $"Country Code {countryCode}, Check Digits {checkDigits}, " +
                $"Bank Code {bankCode}, Account Number {accountNumber}.");

            var accounts = _repository.GetAccounts()
                           .Where(a => a.CountryCode.Equals(countryCode))
                           .Where(a => a.CheckDigits.Equals(checkDigits))
                           .Where(a => a.BankCode.Equals(bankCode))
                           .Where(a => a.AccountNumber.Equals(accountNumber))
                           .FirstOrDefault();

            return accounts;
        }

        public void BlockAccount(int accountId)
        {
            _logger.LogInformation($"Call BlockAccount method with parameter accountId = {accountId}.");

            var account = _repository.GetAccounts()
                          .FirstOrDefault(a => a.Id.Equals(accountId));

            if (account == null)
                throw new ArticleNotFoundException(accountId);

            if (account.IsBlocked)
                throw new InvalidBlockOperationException(typeof(Account), accountId);

            account.IsBlocked = true;
        }

        public void UnblockAccount(int accountId)
        {
            _logger.LogInformation($"Call UnlockAccount method with parameter accountId = {accountId}.");

            var account = _repository.GetAccounts()
                          .FirstOrDefault(a => a.Id == accountId);

            if (account == null)
                throw new ArticleNotFoundException(accountId);

            if (!account.IsBlocked)
                throw new InvalidUnblockOperationException(typeof(Account), accountId);

            account.IsBlocked = false;
        }
    }
}
