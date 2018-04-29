using System.Configuration;

namespace ClearBank.DeveloperTest.Common.Configuration
{
    public class ConfigurationService : IConfigurationService
    {
        string IConfigurationService.AccountDataStoreType() => ConfigurationManager.AppSettings["DataStoreType"];
    }
}
