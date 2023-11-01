using AspNet.Blog.Models.Entities;
using AspNet.Blog.Web.Common;
using AspNet.Blog.Web.Infrastructure.Data;
using AspNet.Blog.Web.Models;
using AspNet.Blog.Web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AspNet.Blog.Web.Pages.Posts
{
    public class IndexModel : PageModel
    {
        private readonly BlogContext blogContext;

        public IndexModel(BlogContext blogContext)
        {
            this.blogContext = blogContext;
        }

        public PagedList<PostListItemModel>? Posts { get; set; }

        public IActionResult OnGet([FromQuery] PostsPageOptions pageOptions)
        {
            IQueryable<Post> posts = blogContext.Posts
                .Include(x => x.Category);

            if (!String.IsNullOrWhiteSpace(pageOptions.Term))
            {
                posts = posts.Where(post =>
                    post.Title.Contains(pageOptions.Term) ||
                    post.Summary.Contains(pageOptions.Term) ||
                    post.Content.Contains(pageOptions.Term)
                );
            }

            if (!String.IsNullOrWhiteSpace(pageOptions.Category))
            {
                posts = posts.Where(x => x.Category.Permalink == pageOptions.Category);
            }

            this.Posts = posts
                .OrderByDescending(x => x.PublishedOn)
                .Select(post => new PostListItemModel
                {
                    PostId = post.Id,
                    Title = post.Title,
                    Permalink = post.Permalink,
                    Summary = post.Summary,
                    PublishedOn = post.PublishedOn.Value.ToShortDateString(),
                    Category = new PostListItemModel.CategoryViewModel
                    {
                        Name = post.Category.Name,
                        Permalink = post.Category.Permalink
                    }
                })
                .ToPagedList(pageOptions.Page, pageOptions.Size);

            return Page();
        }
    }
}
