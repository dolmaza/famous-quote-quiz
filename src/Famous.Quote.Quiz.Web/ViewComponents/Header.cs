using Microsoft.AspNetCore.Mvc;

namespace Famous.Quote.Quiz.Web.ViewComponents
{
    public class Header : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
