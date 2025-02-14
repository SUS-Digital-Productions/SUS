using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SUS.Core.Abstractions.Interfaces
{
    interface IGenericController<TPrimaryKeyType, TRequest, TResponse>
        : IController<TPrimaryKeyType, TRequest, TResponse>
    {
        public Task<ActionResult<IEnumerable<TResponse>>> Get(CancellationToken cancellationToken);
    }
}
