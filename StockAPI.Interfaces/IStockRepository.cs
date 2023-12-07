using StockAPI.Models;

namespace StockAPI.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock> GetStockById(int id);
    }
}
