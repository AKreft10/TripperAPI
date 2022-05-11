using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripperAPI.Middleware.Exceptions;

namespace TripperAPI.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(Forbidden forbiddenException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbiddenException.Message);
            }
            catch(BadRequest badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch(NotFound notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch(Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong...");
            }

        }
    }
}
