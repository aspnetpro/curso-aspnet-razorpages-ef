using AspNet.Blog.Web.Areas.Admin.Models.FormModel;
using AspNet.Blog.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNet.Blog.Web.Areas.Admin.Pages.Manage.Posts
{
    public class AddModel : PageModel
    {
        private readonly BlogContext blogContext;

        public AddModel(BlogContext blogContext)
        {
            this.blogContext = blogContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(
            [FromForm] PostFormModel newPost)
        {


            return Page();
        }
    }
}
