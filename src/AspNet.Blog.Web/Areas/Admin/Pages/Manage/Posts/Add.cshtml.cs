using AspNet.Blog.Models.Entities;
using AspNet.Blog.Web.Areas.Admin.Models.FormModel;
using AspNet.Blog.Web.Common;
using AspNet.Blog.Web.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace AspNet.Blog.Web.Areas.Admin.Pages.Manage.Posts
{
    public class AddModel : BaseModel
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
            [FromForm] PostFormModel formModel)
        {
            Post newPost = new Post();
            newPost.Permalink = formModel.Title.ToSlug();
            newPost.Title = formModel.Title;
            newPost.Summary = formModel.Summary;
            newPost.Content = formModel.Content;
            newPost.Tags = formModel.Tags;

            if (!String.IsNullOrWhiteSpace(formModel.Category))
            {
                string permalink = formModel.Category.ToSlug();
                newPost.Category = this.blogContext.Categories.FirstOrDefault(x => x.Permalink == permalink);
                if (newPost.Category == null)
                {
                    newPost.Category = new Category
                    {
                        Name = formModel.Category,
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
                this.blogContext.Add(newPost);
                await this.blogContext.SaveChangesAsync();

                Success("Your post has been saved");
            }
            catch (Exception)
            {
                // log this
                Error("Your post cannot saved");
            }

            return RedirectToPage("/Manage/Posts/Edit", new { postId = newPost.id });
        }
    }
}
