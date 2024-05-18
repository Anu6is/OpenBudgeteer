using Microsoft.EntityFrameworkCore;
using OpenBudgeteer.Core.Data.Entities;
using OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

namespace OpenBudgeteer.Extensions.MetaData;

public class ExtendedDatabaseContext(DbContextOptions<DatabaseContext> options) : DatabaseContext(options)
{
    public DbSet<AccountDetail> AccountDetail { get; set; }

}
