var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext context) =>
{
    context.Response.Headers["Content-Type"] = "text/html";
    if(context.Request.Method == "GET")
    {
        if (context.Request.Query.ContainsKey("operation"))
        {
            string op = context.Request.Query["operation"];
            bool correctFirst = Int32.TryParse(context.Request.Query["firstNumber"], out int firstNumber);
            bool correctSecond = Int32.TryParse(context.Request.Query["secondNumber"], out int secondNumber);
            bool correctOp = false;
            if (op == "add" || op == "subtract" || op == "multiply" || op == "divide" || op == "percent")
            {
                correctOp = true;
            }

            if (!correctFirst)
            {
                await context.Response.WriteAsync("Invalid input for 'firstNumber'");

            }

            if (!correctSecond)
            {
                await context.Response.WriteAsync("Invalid input for 'secondNumber'");
            }

            if (!correctOp)
            {
                await context.Response.WriteAsync("Invalid input for 'operation'");
            }

            if (correctFirst && correctSecond && correctOp)
            {
                switch (op)
                {
                    case "add":
                        {
                            await context.Response.WriteAsync($"{firstNumber + secondNumber}");
                            break;
                        }
                    case "subtract":
                        {
                            await context.Response.WriteAsync($"{firstNumber - secondNumber}");
                            break;
                        }
                    case "multiply":
                        {
                            await context.Response.WriteAsync($"{firstNumber * secondNumber}");
                            break;
                        }
                    case "divide":
                        {
                            await context.Response.WriteAsync($"{firstNumber / secondNumber}");
                            break;
                        }
                    case "percent":
                        {
                            await context.Response.WriteAsync($"{firstNumber % secondNumber}");
                            break;
                        }
                }
            }
           
        }
    }
});

app.Run();


