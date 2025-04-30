using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace Persistence
{
   public static class InfrastructureServicesRegisteration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbcontext>(Options =>
            {
                Options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IDataseeding, Dataseed>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
