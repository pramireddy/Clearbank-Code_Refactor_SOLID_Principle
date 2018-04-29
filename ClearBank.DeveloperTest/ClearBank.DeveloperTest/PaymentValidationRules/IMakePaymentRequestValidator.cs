using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.PaymentValidationRules
{
    public interface IMakePaymentRequestValidator
    {
        MakePaymentResult IsAccountValidToMakePayment(Account account,MakePaymentRequest makePaymentRequest);
    }
}
