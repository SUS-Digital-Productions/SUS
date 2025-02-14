using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SUS.Pagination.Base;

namespace SUS.Core.Abstractions.Interfaces
{
    interface IGenericService<TResponse, TRequest, TPrimaryKeyType>
    {
        public Task<PaginatedList<TResponse>> GetPageAsync(
            int page,
            int pageSize,
            CancellationToken cancellationToken
        );

        public Task<TResponse> AddAsync(TRequest request, CancellationToken cancellationToken);

        public Task<IEnumerable<TResponse>> GetAllAsync(CancellationToken cancellationToken);

        public Task<TResponse> GetAsync(TPrimaryKeyType key, CancellationToken cancellationToken);

        public Task<TResponse> UpdateAsync(
            TPrimaryKeyType key,
            TRequest request,
            CancellationToken cancellationToken
        );

        public Task DeleteAsync(TPrimaryKeyType key, CancellationToken cancellationToken);
    }
}
