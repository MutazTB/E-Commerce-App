using E_Commerce_App.Models.ViewModels;
using E_Commerce_App.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace E_Commerce_App.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _user;

        public AccountController(IUserService user)
        {
            _user = user;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM viewModel)
        {
            try
            {
                await _user.Register(viewModel);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

            return View("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM viewModel)
        {
            try
            {
                await _user.Login(viewModel);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _user.Logout();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return Content("UnAuthorized");
        }

    }
}
