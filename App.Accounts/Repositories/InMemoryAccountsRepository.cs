using System;
using System.Collections.Generic;
using App.Configuration;
using App.Models.Accounts;
using App.Repositories;

namespace App.Accounts.Repositories
{
    public class InMemoryAccountsRepository : IAccountsRepository, ISingletoneDependency
    {
        private IEnumerable<Account> accounts;

        public InMemoryAccountsRepository()
        {
            accounts = RepositoryInitializer.GetAccounts();
        }

        public IEnumerable<Account> GetAccounts()
        {
            return accounts;
        }

        private static class RepositoryInitializer
        {
            public static IEnumerable<Account> GetAccounts()
            {
                return new List<Account>
                {
                    new Account()
                    {
                        AccountNumber = "0000004149123456789",
                        BankCode = "380884",
                        CheckDigits = "04",
                        CountryCode = "UA",
                        CreatedDate = new DateTime(2016, 5, 26),
                        IsBlocked = false,
                        Owner = "Man1"
                    },
                    new Account()
                    {
                        AccountNumber = "0000004149123456111",
                        BankCode = "380884",
                        CheckDigits = "11",
                        CountryCode = "UA",
                        CreatedDate = new DateTime(2019, 7, 3),
                        IsBlocked = false,
                        Owner = "Man2"
                    },
                    new Account()
                    {
                        AccountNumber = "0000001111111111111",
                        BankCode = "380884",
                        CheckDigits = "32",
                        CountryCode = "UA",
                        CreatedDate = new DateTime(2011, 3, 2),
                        IsBlocked = false,
                        Owner = "Man3"
                    },
                    new Account()
                    {
                        AccountNumber = "",
                        BankCode = "380990",
                        CheckDigits = "23",
                        CountryCode = "UK",
                        CreatedDate = new DateTime(2017, 7, 23),
                        IsBlocked = true,
                        Owner = null
                    }
                };
            }
        }
    }
}
