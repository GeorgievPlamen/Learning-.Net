var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

Dictionary<int, string> countries = new Dictionary<int, string>
{
    {1,"United States" },
    {2,"Canada" },
    {3,"United Kingdom" },
    {4,"India" },
    {5,"Japan" }
};

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/countries", async context =>
    {
        string allCountries = "";

        foreach (var country in countries)
        {
            allCountries += country + "\n";
        }

        await context.Response.WriteAsync(allCountries);
    });
    endpoints.MapGet("/countries/{countryID:int}", async context =>
    {
        int countryId = Convert.ToInt32(context.Request.RouteValues["countryID"]);
        string responseBody = "";

        if (countries.ContainsKey(countryId))
        {
            context.Response.StatusCode = 200;
            responseBody = countries[countryId];
        }
        else if(countryId > 100 || countryId < 1)
        {
            context.Response.StatusCode = 400;
            responseBody = "The CountryID should be between 1 and 100";
        }
        else
        {
            context.Response.StatusCode = 404;
            responseBody = "No Country";
        }

        await context.Response.WriteAsync(responseBody);
    });
});

app.Run();

