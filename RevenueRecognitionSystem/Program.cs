using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RevenueRecognitionSystem.Features.Clients.Repositories;
using RevenueRecognitionSystem.Features.Clients.Services;
using RevenueRecognitionSystem.Features.Contracts.Repositories;
using RevenueRecognitionSystem.Features.Contracts.Services;
using RevenueRecognitionSystem.Features.Discounts.Repositories;
using RevenueRecognitionSystem.Features.Payments.Repositories;
using RevenueRecognitionSystem.Features.Payments.Services;
using RevenueRecognitionSystem.Features.Revenue.Services;
using RevenueRecognitionSystem.Infrastructure.DAL;
using RevenueRecognitionSystem.Middlewares;
using RevenueRecognitionSystem.Modules.Software.Repositories;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RevenueRecognitionSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<RevenueRecognitionDbContext>(opt =>
        {
            opt.UseSqlServer(connectionString);
        });
        
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IClientService, ClientService>();

        builder.Services.AddScoped<ILicenseRepository, LicenseRepository>();
        builder.Services.AddScoped<ILicenseService, LicenseService>();
        
        builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

        builder.Services.AddScoped<ISoftwareRepository, SoftwareRepository>();


        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Event API",
                Version = "v1",
                Description = "API for Revenue Recognition System",
                Contact = new OpenApiContact
                {
                    Name = "API Support",
                    Email = "support@example.com",
                    Url = new Uri("https://example.com/support")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            });
        });
        

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseGlobalExceptionHandling(); 

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Revenue Recognition API v1");
                c.DocExpansion(DocExpansion.List);
                c.DefaultModelsExpandDepth(0);
                c.DisplayRequestDuration();
                c.EnableFilter();
            });
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.MapControllers();
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.Run();
    }
}