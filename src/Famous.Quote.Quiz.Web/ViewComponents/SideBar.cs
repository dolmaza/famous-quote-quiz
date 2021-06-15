using Famous.Quote.Quiz.Domain.AggregatesModel.UserAggregate;
using Famous.Quote.Quiz.Web.Infrastructure;
using Famous.Quote.Quiz.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Famous.Quote.Quiz.Web.ViewComponents
{
    public class SideBar : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var requestedUrl = Request.Path.ToString().Split('?')[0];

            var usersUrl = Url.RouteUrl(RouteNames.User.Users);
            var quizzesUrl = Url.RouteUrl(RouteNames.Quiz.Quizzes);
            var homeUrl = Url.RouteUrl(RouteNames.Home.HomePage);
            var settingsUrl = Url.RouteUrl(RouteNames.Setting.Settings);

            var model = new List<SidebarMenuModel>
            {
                new SidebarMenuModel
                {
                    Caption = "Home",
                    IconName = "fa fa-home",
                    Url = homeUrl,
                    IsActive = homeUrl == requestedUrl
                }
            };

            if (User.IsInRole(UserRole.Admin.ToString()))
            {
                model.Add(new SidebarMenuModel
                {
                    Caption = "Users",
                    IconName = "fa fa-users",
                    Url = usersUrl,
                    IsActive = usersUrl == requestedUrl
                });


            }

            model.Add(new SidebarMenuModel
            {
                Caption = "Quizzes",
                IconName = "fa fa-list",
                Url = quizzesUrl,
                IsActive = quizzesUrl == requestedUrl
            });

            if (User.IsInRole(UserRole.User.ToString()))
            {
                model.Add(new SidebarMenuModel
                {
                    Caption = "Settings",
                    IconName = "fa fa-cog",
                    Url = settingsUrl,
                    IsActive = settingsUrl == requestedUrl
                });
            }

            return View(model);
        }
    }
}
