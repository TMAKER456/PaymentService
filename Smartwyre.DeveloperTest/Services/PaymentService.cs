using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Smartwyre.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountDataStore accountDataStore;

        public PaymentService(IAccountDataStore accountDataStore)
        {
            this.accountDataStore = accountDataStore;
        }


        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            if (!request.IsValid(out List<ValidationResult> validationResults))
            {
                return MakePaymentResult.PaymentFail($"PaymentRequest is not valid. {string.Join(". ", validationResults)}.");
            }

            Account account = accountDataStore.GetAccount(request.DebtorAccountNumber);
            if (account is null)
            {
                return MakePaymentResult.PaymentFail($"Account does not exist with AccountNumber {request.DebtorAccountNumber}.");
            }
            else if (!account.CanMakePayment(request))
            {
                return MakePaymentResult.PaymentFail("Account cannot make payment.");
            }
            
            account.DeductBalance(request.Amount);
            accountDataStore.UpdateAccount(account);

            return MakePaymentResult.PaymentSuccess();
        }
    }
}