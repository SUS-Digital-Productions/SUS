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
        CancellationToken cancellationToken
    )
    {
        var entity = _mapper.Map<TEntity>(request);
        var result = await _repository.AddAsync(entity, cancellationToken);
        return _mapper.Map<TResponse>(result);
    }

    public virtual async Task DeleteAsync(TPrimaryKeyType key, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(key, cancellationToken);
    }

    public virtual async Task<IEnumerable<TResponse>> GetAllAsync(
        CancellationToken cancellationToken
    )
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<TResponse>>(result);
    }

    public virtual async Task<TResponse> GetAsync(
        TPrimaryKeyType key,
        CancellationToken cancellationToken
    )
    {
        var result = await _repository.GetAsync(key, cancellationToken);
        return _mapper.Map<TResponse>(result);
    }

    public virtual async Task<PaginatedList<TResponse>> GetPageAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken
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
        CancellationToken cancellationToken
    )
    {
        var entity = _mapper.Map<TEntity>(request);
        var result = await _repository.UpdateAsync(key, entity, cancellationToken);
        return _mapper.Map<TResponse>(result);
    }
}
