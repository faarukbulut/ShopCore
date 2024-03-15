using Business.Abstract;
using DataAccess.Repositories.Concrete;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductManager(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Create(Product entity)
        {
            _productRepository.Create(entity);
        }

        public void Delete(Product entity)
        {
            _productRepository.Delete(entity);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product GetByID(int id)
        {
            return _productRepository.GetByID(id);
        }

        public void Update(Product entity)
        {
            _productRepository.Update(entity);
        }
    }
}
