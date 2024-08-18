using eCommerceApplication.Models;
using eCommerceApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApplication.Controllers
{
    public class ParentCategoriesController : Controller
    {
        // GET: ParentCategoriesController1
        private readonly IParentCategoriesRepo ParentRepo;


        public ParentCategoriesController(IParentCategoriesRepo ParentRepo)
        {
            this.ParentRepo = ParentRepo;
        }
        public ActionResult Index()
        {
            var result = ParentRepo.GetAllParentCategories();
            return View(result);
        }

        // GET: ParentCategoriesController1/Details/5
        public ActionResult Details(int id)
        {
            var result = ParentRepo.GetParentGategoriesDetails(id);
            return View(result);
        }

        // GET: ParentCategoriesController1/Create
        public ActionResult Create()
        {

            return View( );
        }

        // POST: ParentCategoriesController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParentCategory parentcategory)
        {
            try
            {
             ParentRepo.InsertParentCategory(parentcategory);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ParentCategoriesController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ParentCategoriesController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ParentCategory parentcategory)
        {
            try
            {
                ParentRepo.UpdateParentGategory(id,parentcategory);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("ERROR");
            }
        }

        // GET: ParentCategoriesController1/Delete/5
       

        // POST: ParentCategoriesController1/Delete/5
        
        public ActionResult Delete(int id)
        {
            try
            {
                ParentRepo.DeleteParentCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("ERROR");
            }
        }
    }
}
