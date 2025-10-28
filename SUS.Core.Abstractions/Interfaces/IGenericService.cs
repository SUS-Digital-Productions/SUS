using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SUS.Pagination.Base;

namespace SUS.Core.Abstractions.Interfaces
{
    public interface IGenericService<TPrimaryKeyType, TRequest, TResponse>
    {
        public Task<Page<TResponse>> GetPageAsync(
            PageRequest pageRequest,
            CancellationToken cancellationToken = default
        );

        public Task<TResponse> AddAsync(
            TRequest request,
            CancellationToken cancellationToken = default
        );

        public Task<IEnumerable<TResponse>> GetAllAsync(
            CancellationToken cancellationToken = default
        );

        public Task<TResponse> GetAsync(
            TPrimaryKeyType key,
            CancellationToken cancellationToken = default
        );

        public Task<TResponse> UpdateAsync(
            TPrimaryKeyType key,
            TRequest request,
            CancellationToken cancellationToken = default
        );

        public Task DeleteAsync(TPrimaryKeyType key, CancellationToken cancellationToken = default);

        public Task<TResponse> AddOrGetAsync(
            TPrimaryKeyType key,
            TRequest request,
            CancellationToken cancellationToken = default
        );
    }
}
