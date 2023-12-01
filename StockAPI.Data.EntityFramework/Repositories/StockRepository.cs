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
    public class StockRepository : IStockRepository
    {
        private readonly StockContext _context;

        public StockRepository(StockContext context)
        {
            _context = context;
        }

        public async Task<Stock> GetStockById(int id)
        {
            return await _context.Stocks.FirstAsync(p => p.Id == id);
        }
    }
}
