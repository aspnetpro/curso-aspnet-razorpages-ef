using AspNet.Blog.Models.Entities;

namespace AspNet.Blog.Web.Models.ViewModel;

public record PostDetailsViewModel
{
    public Post? Post { get; set; }
    public int TotalComments { get; set; }

    public IEnumerable<string> GetTags()
    {
        if (String.IsNullOrWhiteSpace(Post?.Tags))
        {
            return Enumerable.Empty<string>();
        }

        return Post.Tags.Split(',');
    }
}
