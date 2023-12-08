using Microsoft.EntityFrameworkCore;
using StockAPI.Interfaces;
using StockAPI.Models;

namespace StockAPI.Data.EntityFramework.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly StockDbContext _context;

        public StockRepository(StockDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> GetStockById(int id)
        {
            return await _context.Stocks.FirstAsync(p => p.Id == id);
        }
    }
}
