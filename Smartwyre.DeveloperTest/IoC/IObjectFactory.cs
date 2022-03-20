namespace Smartwyre.DeveloperTest.IoC
{
    public interface IObjectFactory
    {
        TObject GetInstance<TObject>();
    }
}