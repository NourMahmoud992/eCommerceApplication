using eCommerceApplication.Models;
using eCommerceApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace eCommerceApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepo;


        public ProductsController(IProductRepository productRepo)
        {
            this.productRepo = productRepo;
        }
        // GET: HomeController1

        public ActionResult Index(int ID)
        {
            //return View(productRepo.GetAllProducts);
            
            var result = productRepo.GetAllProducts(ID);
            return View(result);
        }
        //public ActionResult Index()
        //{
        //    //return View(productRepo.GetAllProducts);

        //    var result = productRepo.GetAllProducts();
        //    return View(result);
        //}
        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            
            return View(productRepo.GetProductDetails(id));
        }

        // GET: HomeController1/Create
        public ActionResult Create(int catid) //categoryid
        {
            ViewBag.catid = catid;

            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
           
            try
            {
                productRepo.CreateProduct(product);
                if (product.Image != null && product.Image.Length > 0)
                {

                    string imagePath = productRepo.SaveProductImage(product.Image, product);


                    product.ImagePath = imagePath;
                }
                return RedirectToAction(/*"Index", new { id =product.CategoryID }*/"DisplayAllProducts","Home");

            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            

            return View(productRepo.GetProductDetails(id));
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                productRepo.UpdateProduct(id, product);
                return RedirectToAction("DisplayAllProducts"/*"Index"*/,"Home"/* new { id =product.CategoryID }*/);

            }
            catch
            {
                return View();
            }
        }

       
    
        public ActionResult Delete(int id)
        {
            try
            {
                productRepo.DeleteProduct(id);
                return RedirectToAction(/*nameof(Index)*/ "DisplayAllProducts", "Home");
            }
            catch
            {
                return View();
            }
        }
    }
}
