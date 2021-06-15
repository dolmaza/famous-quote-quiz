using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Famous.Quote.Quiz.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected readonly string _defaultSuccessMessage = "Data saved successfully!";
        protected readonly string _defaultErrorMessage = "Sorry something went wrong!";
        public void InitSuccessMessage(string title = null)
        {
            TempData["Temp_Success_Message"] = title ?? _defaultSuccessMessage;
        }

        public void InitErrorMessage(string title = null)
        {
            TempData["Temp_Error_Message"] = title ?? _defaultErrorMessage;
        }
    }
}
