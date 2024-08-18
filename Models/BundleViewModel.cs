namespace eCommerceApplication.Models
{
    public class BundleViewModel
    {
        public int BundleId { get; set; }
        public string BundleName { get; set; }
        public List<Product> AvailableProducts { get; set; }
        public List<int> SelectedProductIds { get; set; }
        //public string BundleName { get; set; }
        //public List<Product> AvailableProducts { get; set; }
        //public List<int> SelectedProductIds { get; set; }
    }
}
