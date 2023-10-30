namespace AspNet.Blog.Web.Models;

public record PageOptions
{
    private int _page;
    public int Page
    {
        get { return (_page <= 0) ? 1 : _page; }
        set { _page = value; }
    }

    private int _size;
    public int Size
    {
        get { return (_size <= 0) ? 10 : _size; }
        set { _size = value; }
    }
}
