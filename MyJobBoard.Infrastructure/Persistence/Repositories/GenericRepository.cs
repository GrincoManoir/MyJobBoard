// MyJobBoard.Infrastructure/Persistence/GenericRepository.cs
using Microsoft.EntityFrameworkCore;
using MyJobBoard.Application.Interfaces;
using MyJobBoard.Domain.Entities;
using MyJobBoard.Domain.Enums;
using MyJobBoard.Infrastructure.Persistence.Repositories.QueryHelpers;


public class GenericRepository<TEntity> : IGenericRepository<TEntity>  where TEntity : class
{

    private readonly DbSet<TEntity> _myEntityDbSet;

    public GenericRepository(MyJobBoardBusinessDbContext context)
    {
        var entityType = context.Model.FindEntityType(typeof(TEntity));

        if (entityType != null)
        {
            _myEntityDbSet = context.Set<TEntity>();
        }
        else
        {
            throw new InvalidOperationException($"Entity type '{typeof(TEntity).FullName}' not found in DbContext.");
        }
    }

    /// <summary>
    /// Get items from database
    /// </summary>
    /// <param name="filterCriteria"> Specifies the property values to filter by </param>
    /// <param name="range"> a string in the format "i..j" both indices are indexes of elements included in the result set</param>
    /// <param name="sort"></param>
    /// <param name="desc"></param>
    /// <returns>Task<IEnumerable<TEntity>></returns>
    public async Task<IEnumerable<TEntity>> GetItemsAsync(Dictionary<string, object> filterCriteria, string? range, string? sort, bool? desc)
    {
        var resultQuery = GetQueryableItems(filterCriteria, range, sort, desc);
        return await resultQuery.AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Get queryable items from database
    /// </summary>
    /// <param name="filterCriteria"> Specifies the property values to filter by </param>
    /// <param name="range"> a string in the format "i..j" both indices are indexes of elements included in the result set</param>
    /// <param name="sort"></param>
    /// <param name="desc"></param>
    /// <returns>IQueryable<TEntity></returns>
    protected IQueryable<TEntity> GetQueryableItems(Dictionary<string, object> filterCriteria, string? range = null, string? sort = null, bool? desc = null)
    {
        var resultQuery = _myEntityDbSet.FilterByProperties(filterCriteria);
        resultQuery = resultQuery.SortByProperties(sort, desc);
        if (!string.IsNullOrWhiteSpace(range))
        {
            resultQuery = resultQuery.ApplyRange(range);
        }
        return resultQuery;
    }
}



