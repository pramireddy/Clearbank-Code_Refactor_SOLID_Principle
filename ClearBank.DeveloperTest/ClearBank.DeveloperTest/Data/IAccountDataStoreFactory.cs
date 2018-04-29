using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Data
{
    public interface IAccountDataStoreFactory
    {
        IAccountDataStore AccountDataStore(string dataStoreType);
    }
}
