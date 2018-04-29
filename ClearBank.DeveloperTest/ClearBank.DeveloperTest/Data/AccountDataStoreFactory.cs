using System.Collections.Generic;

namespace ClearBank.DeveloperTest.Data
{
    public class AccountDataStoreFactory : IAccountDataStoreFactory
    {
        private IReadOnlyDictionary<string, IAccountDataStore> _accountDataStores;

        public AccountDataStoreFactory()
        {
            InitializeAccountDataStores();
        }

        private void InitializeAccountDataStores() => _accountDataStores = new Dictionary<string, IAccountDataStore>
            {
                { "BACKUP", new BackupAccountDataStore() }
            };

        public IAccountDataStore AccountDataStore(string dataStoreType)
        {
            dataStoreType = dataStoreType?.ToUpperInvariant();
            if (_accountDataStores.ContainsKey(dataStoreType))
                return _accountDataStores[dataStoreType];

            return new AccountDataStore();
        }
    }
}
