using Microsoft.EntityFrameworkCore;
using OpenBudgeteer.Core.Data.Contracts.Repositories;

namespace OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

public class AccountDetailRepository(ExtendedDatabaseContext databaseContext) : IBaseRepository<AccountDetail>
{
    public IQueryable<AccountDetail> All() => databaseContext.AccountDetail.AsNoTracking();

    public IQueryable<AccountDetail> AllWithIncludedEntities() => databaseContext.AccountDetail.Include(x => x.Account).AsNoTracking();

    public AccountDetail? ById(Guid id) => databaseContext.AccountDetail.FirstOrDefault(i => i.Id == id);

    public AccountDetail? ByIdWithIncludedEntities(Guid id) => databaseContext.AccountDetail.Include(x => x.Account).FirstOrDefault(i => i.Id == id);

    public int Create(AccountDetail entity)
    {
        databaseContext.AccountDetail.Add(entity);
        return databaseContext.SaveChanges();
    }

    public int CreateRange(IEnumerable<AccountDetail> entities)
    {
        databaseContext.AccountDetail.AddRange(entities);
        return databaseContext.SaveChanges();
    }

    public int Update(AccountDetail entity)
    {
        databaseContext.AccountDetail.Update(entity);
        return databaseContext.SaveChanges();
    }

    public int UpdateRange(IEnumerable<AccountDetail> entities)
    {
        databaseContext.AccountDetail.UpdateRange(entities);
        return databaseContext.SaveChanges();
    }

    public int Delete(Guid id)
    {
        var entity = databaseContext.AccountDetail.FirstOrDefault(i => i.Id == id)
            ?? throw new Exception($"Account with id {id} not found.");
        
        databaseContext.AccountDetail.Remove(entity);
        
        return databaseContext.SaveChanges();
    }

    public int DeleteRange(IEnumerable<Guid> ids)
    {
        var entities = databaseContext.AccountDetail.Where(i => ids.Contains(i.Id));
        
        if (!entities.Any()) throw new Exception($"No Account found with passed IDs.");

        databaseContext.AccountDetail.RemoveRange(entities);
        return databaseContext.SaveChanges();
    }
}
