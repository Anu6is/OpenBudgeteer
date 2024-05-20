using OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenBudgeteer.Blazor.Models;

public sealed class AccountListModel(BalanceType type, string displayName, string icon)
{
    public List<AccountDetailViewModel> Accounts = [];

    public readonly BalanceType Type = type;
    public readonly string DisplayName = displayName;
    public readonly string Icon = icon;

    public AccountDetailViewModel this[int index]
    {
        get
        {
            if (index >= 0 && index < Accounts.Count)
                return Accounts[index];
            else
                throw new IndexOutOfRangeException();
        }
        set
        {
            if (index >= 0 && index < Accounts.Count)
                Accounts[index] = value;
            else
                throw new IndexOutOfRangeException();
        }
    }

    public AccountListModel FilterByUser(Guid id)
    {
        var userAccounts = Accounts.Where(accounts => accounts.Owners.Any(owner => owner.Id == id)).ToList();

        return new(Type, DisplayName, Icon) { Accounts = userAccounts };
    }
}

public enum BalanceType
{
    Asset,
    Liability,
    Payable,
    Receivable,
    Deferred
}
