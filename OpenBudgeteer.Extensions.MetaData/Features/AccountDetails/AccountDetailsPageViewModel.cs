using OpenBudgeteer.Core.Common;
using OpenBudgeteer.Core.Data.Contracts.Services;
using OpenBudgeteer.Core.ViewModels;
using System.Collections.ObjectModel;

namespace OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

public class AccountDetailsPageViewModel(IServiceManager serviceManager, AccountDetailService accountDetailService) : ViewModelBase(serviceManager)
{
    private ObservableCollection<AccountDetailViewModel> _accounts = [];

    /// <summary>
    /// Collection of ViewModelItems for Model <see cref="AccountDetailViewModel"/>
    /// </summary>
    public ObservableCollection<AccountDetailViewModel> Accounts
    {
        get => _accounts;
        private set => Set(ref _accounts, value);
    }

    /// <summary>
    /// Initialize ViewModel and load data from database
    /// </summary>
    public ViewModelOperationResult LoadData()
    {
        try
        {
            Accounts.Clear();

            foreach (var account in accountDetailService.GetAll())
            {
                var newAccountItem = new AccountDetailViewModel(ServiceManager, account);
                decimal newIn = 0;
                decimal newOut = 0;

                foreach (var transaction in ServiceManager.BankTransactionService.GetFromAccount(account.AccountId))
                {
                    if (transaction.Amount > 0)
                        newIn += transaction.Amount;
                    else
                        newOut += transaction.Amount;
                }

                newAccountItem.Balance = newIn + newOut;
                newAccountItem.In = newIn;
                newAccountItem.Out = newOut;

                Accounts.Add(newAccountItem);
            }
        }
        catch (Exception e)
        {
            return new ViewModelOperationResult(false, $"Error during loading: {e.Message}");
        }
        return new ViewModelOperationResult(true);
    }
}
