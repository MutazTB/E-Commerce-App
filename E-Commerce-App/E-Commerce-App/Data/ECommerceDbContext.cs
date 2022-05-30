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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Clothes", Details = "Clothes" },
                new Category { Id = 2 , Name = "Cars" , Details = "Cars"}

              );

            modelBuilder.Entity<Product>().HasData(
               new Product { Id = 1, Name = "Jeans", Description = "Jeans" , ImageUrl = "Jeans.Url" , Price = 12 , CategoryId = 1},
               new Product { Id = 2, Name = "Jeans", Description = "Jeans", ImageUrl = "Jeans.Url", Price = 12 , CategoryId = 1},
               new Product { Id = 3, Name = "BMW", Description = "BMW", ImageUrl = "BMW.Url", Price = 12000, CategoryId = 2 }


             );
        }
    }

   
    }
