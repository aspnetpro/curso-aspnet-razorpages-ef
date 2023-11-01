using AspNet.Blog.Web.Emails;
using AspNet.Blog.Web.Models.FormModel;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNet.Blog.Web.Pages.Contact;

public class IndexModel : PageModel
{
    public async Task<IActionResult> OnPostAsync([FromForm] ContactFormModel contact)
    {
        var email = await Email
            .From($"{contact.Name} <{contact.Email}>")
            .To("mbanagouro@outlook.com", "Michel Banagouro")
            .Subject("Novo contato!")
            .UsingTemplate(Resource.contato, contact)
            .SendAsync();

        return Page();
    }
}
