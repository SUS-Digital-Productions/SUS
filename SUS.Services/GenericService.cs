using AutoMapper;
using SUS.Core.Abstractions.Interfaces;
using SUS.Pagination.Base;

namespace SUS.Services;

public class GenericService<TPrimaryKeyType, TRequest, TResponse, TEntity>(
    IMapper mapper,
    IGenericRepository<TPrimaryKeyType, TEntity> repository
) : IGenericService<TPrimaryKeyType, TRequest, TResponse>
{
    protected readonly IMapper _mapper = mapper;
    protected readonly IGenericRepository<TPrimaryKeyType, TEntity> _repository = repository;

    public virtual async Task<TResponse> AddAsync(
        TRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var entity = _mapper.Map<TEntity>(request);
        var result = await _repository.AddAsync(entity, cancellationToken);
        return _mapper.Map<TResponse>(result);
    }

    public virtual async Task<TResponse> AddOrGetAsync(
        TPrimaryKeyType key,
        TRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var entity = await _repository.GetAsync(key, cancellationToken);
        if (entity == null)
            return await AddAsync(request, cancellationToken);
        return _mapper.Map<TResponse>(entity);
    }

    public virtual async Task DeleteAsync(
        TPrimaryKeyType key,
        CancellationToken cancellationToken = default
    )
    {
        await _repository.DeleteAsync(key, cancellationToken);
    }

    public virtual async Task<IEnumerable<TResponse>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TResponse>>(result);
    }

    public virtual async Task<TResponse> GetAsync(
        TPrimaryKeyType key,
        CancellationToken cancellationToken = default
    )
    {
        var result = await _repository.GetAsync(key, cancellationToken);
        return _mapper.Map<TResponse>(result);
    }

    public virtual async Task<PaginatedList<TResponse>> GetPageAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var paginatedList = await _repository.GetPageAsync(page, pageSize, cancellationToken);
        return new PaginatedList<TResponse>(
            _mapper.Map<List<TResponse>>(paginatedList.Items),
            paginatedList.TotalCount,
            paginatedList.PageNumber,
            paginatedList.PageSize
        );
    }

    public virtual async Task<TResponse> UpdateAsync(
        TPrimaryKeyType key,
        TRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var entity = _mapper.Map<TEntity>(request);
        var result = await _repository.UpdateAsync(key, entity, cancellationToken);
        return _mapper.Map<TResponse>(result);
    }
}
