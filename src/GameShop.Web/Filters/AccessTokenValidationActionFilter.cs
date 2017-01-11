using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameShop.Web.Filters
{
    public class AccessTokenValidationActionFilter : IAsyncActionFilter
    {
        public AccessTokenValidationActionFilter()
        {
            
        }
        
        async Task IAsyncActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(context.HttpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.HttpContext.Response.Redirect("/signin");
            }

            var token = context.HttpContext.GetAccessToken();

            if(token == null)
            {
                //context.HttpContext.
            }
        }
    }
}