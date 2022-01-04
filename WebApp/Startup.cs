using BLL;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IOrder_DishesManager, Order_DishesManager>();
            services.AddScoped<IOrder_DishesDB, Order_DishesDB>();

            services.AddScoped<IEmployeesManager, EmployeesManager>();
            services.AddScoped<IEmployeesDB, EmployeesDB>();
            
            services.AddScoped<ICustomersManager, CustomersManager>();
            services.AddScoped<ICustomersDB, CustomersDB>();

            services.AddScoped<IOrdersManager, OrdersManager>();
            services.AddScoped<IOrdersDB, OrdersDB>();

            services.AddScoped<ICategoryDishesManager, CategoryDishesManager>();
            services.AddScoped<ICategoryDishesDB, CategoryDishesDB>();

            services.AddScoped<ICategoryRestaurantsManager, CategoryRestaurantsManager>();
            services.AddScoped<ICategoryRestaurantsDB, CategoryRestaurantsDB>();

            services.AddScoped<IDistrictsManager, DistrictsManager>();
            services.AddScoped<IDistrictsDB, DistrictsDB>();

            services.AddScoped<IVillagesManager, VillagesManager>();
            services.AddScoped<IVillagesDB, VillagesDB>();

            services.AddScoped<IRestaurantsManager, RestaurantsManager>();
            services.AddScoped<IRestaurantsDB, RestaurantsDB>();

            services.AddScoped<IDishesManager, DishesManager>();
            services.AddScoped<IDishesDB, DishesDB>();

            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Restaurant}/{action=Index}/{id?}");
            });
        }
    }
}
