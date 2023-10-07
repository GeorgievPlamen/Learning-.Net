using _12.StocksApp;
using _12.StocksApp.ServiceContracts;
using _12.StocksApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));
builder.Services.AddHttpClient();
builder.Services.AddScoped<IFinnhubService, FinnhubCompanyProfileService>();
var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
