using E_Commerce_App.Models;
using E_Commerce_App.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace E_Commerce_App.Pages
{
    [Authorize]
    public class CartModel : PageModel
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; }

        public void OnGet()
        {
            Name = User.Identity.Name;
            Products = CartVM.Products;
        }


    }
}
