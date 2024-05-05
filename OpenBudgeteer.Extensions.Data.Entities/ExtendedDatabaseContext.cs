using Microsoft.EntityFrameworkCore;
using OpenBudgeteer.Core.Data.Entities;
using OpenBudgeteer.Extensions.Data.Entities.Models;

namespace OpenBudgeteer.Extensions.Data.Entities;

public class ExtendedDatabaseContext(DbContextOptions<DatabaseContext> options) : DatabaseContext(options)
{
    public DbSet<AccountDetail> AccountDetail { get; set; }

}
