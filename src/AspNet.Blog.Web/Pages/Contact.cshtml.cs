using AspNet.Blog.Web.Emails;
using AspNet.Blog.Web.Models.FormModel;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNet.Blog.Web.Pages;

public class ContactModel : PageModel
{
    [BindProperty]
    public ContactFormModel ContactForm { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        var email = await Email
            .From($"{ContactForm?.Name} <{ContactForm?.Email}>")
            .To("mbanagouro@outlook.com", "Michel Banagouro")
            .Subject("Novo contato!")
            .UsingTemplate(Resource.contato, ContactForm)
            .SendAsync();

        return Page();
    }
}
