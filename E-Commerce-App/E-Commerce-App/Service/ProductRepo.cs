using E_Commerce_App.Data;
using E_Commerce_App.Models;
using E_Commerce_App.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Service
{
    public class ProductRepo : IProduct
    {
        private ECommerceDbContext _context;

        public ProductRepo(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProduct(Product Product, int categoryId)
        {
            Product.CategoryId = categoryId;
            _context.Entry(Product).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return Product;
        }

        public Task DeleteProduct(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.products.Include(x=>x.category).ToListAsync();
        }

        public async Task<Product> GetProduct(int Id)
        {
            return await _context.products.FindAsync(Id);
        }

        public async Task<List<Product>> GetProducts(int? CategoryId)
        {
            return await _context.products.Where(x => x.CategoryId == CategoryId).ToListAsync();
        }

        public Task<Product> UpdateProduct(int Id, Product Product)
        {
            throw new NotImplementedException();
        }
    }
}
