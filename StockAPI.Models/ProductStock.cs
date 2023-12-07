using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAPI.Models
{
    public class ProductStock
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public Stock Stock { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }
    }
}
