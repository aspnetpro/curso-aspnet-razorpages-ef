namespace AspNet.Blog.Web.Models.ViewModel;

public record CommentListItem
{
    public int Id { get; set; }
    public string? Author { get; set; }
    public string? Email { get; set; }
    public string? Content { get; set; }
    public string? PublishedOn { get; set; }
}
