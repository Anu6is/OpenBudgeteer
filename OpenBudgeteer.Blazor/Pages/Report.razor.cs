using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApexCharts;
using Microsoft.AspNetCore.Components;
using OpenBudgeteer.Blazor.Common;
using OpenBudgeteer.Blazor.ViewModels;
using OpenBudgeteer.Core.Data.Contracts.Services;

namespace OpenBudgeteer.Blazor.Pages;

public partial class Report : ComponentBase
{
    [Inject] private IServiceManager ServiceManager { get; set; } = null!;
    
    private ApexChart<ReportRecord> MonthBalanceChart;
    private ApexChart<ReportRecord> BankBalanceChart;
    private ApexChart<ReportRecord> MonthIncomeExpensesChart;
    private ApexChart<ReportRecord> YearIncomeExpensesChart;
    private List<ApexChart<ReportRecord>> MonthBucketExpensesCharts = new();
    private ApexChart<ReportRecord> InjectMonthBucketExpensesChart
    {
        set => MonthBucketExpensesCharts.Add(value);
    }
    
    private ApexChartOptions<ReportRecord> MonthIncomeExpensesChartOptions = new()
    {
        Legend = new() { Show = false }
    };
    private ApexChartOptions<ReportRecord> YearIncomeExpensesChartOptions = new()
    {
        Legend = new() { Show = false }
    };
    
    private ApexReportViewModel _apexContext = null!;
    private List<Tuple<string, List<ReportRecord>>> _monthBucketExpensesConfigsLeft = null!;
    private List<Tuple<string, List<ReportRecord>>> _monthBucketExpensesConfigsRight = null!;

    protected override async Task OnInitializedAsync()
    {
        _monthBucketExpensesConfigsLeft = new List<Tuple<string, List<ReportRecord>>>();
        _monthBucketExpensesConfigsRight = new List<Tuple<string, List<ReportRecord>>>();
        MonthBucketExpensesCharts = new();
    
        _apexContext = new ApexReportViewModel(ServiceManager);
        await _apexContext.LoadDataAsync();
    
        var halfIndex = _apexContext.MonthBucketExpenses.Count / 2;
        _monthBucketExpensesConfigsLeft.AddRange(_apexContext.MonthBucketExpenses.GetRange(0,halfIndex));
        _monthBucketExpensesConfigsRight.AddRange(_apexContext.MonthBucketExpenses.GetRange(halfIndex,_apexContext.MonthBucketExpenses.Count - halfIndex));
        
        StateHasChanged();
        var tasks = new List<Task>()
        {
            MonthBalanceChart.UpdateSeriesAsync(true),
            BankBalanceChart.UpdateSeriesAsync(true),
            MonthIncomeExpensesChart.UpdateSeriesAsync(true),
            YearIncomeExpensesChart.UpdateSeriesAsync(true)
        };
        tasks.AddRange(MonthBucketExpensesCharts
            .Select(monthBucketExpensesChart => monthBucketExpensesChart.UpdateSeriesAsync()));

        await Task.WhenAll(tasks);
    }
}