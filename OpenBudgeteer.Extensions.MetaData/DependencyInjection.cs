using Microsoft.Extensions.DependencyInjection;
using OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

namespace OpenBudgeteer.Extensions.MetaData;

public static class DependencyInjection
{
    public static IServiceCollection AddMetaData(this IServiceCollection services)
    {
        services.AddScoped<AccountManager>();
        services.AddScoped<AccountDetailService>();
        services.AddScoped<AccountDetailRepository>();
        
        services.AddScoped<ExtendedServiceManager>();
        services.AddDbContext<ExtendedDatabaseContext>();

        return services;
    } 
}
