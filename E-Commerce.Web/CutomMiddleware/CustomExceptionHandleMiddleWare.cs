using DomainLayer.Exceptions;
using E_Commerce.Web.ErrorModel;

namespace E_Commerce.Web.CutomMiddleware
{
    public class CustomExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CustomExceptionHandlerMiddleWare> logger;

        public CustomExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<CustomExceptionHandlerMiddleWare> logger)
        {
            next = Next;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpcontext)
        {
            try
            {
                await next.Invoke(httpcontext);

                await HandleNotFoundEndPointException(httpcontext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Somethin Went Wrong");

                ErrorToReturn response = HandleExceptionAsync(httpcontext, ex);

                //var RespponseToReturn = JsonSerializer.Serialize(response);
                //await httpcontext.Response.WriteAsync(RespponseToReturn)
                //options 
                await httpcontext.Response.WriteAsJsonAsync(response);

            }
        }

        private static ErrorToReturn HandleExceptionAsync(HttpContext httpcontext, Exception ex)
        {
            var Response = new ErrorToReturn()
            {
                ErrorMessage = ex.Message,
            };
            Response.SatusCode = ex switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                BadRequestException badRequestException=>GetBadRequestErrors(badRequestException, Response),
                _ => StatusCodes.Status500InternalServerError

            };

            var response = new ErrorToReturn()
            {
                SatusCode = httpcontext.Response.StatusCode,
                ErrorMessage = ex.Message

            };
            return response;
        }

        private static int GetBadRequestErrors(BadRequestException badRequestException, ErrorToReturn Response)
        {
            Response.Errors=badRequestException.Errors;
            return StatusCodes.Status400BadRequest;
        }

        private static async Task HandleNotFoundEndPointException(HttpContext httpcontext)
        {
            if (httpcontext.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Response = new ErrorToReturn()
                {
                    SatusCode = StatusCodes.Status404NotFound,
                    ErrorMessage = $"End Point {httpcontext.Request.Path} Is not found"
                };
                await httpcontext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}