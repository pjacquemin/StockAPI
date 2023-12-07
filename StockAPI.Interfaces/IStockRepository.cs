using StockAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAPI.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock> GetStockById(int id);
    }
}
