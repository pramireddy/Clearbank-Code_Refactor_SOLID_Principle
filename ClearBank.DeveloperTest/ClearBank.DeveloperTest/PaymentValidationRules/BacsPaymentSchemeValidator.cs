using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.PaymentValidationRules
{
    public class BacsPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public MakePaymentResult IsAccountValid(Account account, MakePaymentRequest makePaymentRequest)
        {
            if (account == null)
                return new MakePaymentResult { Success = false };

            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                return new MakePaymentResult { Success = false };

            return new MakePaymentResult { Success = true };
        }
    }
}
