using eCommerceApplication.Data;
using eCommerceApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;

namespace eCommerceApplication.Services
{

    
    public class ProductRepository: IProductRepository
    {
        public string connstr { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;
        private readonly ApplicationDbContext context;

        public ProductRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            connstr = _configuration.GetConnectionString("DefaultConnection");
            this.context = context;
        }
        public void CreateProduct(Product product)
        {
                context.Products.Add(product);
                

            context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            context.Remove(context.Products.Find(id));
            context.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts(int id)
        {
            //ProductViewModel viewModel = new ProductViewModel
            //{
            //    Products = context.Products.ToList(),
            //    Categories = context.Categories.ToList()
            //};
            //return viewModel;

            //return context.Products
            //.Include(t => t.Categories)
            //.Select(p => new Product(p))
            //.ToListAsync();


            var products = from category in context.Categories
                           where category.ID == id
                           from product in category.Products
                           select product;
            //  var products = context.Products.Where(p => p.CategoryID == id).ToList();

            return products;
          
        }

        public Product GetProductDetails(int id)
        {
            var result = context.Products.Find(id);
            return result;
        }

        public void UpdateProduct(int id, Product product)
        {
          
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();
        }
        public string SaveProductImage(IFormFile imageFile, Product product)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string imagePath = Path.Combine("wwwroot/images", uniqueFileName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            product.ImagePath = imagePath.Remove(0, 7);
            context.SaveChanges();

            return imagePath;
        }
    }
}
