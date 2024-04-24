using AspNet.Blog.Models.Entities;
using AspNet.Blog.Web.Areas.Admin.Models.ViewModel;
using AspNet.Blog.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNet.Blog.Web.Areas.Admin.Pages.Posts;

[Authorize]
public class Index2Model(BlogContext blogContext) 
    : PageModel
{
    public IList<PostListItemModel> Posts { get; set; }

    public void OnGet()
    {
        this.Posts = blogContext.Posts
            .OrderByDescending(x => x.Id)
            .Select(x => new PostListItemModel
            {
                PostId = x.Id,
                Title = x.Title,
                PublishedOn = x.PublishedOn.Value.ToShortDateString(),
                EditUrl = Url.Page("/Posts/Edit", null, new { postId = x.Id, area = "Admin" }),
                DeleteUrl = Url.Page("/Posts/Delete", null, new { postId = x.Id, area = "Admin" })
            })
            .ToList();
    }
}
