using Microsoft.EntityFrameworkCore;
using Sukuna.DataAccess.Data;
using Sukuna.DataAccess;
using Sukuna.Service.Services;
using Sukuna.Business.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sukuna.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Seed data if necessary
            if (args.Length == 1 && args[0].ToLower() == "seeddata")
                SeedData(host);

            host.Run();
        }

        private static void SeedData(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seed = services.GetRequiredService<Seed>();
                seed.SeedDataContext();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        services.AddCors(options =>
                        {
                            options.AddPolicy("AllowLocalhost3000",
                                builder =>
                                {
                                    builder.WithOrigins("http://localhost:3000")
                                           .AllowAnyHeader()
                                           .AllowAnyMethod();
                                });
                        });

                        services.AddControllers();

                        services.AddTransient<Seed>();
                        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                        services.AddScoped<IArticleService, ArticleService>();
                        services.AddScoped<IClientService, ClientService>();
                        services.AddScoped<IUserService, UserService>();
                        services.AddScoped<ITvaTypeService, TvaTypeService>();
                        services.AddScoped<IOrderLineService, OrderLineService>();
                        services.AddScoped<ISupplierService, SupplierService>();
                        services.AddScoped<ISupplierOrderService, SupplierOrderService>();
                        services.AddScoped<IClientOrderService, ClientOrderService>();

                        services.AddScoped<DataContext>();
                        services.AddDbContext<DataContext>(option =>
                        {
                            option.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"));
                            option.EnableSensitiveDataLogging();
                        });

                        services.AddSwaggerGen();
                    })
                    .Configure(app =>
                    {
                        var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

                        if (env.IsDevelopment())
                        {
                            app.UseSwagger();
                            app.UseSwaggerUI();
                        }

                        app.UseHttpsRedirection();
                        app.UseRouting();
                        app.UseCors("AllowLocalhost3000");
                        app.UseAuthorization();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });
                });
    }
}
