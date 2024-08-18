using eCommerceApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        [DbFunction("dbo", "GetAllProducts")]
        public virtual IQueryable<Product> GetAllProducts()
        {
            throw new InvalidOperationException();
        }
        public DbSet<Product>? Products { get; set; }
        public DbSet<ParentCategory>? ParentCategories { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Bundle> Bundles {get; set; }

    }
}