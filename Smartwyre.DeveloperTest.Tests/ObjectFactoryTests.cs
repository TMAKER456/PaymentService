using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.IoC;
using Smartwyre.DeveloperTest.Services;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests
{
    public class ObjectFactoryTests
    {
        [Fact]
        public void ObjectFactoryReturnsObjects()
        {
            var objectFactory = ObjectFactory.Instance;

            var dataStore = objectFactory.GetInstance<IAccountDataStore>();
            var paymentService = objectFactory.GetInstance<IPaymentService>();

            var objFactoryInstance1 = objectFactory.GetInstance<IObjectFactory>();
            var objFactoryInstance2 = objectFactory.GetInstance<IObjectFactory>();

            Assert.IsAssignableFrom<IAccountDataStore>(dataStore);
            Assert.IsAssignableFrom<IPaymentService>(paymentService);
            Assert.IsAssignableFrom<IObjectFactory>(objFactoryInstance1);
            Assert.IsAssignableFrom<IObjectFactory>(objFactoryInstance2);

            // Expect singleton
            Assert.Same(objFactoryInstance1, objectFactory);
            Assert.Same(objFactoryInstance1, objFactoryInstance2);
        }
    }
}