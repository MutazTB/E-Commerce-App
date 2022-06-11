using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Commerce_App.Data;
using E_Commerce_App.Models;
using E_Commerce_App.Service.Interface;
using E_Commerce_App.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace E_Commerce_App.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProduct _product;
        private readonly IConfiguration _configuration;

        public ProductsController(IProduct product , IConfiguration configuration)
        {
            _product = product;
            _configuration = configuration;
        }

        public async Task<IActionResult> AllProducts()
        {
            return View(await _product.GetAllProducts());
        }

        // GET: Products
        [Route("Products/Index/{CategoryId}")]
        public async Task<IActionResult> Index(int CategoryId)
        {
            ViewData["CategoryId"] = CategoryId;
            return View(await _product.GetProducts(CategoryId));
        }

        // GET: Product/5
        public async Task<IActionResult> GetProduct(int Id)
        {
            return View(await _product.GetProduct(Id));
        }

        
        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _product.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateProduct()
        {
            CreateProductVM viewModel = new CreateProductVM
            {
                Categories = await _product.GetCategories()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product viewModel, IFormFile file)
        {

            BlobContainerClient container = new BlobContainerClient(_configuration.GetConnectionString("AzureBlob"), "images");
            await container.CreateIfNotExistsAsync();
            BlobClient blob = container.GetBlobClient(file.FileName);
            using var stream = file.OpenReadStream();

            BlobUploadOptions options = new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType }
            };
            if (!blob.Exists())
            {
                await blob.UploadAsync(stream, options);
            }

            viewModel.ImageUrl = blob.Uri.ToString();
            if (ModelState.IsValid)
            {                
            await _product.CreateProduct(viewModel, viewModel.CategoryId);             
            }
            stream.Close();

            return RedirectToAction("AllProducts");
    }


        [Authorize(Roles = "Admin")]
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Products/Create/{categoryId}")]
        public async Task<IActionResult> Create(Product product, int categoryId)
        {
            //return Content("categoryId: " + categoryId);

            if (ModelState.IsValid)
            {
                //_context.Add(product);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));

                await _product.CreateProduct(product, categoryId);
            }

            return RedirectToAction("Index", new { CategoryId = categoryId });
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _product.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public async Task<IActionResult> Edit(int id,Product product, IFormFile file)
        {
            BlobContainerClient container = new BlobContainerClient(_configuration.GetConnectionString("AzureBlob"), "images");
            await container.CreateIfNotExistsAsync();
            BlobClient blob = container.GetBlobClient(file.FileName);
            using var stream = file.OpenReadStream();

            BlobUploadOptions options = new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType }
            };
            if (!blob.Exists())
            {
                await blob.UploadAsync(stream, options);
            }

            product.ImageUrl = blob.Uri.ToString();
            
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   var pro = await _product.UpdateProduct(id ,product);                    
                }
                catch (DbUpdateConcurrencyException)
                {                    
                        return NotFound();                    
                }
                return RedirectToAction("Index", new { CategoryId = product.CategoryId });
            }
            stream.Close();
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _product.GetProduct(id);                
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _product.GetProduct(id);
             await _product.DeleteProduct(id);
            return RedirectToAction("Index", new { CategoryId = product.CategoryId });
        }

        //private bool ProductExists(int id)
        //{
        //    return _context.products.Any(e => e.Id == id);
        //}
        
    }
}
