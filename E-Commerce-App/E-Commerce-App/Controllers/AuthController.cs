using E_Commerce_App.Models.DTOs;
using E_Commerce_App.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private IUserService _userService;

        public UsersController(IUserService service)
        {
            _userService = service;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO data)
        {
            var user = await _userService.Register(data, this.ModelState);

            if (ModelState.IsValid)
            {
                return user;
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("Signin")]
        public async Task<ActionResult<UserDTO>> Signin(LoginDTO data)
        {
            var user = await _userService.Authenticate(data.UserName, data.Password);

            if (user == null)
            {
                return Unauthorized();
            }
            return user;
        }
    }
}
