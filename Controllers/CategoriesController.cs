using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eCommerceApplication.Data;
using eCommerceApplication.Models;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using NuGet.Packaging;

namespace eCommerceApplication.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public IActionResult Index(int id) //parentcategoryID
        {
            if (ModelState.IsValid)
            {
                var Result = _context.Categories.Where(c => c.ParentCategoryId == id).ToList();
             
                try
                {
                    if (Result.FirstOrDefault() != null)
                    {
                        ViewBag.ParentCategoryId = Result.FirstOrDefault().ParentCategoryId;

                    }
                    else
                    {
                        ViewBag.ParentCategoryId = 0;

                    }
                    return View(Result);

                }
                catch 
                {
                    return View("ERROR");
                }

            }

            return NotFound();
                
        }




        public IActionResult AddProducts(int id) // Receive Category ID
        {
            // Find the bundle by ID
            var category = _context.Categories.Include(b => b.Products).FirstOrDefault(b => b.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            // Load available products
            var availableProducts = _context.Products.ToList();

            // Create a view model
            CategoryViewModel viewModel = new CategoryViewModel
            {

                CategoryId = id,
                ParentCategoryId = category.ParentCategoryId,
                CategoryName = category.Name, // Pass bundle name
                AvailableProducts = availableProducts
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult AddProducts(CategoryViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            var category = _context.Categories.Include(b => b.Products).FirstOrDefault(b => b.ID == model.CategoryId);
            
            if (category == null)
            {
                return NotFound();
            }

            var selectedProducts = _context.Products.Where(p => model.SelectedProductIds.Contains(p.ID)).ToList();
            category.Products.AddRange(selectedProducts);
         

            _context.SaveChanges();

            return RedirectToAction("Index", new { id = category.ParentCategoryId });
            //}

            //model.AvailableProducts = _context.Products.ToList();
            //return View(model);
        }





        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory).Include(c=>c.Products)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create(int id)   //parentcategoryid
        {
            ViewData["ParentCategoryId"] = new SelectList(_context.ParentCategories, "Id", "Id");
            ViewBag.ParentId = id;
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Category category )
        {
            if (ModelState.IsValid)
            {
                var result = _context.ParentCategories.Find(category.ParentCategoryId);
                category.ParentCategory = result;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddProducts",new {category.ID}) ;
            }
            ViewData["ParentCategoryId"] = new SelectList(_context.ParentCategories, "Id", "Id", category.ParentCategoryId);
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null || _context.Categories == null)
            //{
            //    return NotFound();
            //}

            var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(m => m.ID == id); ;
            if (category == null)
            {
                return NotFound();
            }
            //var categoryProductIds = category.Products.Select(p => p.ID).ToList();
            //var availableProducts = _context.Products
            //    .Where(p => !categoryProductIds.Contains(p.ID))
            //    .ToList();
            var categoryProductIds = category.Products.Select(p => p.ID).ToList();
            var availableProducts = _context.Products
                .Where(p => !categoryProductIds.Contains(p.ID))
                .ToList();
            ViewBag.AvailableProducts = availableProducts;
            ViewData["ParentCategoryId"] = new SelectList(_context.ParentCategories, "Id", "Id", category.ParentCategoryId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category, int id, int? addProduct, int? removeProduct, string action)
        {
            var category01 = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.ID == id);

            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (action == "AddProduct" && addProduct != null)
                    {
                        var product = await _context.Products.FindAsync(addProduct);
                        if (product != null)
                        {
                            category.Products.Add(product);
                        }
                    }
                    else if (action == "RemoveProduct" && removeProduct != null)
                    {
                        var product = category.Products.FirstOrDefault(p => p.ID == removeProduct);
                        if (product != null)
                        {
                            category.Products.Remove(product);
                        }
                    }
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //  return RedirectToAction("UpdateProducts", new { id = category.ID});
                  return RedirectToAction("Index", new { id = category.ParentCategoryId });

            }
            ViewData["ParentCategoryId"] = new SelectList(_context.ParentCategories, "Id", "Id", category.ParentCategoryId);
            return View(category);
        }




        public async Task<IActionResult> UpdateProducts(int id)
        {
            var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(m => m.ID == id); ;
            if (category == null)
            {
                return NotFound();
            }
            //var categoryProductIds = category.Products.Select(p => p.ID).ToList();
            //var availableProducts = _context.Products
            //    .Where(p => !categoryProductIds.Contains(p.ID))
            //    .ToList();
            var categoryProductIds = category.Products.Select(p => p.ID).ToList();
            var availableProducts = _context.Products
                .Where(p => !categoryProductIds.Contains(p.ID))
                .ToList();
            ViewBag.AvailableProducts = availableProducts;
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProducts(int id, int[]? addProduct, int[]? removeProduct, string action)
        
        {
            
             var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.ID == id);

            if (ModelState.IsValid)
            {
                try
                {

                    if (action == "AddProduct" && addProduct != null)
                    {
                        foreach (var item in addProduct)
                        {
                            var product = await _context.Products.FindAsync(item);
                            if (product != null)
                            {
                                category.Products.Add(product);
                            }
                        }
                    }
                    else if (action == "RemoveProduct" && removeProduct != null)
                    {
                        foreach (var item in removeProduct)
                        {
                            var product = category.Products.FirstOrDefault(p => p.ID == item);
                            if (product != null)
                            {
                                category.Products.Remove(product);
                            }
                        }
                    }
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = category.ParentCategoryId });
            }
            return View(category);
        }


        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.ParentCategory)
                .FirstOrDefaultAsync(m => m.ID == id);
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
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = category.ParentCategoryId });
        }

        private bool CategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
