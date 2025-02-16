using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SUS.Core.Abstractions.Interfaces;

namespace SUS.Web.Controllers;

public class GenericController<TPrimaryKeyType, TRequest, TResponse>(
    IGenericService<TPrimaryKeyType, TRequest, TResponse> genericService,
    ILogger<GenericController<TPrimaryKeyType, TRequest, TResponse>> logger
) : ControllerBase, IGenericController<TPrimaryKeyType, TRequest, TResponse>
{
    protected readonly IGenericService<TPrimaryKeyType, TRequest, TResponse> _service =
        genericService;
    protected readonly ILogger<GenericController<TPrimaryKeyType, TRequest, TResponse>> _logger =
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

    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TResponse>>> Get(
        CancellationToken cancellationToken
    )
    {
        try
        {
            return Ok(await _service.GetAllAsync(cancellationToken));
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
