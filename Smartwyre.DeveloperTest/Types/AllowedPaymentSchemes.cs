using System;

namespace Smartwyre.DeveloperTest.Types
{
    [Flags]
    public enum AllowedPaymentSchemes
    {
        ExpeditedPayments = 1 << 0,
        BankToBankTransfer = 1 << 1,
        AutomatedPaymentSystem = 1 << 2
    }
}