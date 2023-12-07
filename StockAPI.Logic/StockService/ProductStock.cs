using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAPI.Logic.StockService
{
    public class ProductStock
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public Stock Stock { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public int Quantity { get; set; }

    }

    public static class ProductStockExtensions
    {
        public static ProductStock? ToLogic(this Models.ProductStock productStock)
        {
            if (productStock == null)
                return null;

            return new ProductStock
            {
                StockId = productStock.StockId,
                ProductId = productStock.ProductId,
                Quantity = productStock.Quantity,
            };
        }
        public static List<ProductStock?> ToLogic(this IEnumerable<Models.ProductStock> productStocks)
        {
            return productStocks.Select(e => e.ToLogic()).ToList();
        }
    }
}
