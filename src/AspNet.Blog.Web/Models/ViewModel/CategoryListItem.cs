namespace AspNet.Blog.Web.Models.ViewModel;

public record CategoryListItem
{
    public string? Name { get; set; }
    public string? Permalink { get; set; }
    public int TotalPosts { get; set; }
}
