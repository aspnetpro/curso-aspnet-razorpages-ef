namespace AspNet.Blog.Web.Models.ViewModel;

public record PostListItemModel
{
    public int PostId { get; set; }
    public string? Permalink { get; set; }
    public string? Title { get; set; }
    public string? Summary { get; set; }
    public string? PublishedOn { get; set; }

    public CategoryViewModel? Category { get; set; }

    public record CategoryViewModel
    {
        public string? Name { get; set; }
        public string? Permalink { get; set; }
    }
}
