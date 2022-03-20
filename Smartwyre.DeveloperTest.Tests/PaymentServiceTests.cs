using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class PaymentServiceTests
    {
        private static MakePaymentRequest GetPaymentRequest(PaymentScheme paymentScheme, decimal amount = 50)
            => new MakePaymentRequest()
            {
                DebtorAccountNumber = MockFactory.DefaultAccountNumber,
                PaymentScheme = paymentScheme,
                Amount = amount
            };

        [Fact]
        public void NullAccount_FailedPayment()
        {
            PaymentService paymentService = new PaymentService(MockFactory.StubAccountDataStore());

            var paymentResult = paymentService.MakePayment(GetPaymentRequest(PaymentScheme.ExpeditedPayments));

            Assert.False(paymentResult.Success);
            Assert.StartsWith("Account does not exist with AccountNumber", paymentResult.Error);
        }


        [Fact]
        public void AccountWithBalanceAndExpeditedPayments_CanMakeExpeditedPayment()
        {
            const decimal startBalance = 50;
            Account account = MockFactory.CreateAccountWithAllowedPaymentSchemes(AllowedPaymentSchemes.ExpeditedPayments, balance: startBalance);
            PaymentService paymentService = new PaymentService(MockFactory.StubAccountDataStore(account));

            var paymentRequest = GetPaymentRequest(PaymentScheme.ExpeditedPayments);
            var paymentResult = paymentService.MakePayment(paymentRequest);

            Assert.True(paymentResult.Success);
            Assert.Equal(startBalance - paymentRequest.Amount, account.Balance);
        }

        [Fact]
        public void AccountWithoutBalanceAndExpeditedPayments_FailExpeditedPayment()
        {
            Account account = MockFactory.CreateAccountWithAllowedPaymentSchemes(AllowedPaymentSchemes.ExpeditedPayments);
            decimal startBalance = account.Balance;
            PaymentService paymentService = new PaymentService(MockFactory.StubAccountDataStore(account));

            var paymentResult = paymentService.MakePayment(GetPaymentRequest(PaymentScheme.ExpeditedPayments));

            Assert.False(paymentResult.Success);
            Assert.Equal(startBalance, account.Balance);
        }


        [Fact]
        public void AccountNotLiveAndAutomatedPayment_FailAutomatedPayment()
        {
            foreach (int invalidStatusForAutomatedPayment in new[] { (int)AccountStatus.InboundPaymentsOnly, (int)AccountStatus.Disabled, int.MaxValue })
            {
                Account account = MockFactory.CreateAccountWithAllowedPaymentSchemes(AllowedPaymentSchemes.AutomatedPaymentSystem, status: (AccountStatus)invalidStatusForAutomatedPayment);
                decimal startBalance = account.Balance;
                PaymentService paymentService = new PaymentService(MockFactory.StubAccountDataStore(account));

                var paymentRequest = GetPaymentRequest(PaymentScheme.AutomatedPaymentSystem);
                var paymentResult = paymentService.MakePayment(paymentRequest);

                Assert.False(paymentResult.Success);
                Assert.Equal(startBalance, account.Balance);
            }
        }

        [Fact]
        public void AccountLiveAndAutomatedPayment_CanMakeAutomatedPayment()
        {
            Account account = MockFactory.CreateAccountWithAllowedPaymentSchemes(AllowedPaymentSchemes.AutomatedPaymentSystem, status: AccountStatus.Live);
            decimal startBalance = account.Balance;
            PaymentService paymentService = new PaymentService(MockFactory.StubAccountDataStore(account));

            var paymentRequest = GetPaymentRequest(PaymentScheme.AutomatedPaymentSystem);
            var paymentResult = paymentService.MakePayment(paymentRequest);

            Assert.True(paymentResult.Success);
            Assert.Equal(startBalance - paymentRequest.Amount, account.Balance);
        }

        [Fact]
        public void AccountBankToBankTransfer_CanMakeBankToBankTransfer()
        {
            Account account = MockFactory.CreateAccountWithAllowedPaymentSchemes(AllowedPaymentSchemes.BankToBankTransfer);
            decimal startBalance = account.Balance;
            PaymentService paymentService = new PaymentService(MockFactory.StubAccountDataStore(account));

            var paymentRequest = GetPaymentRequest(PaymentScheme.BankToBankTransfer);
            var paymentResult = paymentService.MakePayment(paymentRequest);

            Assert.True(paymentResult.Success);
            Assert.Equal(startBalance - paymentRequest.Amount, account.Balance);
        }

        [Fact]
        public void AccountLiveWithBalanceAndAllSchemes_CanMakeAllPayments()
        {
            const decimal startBalance = 600;
            Account account = MockFactory.CreateAccountWithAllowedPaymentSchemes(AllowedPaymentSchemes.AutomatedPaymentSystem | AllowedPaymentSchemes.BankToBankTransfer | AllowedPaymentSchemes.ExpeditedPayments, balance: startBalance, status: AccountStatus.Live);
            PaymentService paymentService = new PaymentService(MockFactory.StubAccountDataStore(account));

            PaymentScheme[] paymentSchemes = new[] { PaymentScheme.AutomatedPaymentSystem, PaymentScheme.BankToBankTransfer, PaymentScheme.ExpeditedPayments };
            decimal amountDeducted = 0;

            foreach (PaymentScheme paymentScheme in paymentSchemes)
            {
                var paymentRequest = GetPaymentRequest(paymentScheme);
                var paymentResult = paymentService.MakePayment(paymentRequest);
                amountDeducted += paymentRequest.Amount;

                Assert.True(paymentResult.Success);
            }

            Assert.Equal(startBalance - amountDeducted, account.Balance);
        }
    }
}