using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.PaymentValidationRules;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.PaymentValidationRules
{
    public class FasterPaymentSchemeValidatorTest
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
        /// Returns success when MakePaymentRequest.PaymentScheme is "FasterPayments"
        /// </summary>
        [Test]
        [TestCase(AllowedPaymentSchemes.Bacs, false)]
        [TestCase(AllowedPaymentSchemes.Chaps, false)]
        [TestCase(AllowedPaymentSchemes.FasterPayments, true)]
        public void Test_Verify_FasterPaymentPaymentSchemeValidator_Result(AllowedPaymentSchemes paymentScheme, bool validationResult)
        {
            // arrange
            FasterPaymentSchemeValidator fasterPaymentSchemeValidator = new FasterPaymentSchemeValidator();
            _account.AllowedPaymentSchemes = paymentScheme;

            // act
            MakePaymentResult makePaymentResult =
                     fasterPaymentSchemeValidator.IsAccountValid(_account, _makePaymentRequest);

            // assert
            Assert.AreEqual(makePaymentResult.Success, validationResult);
        }

        [Test]
        [TestCase(AllowedPaymentSchemes.FasterPayments, true)]
        public void Test_Verify_FasterPaymentPaymentSchemeValidator_Result_When_Account_Balance_LesstThan_Requested_Amount(AllowedPaymentSchemes paymentScheme, bool validationResult)
        {
            // arrange
            FasterPaymentSchemeValidator fasterPaymentSchemeValidator = new FasterPaymentSchemeValidator();
            _account.AllowedPaymentSchemes = paymentScheme;
            _account.Balance = 20;
            _makePaymentRequest.Amount = 2;

            // act
            MakePaymentResult makePaymentResult =
                     fasterPaymentSchemeValidator.IsAccountValid(_account, _makePaymentRequest);

            // assert
            Assert.AreEqual(makePaymentResult.Success, validationResult);
        }

        [Test]
        [TestCase(AllowedPaymentSchemes.FasterPayments, false)]
        public void Test_Verify_FasterPaymentPaymentSchemeValidator_Result_When_Account_Balance_GreatertThan_Requested_Amount(AllowedPaymentSchemes paymentScheme, bool validationResult)
        {
            // arrange
            FasterPaymentSchemeValidator fasterPaymentSchemeValidator = new FasterPaymentSchemeValidator();
            _account.AllowedPaymentSchemes = paymentScheme;
            _account.Balance = 10;
            _makePaymentRequest.Amount = 20;

            // act
            MakePaymentResult makePaymentResult =
                     fasterPaymentSchemeValidator.IsAccountValid(_account, _makePaymentRequest);

            // assert
            Assert.AreEqual(makePaymentResult.Success, validationResult);
        }

        [Test]
        public void Test_Verify_FasterPaymentPaymentSchemeValidator_Result_With_Null_Account()
        {
            // arrange
            FasterPaymentSchemeValidator fasterPaymentSchemeValidator = new FasterPaymentSchemeValidator();

            // act
            MakePaymentResult makePaymentResult =
                     fasterPaymentSchemeValidator.IsAccountValid(null, _makePaymentRequest);

            // assert
            Assert.IsFalse(makePaymentResult.Success);
        }
    }
}
