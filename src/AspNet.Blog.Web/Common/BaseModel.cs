using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNet.Blog.Web.Common
{
    public class BaseModel : PageModel
    {
        const string MSG_SUCCESS = "MSG_SUCCESS";
        const string MSG_ERROR = "MSG_ERROR";

        protected void Success(string message)
        {
            TempData[MSG_SUCCESS] = message;
        }

        protected void Error(string message)
        {
            TempData[MSG_ERROR] = message;
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            ViewData[MSG_SUCCESS] = TempData[MSG_SUCCESS];
            ViewData[MSG_ERROR] = TempData[MSG_ERROR];

            base.OnPageHandlerExecuting(context);
        }
    }
}
