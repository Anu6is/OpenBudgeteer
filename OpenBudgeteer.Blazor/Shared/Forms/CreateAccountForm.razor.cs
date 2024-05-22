using Microsoft.AspNetCore.Components;
using MudBlazor;
using OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

namespace OpenBudgeteer.Blazor.Shared.Forms;

public partial class CreateAccountForm : ComponentBase
{
    public class SubTypeConverter : BoolConverter<string>
    {
        private readonly AccountType _accountType;

        public SubTypeConverter(AccountType accountType)
        {
            _accountType = accountType;

            SetFunc = OnSet;
            GetFunc = OnGet;
        }

        private string? OnGet(bool? value)
        {
            return _accountType switch
            {
                AccountType.Deposit => value is true ? SubType.Deposit.Checking : SubType.Deposit.Savings,
                AccountType.Credit => value is true ? SubType.Credit.Mastercard : SubType.Credit.Visa,
                _ => null
            };
        }

        private bool? OnSet(string? value)
        {
            if (value is null) return null;

            return _accountType switch
            {
                AccountType.Deposit => value is SubType.Deposit.Checking,
                _ => null
            };
        }
    }
}
