namespace AspNet.Blog.Web.Models.FormModel;

public class CommentFormModel
{
    public int PostId { get; set; }
    public string? Author { get; set; }
    public string? Email { get; set; }
    public string? Content { get; set; }
}
