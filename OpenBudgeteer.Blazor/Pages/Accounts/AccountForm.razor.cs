using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

namespace OpenBudgeteer.Blazor.Pages.Accounts;

public partial class AccountForm : ComponentBase
{
    private IEnumerable<string> GetSubTypes()
    {
        return AccountType switch
        {
            AccountType.Deposit => [SubType.Deposit.Checking, SubType.Deposit.Savings],
            AccountType.Credit => [SubType.Credit.Mastercard, SubType.Credit.Visa],
            AccountType.Loan => [SubType.Loan.Lending, SubType.Loan.Borrowing],
            AccountType.Investment => [
                SubType.Investment.Annuity, 
                SubType.Investment.Brokerage, 
                SubType.Investment.MutualFund, 
                SubType.Investment.Pension, 
                SubType.Investment.Property, 
                SubType.Investment.Retirement
            ],
            _ => []
        };
    }
}
