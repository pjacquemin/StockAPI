
namespace StockAPI.Data.EntityFramework
{
    public static class DbInitializer
    {
        public static void Initialize(StockContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
