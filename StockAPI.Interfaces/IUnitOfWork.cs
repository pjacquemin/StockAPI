
namespace StockAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IStockRepository StockRepository { get; }
        IProductRepository ProductRepository { get; }
        Task SaveChangesAsync();
    }
}
