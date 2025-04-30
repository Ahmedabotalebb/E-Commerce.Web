
using System.Data;
using DomainLayer.Contracts;
using E_Commerce.Web.CutomMiddleware;
using E_Commerce.Web.CutomMiddleware;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repository;
using Service;
using ServiceApstraction;
using Shared.ErrorModel;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<StoreDbcontext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IDataseeding, Dataseed>();
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(Service.ProductService).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>();

            builder.Services.Configure<ApiBehaviorOptions>((Options) =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorsResponse;
          

                
            });

            var app = builder.Build();



            using var scoope = app.Services.CreateScope();
            var ObjectOfDataseeding = scoope.ServiceProvider.GetRequiredService<IDataseeding>();
             await ObjectOfDataseeding.DataSeedAsync();

            // Configure the HTTP request pipeline
            #region Custom middelware

            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();


            app.Use(async (RequesstContext, NextMiddleware) =>
            {
                Console.WriteLine("Request Under PRocessing");
                await NextMiddleware.Invoke();
                Console.WriteLine("Waiting Response");
                Console.WriteLine(RequesstContext.Response.Body);
            });

            #endregion

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
