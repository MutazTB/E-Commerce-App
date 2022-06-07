using E_Commerce_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Data
{
    public class ECommerceDbContext : IdentityDbContext<ApplicationUser>
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

            // any unique string id
            const string ADMIN_ID = "a18be9c0";
            const string ADMIN_ROLE_ID = "ad376a8f";

            const string EDITOR_ID = "a50ze710";
            const string EDITOR_ROLE_ID = "bd586a8f";

            // create an Admin role
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "Admin",
                NormalizedName = "Admin"
            }, new IdentityRole
            {
                Id = EDITOR_ROLE_ID,
                Name = "Editor",
                NormalizedName = "Editor"
            });

            // create a User
            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "Admin123#"),
                SecurityStamp = string.Empty
            },
            new ApplicationUser
            {
                Id = EDITOR_ID,
                UserName = "editor",
                NormalizedUserName = "editor",
                Email = "editor@gmail.com",
                NormalizedEmail = "editor@gmail.com",
                EmailConfirmed = false,
                PasswordHash = hasher.HashPassword(null, "Editor123#"),
                SecurityStamp = string.Empty
            });

            // assign that user to the admin role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            },
            new IdentityUserRole<string>
            {
                RoleId = EDITOR_ROLE_ID,
                UserId = EDITOR_ID
            });
        }
    }
}