using DomainLayer.Contracts;
using E_Commerce.Web.CutomMiddleware;

namespace E_Commerce.Web.Extention
{
    public static class WebAppliactionRegisteration
    {
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var scoope = app.Services.CreateScope();
            var ObjectOfDataseeding = scoope.ServiceProvider.GetRequiredService<IDataseeding>();
            await ObjectOfDataseeding.DataSeedAsync();
        }

        public static IApplicationBuilder UseCustomExceptionMiddelWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }


        }
    }
