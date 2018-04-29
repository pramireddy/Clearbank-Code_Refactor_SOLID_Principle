using ClearBank.DeveloperTest.PaymentValidationRules;
using ClearBank.DeveloperTest.Types;
using NUnit.Framework;

namespace ClearBank.DeveloperTest.Tests.PaymentValidationRules
{
    [TestFixture]
    public class BacsPaymentSchemeValidatorTest
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
        /// Returns success when MakePaymentRequest.PaymentScheme is "Bacs"
        /// </summary>
        [Test]
        [TestCase(AllowedPaymentSchemes.Bacs,true)]
        [TestCase(AllowedPaymentSchemes.Chaps, false)]
        [TestCase(AllowedPaymentSchemes.FasterPayments, false)]
        public void Test_Verify_BacsPaymentSchemeValidator_Result(AllowedPaymentSchemes paymentScheme,bool validationResult)
        {
            // arrange
            BacsPaymentSchemeValidator bacsPaymentSchemeValidator = new BacsPaymentSchemeValidator();
            _account.AllowedPaymentSchemes = paymentScheme;

            // act
            MakePaymentResult makePaymentResult =
                     bacsPaymentSchemeValidator.IsAccountValid(_account, _makePaymentRequest);

            // assert
            Assert.AreEqual(makePaymentResult.Success,validationResult);
        }

        [Test]
        public void Test_Verify_BacsPaymentSchemeValidator_Result_With_Null_Account()
        {
            // arrange
            BacsPaymentSchemeValidator bacsPaymentSchemeValidator = new BacsPaymentSchemeValidator();
    
            // act
           MakePaymentResult makePaymentResult = 
                    bacsPaymentSchemeValidator.IsAccountValid(null, _makePaymentRequest);
            
            // assert
            Assert.IsFalse(makePaymentResult.Success);
        }


    }
}
