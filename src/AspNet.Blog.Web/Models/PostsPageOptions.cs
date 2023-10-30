namespace AspNet.Blog.Web.Models;

public record PostsPageOptions : PageOptions
{
    public string? Term { get; set; }
    public string? Category { get; set; }
}
