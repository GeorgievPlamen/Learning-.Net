using _12.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<WeatherApiOptions>
    (builder.Configuration.GetSection("weatherApi"));
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("MyOwnConfig.json", optional: true, reloadOnChange: true);
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
/*app.UseEndpoints(endpoints =>
{
    endpoints.Map("/config", async context =>
    {
        await context.Response.WriteAsync(app.Configuration["MyKey"]);
    });

});*/
app.MapControllers();

app.Run();
