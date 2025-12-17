using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.DTO;
using UserService.Models;
using UserService.Repository;
using UserService.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IService<UserReceive, UserSend>, UserAppService>();


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
