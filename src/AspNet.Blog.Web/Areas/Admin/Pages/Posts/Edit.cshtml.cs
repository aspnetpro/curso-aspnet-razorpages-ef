using AspNet.Blog.Web.Areas.Admin.Models.FormModel;
using AspNet.Blog.Web.Areas.Admin.Models.ViewModel;
using AspNet.Blog.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Blog.Web.Areas.Admin.Pages.Posts;

[Authorize]
public class EditModel(BlogContext blogContext) 
    : PageModel
{
    public PostViewModel Post { get; set; }

    public async Task<IActionResult> OnGetAsync(
        [FromRoute] int? postId)
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
            Title = post.Title,
            Summary = post.Summary,
            Content = post.Content
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(
        [FromForm] PostFormModel editPost)
    {


        return Page();
    }
}
