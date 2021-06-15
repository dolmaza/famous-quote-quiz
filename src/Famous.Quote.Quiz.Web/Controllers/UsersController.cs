using Famous.Quote.Quiz.Application.Services;
using Famous.Quote.Quiz.Application.Services.DataModels;
using Famous.Quote.Quiz.Domain.Exceptions;
using Famous.Quote.Quiz.Web.Infrastructure;
using Famous.Quote.Quiz.Web.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Famous.Quote.Quiz.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("users", Name = RouteNames.User.Users)]
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetUsersAsync();

            return View(new UserViewModel
            {
                Users = users
            });
        }

        [HttpGet]
        [Route("users/create", Name = RouteNames.User.Create)]
        public IActionResult Create()
        {
            return View(new UserEditorModel { User = new UserDto() });
        }

        [HttpPost]
        [Route("users/create", Name = RouteNames.User.Create)]
        public async Task<IActionResult> Create(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return View(new UserEditorModel { User = user });
            }

            try
            {
                var id = await _userService.AddNewUserAsync(user);

                InitSuccessMessage();
                return RedirectToRoute(RouteNames.User.Update, new { id = id });
            }
            catch (Exception)
            {
                InitErrorMessage();
                return View(new UserEditorModel { User = user });
            }
        }

        [HttpGet]
        [Route("users/{id}/update", Name = RouteNames.User.Update)]
        public async Task<IActionResult> Update(int id)
        {
            var userDto = await _userService.GetSingleUserByIdAsync(id);

            if (userDto == default)
            {
                return NotFound();
            }

            return View(new UserEditorModel { User = userDto });
        }

        [HttpPost]
        [Route("users/{id}/update", Name = RouteNames.User.Update)]
        public async Task<IActionResult> Update(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return View(new UserEditorModel { User = user });
            }

            try
            {
                await _userService.UpdateUserAsync(user);

                InitSuccessMessage();
                return RedirectToRoute(RouteNames.User.Update, new { id = user.Id });
            }
            catch (FamousQuoteQuizDomainException ex)
            {
                InitErrorMessage(ex.Message);
                return View(new UserEditorModel { User = user });
            }
            catch (Exception)
            {
                InitErrorMessage();
                return View(new UserEditorModel { User = user });
            }
        }

        [HttpPost]
        [Route("users/{id}/delete", Name = RouteNames.User.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);

                return Ok(new { message = "User deleted successfully" });
            }
            catch (FamousQuoteQuizDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = _defaultErrorMessage });
            }
        }

        [HttpPost]
        [Route("users/{id}/disable", Name = RouteNames.User.Disable)]
        public async Task<IActionResult> Disable(int id)
        {
            try
            {
                await _userService.DisableUserAsync(id);

                return Ok(new { message = "User disabled successfully" });
            }
            catch (FamousQuoteQuizDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = _defaultErrorMessage });
            }
        }

        [HttpPost]
        [Route("users/{id}/activate", Name = RouteNames.User.Activate)]
        public async Task<IActionResult> Activate(int id)
        {
            try
            {
                await _userService.ActivateUserAsync(id);

                return Ok(new { message = "User activated successfully" });
            }
            catch (FamousQuoteQuizDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = _defaultErrorMessage });
            }
        }
    }
}
