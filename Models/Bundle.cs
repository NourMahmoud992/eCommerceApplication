using System.ComponentModel.DataAnnotations;

namespace eCommerceApplication.Models
{
    public class Bundle
    {
        
        
        public int ID { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Discount { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();

    }
}
