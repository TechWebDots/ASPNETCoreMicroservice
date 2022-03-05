using DemoMicroservice;
using DemoMicroservice.Infrastructure;
using DemoMicroservice.Repository;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var apiSettings = new AppSettings();
ConfigurationBinder.Bind(builder.Configuration.GetSection("AppSettings"), apiSettings);
builder.Services.AddSingleton<IAppSettings>(apiSettings);
builder.Services.AddTransient<IProductRepository, ProductRepository>();
//builder.Services.AddMvc(config=> config.Filters.Add(typeof(SecurityHeadersAttribute)));
builder.Services.AddDbContext<ProductContext>(config => config.UseSqlServer(builder.Configuration.GetConnectionString("ProductDB")));

var app = builder.Build();

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
