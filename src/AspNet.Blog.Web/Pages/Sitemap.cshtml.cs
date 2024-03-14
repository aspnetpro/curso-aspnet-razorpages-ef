using AspNet.Blog.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNet.Blog.Web.Pages;

public class SitemapModel(BlogContext blogContext)
    : PageModel
{
    public IList<string?> PostsPermalink { get; set; }
    public IList<string?> CategoriesPermalink { get; set; }

    public void OnGet()
    {
        this.PostsPermalink = blogContext.Posts
            .OrderByDescending(x => x.Id)
            .Select(x => x.Permalink)
            .ToList();

        this.CategoriesPermalink = blogContext.Categories
            .OrderBy(x => x.Name)
            .Select(x => x.Permalink)
            .ToList();
    }
}
