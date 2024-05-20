using Microsoft.Extensions.Options;
using OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;
using System.Collections.Generic;
using System.Linq;

namespace OpenBudgeteer.Blazor.Services.Providers;

public class CurrencyProvider(IOptions<CurrencyOptions> options)
{
    public Currency DefaultCurrency => Currencies.FirstOrDefault(x => x.IsoCode.Equals(options.Value.Default), Currencies.First());
    public IEnumerable<Currency> Currencies { get; } = options.Value.Currencies;
}

public class CurrencyOptions
{
    public static string Section { get; } = nameof(CurrencyOptions);

    public string? Default { get; set; }
    public Currency[] Currencies { get; set; } = [];
}
