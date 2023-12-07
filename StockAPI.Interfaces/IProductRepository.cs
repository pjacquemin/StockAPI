using StockAPI.Models;

namespace StockAPI.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);
    }
}
