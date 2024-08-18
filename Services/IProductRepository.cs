using eCommerceApplication.Models;

namespace eCommerceApplication.Services
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProducts(int id);
        public Product GetProductDetails(int id);
        public void CreateProduct(Product product);
        public void UpdateProduct(int id,Product product);
        public void DeleteProduct(int id);
        public string SaveProductImage(IFormFile imageFile, Product product);
    }
}
