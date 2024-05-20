using OpenBudgeteer.Core.Data.Contracts.Services;
using System.Collections.Immutable;

namespace OpenBudgeteer.Extensions.MetaData.Features.AccountDetails;

public class AccountDetailService(AccountDetailRepository repository) : IBaseService<AccountDetail>
{
    public AccountDetail Get(Guid id)
    {
        try
        {
            var result = repository.ByIdWithIncludedEntities(id) ?? throw new Exception($"{nameof(AccountDetail)} not found in database");

            if (result.Account.IsActive == 0) result.Account.Name += " (Inactive)";

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception($"Error querying database: {e.Message}");
        }
    }

    public IEnumerable<AccountDetail> GetAll()
    {
        try
        {
            var result = repository.AllWithIncludedEntities().ToImmutableArray();

            foreach (var accountDetail in result.Where(x => x.Account.IsActive == 0))
            {
                accountDetail.Account.Name += " (Inactive)";
            }

            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception($"Error on querying database: {e.Message}");
        }
    }

    public IEnumerable<AccountDetail> GetActiveAccounts()
    {
        try
        {
            return [.. repository.AllWithIncludedEntities()
                .Where(x => x.Account.IsActive == 1)
                .OrderBy(x => x.Account.Name)];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception($"Error on querying database: {e.Message}");
        }
    }

    public AccountDetail Create(AccountDetail entity)
    {
        try
        {
            var result = repository.Create(entity);

            if (result == 0) throw new Exception($"Unable to create {nameof(AccountDetail)} in database");

            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception($"Errors during database update: {e.Message}");
        }
    }

    public virtual AccountDetail Update(AccountDetail entity)
    {
        try
        {
            var result = repository.Update(entity);
            if (result == 0) throw new Exception($"Unable to update {nameof(AccountDetail)} in database");
            return entity;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception($"Errors during database update: {e.Message}");
        }
    }

    public void Delete(Guid id)
    {
        try
        {
            var result = repository.Delete(id);

            if (result == 0) throw new Exception($"Unable to delete {nameof(AccountDetail)} in database");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception($"Errors during database update: {e.Message}");
        }
    }
}
