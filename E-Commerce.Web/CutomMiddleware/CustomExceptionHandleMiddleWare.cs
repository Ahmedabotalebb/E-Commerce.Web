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

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Somethin Went Wrong");

                httpcontext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                httpcontext.Response.ContentType = "application/json";


                var response = new ErrorToReturn()
                {
                    SatusCode = StatusCodes.Status500InternalServerError,
                    ErrorMessage = ex.Message

                };

                //var RespponseToReturn = JsonSerializer.Serialize(response);
                //await httpcontext.Response.WriteAsync(RespponseToReturn)
                //options 
                await httpcontext.Response.WriteAsJsonAsync(response);

            }
        }
    }
}