using MudBlazor;
using OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;
using System.ComponentModel.DataAnnotations;

namespace OpenBudgeteer.Blazor.Models;

public sealed class AccountModel
{
    [Required, Label("Name")] public string Title { get; set; } = null!;
    [Label("Currency")] public Currency Currency { get; set; } = null!;
    [Label("Balance")] public decimal? Balance { get; set; } = 0;
    public string? Alias { get; set; }
    public string? SubType { get; set; }
    public AccountType AccountType { get; set; }
}
