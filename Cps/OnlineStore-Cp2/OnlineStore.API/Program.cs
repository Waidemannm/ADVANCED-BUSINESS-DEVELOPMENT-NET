using Microsoft.EntityFrameworkCore;
using OnlineInfrastructure.Persistence;
using OnlineStore.Application.Interfaces;
using OnlineStore.Application.Services;

namespace OnlineSore.Domain;

/// <summary>
/// Ponto de entrada da aplicação.
/// Configura e executa o pipeline da API ASP.NET Core.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddControllers();
        
        
        builder.Services.AddScoped<IRatingProductService, RatingProductService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<ICostumerService, CostumerService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IAddressService, AddressService>();

        builder.Services.AddDbContext<OnlineStoreContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("OnlineStoreContextOracle");
            
            
            options.UseOracle(connectionString);
            
        });

        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi(); 
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}