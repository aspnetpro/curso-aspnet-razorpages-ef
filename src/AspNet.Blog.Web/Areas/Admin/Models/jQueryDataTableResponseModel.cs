namespace AspNet.Blog.Web.Areas.Admin.Models;

public class jQueryDataTableResponseModel
{
    public string sEcho { get; set; }
    public int iTotalRecords { get; set; }
    public int iTotalDisplayRecords { get; set; }
    public object aaData { get; set; }
}
