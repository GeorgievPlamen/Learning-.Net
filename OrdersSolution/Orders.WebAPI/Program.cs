using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Orders.WebAPI.AppDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

//Swagger
var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHsts();
app.UseHttpsRedirection();


app.UseAuthorization();

app.MapControllers();

app.Run();
