﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "You have missed to fill the username")]
        [Display(Name = "User Name")]
        [MinLength(3)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
