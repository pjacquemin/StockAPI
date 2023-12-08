
namespace StockAPI.Data.EntityFramework
{
    public static class DbInitializer
    {
        public static void Initialize(StockDbContext context)
        {
            context.Database.EnsureCreated();
            context.SaveChanges();
        }
    }
}
