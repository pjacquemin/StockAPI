using StockAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAPI.Data.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StockContext _context;
        public IStockRepository StockRepository { get; }
        public IProductRepository ProductRepository { get; }
        public UnitOfWork(StockContext context, IStockRepository stockRepository, IProductRepository productRepository) 
        {
            _context = context;
            StockRepository = stockRepository;
            ProductRepository = productRepository;
        }
    }
}
