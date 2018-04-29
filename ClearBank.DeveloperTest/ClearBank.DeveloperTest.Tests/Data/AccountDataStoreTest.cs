using ClearBank.DeveloperTest.Data;
using NUnit.Framework;
using System;

namespace ClearBank.DeveloperTest.Tests.Data
{
    [TestFixture]
    public class AccountDataStoreTest
    {
        /// <summary>
        ///  Account Data Store type should return correctly  based on  DataStoreType
        /// </summary>
        [TestCase("", typeof(AccountDataStore))]
        [TestCase("BACKUP", typeof(BackupAccountDataStore))]
        [TestCase("backup", typeof(BackupAccountDataStore))]
        public void Test_To_Verify_AccountDataStoreType(string dataStoreType, Type accountDataStoreType)
        {
            // arrange
            var accountDataStoreFactory = new AccountDataStoreFactory();

            // act
            var accountDataStore = accountDataStoreFactory.AccountDataStore(dataStoreType);

            // assert
            Assert.That(accountDataStore, Is.TypeOf(accountDataStoreType));
        }
    }
}
