using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SUS.Pagination.Base;

namespace SUS.Core.Abstractions.Interfaces
{
    interface IGenericRepository<TPrimaryKeyType, TType>
    {
        public Task<PaginatedList<TType>> GetPageAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken
        );

        public Task<IEnumerable<TType>> GetAllAsync(CancellationToken cancellationToken);

        public Task<TType> GetAsync(TPrimaryKeyType key, CancellationToken cancellationToken);

        public Task<TType> AddAsync(TType entity, CancellationToken cancellationToken);

        public Task<TType> UpdateAsync(
            TPrimaryKeyType key,
            TType entity,
            CancellationToken cancellationToken
        );

        public Task DeleteAsync(TPrimaryKeyType key, CancellationToken cancellationToken);
    }
}
