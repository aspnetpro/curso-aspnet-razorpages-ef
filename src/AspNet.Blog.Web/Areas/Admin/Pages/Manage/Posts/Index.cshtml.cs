using AspNet.Blog.Models.Entities;
using AspNet.Blog.Web.Areas.Admin.Models;
using AspNet.Blog.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNet.Blog.Web.Areas.Admin.Pages.Manage.Posts;

public class IndexModel : PageModel
{
    private readonly BlogContext blogContext;

    public IndexModel(BlogContext blogContext)
    {
        this.blogContext = blogContext;
    }

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnGetDatatableAsync(
        [FromQuery] jQueryDataTableRequestModel request)
    {
        IQueryable<Post> posts = this.blogContext.Posts.OrderByDescending(x => x.PublishedOn);

        if (!String.IsNullOrWhiteSpace(request.sSearch))
        {
            posts = posts.Where(x =>
                        x.Title.Contains(request.sSearch) ||
                        x.Summary.Contains(request.sSearch)
                    );
        }

        int total = posts.Count();
        posts = posts.Skip(request.iDisplayStart).Take(request.iDisplayLength);

        var model = new jQueryDataTableResponseModel
        {
            sEcho = request.sEcho,
            iTotalRecords = total,
            iTotalDisplayRecords = total,
            aaData = from r in posts.ToList()
                     select new
                     {
                         PostId = r.Id,
                         Title = r.Title,
                         PublishedOn = r.PublishedOn.Value.ToShortDateString(),
                         EditUrl = Url.Page("/Manage/Posts/Edit", new { postId = r.Id }),
                         DeleteUrl = Url.Page("/Manage/Posts/Delete", new { postId = r.Id })
                     }
        };

        return new JsonResult(model);
    }
}
