namespace GameStats.Model;

public static class ExtensionMethods
{
    public static IQueryable<T> ApplyPaging<T, TFilter>(this IQueryable<T> query, PagedQuery<TFilter> pageQuery)
    {
        if (pageQuery.Offset > 0)
        {
            query = query.Skip(pageQuery.Offset);
        }
        if (pageQuery.Take > 0)
        {
            query = query.Take(pageQuery.Take);
        }
        return query;
    }
}
