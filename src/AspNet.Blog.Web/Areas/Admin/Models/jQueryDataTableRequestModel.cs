namespace AspNet.Blog.Web.Areas.Admin.Models;

public class jQueryDataTableRequestModel
{
    public string sEcho { get; set; }
    public string sSearch { get; set; }
    public int iDisplayLength { get; set; }
    public int iDisplayStart { get; set; }
    public int iColumns { get; set; }
    public int iSortingCols { get; set; }
    public string sColumns { get; set; }
}
