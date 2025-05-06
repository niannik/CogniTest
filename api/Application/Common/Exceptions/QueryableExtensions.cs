using System.Linq.Expressions;

namespace Application.Common.Exceptions;

public static class QueryableExtensions
{
    public static IQueryable<T> When<T>(this IQueryable<T> query, bool criteria, Expression<Func<T, bool>> predicate) =>
        criteria ? query.Where(predicate) : query;

    public static IEnumerable<T> When<T>(this IEnumerable<T> query, bool criteria, Func<T, bool> predicate) =>
        criteria ? query.Where(predicate) : query;
}