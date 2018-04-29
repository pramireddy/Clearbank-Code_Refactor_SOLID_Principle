using System.Collections.Generic;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.PaymentValidationRules
{
    public class MakePaymentRequestValidator : IMakePaymentRequestValidator
    {
        private IReadOnlyDictionary<PaymentScheme, IPaymentSchemeValidator> _paymentSchemeValidators;

        public MakePaymentRequestValidator()
        {
            InitializePaymentSchemeValidator();
        }

        public MakePaymentResult IsAccountValidToMakePayment(Account account, MakePaymentRequest makePaymentRequest)
        {
            
            if (_paymentSchemeValidators.ContainsKey(makePaymentRequest.PaymentScheme))
                return new MakePaymentResult { Success = false };

            return _paymentSchemeValidators[makePaymentRequest.PaymentScheme].IsAccountValid(account, makePaymentRequest);
        }

        private void InitializePaymentSchemeValidator() => _paymentSchemeValidators = new Dictionary<PaymentScheme, IPaymentSchemeValidator>
            {
                { PaymentScheme.Bacs, new BacsPaymentSchemeValidator() } ,
                { PaymentScheme.Chaps, new BacsPaymentSchemeValidator() },
                { PaymentScheme.FasterPayments, new BacsPaymentSchemeValidator() }
            };

    }
}
