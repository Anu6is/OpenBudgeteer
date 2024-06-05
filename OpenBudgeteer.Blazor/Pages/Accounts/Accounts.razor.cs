using Microsoft.AspNetCore.Components;
using MudBlazor;
using OpenBudgeteer.Blazor.Models;
using OpenBudgeteer.Blazor.Services;
using OpenBudgeteer.Blazor.Shared;
using OpenBudgeteer.Core.Common;
using OpenBudgeteer.Core.Data.Contracts.Services;
using OpenBudgeteer.Core.ViewModels.Helper;
using OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;
using System.Collections.Generic;
using System.Linq;
using static OpenBudgeteer.Blazor.Shared.FeedbackMessage;

namespace OpenBudgeteer.Blazor.Pages.Accounts;

public partial class Accounts : ComponentBase
{
    [Inject] private IServiceManager ServiceManager { get; set; } = null!;
    [Inject] private AccountManager AccountManager { get; set; } = null!;
    [Inject] private AccountDetailService AccountDetailService { get; set; } = null!;

    private bool _gridView;
    private bool _initialized;
    private bool _showInactive;
    private Currency? _selectedCurrency;
    private AccountDetailsPageViewModel _dataContext = null!;
    private Dictionary<string, AccountListModel> _accounts = [];


    private TransactionListingViewModel? _transactionModalDialogDataContext;

    protected override void OnInitialized()
    {
        _dataContext = new AccountDetailsPageViewModel(ServiceManager, AccountDetailService);

        HandleResult(_dataContext.LoadData());

        RefreshAccountList();

        _initialized = true;

        base.OnInitialized();
    }

    private void CreateNewAccount(AccountDetailViewModel account)
    {
        HandleResult(AccountManager.CreateAccount(account));

        DrawerService.ToggleDrawer();

        _selectedCurrency = account.Currency;

        Feedback?.SendNotification(new AlertMessage(Severity.Success, $"{account.AccountType} account successfully created"));
    }

    private void EditAccount(AccountDetailViewModel account)
    {
        DrawerService.RenderFragment = FragmentExtension.CreateRenderFragmentFrom<FormComponent<AccountForm, AccountDetailViewModel>, AccountDetailViewModel>(SaveChanges,
            new Dictionary<string, object>()
            {
                { "AccountDetailViewModel", account }
            });

        DrawerService.ToggleDrawer($"Edit {account.AccountType} Account");
    }


    private void SaveChanges(AccountDetailViewModel account)
    {
        HandleResult(AccountManager.UpdateAccount(account));

        DrawerService.ToggleDrawer();

        Feedback?.SendNotification(new AlertMessage(Severity.Success, $"{account.AccountType} account successfully updated"));
    }

    private void CancelChanges()
    {
        HandleResult(_dataContext.LoadData());
    }

    private void CloseAccount(AccountDetailViewModel account)
    {
        HandleResult(account.CloseAccount());
    }

    private void HandleResult(ViewModelOperationResult result)
    {
        if (!result.IsSuccessful)
        {
            Feedback?.SendNotification(new AlertMessage(Severity.Error, result.Message));
        }

        if (!result.ViewModelReloadRequired) return;

        RefreshAccountList();

        StateHasChanged();
    }

    private void RefreshAccountList()
    {
        InitializeAccountCategories();

        var result = _dataContext.LoadData();

        if (!result.IsSuccessful)
        {
            Feedback?.SendNotification(new AlertMessage(Severity.Error, result.Message));

            return;
        }

        var accountList = _dataContext.Accounts.ToArray();

        foreach (var account in _dataContext.Accounts.OrderBy(x => x.Name))
        {
            InsertAccount(account);
        }
    }

    private void InitializeAccountCategories()
    {
        var defaultAccounts = new Dictionary<string, AccountListModel>
        {
            {"Cash", new AccountListModel(BalanceType.Asset, "Cash On Hand", Icons.Material.Filled.AccountBalanceWallet)},
            {"Deposit", new AccountListModel(BalanceType.Asset, "Bank Accounts", Icons.Material.Filled.Savings)},
            {"Credit", new AccountListModel(BalanceType.Liability, "Credit Cards", Icons.Material.Filled.CreditCard)},
            {"Borrowing", new AccountListModel(BalanceType.Payable, "Borrowing", Icons.Material.Filled.Book)},
            {"Receivables", new AccountListModel(BalanceType.Receivable, "Receivables", Icons.Material.Filled.CollectionsBookmark)},
            {"Investment", new AccountListModel(BalanceType.Deferred, "Investments", Icons.Material.Filled.SsidChart)},
        };

        _accounts = new Dictionary<string, AccountListModel>(defaultAccounts);
    }

    private AccountListModel GetCategoryList(AccountDetailViewModel account)
    {
        var accountType = account.AccountType;

        var list = accountType == AccountType.Loan
            ? _accounts[account.SubType is SubType.Loan.Lending
                ? "Receivables"
                : "Borrowing"]
            : _accounts[accountType.ToString()];

        return list;
    }

    private void InsertAccount(AccountDetailViewModel account)
    {
        var list = GetCategoryList(account);

        list.Accounts.Add(account);

        list.Accounts = [.. list.Accounts.OrderBy(x => x.Name).ThenBy(x => x.Alias)];
    }

    private void ShowDrawer<T>(AccountType accountType) where T : IFragment, new()
    {
        DrawerService.RenderFragment = FragmentExtension.CreateRenderFragmentFrom<FormComponent<T, AccountDetailViewModel>, AccountDetailViewModel>(CreateNewAccount,
            new Dictionary<string, object>()
            {
                { "AccountType", accountType },
                { "AvailableAccounts", accountType == AccountType.Loan 
                    ? _dataContext.Accounts.Where(x => x.AccountType == AccountType.Cash || x.AccountType == AccountType.Deposit) : [] }
            });

        DrawerService.ToggleDrawer("Add New Account");
    }

    private void OnVisibilityChanged(bool toggled)
    {
        _showInactive = toggled;

        HandleResult(_dataContext.LoadData());
    }

    private void OnViewChanged(bool toggled)
    {
        _gridView = toggled;
    }

    private async void DisplayAccountTransactions(AccountDetailViewModel account)
    {

        _transactionModalDialogDataContext = new TransactionListingViewModel(ServiceManager);
        HandleResult(await _transactionModalDialogDataContext.LoadDataAsync(account.AccountId));

        StateHasChanged();
    }
}