using Microsoft.EntityFrameworkCore;
using StockAPI.Interfaces;
using StockAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAPI.Data.EntityFramework.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StockContext _context;

        public ProductRepository(StockContext context) 
        {
            _context = context;
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstAsync(p => p.Id == id);
        }
    }
}
