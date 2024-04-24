namespace AspNet.Blog.Web.Areas.Admin.Models.ViewModel;

public record PostListItemModel
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? PublishedOn { get; set; }
    public string? EditUrl { get; set; }
    public string? DeleteUrl { get; set; }
}
