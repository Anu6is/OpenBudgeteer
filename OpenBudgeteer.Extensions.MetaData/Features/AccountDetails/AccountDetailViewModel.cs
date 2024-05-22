using OpenBudgeteer.Core.Data.Contracts.Services;
using OpenBudgeteer.Core.Data.Entities.Models;
using OpenBudgeteer.Core.ViewModels.EntityViewModels;
using OpenBudgeteer.Extensions.MetaData.Features.Users;

namespace OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

public class AccountDetailViewModel : AccountViewModel
{
    public AccountType AccountType { get; set; }

    public Currency? Currency { get; set; }

    public string? Alias { get; set; }

    public string? SubType { get; set; }

    public DateTime? EffectiveDate { get; set; }

    public ICollection<BudgetUser> Owners { get; set; } = [];

    public AccountDetailViewModel() : this(null, null) { }

    public AccountDetailViewModel(IServiceManager serviceManager, AccountDetail? accountDetail) : base(serviceManager, accountDetail?.Account)
    {
        if (accountDetail is null) return;

        Alias = accountDetail.Alias;
        SubType = accountDetail.SubType;
        Currency = accountDetail.Currency;
        AccountType = accountDetail.AccountType;
        EffectiveDate = accountDetail.EffectiveDate;
    }

    protected AccountDetailViewModel(AccountDetailViewModel viewModel) : base(viewModel)
    {
        Alias = viewModel.Alias;
        SubType = viewModel.SubType;
        Balance = viewModel.Balance;
        Currency = viewModel.Currency;
        AccountType = viewModel.AccountType;
        EffectiveDate = viewModel.EffectiveDate;
    }

    public Account ToAccount()
    {
        return new Account
        {
            Id = AccountId,
            Name = Name,
            IsActive = IsActive ? 1 : 0
        };
    }

    public AccountDetail ToAccountDetail()
    {
        return new AccountDetail
        {
            Alias = Alias,
            SubType = SubType,
            Currency = Currency!,
            AccountId = AccountId,
            AccountType = AccountType,
            EffectiveDate = EffectiveDate
        };
    }
}
