using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD.Filters.ExceptionFilters
{
    public class HandleExceptionFilter : IExceptionFilter
    {
        private readonly Logger<HandleExceptionFilter> _logger;
        private readonly IHostEnvironment _hostEnvironment;
        public HandleExceptionFilter(Logger<HandleExceptionFilter> logger, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("Exception filter {FilterName}.{MethodName}\n{ExceptionType}\n{ExceptionMessage}",
                nameof(HandleExceptionFilter),
                nameof(OnException),
                context.Exception.GetType().ToString(),
                context.Exception.Message);

            if (_hostEnvironment.IsDevelopment())
            {
                context.Result = new ContentResult()
                {
                    Content = context.Exception.Message,
                    StatusCode = 500
                };
            }
        }
    }
}
