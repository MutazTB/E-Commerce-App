using E_Commerce_App.Models;
using E_Commerce_App.Models.DTOs;
using E_Commerce_App.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce_App.Service
{
    public class IdentityUserService : IUserService
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;


        public IdentityUserService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task Login(LoginVM viewModel, ModelStateDictionary modelState)
        {
            var user = await _userManager.FindByNameAsync(viewModel.UserName);

            if (user == null)
            {
                throw new Exception("User is null");
            }

            var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }
            else
            {
                modelState.AddModelError(string.Empty, "Invalid Login");
            }

        }

        public async Task Register(RegisterVM data, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser
            {
                UserName = data.UserName,
                Email = data.Email,
            };

            var result = await _userManager.CreateAsync(user, data.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    var errorKey =
                        error.Code.Contains("Password") ? nameof(data.Password) :
                        error.Code.Contains("Email") ? nameof(data.Email) :
                        error.Code.Contains("UserName") ? nameof(data.UserName) :
                        "";
                    modelState.AddModelError(errorKey, error.Description);
                }
            }
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
