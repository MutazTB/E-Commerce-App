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

        public Task DeleteCategory(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetCategories()
        {
            return await _context.categories.ToListAsync();
        }

        public Task<Category> GetCategory(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategory(int Id, Category Category)
        {
            throw new NotImplementedException();
        }
    }
}
