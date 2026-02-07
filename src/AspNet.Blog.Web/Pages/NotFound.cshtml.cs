using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace AspNet.Blog.Web.Pages;

public class NotFoundModel : PageModel
{
    private readonly ILogger<NotFoundModel> _logger;

    public NotFoundModel(ILogger<NotFoundModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
