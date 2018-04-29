using ClearBank.DeveloperTest.Common.Configuration;
using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.PaymentValidationRules;
using ClearBank.DeveloperTest.Types;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private IAccountDataStoreFactory _accountDataStoreFactory;
        private IConfigurationService _configurationService;
        private IMakePaymentRequestValidator _makePaymentRequestValidator;

        public PaymentService(IConfigurationService configurationService,
                              IAccountDataStoreFactory accountDataStoreFactory,
                              IMakePaymentRequestValidator makePaymentRequestValidator)
        {
            _accountDataStoreFactory = accountDataStoreFactory;
            _configurationService = configurationService;
            _makePaymentRequestValidator = makePaymentRequestValidator;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var accountDataStore =
                _accountDataStoreFactory.AccountDataStore(_configurationService.AccountDataStoreType());

            Account account = accountDataStore.GetAccount(request.DebtorAccountNumber);

            var validateMakePaymentRequest =
                _makePaymentRequestValidator.IsAccountValidToMakePayment(account, request);

            if (!validateMakePaymentRequest.Success)
                return validateMakePaymentRequest;

            account.Balance -= request.Amount;
            accountDataStore.UpdateAccount(account);

            return new MakePaymentResult { Success = true };
        }
    }
}
