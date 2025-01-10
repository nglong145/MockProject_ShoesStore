using Microsoft.EntityFrameworkCore;
using ShoesStoreApp.BLL.Services.Base;
using ShoesStoreApp.BLL.Services.BlogService;
using ShoesStoreApp.BLL.Services.BrandService;
using ShoesStoreApp.BLL.Services.SizeService;
using ShoesStoreApp.DAL.Data;
using ShoesStoreApp.DAL.Infrastructure;
using ShoesStoreApp.DAL.Models;
using ShoesStoreApp.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ShoesStoreAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ISizeSrevice, SizeService>();

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
