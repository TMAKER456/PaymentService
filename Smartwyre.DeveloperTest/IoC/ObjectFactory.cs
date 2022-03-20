using StructureMap;

namespace Smartwyre.DeveloperTest.IoC
{
    public class ObjectFactory : IObjectFactory
    {
        public static IObjectFactory Instance { get; } = new ObjectFactory();

        internal IContainer Container { get; }

        private ObjectFactory()
        {
            this.Container = new Container(new DefaultRegistry());
            Container.Configure(_ => _.For<IObjectFactory>().Singleton().Use(this));
        }


        public TObject GetInstance<TObject>() => Container.GetInstance<TObject>();
    }
}