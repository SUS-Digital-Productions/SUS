using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SUS.Core.Abstractions.Interfaces
{
    public interface IController<TPrimaryKeyType, TRequest, TResponse>
    {
        public Task<ActionResult<TResponse>> Get(
            TPrimaryKeyType id,
            CancellationToken cancellationToken
        );

        public Task<ActionResult<TResponse>> Post(
            [FromBody] TRequest request,
            CancellationToken cancellationToken
        );

        public Task<ActionResult<TResponse>> Put(
            TPrimaryKeyType id,
            [FromBody] TRequest request,
            CancellationToken cancellationToken
        );

        public Task<IActionResult> Delete(TPrimaryKeyType id, CancellationToken token);
    }
}
