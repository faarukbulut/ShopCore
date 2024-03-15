namespace Business.Abstract
{
    public interface IGenericService<T> where T : class
    {
        T GetByID(int id);
        List<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
