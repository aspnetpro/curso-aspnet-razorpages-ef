namespace AspNet.Blog.Web.Common;

public interface IPagedList
{
    int PageIndex { get; }
    int PageSize { get; }
    int TotalCount { get; }
    int TotalPages { get; }
    int PreviousPage { get; }
    int NextPage { get; }
    bool HasPreviousPage { get; }
    bool HasNextPage { get; }
}

public class PagedList<T> : List<T>, IPagedList
{
    public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
    {
        pageIndex = (pageIndex > 0) ? pageIndex : 1;
        pageSize = (pageSize > 0) ? pageSize : 1;

        int total = source.Count();
        this.TotalCount = total;
        this.TotalPages = total / pageSize;

        if (total % pageSize > 0)
            TotalPages++;

        this.PageSize = pageSize;
        this.PageIndex = pageIndex;

        this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
    }

    public PagedList(IList<T> source, int pageIndex, int pageSize)
    {
        pageIndex = (pageIndex > 0) ? pageIndex : 1;
        pageSize = (pageSize > 0) ? pageSize : 1;

        TotalCount = source.Count();
        TotalPages = TotalCount / pageSize;

        if (TotalCount % pageSize > 0)
            TotalPages++;

        this.PageSize = pageSize;
        this.PageIndex = pageIndex;
        this.AddRange(source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
    }

    public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
    {
        pageIndex = (pageIndex > 0) ? pageIndex : 1;
        pageSize = (pageSize > 0) ? pageSize : 1;

        TotalCount = totalCount;
        TotalPages = TotalCount / pageSize;

        if (TotalCount % pageSize > 0)
            TotalPages++;

        this.PageSize = pageSize;
        this.PageIndex = pageIndex;
        this.AddRange(source);
    }

    public int PageIndex { get; private set; }

    public int PageSize { get; private set; }

    public int TotalCount { get; private set; }

    public int TotalPages { get; private set; }

    public int PreviousPage
    {
        get { return (PageIndex - 1); }
    }

    public int NextPage
    {
        get { return (PageIndex + 1); }
    }

    public bool HasPages
    {
        get { return (TotalPages > 1); }
    }
    public bool HasPreviousPage
    {
        get { return (PageIndex > 1); }
    }
    public bool HasNextPage
    {
        get { return (PageIndex < TotalPages); }
    }
}

public static class PagedListExtensions
{
    public static PagedList<T> ToPagedList<T>(this IQueryable<T> list, int pageIndex, int pageSize)
    {
        return new PagedList<T>(list, pageIndex, pageSize);
    }

    public static PagedList<T> ToPagedList<T>(this IList<T> list, int pageIndex, int pageSize)
    {
        return new PagedList<T>(list, pageIndex, pageSize);
    }

    public static PagedList<T> ToPagedList<T>(this IEnumerable<T> list, int pageIndex, int pageSize, int totalCount)
    {
        return new PagedList<T>(list, pageIndex, pageSize, totalCount);
    }
}
