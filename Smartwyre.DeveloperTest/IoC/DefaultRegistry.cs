using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using StructureMap;

namespace Smartwyre.DeveloperTest.IoC
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For<IPaymentService>().Use<PaymentService>();
            For<IAccountDataStore>().Singleton().Use<AccountDataStore>();
        }
    }
}