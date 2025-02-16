using Microsoft.EntityFrameworkCore;
using SUS.Core.Abstractions.Interfaces;
using SUS.Data.Pagination;
using SUS.Pagination.Base;

namespace SUS.Data.Repositories;

public class GenericRepository<TPrimaryKeyType, TEntity, TDatabaseContext>(TDatabaseContext context)
    : IGenericRepository<TPrimaryKeyType, TEntity>
    where TEntity : class
    where TDatabaseContext : DbContext
{
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    protected readonly TDatabaseContext _context = context;

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var resultingEntity = await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return resultingEntity.Entity;
    }

    public virtual async Task DeleteAsync(TPrimaryKeyType key, CancellationToken cancellationToken)
    {
        var entity = await _dbSet.FindAsync(key, cancellationToken);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<TEntity> GetAsync(
        TPrimaryKeyType key,
        CancellationToken cancellationToken
    )
    {
        return await _dbSet.FindAsync(key, cancellationToken);
    }

    public virtual async Task<PaginatedList<TEntity>> GetPageAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        return await _dbSet.PageAsync(page, pageSize);
    }

    public virtual async Task<TEntity> UpdateAsync(
        TPrimaryKeyType key,
        TEntity entity,
        CancellationToken cancellationToken
    )
    {
        var entityToUpdate = await _dbSet.FindAsync(key, cancellationToken);
        if (entityToUpdate != null)
        {
            foreach (var field in _context.Entry(entity).Properties)
            {
                if (field.Metadata.IsPrimaryKey() == false)
                {
                    if (field.CurrentValue is not null)
                        _context.Entry(entityToUpdate).Property(field.Metadata).CurrentValue =
                            field.CurrentValue;
                }
            }
            _dbSet.Update(entityToUpdate);
            await _context.SaveChangesAsync(cancellationToken);
        }
        return entityToUpdate;
    }
}
