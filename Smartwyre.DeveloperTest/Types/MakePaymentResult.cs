namespace Smartwyre.DeveloperTest.Types
{
    public class MakePaymentResult
    {
        public static MakePaymentResult PaymentSuccess() => new MakePaymentResult(true);
        public static MakePaymentResult PaymentFail(string error = null) => new MakePaymentResult(false) { Error = error};

        public bool Success { get; private set; }

        public string Error { get; private set; }


        private MakePaymentResult()
        { }

        private MakePaymentResult(bool success)
        {
            Success = success;
        }
    }
}