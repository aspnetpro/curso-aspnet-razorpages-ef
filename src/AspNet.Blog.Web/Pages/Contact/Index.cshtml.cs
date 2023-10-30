using AspNet.Blog.Web.Models.FormModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNet.Blog.Web.Pages.Contact;

public class IndexModel : PageModel
{
    public async Task<IActionResult> OnPostAsync([FromForm] ContactFormModel contact)
    {
        return Page();
    }
}
