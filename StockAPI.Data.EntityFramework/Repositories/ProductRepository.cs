using Microsoft.EntityFrameworkCore;
using StockAPI.Interfaces;
using StockAPI.Models;

namespace StockAPI.Data.EntityFramework.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StockDbContext _context;

        public ProductRepository(StockDbContext context) 
        {
            _context = context;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstAsync(p => p.Id == id);
        }
    }
}
