namespace eCommerceApplication.Models
{
    public class ProductCategoryViewModel
    {
        public List<int>? SelectedCategories { get; set; }
        public List<Category>? Categories { get; set; }
        public List<Product>? Products { get; set; }
    }
}
