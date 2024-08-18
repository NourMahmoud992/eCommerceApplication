using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceApplication.Models
{
    public class ParentCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [NotMapped]
        public IFormFile? Image { get; set; }
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();


    }
}
