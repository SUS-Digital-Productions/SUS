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

    public virtual IQueryable<TEntity> AsQueryable()
    {
        return _list.AsQueryable();
    }

    public virtual Task DeleteAsync(int key, CancellationToken cancellationToken = default)
    {
        if (key >= 0 && key < _list.Count)
        {
            _list.RemoveAt(key);
        }
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
        if (key >= 0 && key < _list.Count)
        {
            return Task.FromResult(_list[key]);
        }
        return null;
    }

    public virtual Task<TEntity> GetOrAddAsync(
        int key,
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        return GetAsync(key, cancellationToken) ?? AddAsync(entity, cancellationToken);
    }

    public virtual Task<Page<TEntity>> GetPageAsync(
        PageRequest pageRequest,
        CancellationToken cancellationToken = default
    )
    {
        return _list.PageAsync(pageRequest.Page, pageRequest.PageSize);
    }

    public virtual Task<TEntity> UpdateAsync(
        int key,
        TEntity entity,
        CancellationToken cancellationToken = default
    )
    {
        if (key >= 0 && key < _list.Count)
        {
            _list[key] = entity;
        }
        return Task.FromResult(entity);
    }
}
