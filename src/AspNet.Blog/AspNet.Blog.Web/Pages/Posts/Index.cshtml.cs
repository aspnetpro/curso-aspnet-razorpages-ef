using AspNet.Blog.Data;
using AspNet.Blog.Models.Entities;
using AspNet.Blog.Web.Common;
using AspNet.Blog.Web.Models;
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

        public PagedList<Post>? Posts { get; set; }

        public IActionResult OnGet([FromQuery] PostsPageOptions pageOptions)
        {
            var posts = blogContext.Posts
                .Include(x => x.Category);

            if (!String.IsNullOrWhiteSpace(pageOptions.Term))
            {
                posts.Where(post =>
                    post.Title.Contains(pageOptions.Term) ||
                    post.Summary.Contains(pageOptions.Term) ||
                    post.Content.Contains(pageOptions.Term)
                );
            }

            if (!String.IsNullOrWhiteSpace(pageOptions.Category))
            {
                posts.Where(x => x.Category.Permalink == pageOptions.Category);
            }

            this.Posts = posts
                .OrderByDescending(x => x.PublishedOn)
                .ToPagedList(pageOptions.Page, pageOptions.Size);

            return Page();
        }
    }
}
