
using System.Data;
using DomainLayer.Contracts;
using E_Commerce.Web.CutomMiddleware;
using E_Commerce.Web.CutomMiddleware;
using E_Commerce.Web.Extention;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Repository;
using Service;
using ServiceApstraction;
using Shared.ErrorModel;

namespace E_Commerce.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddSwaggerServices();

        builder.Services.AddInfrastructureServices(builder.Configuration);
        
        builder.Services.AddApplicationServices(); ///custom


        builder.Services.AddWebApplicationServices();

        var app = builder.Build();



        await app.SeedDataBaseAsync();

        // Configure the HTTP request pipeline
        #region Custom middelware

        app.UseCustomExceptionMiddelWare();

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
