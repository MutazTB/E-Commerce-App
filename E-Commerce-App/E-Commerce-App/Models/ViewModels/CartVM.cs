using System.Collections.Generic;

namespace E_Commerce_App.Models.ViewModels
{
    public class CartVM
    {
        public static List<Product> Products { get; set; }

        static CartVM()
        {
            Products = new List<Product>();
        }
    }
}
