using System;
using System.ComponentModel.DataAnnotations;

namespace Smartwyre.DeveloperTest.Types
{
    public class MakePaymentRequest
    {
        public string CreditorAccountNumber { get; set; }

        [Required]
        [MinLength(1)]
        public string DebtorAccountNumber { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        [EnumDataType(typeof(PaymentScheme))]
        public PaymentScheme PaymentScheme { get; set; }
    }
}