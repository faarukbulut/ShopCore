using System.Linq.Expressions;

namespace DataAccess.Repositories.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        T GetByID(int id);
        T GetOne(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
