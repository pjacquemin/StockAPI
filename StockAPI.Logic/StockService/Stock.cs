
namespace StockAPI.Logic.StockService
{
    public class Stock
    {
        public int Id { get; set; }
        public string Place { get; set; } = null!;
        public virtual List<ProductStock?> ProductStocks { get; set; } = new();

    }

    public static class StockExtensions
    {
        public static Stock? ToLogic(this Models.Stock stock)
        {
            if (stock == null)
                return null;

            return new Stock
            {
                Id = stock.Id,
                Place = stock.Place,
                ProductStocks = stock.ProductStocks.ToLogic(),
            };
        }
    }
}
