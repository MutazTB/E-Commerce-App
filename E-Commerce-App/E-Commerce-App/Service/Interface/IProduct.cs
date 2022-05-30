using E_Commerce_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Service.Interface
{
    public interface IProduct
    {
        Task<Product> CreateProduct(Product Product);

        Task<List<Product>> GetProducts();

        Task<Product> GetProduct(int Id);

        Task<Product> UpdateProduct(int Id, Product Product);

        Task DeleteProduct(int Id);
    }
}
