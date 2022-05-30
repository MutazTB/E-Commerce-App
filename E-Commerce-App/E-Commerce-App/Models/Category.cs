using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        List<Product> products { get; set; }
    }
}
