using Microsoft.Extensions.Hosting;

namespace eCommerceApplication.Models
{
    public class CategoryProduct
    {
        public int ProductsID { get; set; }
        public Product Product { get; set; }

        public int CategoriesID { get; set; }
        public Category Category { get; set; }


    }
}
