using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor;
using MudBlazor.Services;
using OpenBudgeteer.Blazor;
using OpenBudgeteer.Blazor.Services;
using OpenBudgeteer.Blazor.Services.Providers;
using OpenBudgeteer.Core.Common;
using OpenBudgeteer.Core.Data;
using OpenBudgeteer.Core.Data.Contracts.Services;
using OpenBudgeteer.Core.Data.Entities;
using OpenBudgeteer.Core.ViewModels.Helper;
using OpenBudgeteer.Extensions.MetaData;
using System;
using System.Text;
using Tewr.Blazor.FileReader;

const string APPSETTINGS_CULTURE = "APPSETTINGS_CULTURE";
const string APPSETTINGS_TITLE = "APPSETTINGS_TITLE";

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CurrencyOptions>(builder.Configuration.GetSection(CurrencyOptions.Section));

builder.Services.AddLocalization();
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
builder.Services.AddMetaData();
builder.Services.AddMudServices(config => config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter);
builder.Services.AddFileReaderService();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHostedService<HostedDatabaseMigrator>();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddSingleton<CurrencyProvider>();
builder.Services.AddScoped<PreferenceManager>();
builder.Services.AddScoped<DrawerService>();
builder.Services.AddScoped<IServiceManager, ExtendedServiceManager>(x => new ExtendedServiceManager(x.GetRequiredService<DbContextOptions<DatabaseContext>>()));
builder.Services.AddScoped(x => new YearMonthSelectorViewModel(x.GetRequiredService<IServiceManager>()));

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // Required to read ANSI Text files

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRequestLocalization(builder.Configuration.GetValue<string>(APPSETTINGS_CULTURE, "en-US")!);
AppSettings.Title = builder.Configuration.GetValue(APPSETTINGS_TITLE, "OpenBudgeteer")!;

//app.UseRouting();
app.UseAntiforgery();
/*app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorComponents<App>().AddInteractiveServerRenderMode();
});*/
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

