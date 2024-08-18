using eCommerceApplication.Data;
using eCommerceApplication.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Xml.Linq;

namespace eCommerceApplication.Services
{
    public class HomeRepo:IHomeRepo
    {
        public string connstr { get; set; }
        public IConfiguration _configuration;
        public SqlConnection conn;
        private ApplicationDbContext context;

        public HomeRepo(ApplicationDbContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            connstr = _configuration.GetConnectionString("DefaultConnection");
            this.context = context;
        }

        public List<Product> SearchProducts(string searchitem)
        {
            var Result = context.Products.Where(ww => ww.Name.Contains(searchitem)).ToList();
            var Result02 = context.Products.Where(ww => ww.Description.Contains(searchitem)).ToList();
            var finalResult = Result.Union(Result02).ToList();
            return finalResult;

        }

        public ProductCategoryViewModel GetProducts(ProductCategoryViewModel? viewModel)
        {

            try
            {


                if (viewModel.SelectedCategories == null)
                {
                    List<Product> products = new List<Product>();
                    using (conn = new SqlConnection(connstr))
                    {
                        conn.Open();
                        var cmd = new SqlCommand("GetAllProducts", conn);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            Product product = new Product();
                            product.ID = Convert.ToInt32(rdr["ID"]);
                            product.Name = rdr["Name"].ToString();
                            product.Description = rdr["Description"].ToString();
                            product.Price = Convert.ToInt32(rdr["Price"]);
                            products.Add(product);

                        }

                    }
                    viewModel.Products = products.ToList();
                   // viewModel.Products = context.GetAllProducts().ToList();
                }
                else
                {
                    viewModel.Products = context.Products
                   .Where(p => p.Categories.Any(c => viewModel.SelectedCategories.Contains(c.ID)))
                   .ToList();

                }
                viewModel.Categories = context.Categories.ToList();
                return viewModel;
            }
            catch
            {
                throw;
            }
        }
        public List<Category> GetCategories() 
        {
            return context.Categories.ToList();
        }

    }
}
