using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eCommerceApplication.Data;
using eCommerceApplication.Models;
using NuGet.Packaging;
using System.Reflection.Metadata;

namespace eCommerceApplication.Controllers
{
    public class BundlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BundlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bundles
        public async Task<IActionResult> Index()
        {
              return _context.Bundles != null ? 
                          View(await _context.Bundles.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bundles'  is null.");
        }

        public ActionResult CalculateFinalPrice(double price, double discount)
        {
            // Validate inputs
            if (price < 0 || discount < 0 || discount > 100)
            {
                // Handle invalid inputs
                ViewBag.ErrorMessage = "Invalid price or discount.";
                return View("Error");
            }
            double finalPrice = price - (price * (discount / 100));

            // Calculate final price after discount


            // Pass the final price to the view
            ViewBag.FinalPrice = finalPrice;
            return View();
           // return RedirectToAction(nameof(Index));
        }
        // GET: Bundles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bundles == null)
            {
                return NotFound();
            }

            var bundle = await _context.Bundles
                .Include(c => c.Products)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bundle == null)
            {
                return NotFound();
            }

            return View(bundle);
        }

        // GET: Bundles/Create
       
        public IActionResult Create ()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Bundle bundle)
        {


            if (ModelState.IsValid)
            {
                _context.Add(bundle);
                _context.SaveChanges();
                return RedirectToAction(nameof(AddProducts), new { id = bundle.ID }); // Pass bundle ID
            }
            return View(bundle);
        }
        public IActionResult AddProducts(int id) // Receive bundle ID
        {
            // Find the bundle by ID
            var bundle = _context.Bundles.Include(b => b.Products).FirstOrDefault(b => b.ID == id);
            if (bundle == null)
            {
                return NotFound();
            }

            // Load available products
            var availableProducts = _context.Products.ToList();

            // Create a view model
            BundleViewModel viewModel = new BundleViewModel
            {

                BundleId = id,

                BundleName = bundle.Name, // Pass bundle name
                AvailableProducts = availableProducts
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult AddProducts(BundleViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var bundle = _context.Bundles.Include(b => b.Products).FirstOrDefault(b => b.ID == model.BundleId);
                if (bundle == null)
                {
                    return NotFound();
                }

                var selectedProducts = _context.Products.Where(p => model.SelectedProductIds.Contains(p.ID)).ToList();
                bundle.Products.AddRange(selectedProducts);
                bundle.Price = selectedProducts.Sum(product => product.Price);

                _context.SaveChanges();

                return RedirectToAction(nameof(CalculateFinalPrice), new { price = bundle.Price, discount = bundle.Discount });
            //}

            //model.AvailableProducts = _context.Products.ToList();
            //return View(model);
        }

        // GET: Bundles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bundles == null)
            {
                return NotFound();
            }

            var bundle = await _context.Bundles.FindAsync(id);
            if (bundle == null)
            {
                return NotFound();
            }
            return View(bundle);
        }

        // POST: Bundles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,Price,Discount")] Bundle bundle)
        {
            if (id != bundle.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bundle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BundleExists(bundle.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bundle);
        }

        // GET: Bundles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bundles == null)
            {
                return NotFound();
            }

            var bundle = await _context.Bundles
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bundle == null)
            {
                return NotFound();
            }

            return View(bundle);
        }

        // POST: Bundles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bundles == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bundles'  is null.");
            }
            var bundle = await _context.Bundles.FindAsync(id);
            if (bundle != null)
            {
                _context.Bundles.Remove(bundle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BundleExists(int id)
        {
          return (_context.Bundles?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
