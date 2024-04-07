
using Microsoft.EntityFrameworkCore;
using Sukuna.DataAccess.Data;
using Sukuna.DataAccess;
using Sukuna.Service.Services;
using Sukuna.Business.Interfaces;

namespace Sukuna.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddTransient<Seed>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddScoped<IClientService, ClientService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ITvaTypeService, TvaTypeService>();
            builder.Services.AddScoped<IOrderLineService, OrderLineService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<DataContext>();
            builder.Services.AddDbContext<DataContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                option.EnableSensitiveDataLogging();
            });

            var app = builder.Build();

            if (args.Length == 1 && args[0].ToLower() == "seeddata")
                SeedData(app);

            void SeedData(IHost app)
            {
                var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

                using (var scope = scopedFactory.CreateScope())
                {
                    var service = scope.ServiceProvider.GetService<Seed>();
                    service.SeedDataContext();
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}