using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD.Filters.ActionFilters
{
    public class ResponseHeaderFilterFactoryClassAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => false;
        public string? Key { get; set; }
        public string? Value { get; set; }
        public int Order { get; set; }

        public ResponseHeaderFilterFactoryClassAttribute(string key, string value, int order)
        {
            Key = key;
            Value = value;
            Order = order;
        }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
           var filter = new ResponseHeaderActionFilter(Key, Value, Order);

           return filter;
        }
    }

    public class ResponseHeaderActionFilter : IAsyncActionFilter, IOrderedFilter
    {
        private readonly string _key;
        private readonly string _value;

        public ResponseHeaderActionFilter(string key, string value, int order)
        {
            _key = key;
            _value = value;
            Order = order;
        }

        public int Order { get; set; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.Response.Headers[_key] = _value;
            await next();
        }
    }
}
