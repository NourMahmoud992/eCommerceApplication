using eCommerceApplication.Models;

namespace eCommerceApplication.Services
{
    public interface IParentCategoriesRepo
    {
        public IEnumerable<ParentCategory> GetAllParentCategories();
        public ParentCategory GetParentGategoriesDetails(int id);
        public void InsertParentCategory(ParentCategory parentcategory);
        public void UpdateParentGategory(int id, ParentCategory parentcategory);
        public void DeleteParentCategory(int id);
    }
}
