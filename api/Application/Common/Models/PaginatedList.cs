using Microsoft.EntityFrameworkCore;

namespace Application.Common.Models;

public record PaginatedList<T>
{
    public IReadOnlyList<T> Items { get; }
    public int PageIndex { get; }
    public int PageSize { get; }
    public int TotalCount { get; }

    public PaginatedList(IReadOnlyList<T> items, int totalCount, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items;
    }

    public static async Task<PaginatedList<T>> ToPaginatedListAsync(IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellation = default)
    {
        var count = await source.CountAsync(cancellation);

        var items = await source
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellation);

        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}

public record PaginatedListWithHasMore<T>
{
    public IReadOnlyList<T> Items { get; }
    public int PageIndex { get; }
    public int PageSize { get; }
    public bool HasMore { get; }

    public PaginatedListWithHasMore(IReadOnlyList<T> items, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;

        if (items.Count > pageSize)
        {
            HasMore = true;
            Items = items.Take(pageSize).ToList();
        }
        else
        {
            HasMore = false;
            Items = items;
        }
    }
}

public static class PaginatedListExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize, CancellationToken cancellation = default)
        => await PaginatedList<T>.ToPaginatedListAsync(source, pageIndex, pageSize, cancellation);

    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, PaginationDto paginationDto, CancellationToken cancellation = default)
        => await PaginatedList<T>.ToPaginatedListAsync(source, paginationDto.PageIndex, paginationDto.PageSize, cancellation);
}

