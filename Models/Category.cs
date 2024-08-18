using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceApplication.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int ParentCategoryId { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public virtual ParentCategory? ParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new HashSet<Product>();


    }
}
