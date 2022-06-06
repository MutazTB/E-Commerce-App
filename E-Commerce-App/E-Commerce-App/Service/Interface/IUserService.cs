using E_Commerce_App.Models.DTOs;
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
        public Task<UserDTO> Register(RegisterUserDTO registerDto, ModelStateDictionary modelstate);

        public Task<UserDTO> Authenticate(string username, string password);
        
        public Task<UserDTO> GetUser(ClaimsPrincipal principal);
    }
}
