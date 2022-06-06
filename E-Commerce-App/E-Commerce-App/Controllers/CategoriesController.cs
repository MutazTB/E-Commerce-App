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
using Microsoft.AspNetCore.Authorization;

namespace E_Commerce_App.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategory _category;

        public CategoriesController(ICategory category)
        {
            _category = category;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _category.GetCategories());
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Details")] Category category)
        {
            if (ModelState.IsValid)
            {
                //_context.Add(category);
                //await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));

                await _category.CreateCategory(category);
                return RedirectToAction(nameof(Index));

            }
            return View(category);
        }


        // GET: Categories/Details/5  test
       
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _category.GetCategory(id);                
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _category.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Details")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var cate = await _category.UpdateCategory(id ,category);
                   
                }
                catch (DbUpdateConcurrencyException)
                {                    
                    return NotFound();                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _category.GetCategory(id);                
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {           
             await _category.DeleteCategory(id);
            return RedirectToAction(nameof(Index));
        }
        /*
        private bool CategoryExists(int id)
        {
            return _context.categories.Any(e => e.Id == id);
        }

        */

    }

}
