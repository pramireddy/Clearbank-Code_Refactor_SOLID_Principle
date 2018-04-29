using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.PaymentValidationRules
{
    public class ChapsPaymentSchemeValidator : IPaymentSchemeValidator
    {
        public MakePaymentResult IsAccountValid(Account account, MakePaymentRequest makePaymentRequest)
        {
            if (account == null)
                return new MakePaymentResult { Success = false };

            if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
                return new MakePaymentResult { Success = false };

            if (account.Status != AccountStatus.Live)
                return new MakePaymentResult { Success = false };

            return new MakePaymentResult { Success = true };
        }
    }
}
