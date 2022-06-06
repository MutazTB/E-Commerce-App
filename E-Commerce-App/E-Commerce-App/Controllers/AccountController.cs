using E_Commerce_App.Models.DTOs;
using E_Commerce_App.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Controllers
{
    public class AccountController : Controller
    {
        private IUserService userService;

        public AccountController(IUserService userSer)
        {
            userService = userSer;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> SignUp(RegisterUserDTO register)
        {
            var user = await userService.Register(register, this.ModelState);
            if (ModelState.IsValid)
            {
                return Redirect("/");
            }
            return View();
        }


        public async Task<ActionResult<UserDTO>> Authenticate(LoginDTO login)
        {
            var user = await userService.Authenticate(login.Username, login.Password);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            return Redirect("/");
        }

    }
}
