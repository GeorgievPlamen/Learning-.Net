using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;

namespace IntroToMiddleware.CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            bool email = false;
            bool password = false;

            StreamReader reader = new StreamReader(httpContext.Request.Body);
            string body = await reader.ReadToEndAsync();

            Dictionary<string, StringValues> requestDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);

            if (httpContext.Request.Method == "GET")
            {
                httpContext.Response.StatusCode = 200;
                return;
            }

            if (requestDict.ContainsKey("email"))
            {
                string emailS = requestDict["email"][0];
                if (emailS == "admin@example.com")
                {
                    email = true;
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("Invalid input for 'email'");
                }
            }
            else
            {
                httpContext.Response.StatusCode = 400;

                await httpContext.Response.WriteAsync("Invalid input for 'email'");
            }

            if (requestDict.ContainsKey("password"))
            {
                if (requestDict["password"][0] == "admin1234")
                {
                    password = true;
                }
                else
                {
                    httpContext.Response.StatusCode = 400;
                    await httpContext.Response.WriteAsync("Invalid input for 'password'");
                }
            }
            else
            {
                httpContext.Response.StatusCode = 400;
                await httpContext.Response.WriteAsync("Invalid input for 'password'");
            }

            if(email && password)
            {
                httpContext.Response.StatusCode = 200;
                await httpContext.Response.WriteAsync("Successful login");
            }
            else
            {
                await httpContext.Response.WriteAsync("Invalid login");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
