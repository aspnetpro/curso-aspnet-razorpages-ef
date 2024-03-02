using AspNet.Blog.Models.Entities;
using AspNet.Blog.Web.Areas.Admin.Models.FormModel;
using AspNet.Blog.Web.Common;
using AspNet.Blog.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Blog.Web.Areas.Admin.Pages.Posts;

[Authorize]
public class AddModel(BlogContext blogContext) 
    : BaseModel
{
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public PostFormModel PostForm { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        Post newPost = new Post
        {
            Permalink = PostForm.Permalink,
            Title = PostForm.Title,
            Summary = PostForm.Summary,
            Content = PostForm.Content,
            Tags = PostForm.Tags,
            PublishedOn = DateTime.Now
        };

        if (!String.IsNullOrWhiteSpace(PostForm.Category))
        {
            string permalink = PostForm.Category.ToSlug();
            newPost.Category = blogContext.Categories.FirstOrDefault(x => x.Permalink == permalink);
            if (newPost.Category == null)
            {
                newPost.Category = new Category
                {
                    Name = PostForm.Category,
                    Permalink = permalink
                };
            }
        }
        else
        {
            newPost.Category = null;
        }

        try
        {
            blogContext.Add(newPost);
            await blogContext.SaveChangesAsync();

            Success("Your post has been saved");
        }
        catch (Exception)
        {
            // log this
            Error("Your post cannot saved");
            return Page();
        }

        return RedirectToPage("/Posts/Edit", new { postId = newPost.Id });
    }
}
