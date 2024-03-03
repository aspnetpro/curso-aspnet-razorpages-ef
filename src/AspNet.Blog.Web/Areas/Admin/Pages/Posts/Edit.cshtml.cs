using AspNet.Blog.Models.Entities;
using AspNet.Blog.Web.Areas.Admin.Models.FormModel;
using AspNet.Blog.Web.Common;
using AspNet.Blog.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Blog.Web.Areas.Admin.Pages.Posts;

[Authorize]
public class EditModel(BlogContext blogContext) 
    : BaseModel
{
    public async Task<IActionResult> OnGetAsync(
        [FromRoute] int? postId)
    {
        if (postId == null)
        {
            return NotFound();
        }

        Post? post = await GetPostById(postId);
        if (post == null)
        {
            return NotFound();
        }

        this.PostForm = new PostFormModel
        {
            PostId = post.Id,
            Title = post.Title,
            Summary = post.Summary,
            Content = post.Content,
            Category = post.Category.Name,
            Tags = post.Tags
        };

        return Page();
    }

    [BindProperty]
    public PostFormModel PostForm { get; set; }

    public async Task<IActionResult> OnPostAsync(
        [FromRoute] int? postId)
    {
        Post? post = await GetPostById(postId);
        if (post == null)
        {
            return NotFound();
        }

        post.Permalink = PostForm.Permalink;
        post.Title = PostForm.Title;
        post.Summary = PostForm.Summary;
        post.Content = PostForm.Content;
        post.Tags = PostForm.Tags;

        if (!String.IsNullOrWhiteSpace(PostForm.Category))
        {
            string permalink = PostForm.Category.ToSlug();
            post.Category = blogContext.Categories.FirstOrDefault(x => x.Permalink == permalink);
           if (post.Category == null)
            {
                post.Category = new Category
                {
                    Name = PostForm.Category,
                    Permalink = permalink
                };
            }
        }
        else
        {
            post.Category = null;
        }

        try
        {
            blogContext.Update(post);
            await blogContext.SaveChangesAsync();

            Success("Your post has been saved");
        }
        catch (Exception)
        {
            // log this
            Error("Your post cannot saved");
            return Page();
        }

        return Page();
    }

    private Task<Post?> GetPostById(int? postId)
    {
        return blogContext.Posts
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == postId);
    }
}
