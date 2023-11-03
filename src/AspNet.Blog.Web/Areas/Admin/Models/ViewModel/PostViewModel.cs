namespace AspNet.Blog.Web.Areas.Admin.Models.ViewModel;

public record PostViewModel
{
    public int? PostId { get; set; }
    public string? Permalink { get; set; }
    public string? Title { get; set; }
    public string? Category { get; set; }
    public string? Summary { get; set; }
    public string? Content { get; set; }
    public string? Tags { get; set; }
}
