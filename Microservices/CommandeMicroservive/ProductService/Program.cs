using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.DTO;
using ProductService.Models;
using ProductService.Repository;
using ProductService.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
builder.Services.AddScoped<IService<ProductReceive, ProductSend>, ProductAppService>();


string connectionString = builder.Configuration.GetConnectionString("default");
builder.Services.AddDbContext<AppDbContext>(option =>
    option.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();