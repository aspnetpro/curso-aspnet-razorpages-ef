using AspNet.Blog.Web.Emails;
using AspNet.Blog.Web.Models.FormModel;
using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing.Template;

namespace AspNet.Blog.Web.Pages;

public class ContactModel : PageModel
{
    private readonly IFluentEmail email;

    [BindProperty]
    public ContactFormModel ContactForm { get; set; }

    public ContactModel(IFluentEmail email)
    {
        this.email = email;
    }

    public void OnPost()
    {
        var response = this.email
            .To("michel@leanwork.com.br", "Michel Banagouro")
            .ReplyTo(ContactForm?.Email, ContactForm?.Name)
            .Subject("Novo contato!")
            .Body(ContactForm?.Message)
            //.UsingTemplate(Templates.Index, ContactForm)
            .Send();

        if (!response.Successful)
        {
            ViewData["MSG_ERROR"] = response.ErrorMessages.FirstOrDefault();
        }
    }
}
