using ClearBank.DeveloperTest.PaymentValidationRules;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.PaymentValidationRules
{
    public class ChapsPaymentSchemeValidatorTest
    {
        private Account _account;
        private MakePaymentRequest _makePaymentRequest;

        [SetUp]
        public void Setup()
        {
            _account = new Account();
            _makePaymentRequest = new MakePaymentRequest();
        }

        /// <summary>
        /// Returns success when MakePaymentRequest.PaymentScheme is "Chaps"
        /// </summary>
        [Test]
        [TestCase(AllowedPaymentSchemes.Bacs, false)]
        [TestCase(AllowedPaymentSchemes.Chaps, true)]
        [TestCase(AllowedPaymentSchemes.FasterPayments, false)]
        public void Test_Verify_ChapsPaymentSchemeValidator_Result(AllowedPaymentSchemes paymentScheme, bool validationResult)
        {
            // arrange
            ChapsPaymentSchemeValidator chapsPaymentSchemeValidator = new ChapsPaymentSchemeValidator();
            _account.AllowedPaymentSchemes = paymentScheme;

            // act
            MakePaymentResult makePaymentResult =
                     chapsPaymentSchemeValidator.IsAccountValid(_account, _makePaymentRequest);

            // assert
            Assert.AreEqual(makePaymentResult.Success, validationResult);
        }

        /// <summary>
        /// Returns success when MakePaymentRequest.PaymentScheme is "Chaps" and Account Status is "Live"
        /// </summary>
        [Test]
        [TestCase(AllowedPaymentSchemes.Bacs,AccountStatus.Disabled, false)]
        [TestCase(AllowedPaymentSchemes.Chaps, AccountStatus.Live, true)]
        [TestCase(AllowedPaymentSchemes.Chaps, AccountStatus.Disabled, false)]
        [TestCase(AllowedPaymentSchemes.Chaps, AccountStatus.InboundPaymentsOnly, false)]
        [TestCase(AllowedPaymentSchemes.FasterPayments, AccountStatus.Disabled, false)]
        [TestCase(AllowedPaymentSchemes.FasterPayments, AccountStatus.InboundPaymentsOnly, false)]
        public void Test_Verify_ChapsPaymentSchemeValidator_Result_With_AccountStatus(AllowedPaymentSchemes paymentScheme,AccountStatus accountStatus, bool validationResult)
        {
            // arrange
            ChapsPaymentSchemeValidator chapsPaymentSchemeValidator = new ChapsPaymentSchemeValidator();
            _account.AllowedPaymentSchemes = paymentScheme;
            _account.Status = accountStatus;

            // act
            MakePaymentResult makePaymentResult =
                     chapsPaymentSchemeValidator.IsAccountValid(_account, _makePaymentRequest);

            // assert
            Assert.AreEqual(makePaymentResult.Success, validationResult);
        }

        [Test]
        public void Test_Verify_ChapsPaymentSchemeValidator_Result_With_Null_Account()
        {
            // arrange
            ChapsPaymentSchemeValidator chapsPaymentSchemeValidator = new ChapsPaymentSchemeValidator();

            // act
            MakePaymentResult makePaymentResult =
                     chapsPaymentSchemeValidator.IsAccountValid(null, _makePaymentRequest);

            // assert
            Assert.IsFalse(makePaymentResult.Success);
        }
    }
}
