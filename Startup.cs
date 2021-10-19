using eTicket_Demo.Data;
using eTicket_Demo.Data.Cart;
using eTicket_Demo.Data.Interfaces;
using eTicket_Demo.Data.Services;
using eTicket_Demo.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTicket_Demo
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
            //DbContext Configuration
            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("eTicketConnection")));
            services.AddTransient<AppDbContext>();

            //Identity Configuration
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //Services configurations
            services.AddScoped<IMovieServices, MovieServices>();
            services.AddScoped<IActorServices, ActorServices>();
            services.AddScoped<ICinemaServices, CinemaServices>();
            services.AddScoped<IProducerServices, ProducerServices>();
            services.AddScoped<IOrderServices, OrderServices>();
            services.AddScoped<IAppUserServices, AppUserServices>();
            services.AddTransient<EmailServices>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

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

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            AppDbInitializer.Seed(app);

            using(var scope = app.ApplicationServices.CreateScope())
            {
                AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                DataSeeder.AddSuperUser(dbContext);
            }
        }
    }
}
