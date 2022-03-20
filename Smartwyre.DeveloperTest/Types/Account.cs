namespace Smartwyre.DeveloperTest.Types
{
    public class Account
    {
        public string AccountNumber { get; private set; }
        public decimal Balance { get; private set; }
        public AccountStatus Status { get; private set; }
        public AllowedPaymentSchemes AllowedPaymentSchemes { get; private set; }


        internal Account()
        { }

        internal Account(string accountNumber, decimal balance, AccountStatus status, AllowedPaymentSchemes allowedPaymentSchemes)
        {
            AccountNumber = accountNumber;
            Balance = balance;
            Status = status;
            AllowedPaymentSchemes = allowedPaymentSchemes;
        }


        public bool CanMakePayment(MakePaymentRequest paymentRequest) => CanMakePayment(paymentRequest.PaymentScheme, paymentRequest.Amount);

        public bool CanMakePayment(PaymentScheme paymentScheme, decimal? amount) 
            => AllowedPaymentSchemes.HasFlag(paymentScheme.ToAllowedPaymentSchemes()) 
                && paymentScheme switch
                    {
                        PaymentScheme.BankToBankTransfer => true,
                        PaymentScheme.ExpeditedPayments => !amount.HasValue || Balance >= amount.Value,
                        PaymentScheme.AutomatedPaymentSystem => Status == AccountStatus.Live,
                        _ => false,
                    };

        public void DeductBalance(decimal amount) => Balance -= amount;
    }
}