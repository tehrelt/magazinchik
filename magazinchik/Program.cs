using System.Net;
using magazinchik.Configs;
using magazinchik.DAL;
using magazinchik.Repositories.Interfaces;
using magazinchik.Repositories.MySQL;
using magazinchik.Services;
using magazinchik.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SneakersShopContext>(
    options => options.UseMySQL(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

builder.Services.AddOptions();
builder.Services.Configure<MinioConfig>(builder.Configuration.GetSection("MinIO"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IManufacturerRepository, ManufacturerMySql>();

builder.Services.AddTransient<IManufacturerService, ManufacturerService>();

var app = builder.Build();

app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();