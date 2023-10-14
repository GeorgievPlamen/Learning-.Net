using Microsoft.Extensions.DependencyInjection;
using Services;
using ServicesContracts;
using ServicesContracts.DTO;
using StocksApp_xUnit.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<FinnhubOptions>(builder.Configuration.GetSection("FinnhubApi"));
builder.Services.AddScoped<IStocksService, StockService>();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddHttpClient();
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
