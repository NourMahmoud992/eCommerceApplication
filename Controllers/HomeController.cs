using eCommerceApplication.Data;
using eCommerceApplication.Models;
using eCommerceApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace eCommerceApplication.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IHomeRepo homeRepo;
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(IHomeRepo homeRepo, ILogger<HomeController> logger, ApplicationDbContext context) 
        {
            this.homeRepo = homeRepo;
            _logger = logger;
            _context = context;



        }
        public ActionResult Search(string searchTerm)
        {

            return View(homeRepo.SearchProducts(searchTerm));
        }



        public ActionResult DisplayAllProducts()
        {
            var viewModel = new ProductCategoryViewModel
            {
                //Categories = _context.Categories.ToList(),
                //Products = _context.Products.ToList()
                Categories = homeRepo.GetCategories(),
                Products = _context.Products.ToList(),
                SelectedCategories = new List<int>(),
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult DisplayAllProducts(ProductCategoryViewModel viewModel)
        {
            var newproduct =  homeRepo.GetProducts(viewModel);
            return View(newproduct);
            //if (viewModel.SelectedCategories != null && viewModel.SelectedCategories.Any())
            //{
            //    viewModel.Products = _context.Products
            //        .Where(p => viewModel.SelectedCategories.Contains(p.CategoryID))
            //        .ToList();
            //}
            //else
            //{
            //    viewModel.Products = new List<Product>(); // No categories selected, return empty list
            //}

            //viewModel.Categories = _context.Categories.ToList();

            //return View(viewModel);
        }
        //public ActionResult DisplayAllProducts(ProductCategoryViewModel viewModel)
        //{
        //    ViewBag.SelectedCategoryIds = selectedCategoryIds;
        //    var categoriesids = homeRepo.GetProducts(selectedCategoryIds);
        //    return View(categoriesids);
        //}
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}