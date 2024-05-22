﻿using Microsoft.EntityFrameworkCore;
using OpenBudgeteer.Core.Data.Entities.Models;
using OpenBudgeteer.Extensions.MetaData.Features.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

public class AccountDetail : IEntity
{
    [Key, Column("AccountDetailId")]
    public Guid Id { get; set; }

    [Required]
    public Guid AccountId { get; set; }

    [Required]
    public AccountType AccountType { get; set; }

    [Required]
    public required Currency Currency { get; set; }

    public string? Alias { get; set; }

    public string? SubType { get; set; }
    public DateTime? EffectiveDate { get; set; }

    public Account Account { get; set; } = null!;

    public ICollection<BudgetUser> AccountOwners { get; set; } = null!;
}

[Owned]
public record Currency(string IsoCode, string Symbol, short Precision = 2);

public enum AccountType
{
    Credit,
    Deposit,
    Investment,
    Loan,
    Cash
}
