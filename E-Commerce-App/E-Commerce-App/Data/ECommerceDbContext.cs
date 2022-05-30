using E_Commerce_App.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Data
{
    public class ECommerceDbContext : DbContext
    {
        public DbSet<Product> products { get; set; }

        public DbSet<Category> categories { get; set; }

        public ECommerceDbContext(DbContextOptions options) : base(options)
        {
        }

    }
}
