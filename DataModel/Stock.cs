using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Place { get; set; } = null!;
        public virtual List<ProductStock> ProductStocks { get; set; } = new();
    }
}
