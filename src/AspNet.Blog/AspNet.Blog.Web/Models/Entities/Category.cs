namespace AspNet.Blog.Models.Entities;

public class Category
{
    public int Id { get; set; }
    public string? Permalink { get; set; }
    public string? Name { get; set; }

    public ICollection<Post> Posts { get; set; } = new List<Post>();
}
