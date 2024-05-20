using OpenBudgeteer.Core.Common;
using OpenBudgeteer.Core.Data.Entities.Models;

namespace OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

public class AccountManager(ExtendedServiceManager serviceManager)
{
    public ViewModelOperationResult CreateAccount(AccountDetailViewModel accountDetailModel)
    {
        try
        {
            var account = accountDetailModel.ToAccount();
            account = serviceManager.AccountService.Create(account);

            var accountDetails = accountDetailModel.ToAccountDetail();
            accountDetails.AccountId = account.Id;
            serviceManager.AccountDetailService.Create(accountDetails);

            var initialBalance = new BankTransaction
            {
                Id = Guid.Empty,
                TransactionDate = DateTime.UtcNow.Date.Subtract(TimeSpan.FromDays(1)),
                AccountId = account.Id,
                Memo = "Initial Balance",
                Amount = accountDetailModel.Balance
            };

            serviceManager.BankTransactionService.Create(initialBalance);

            return new ViewModelOperationResult(true, true);
        }
        catch (Exception e)
        {
            return new ViewModelOperationResult(false, e.Message);
        }
    }
}
