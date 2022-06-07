using E_Commerce_App.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce_App.Service.Interface
{
    public interface IUserService
    {
        public Task Register(RegisterVM viewModel);

        public Task Login(LoginVM viewModel);

        public Task Logout();
    }
}
