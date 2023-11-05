using ServiceContracts;
using Services;
using Microsoft.EntityFrameworkCore;
using Entities;
using RepositoryContracts;
using Repositories;
using Serilog;
using CRUD.Filters.ActionFilters;

var builder = WebApplication.CreateBuilder(args);

/*//Logging
builder.Host.ConfigureLogging(loggingProvider =>
{
    loggingProvider.ClearProviders();
    loggingProvider.AddConsole();
    loggingProvider.AddDebug();
    loggingProvider.AddEventLog();
});*/

//Serilog
builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration loggerConfiguration) =>
{
    loggerConfiguration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services);
});

builder.Services.AddControllersWithViews(options =>
{
    //options.Filters.Add<ResponseHeaderActionFilter>();
    var logger = builder.Services.BuildServiceProvider().GetRequiredService<ILogger<ResponseHeaderActionFilter>>();
    options.Filters.Add(new ResponseHeaderActionFilter(logger, "my-key-from-global", "my-value-from-global", 2));
});
builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IPersonsRepository, PersonsRepository>();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PersonsDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields =
    Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties | Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.ResponsePropertiesAndHeaders;
});

var app = builder.Build();


if(builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
 
if(builder.Environment.IsEnvironment("Test") == false)
{
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");
}

/*app.Logger.LogDebug("debugging");
app.Logger.LogInformation("Info-msg");*/

app.UseHttpLogging();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

public partial class Program { } //make the auto-generated Program class be available to us
