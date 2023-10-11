using Microsoft.Extensions.DependencyInjection;
using StocksApp_xUnit.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<FinnhubOptions>(builder.Configuration.GetSection("FinnhubApi"));
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
