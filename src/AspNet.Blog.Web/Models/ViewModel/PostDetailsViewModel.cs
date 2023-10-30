namespace AspNet.Blog.Web.Models.ViewModel;

public record PostDetailsViewModel
{
    public int PostId { get; set; }
    public string? Permalink { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Tags { get; set; }
    public string? PublishedOn { get; set; }
    public int TotalComments { get; set; }
    
    public CategoryViewModel? Category { get; set; }

    public IEnumerable<string> GetTags()
    {
        if (String.IsNullOrWhiteSpace(this?.Tags))
        {
            return Enumerable.Empty<string>();
        }

        return this.Tags.Split(',');
    }

    public record CategoryViewModel
    {
        public string? Name { get; set; }
        public string? Permalink { get; set; }
    }
}
