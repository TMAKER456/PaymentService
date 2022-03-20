using Smartwyre.DeveloperTest.Types;
using System;

namespace Smartwyre.DeveloperTest.Data
{
    public class AccountDataStore : IAccountDataStore
    {
        public Account GetAccount(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                return null;
            }

            // Access database to retrieve account, code removed for brevity 
            return new Account();
        }

        public void UpdateAccount(Account account)
        {
            if (account is null) throw new ArgumentNullException(nameof(account));
            if (string.IsNullOrWhiteSpace(account.AccountNumber)) throw new ArgumentException("Cannot update an account without an account number.");

            // Update account in database, code removed for brevity
        }
    }
}
