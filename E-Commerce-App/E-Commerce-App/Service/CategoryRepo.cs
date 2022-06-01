using E_Commerce_App.Data;
using E_Commerce_App.Models;
using E_Commerce_App.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_App.Service
{
    public class CategoryRepo : ICategory
    {

        private ECommerceDbContext _context;

        public CategoryRepo(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(Category Category)
        {
            _context.Entry(Category).State = EntityState.Added;
            await _context.SaveChangesAsync();

            return Category;
        }

        public async Task DeleteCategory(int Id)
        {
            Category category = await _context.categories.FindAsync(Id);
            _context.Entry(category).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int Id)
        {
            return await _context.categories.FindAsync(Id);
        }

        public async Task<Category> UpdateCategory(int Id, Category Category)
        {
            Category UpdatedCategory = new Category
            {
                Id = Category.Id,
                Name = Category.Name,
                Details = Category.Details                
            };
            _context.Entry(UpdatedCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Category;
        }
    }
}
