using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceApplication.Models
{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CategoryID { get; set; } 
        [NotMapped]
        public IFormFile? Image { get; set; }

        public string? ImagePath { get; set; }

        public double Price { get; set; }
        public DateTime? CreatedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Required]
        public string? SKU { get; set; }
        public virtual Bundle? Bundles { get; set; }
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();



    }
}
