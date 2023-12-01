using Microsoft.EntityFrameworkCore;
using StockAPI.Data.EntityFramework;
using StockAPI.Data.EntityFramework.Repositories;
using StockAPI.Interfaces;

namespace StockAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<StockContext>(options =>
                options.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=StockManagement;Trusted_Connection=True;"));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IStockRepository, StockRepository>();

            services.AddControllersWithViews();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
        }
    }
}
