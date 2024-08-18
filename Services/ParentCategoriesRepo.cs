using eCommerceApplication.Data;
using eCommerceApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApplication.Services
{

    public class ParentCategoriesRepo : IParentCategoriesRepo
    {
        private readonly ApplicationDbContext context;

        public ParentCategoriesRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void DeleteParentCategory(int id)
        {
            context.ParentCategories.Remove(context.ParentCategories.Find(id));
            context.SaveChanges();
        }

        public IEnumerable<ParentCategory> GetAllParentCategories()
        {
           return context.ParentCategories.ToList();

        }

        public ParentCategory GetParentGategoriesDetails(int id)
        {
            if (id == null || context.ParentCategories == null)
            {
                return null;
            }

            var parentCategory = context.ParentCategories.FirstOrDefault(m => m.Id == id);

            return parentCategory;
        }

        public void InsertParentCategory(ParentCategory parentcategory)
        {
            try
            {
                context.Add(parentcategory);
                context.SaveChangesAsync();
            }
            catch
            {
               
            }
        }

        public void UpdateParentGategory(int id,ParentCategory parentCategory)

        {

           
            context.Entry(parentCategory).State = EntityState.Modified;
            
            try
            {
                context.SaveChanges();
            }
            catch
            {
                throw;
            }
           
        }

    }
}
