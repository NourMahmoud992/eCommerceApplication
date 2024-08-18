using eCommerceApplication.Models;
using System.Drawing;

namespace eCommerceApplication.Services
{
    public interface IHomeRepo
    {
       public  List<Product> SearchProducts(string searchitem);
       public ProductCategoryViewModel GetProducts(ProductCategoryViewModel? viewModel);
        public List<Category> GetCategories();
    }
}
