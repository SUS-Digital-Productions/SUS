using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SUS.Pagination.Base;

namespace SUS.Data.Pagination;

public static class PaginationExtensions
{
    public static async Task<PaginatedList<TType>> PageAsync<TType>(
        this IQueryable<TType> source,
        int page,
        int size
    )
    {
        var count = await source.CountAsync();
        var items = await source.Skip((page - 1) * size).Take(size).ToListAsync();
        return new PaginatedList<TType>(items, count, page, size);
    }

    public static async Task<PaginatedList<TType>> PageAsync<TType>(
        this DbSet<TType> set,
        int page,
        int size
    )
        where TType : class
    {
        var count = await set.CountAsync();
        var items = await set.Skip((page - 1) * size).Take(size).ToListAsync();
        return new PaginatedList<TType>(items, count, page, size);
    }

    public static Task<PaginatedList<TType>> PageAsync<TType>(
        this IEnumerable<TType> enumerable,
        int page,
        int size
    )
    {
        var count = enumerable.Count();
        var items = enumerable.Skip((page - 1) * size).Take(size).ToList();
        return Task.FromResult(new PaginatedList<TType>(items, count, page, size));
    }

    public static Task<PaginatedList<DestType>> Map<SrcType, DestType>(
        this PaginatedList<SrcType> source,
        IMapper mapper
    )
    {
        return Task.FromResult(
            new PaginatedList<DestType>(
                mapper.Map<List<DestType>>(source.Items),
                source.TotalCount,
                source.PageNumber,
                source.PageSize
            )
        );
    }
}
