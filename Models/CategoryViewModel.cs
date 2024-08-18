namespace eCommerceApplication.Models
{
    public class CategoryViewModel
    {

       
        public List<Category> Categories { get; set; }

        public List<Product> AvailableProducts { get; set; }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public List<int> SelectedProductIds { get; set; }
    }

}
