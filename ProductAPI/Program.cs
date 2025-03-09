using DAL.Models;
using Microsoft.EntityFrameworkCore;
using BAL.Interfaces;
using BAL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using Common.AppSetings;
using ProductAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AppSettings>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IValidationUtility, ValidationUtility>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<ProductContext>((serviceProvider, options) =>
{
    var appSettings = serviceProvider.GetRequiredService<AppSettings>();
    options.UseSqlServer(appSettings.DatabaseConnectionString);
});

var configuration = builder.Configuration;
builder.Services.AddSingleton<IUniqueIdGenerator>(sp =>
{
    var appSettings = sp.GetRequiredService<AppSettings>();
    return new UniqueIdGenerator(Convert.ToInt32(appSettings.InstanceId));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
