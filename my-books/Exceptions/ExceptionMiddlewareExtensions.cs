using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using my_books.Data.ViewModels;
using System.Net;

namespace my_books.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {

        public static void configureBuildInExceptionHandler (this IApplicationBuilder app,ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler (appError=>
                {
                    appError.Run(async context =>
                    {
                        var logger = loggerFactory.CreateLogger("configureBuildInExceptionHandler");
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                         context.Response.ContentType="application/json";
                        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                        var contextRequest = context.Features.Get<IHttpRequestFeature>();

                        if(context!=null)
                        {

                            var logg=new ErrorVM()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = contextFeature.Error.Message,
                                Path = contextRequest.Path
                            }.ToString();

                            logger.LogError(logg);
                            await context.Response.WriteAsync(new ErrorVM()
                            {
                                StatusCode=context.Response.StatusCode,
                                Message=contextFeature.Error.Message,
                                Path=contextRequest.Path
                            }.ToString());
                        }
                    });
            });
        }

        public static void ConfigureCustomeExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
