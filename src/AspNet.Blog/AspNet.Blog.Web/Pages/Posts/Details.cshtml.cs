using AspNet.Blog.Data;
using AspNet.Blog.Web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AspNet.Blog.Web.Pages.Posts;

public class DetailsModel : PageModel
{
    private readonly BlogContext blogContext;
    private readonly IMemoryCache memoryCache;

    public DetailsModel(BlogContext blogContext,
        IMemoryCache memoryCache)
    {
        this.blogContext = blogContext;
        this.memoryCache = memoryCache;
    }

    public PostDetailsViewModel Post { get; set; }

    public async Task<IActionResult> OnGetAsync([FromRoute] string permalink)
    {
        var cacheKey = $"post-{permalink}";

        var model = await memoryCache
            .GetOrCreateAsync(cacheKey, async (policy) =>
            {
                policy.AbsoluteExpiration = DateTime.Now.AddHours(1);

                var query = from p in blogContext.Posts
                            .Include(x => x.Category)
                            where p.Permalink == permalink
                            select new PostDetailsViewModel
                            {
                                Post = p,
                                TotalComments = p.Comments.Count()
                            };

                return await query.FirstOrDefaultAsync();
            });

        if (model == null)
        {
            return NotFound();  
        }

        this.Post = model;

        return Page();
    }
}
