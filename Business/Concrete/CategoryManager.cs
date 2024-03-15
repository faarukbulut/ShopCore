using Business.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly CategoryManager _categoryManager;

        public CategoryManager(CategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public void Create(Category entity)
        {
            _categoryManager.Create(entity);
        }

        public void Delete(Category entity)
        {
            _categoryManager.Delete(entity);
        }

        public List<Category> GetAll()
        {
            return _categoryManager.GetAll().ToList();
        }

        public Category GetByID(int id)
        {
            return _categoryManager.GetByID(id);
        }

        public void Update(Category entity)
        {
            _categoryManager.Update(entity);
        }
    }
}
