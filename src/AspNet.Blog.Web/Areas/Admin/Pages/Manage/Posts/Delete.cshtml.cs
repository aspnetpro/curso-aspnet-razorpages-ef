using AspNet.Blog.Web.Areas.Admin.Models.ViewModel;
using AspNet.Blog.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Blog.Web.Areas.Admin.Pages.Manage.Posts;

public class DeleteModel : PageModel
{
    private readonly BlogContext blogContext;

    public DeleteModel(BlogContext blogContext)
    {
        this.blogContext = blogContext;
    }

    public PostViewModel Post { get; set; }

    public async Task<IActionResult> OnGetAsync([FromQuery] int? postId)
    {
        if (postId == null)
        {
            return NotFound();
        }

        var post = await blogContext.Posts
            .FirstOrDefaultAsync(x => x.Id == postId);
        if (post == null)
        {
            return NotFound();
        }

        this.Post = new PostViewModel
        {
            PostId = post.Id,
            Title = post.Title
        };

        return Page();
    }

    public async Task<IActionResult> OnDeleteAsync(
        [FromQuery] int? postId)
    {
        if (postId == null)
        {
            return NotFound();
        }

        await blogContext.Posts
            .Where(post => post.Id == postId)
            .ExecuteDeleteAsync();
        await blogContext.SaveChangesAsync();

        return RedirectToPage("/Admin/Posts/Index");
    }
}
