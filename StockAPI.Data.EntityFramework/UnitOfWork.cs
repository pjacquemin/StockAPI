using StockAPI.Interfaces;

namespace StockAPI.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StockDbContext _context;
        public IStockRepository StockRepository { get; }
        public IProductRepository ProductRepository { get; }
        public UnitOfWork(StockDbContext context, IStockRepository stockRepository, IProductRepository productRepository) 
        {
            _context = context;
            StockRepository = stockRepository;
            ProductRepository = productRepository;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
