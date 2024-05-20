using System;

namespace OpenBudgeteer.Blazor.Models;

public sealed record BalanceModel(string TextOne, double LineOne, string TextTwo, double LineTwo)
{
    public required Func<double, double, double> Amount { get; init; }
    public required Func<double, double, double> BarMax { get; init; }
    public required Func<double, double, double> BarValue { get; init; }

    public double Max() => BarMax(LineOne, LineTwo);
    public double Value() => BarValue(LineOne, LineTwo);
    public double DisplayTotal() => Amount(LineOne, LineTwo);
}
