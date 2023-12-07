namespace StockAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public virtual List<ProductStock> ProductStocks { get; set; } = new();
    }
}