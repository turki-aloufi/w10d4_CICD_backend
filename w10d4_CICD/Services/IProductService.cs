using w10d4_CICD.Models;

namespace w10d4_CICD.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
    }
}
