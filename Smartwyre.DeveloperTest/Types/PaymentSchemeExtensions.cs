using System;

namespace Smartwyre.DeveloperTest.Types
{
    public static class PaymentSchemeExtensions
    {
        public static AllowedPaymentSchemes ToAllowedPaymentSchemes(this PaymentScheme paymentScheme)
        {
            switch (paymentScheme)
            {
                case PaymentScheme.BankToBankTransfer: return AllowedPaymentSchemes.BankToBankTransfer;
                case PaymentScheme.ExpeditedPayments: return AllowedPaymentSchemes.ExpeditedPayments;
                case PaymentScheme.AutomatedPaymentSystem: return AllowedPaymentSchemes.AutomatedPaymentSystem;
                default:
                    throw new NotImplementedException($"{nameof(PaymentScheme)} {paymentScheme} does not have an equivalent {nameof(AllowedPaymentSchemes)}.");
            }
        }
    }
}