namespace AspNet.Blog.Models.Entities;

public class Post
{
    public int Id { get; set; }
    public string? Permalink { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public string? Content { get; set; }
    public string? Tags { get; set; }
    public DateTime? PublishedOn { get; set; }

    public Category Category { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
