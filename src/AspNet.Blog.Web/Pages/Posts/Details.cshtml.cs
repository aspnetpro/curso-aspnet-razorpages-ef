using AspNet.Blog.Models.Entities;
using AspNet.Blog.Web.Common;
using AspNet.Blog.Web.Infrastructure.Data;
using AspNet.Blog.Web.Models;
using AspNet.Blog.Web.Models.FormModel;
using AspNet.Blog.Web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AspNet.Blog.Web.Pages.Posts;

public class DetailsModel(BlogContext blogContext,
        IMemoryCache memoryCache) : PageModel
{
    public PostDetailsViewModel? Post { get; set; }

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
                                PostId = p.Id,
                                Title = p.Title,
                                Permalink = p.Permalink,
                                Content = p.Content,
                                Tags = p.Tags,
                                PublishedOn = p.PublishedOn.Value.ToShortDateString(),
                                Category = new PostDetailsViewModel.CategoryViewModel
                                {
                                    Name = p.Category.Name,
                                    Permalink = p.Category.Permalink
                                },
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

    public IActionResult OnGetCommentsAsync([FromRoute] string permalink
        , [FromQuery] PageOptions pageOptions)
    {
        var model = (from c in blogContext.Comments
                     where c.Post.Permalink == permalink
                     select new CommentListItem
                     {
                         Id = c.Id,
                         Author = c.Author,
                         Email = c.Email,
                         Content = c.Content,
                         PublishedOn = c.PublishedOn.ToShortDateString() //.ToString("dd/MM/yyyy hh:mm")
                     })
                     .ToPagedList(pageOptions.Page, pageOptions.Size);

        return Partial("_Comments", model);
    }

    public async Task<IActionResult> OnPostAddCommentAsync([FromBody] CommentFormModel formModel)
    {
        var comment = new Comment
        {
            Author = formModel.Author,
            Email = formModel.Email,
            Content = formModel.Content,
            Post = await blogContext.Posts.FindAsync(formModel.PostId)
        };
        blogContext.Comments.Add(comment);
        await blogContext.SaveChangesAsync();

        memoryCache.Remove("post-" + comment.Post.Permalink);

        return RedirectToPage("/Posts/Details", new { permalink = comment.Post.Permalink });
    }
}
