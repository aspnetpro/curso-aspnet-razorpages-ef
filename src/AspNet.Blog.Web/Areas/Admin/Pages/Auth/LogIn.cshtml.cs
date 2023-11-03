using AspNet.Blog.Web.Areas.Admin.Models.FormModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AspNet.Blog.Web.Areas.Admin.Pages.Auth;

public class LogInModel : PageModel
{
    public IActionResult OnGet()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Manage/Dashboard");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(
        [FromForm, FromQuery] LogInFormModel logInModel)
    {
        if (logInModel.Username == "admin" && logInModel.Password == "admin")
        {
            var user = new
            {
                Id = Guid.NewGuid(),
                Name = "Administrator"
            };

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var identity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = logInModel.RememberMe });

            if (!String.IsNullOrWhiteSpace(logInModel.ReturnUrl))
            {
                return Redirect(logInModel.ReturnUrl);
            }

            return RedirectToPage("/Manage/Dashboard");
        }

        ViewData["Fail"] = true;

        return Page();
    }
}
