using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SUS.Pagination.Base;

namespace SUS.Core.Abstractions.Interfaces
{
    interface IGenericPagedController<TPrimaryKeyType, TRequest, TResponse>
        : IController<TPrimaryKeyType, TRequest, TResponse>
    {
        public Task<ActionResult<PaginatedList<TResponse>>> GetPage(
            [FromQuery] int page,
            [FromQuery] int size,
            CancellationToken cancellationToken
        );
    }
}
