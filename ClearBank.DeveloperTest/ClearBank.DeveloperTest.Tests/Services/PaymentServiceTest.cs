using NUnit.Framework;
using Moq;
using System;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Common.Configuration;
using ClearBank.DeveloperTest.PaymentValidationRules;
using ClearBank.DeveloperTest.Services;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Tests.Services
{
    [TestFixture]
    public class PaymentServiceTest
    {
        private Mock<IAccountDataStoreFactory> _accountDataStoreFactory;
        private Mock<IConfigurationService> _configurationService;
        private Mock<IMakePaymentRequestValidator> _makePaymentRequestValidator;
        private IPaymentService _paymentSrevice;
        private Mock<IAccountDataStore> _accountDataStore;

        [SetUp]
        public void Setup()
        {
            _accountDataStoreFactory = new Mock<IAccountDataStoreFactory>();
            _configurationService = new Mock<IConfigurationService>();
            _makePaymentRequestValidator = new Mock<IMakePaymentRequestValidator>();
            _accountDataStore = new Mock<IAccountDataStore>();
            _paymentSrevice = new PaymentService(_configurationService.Object, _accountDataStoreFactory.Object, _makePaymentRequestValidator.Object);
        }

        [Test]
        public void Test_PaymentService_MakePayment_Success()
        {
            // arrange
            MakePaymentRequest makePaymentRequest = new MakePaymentRequest
            {
                Amount = 50,
                CreditorAccountNumber = "PR2408",
                DebtorAccountNumber = "DC2404",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.Bacs

            };

            Account account = new Account
            {
                AccountNumber = "PR2408",
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps,
                Balance = 250,
                Status = AccountStatus.Live,
            };

            MakePaymentResult makePaymentResult = new MakePaymentResult { Success = true };
            _accountDataStore.Setup(x => x.GetAccount(makePaymentRequest.DebtorAccountNumber)).Returns(account);
            _accountDataStoreFactory.Setup(x => x.AccountDataStore(It.IsAny<string>())).Returns(_accountDataStore.Object);
            _makePaymentRequestValidator.Setup(x => x.IsAccountValidToMakePayment(account, makePaymentRequest)).Returns(makePaymentResult);
            
            // act
            makePaymentResult = _paymentSrevice.MakePayment(makePaymentRequest);

            // assert
            Assert.AreEqual(account.Balance, 200);
            Assert.AreEqual(makePaymentResult.Success, true);
        }

        [Test]
        public void Test_PaymentService_MakePayment_Success_And_AccountUpdated()
        {
            // arrange
            MakePaymentRequest makePaymentRequest = new MakePaymentRequest
            {
                Amount = 50,
                CreditorAccountNumber = "PR2408",
                DebtorAccountNumber = "DC2404",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.Bacs

            };

            Account account = new Account
            {
                AccountNumber = "PR2408",
                AllowedPaymentSchemes = AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps,
                Balance = 250,
                Status = AccountStatus.Live,
            };

            MakePaymentResult makePaymentResult = new MakePaymentResult { Success = true };
            _accountDataStore.Setup(x => x.GetAccount(makePaymentRequest.DebtorAccountNumber)).Returns(account);
            _accountDataStoreFactory.Setup(x => x.AccountDataStore(It.IsAny<string>())).Returns(_accountDataStore.Object);
            _makePaymentRequestValidator.Setup(x => x.IsAccountValidToMakePayment(account, makePaymentRequest)).Returns(makePaymentResult);

            // act
            makePaymentResult = _paymentSrevice.MakePayment(makePaymentRequest);

            // assert
            Assert.AreEqual(account.Balance, 200);
            Assert.AreEqual(makePaymentResult.Success, true);
            _accountDataStore.Verify(x => x.UpdateAccount(account), Times.Once);
            _accountDataStore.Verify(x => x.GetAccount(makePaymentRequest.DebtorAccountNumber), Times.Once);
        }

        [Test]
        public void Test_PaymentService_MakePayment_UnSuccess_when_Account_Is_Null()
        {
            // arrange
            MakePaymentRequest makePaymentRequest = new MakePaymentRequest
            {
                Amount = 50,
                CreditorAccountNumber = "PR2408",
                DebtorAccountNumber = "DC2404",
                PaymentDate = DateTime.Now,
                PaymentScheme = PaymentScheme.Bacs

            };


            MakePaymentResult makePaymentResult = new MakePaymentResult { Success = false };
            _accountDataStore.Setup(x => x.GetAccount(makePaymentRequest.DebtorAccountNumber)).Returns<Account>(null);
            _accountDataStoreFactory.Setup(x => x.AccountDataStore(It.IsAny<string>())).Returns(_accountDataStore.Object);
            _makePaymentRequestValidator.Setup(x => x.IsAccountValidToMakePayment(null, makePaymentRequest)).Returns(makePaymentResult);

            // act
            makePaymentResult = _paymentSrevice.MakePayment(makePaymentRequest);

            // assert
            Assert.AreEqual(makePaymentResult.Success, false);
            _accountDataStore.Verify(x => x.UpdateAccount(null), Times.Never);
        }
    }
}
