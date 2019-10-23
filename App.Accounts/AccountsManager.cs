using App.Models.Accounts;
using App.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace App.Accounts
{
    public interface IAccountsManager
    {
        Account GetAccount(string countryCode, string checkDigits, string bankCode, string accountNumber);
        IEnumerable<Account> GetAccounts();
        bool BlockAccount(int accountId);
        bool UnblockAccount(int accountId);
    }

    public class AccountsManager : IAccountsManager
    {
        readonly IAccountsRepository _repository;
        public AccountsManager(IAccountsRepository repo)
        {
            _repository = repo;
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _repository.GetAccounts();
        }

        public Account GetAccount(string countryCode, string checkDigits, string bankCode, string accountNumber)
        {
            var accounts = _repository.GetAccounts()
                           .Where(a => a.CountryCode.Equals(countryCode))
                           .Where(a => a.CheckDigits.Equals(checkDigits))
                           .Where(a => a.BankCode.Equals(bankCode))
                           .Where(a => a.AccountNumber.Equals(accountNumber))
                           .FirstOrDefault();

            return accounts;
        }

        public bool BlockAccount(int accountId)
        {
            var account = _repository.GetAccounts()
                          .FirstOrDefault(a => a.Id.Equals(accountId));

            if (account == null || account.IsBlocked)
                return false;

            account.IsBlocked = true;
            return true;
        }

        public bool UnblockAccount(int accountId)
        {
            var account = _repository.GetAccounts()
                          .FirstOrDefault(a => a.Id == accountId);

            if (account == null || !account.IsBlocked)
                return false;

            account.IsBlocked = false;
            return true;
        }
    }
}
