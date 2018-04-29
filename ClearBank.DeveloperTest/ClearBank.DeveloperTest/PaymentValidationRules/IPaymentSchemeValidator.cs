using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.PaymentValidationRules
{
    public interface IPaymentSchemeValidator
    {
        MakePaymentResult IsAccountValid(Account account, MakePaymentRequest makePaymentRequest);
    }
}
