using E_Commerce_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_App.Service.Interface
{
    public interface ICategory
    {
        Task<Category> CreateCategory(Category Category);

        Task<List<Category>> GetCategories();

        Task<Category> GetCategory(int Id);

        Task<Category> UpdateCategory(int Id, Category Category);

        Task DeleteCategory(int Id);
    }
}
