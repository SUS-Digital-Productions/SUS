using SUS.Core.Abstractions.Interfaces;
using SUS.Data.Pagination;
using SUS.Pagination.Base;

namespace SUS.Data.Repositories;

public class GenericInMemoryRepository<TEntity> : IGenericRepository<int, TEntity>
{
    private readonly List<TEntity> _list = [];

    public virtual Task<TEntity> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        _list.Add(entity);
        return Task.FromResult(entity);
    }

    public virtual Task DeleteAsync(int key, CancellationToken cancellationToken = default)
    {
        _list.RemoveAt(key);
        return Task.CompletedTask;
    }

    public virtual Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        return Task.FromResult<IEnumerable<TEntity>>(_list);
    }

    public virtual Task<TEntity> GetAsync(int key, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_list[key]);
    }

    public virtual Task<PaginatedList<TEntity>> GetPageAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        return _list.PageAsync(page, pageSize);
    }

    public virtual Task<TEntity> UpdateAsync(
        int key,
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        _list[key] = entity;
        return Task.FromResult(entity);
    }
}
