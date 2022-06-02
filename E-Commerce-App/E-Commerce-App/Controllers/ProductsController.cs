﻿using System;
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

namespace E_Commerce_App.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProduct _product;

        public ProductsController(IProduct product)
        {
            _product = product;
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
    public async Task<IActionResult> CreateProduct([Bind("Name,Price,Description,ImageUrl,CategoryId")] CreateProductVM viewModel)
    {

        if (ModelState.IsValid)
        {
                Product product = new Product
                {
                    Name = viewModel.Name,
                    Price = viewModel.Price,
                    Description = viewModel.Description,
                    ImageUrl = viewModel.ImageUrl
                };

            await _product.CreateProduct(product, viewModel.CategoryId);
        }

        return RedirectToAction("AllProducts");
    }


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
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,ImageUrl")] Product product, int categoryId)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,ImageUrl,CategoryId")] Product product)
        {
            
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
            return View(product);
        }

        // GET: Products/Delete/5
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
