using App.Models.Accounts;
using System.Collections.Generic;

namespace App.Repositories
{
    public interface IAccountsRepository
    {
        IEnumerable<Account> GetAccounts();
    }
}
