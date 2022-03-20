using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests
{
    internal class MockFactory
    {
        public const string DefaultAccountNumber = "abc123";

        public static Account CreateAccountWithAllowedPaymentSchemes(AllowedPaymentSchemes allowedPaymentSchemes, decimal balance = 0, AccountStatus status = 0)
            => new Account(DefaultAccountNumber, balance, status, allowedPaymentSchemes);

        public static IAccountDataStore StubAccountDataStore(Account accountToReturn = null)
        {
            var moqAccountDataStore = new Moq.Mock<IAccountDataStore>();
            if (accountToReturn is not null)
            {
                moqAccountDataStore.Setup(ads => ads.GetAccount(DefaultAccountNumber))
                                .Returns(accountToReturn);
            }
            return moqAccountDataStore.Object;
        }
    }
}