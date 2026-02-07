using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNet.Blog.Web.Common
{
    public class BaseModel : PageModel
    {
        [TempData]
        public string SuccessMessage { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        protected void Success(string message)
        {
            SuccessMessage = message;
        }

        protected void Error(string message)
        {
            ErrorMessage = message;
        }
    }
}
