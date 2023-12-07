using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IStockRepository StockRepository { get; }
        IProductRepository ProductRepository { get; }
    }
}
