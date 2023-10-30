using AspNet.Blog.Web.Infrastructure.Data;
using AspNet.Blog.Web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Blog.Web.Components;

[ViewComponent(Name = "CategoryListViewComponent")]
public class CategoryListViewComponent : ViewComponent
{
    private readonly BlogContext blogContext;

    public CategoryListViewComponent(BlogContext blogContext)
    {
        this.blogContext = blogContext;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var model = await (from c in blogContext.Categories
                           select new CategoryListItem
                           {
                               Permalink = c.Permalink,
                               Name = c.Name,
                               TotalPosts = c.Posts.Count()
                           })
                           .ToListAsync();

        return View("Default", model);
    }
}
