﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD.Filters.AuthorizationFilters
{
    public class TokenAuthorizationFilter : IAuthorizationFilter
    {
        private readonly Logger<TokenAuthorizationFilter> _logger;

        public TokenAuthorizationFilter(Logger<TokenAuthorizationFilter> logger)
        {
            _logger = logger;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Cookies.ContainsKey("Auth-Key") == false)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return;
            }

            if (context.HttpContext.Request.Cookies["Auth-Key"] != "A100")
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
        }
    }
}
