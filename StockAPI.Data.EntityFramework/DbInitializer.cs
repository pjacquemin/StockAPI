using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
