using Microsoft.EntityFrameworkCore;
using OpenBudgeteer.Core.Data.Entities;
using OpenBudgeteer.Core.Data.Services;
using OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

namespace OpenBudgeteer.Extensions.MetaData;

public class ExtendedServiceManager(DbContextOptions<DatabaseContext> dbContextOptions) : ServiceManager(dbContextOptions)
{
    private readonly DbContextOptions<DatabaseContext> _dbContextOptions = dbContextOptions;

    public AccountDetailService AccountDetailService => new(new AccountDetailRepository(new ExtendedDatabaseContext(_dbContextOptions)));
}
