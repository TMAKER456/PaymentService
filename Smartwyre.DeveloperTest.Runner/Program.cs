using Smartwyre.DeveloperTest.IoC;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Smartwyre.DeveloperTest.Runner
{
    public static class Program
    {
        private static IObjectFactory ObjectFactory { get; set; }

        /// <summary>
        /// args[0] = <see cref="MakePaymentRequest.DebtorAccountNumber"/>
        /// args[1] = <see cref="MakePaymentRequest.Amount"/>
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static int Main(string[] args)
        {
            ObjectFactory = IoC.ObjectFactory.Initialise();

            MakePaymentRequest request = ParseArgs(args);

            if (!request.IsValid(out List<ValidationResult> validationResults))
            {
                throw new InvalidOperationException($"Cannot make payment request: {string.Join(". ", validationResults.Select(vr => vr.ErrorMessage))}");
            }

            IPaymentService paymentService = ObjectFactory.GetInstance<IPaymentService>();
            MakePaymentResult paymentResult = paymentService.MakePayment(request);

            if (paymentResult.Success)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private static MakePaymentRequest ParseArgs(string[] args)
        {
            if (args?.Length == 2)
            {
                return new MakePaymentRequest()
                {
                    DebtorAccountNumber = args[0],
                    Amount = decimal.TryParse(args[1], out decimal value) ? value : 0
                };
            }

            return null;
        }
    }
}