using System;
using System.Data.Common;
using JkvoXyz.Config;
using JkvoXyz.Data;
using MySql.Data;

namespace JkvoXyz {
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = configBuilder.Build();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IDbShard>(provider =>
            {
#if DEBUG
                var dbConfig = new FullDbConfig();
                config.GetSection("Database").Bind(dbConfig);
                return new MySqlShard(
                    dbConfig.Host,
                    dbConfig.Database,
                    dbConfig.Username,
                    dbConfig.Password
                );
#else
                return new MySqlShard();
#endif
            });

            var coreDbConfig = new CoreDbConfig();
            config.GetSection("Database").Bind(coreDbConfig);
            builder.Services.AddSingleton<CoreDbConfig>(coreDbConfig);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}