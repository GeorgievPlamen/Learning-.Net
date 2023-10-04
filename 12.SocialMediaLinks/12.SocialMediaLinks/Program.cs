using _12.SocialMediaLinks.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.Configure<SocialMediaLinksOptions>(builder.Configuration.GetSection("SocialMediaLinks"));
var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();
