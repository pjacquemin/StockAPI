using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAPI.Logic.StockService
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;

    }

    public static class ProductExtensions
    {
        public static Product? ToLogic(this Models.Product product)
        {
            if (product == null)
                return null;

            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
            };
        }
    }
}
