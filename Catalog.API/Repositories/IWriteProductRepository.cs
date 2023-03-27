using Catalog.API.Entities;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface IWriteProductRepository
    {
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
    }
}
