using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SUS.Core.Abstractions.Interfaces;
using SUS.Pagination.Base;

namespace SUS.Web.Controllers;

public class GenericPagedController<TPrimaryKeyType, TRequest, TResponse>(
    ILogger<GenericPagedController<TPrimaryKeyType, TRequest, TResponse>> logger,
    IGenericService<TPrimaryKeyType, TRequest, TResponse> service
) : ControllerBase, IGenericPagedController<TPrimaryKeyType, TRequest, TResponse>
{
    private readonly IGenericService<TPrimaryKeyType, TRequest, TResponse> _service = service;
    private readonly ILogger<GenericPagedController<TPrimaryKeyType, TRequest, TResponse>> _logger =
        logger;

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(TPrimaryKeyType id, CancellationToken token)
    {
        try
        {
            await _service.DeleteAsync(id, token);
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<TResponse>> Get(
        TPrimaryKeyType id,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return Ok(await _service.GetAsync(id, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    [HttpGet]
    public virtual async Task<ActionResult<PaginatedList<TResponse>>> GetPage(
        [FromQuery] int page = 1,
        [FromQuery] int size = 20,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            return Ok(await _service.GetPageAsync(page, size, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    [HttpPost]
    public virtual async Task<ActionResult<TResponse>> Post(
        [FromBody] TRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return Ok(await _service.AddAsync(request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<TResponse>> Put(
        TPrimaryKeyType id,
        [FromBody] TRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return Ok(await _service.UpdateAsync(id, request, cancellationToken));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return BadRequest();
        }
    }
}
