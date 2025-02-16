using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SUS.Pagination.Base;

namespace SUS.Core.Abstractions.Interfaces
{
    public interface IGenericRepository<TPrimaryKeyType, TEntity>
    {
        public Task<PaginatedList<TEntity>> GetPageAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken
        );

        public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        public Task<TEntity> GetAsync(TPrimaryKeyType key, CancellationToken cancellationToken);

        public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);

        public Task<TEntity> UpdateAsync(
            TPrimaryKeyType key,
            TEntity entity,
            CancellationToken cancellationToken
        );

        public Task DeleteAsync(TPrimaryKeyType key, CancellationToken cancellationToken);
    }
}
