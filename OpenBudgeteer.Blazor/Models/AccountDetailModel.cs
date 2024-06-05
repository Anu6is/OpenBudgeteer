﻿using MudBlazor;
using OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;
using System;
using System.ComponentModel.DataAnnotations;

namespace OpenBudgeteer.Blazor.Models;

public sealed class AccountDetailModel
{
    [Required, Label("Account Name")] public string Title { get; set; } = null!;
    [Label("Currency")] public Currency Currency { get; set; } = null!;
    [Label("Balance")] public decimal? Balance { get; set; } = 0;
    public string? Alias { get; set; }
    public string? SubType { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public AccountType AccountType { get; set; }
    public Guid? AssociatedAccountId { get; set; }
}