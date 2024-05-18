using OpenBudgeteer.Core.Data.Contracts.Services;
using OpenBudgeteer.Core.Data.Entities.Models;
using OpenBudgeteer.Core.ViewModels.EntityViewModels;
using OpenBudgeteer.Extensions.MetaData.Features.Users;

namespace OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

public class AccountDetailViewModel : AccountViewModel, ICloneable
{
    public AccountType AccountType { get; set; }

    public required Currency Currency { get; set; }

    public string? Alias { get; set; }

    public string? SubType { get; set; }

    public ICollection<BudgetUser> Owners { get; set; } = [];

    public AccountDetailViewModel(IServiceManager serviceManager, Account? account) : base(serviceManager, account ?? new()
    {
        Id = Guid.Empty,
        Name = string.Empty,
        IsActive = 1
    }) { }

    protected AccountDetailViewModel(AccountDetailViewModel viewModel) : base(viewModel) { }

    internal AccountDetail ConvertToDto()
    {
        throw new NotImplementedException();
    }
}
