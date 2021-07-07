using BibliotecaCQRS.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Identity;
using NetDevPack.Identity.Jwt;

namespace BibliotecaCQRS.Infra.CrossCutting.Identity
{
    public static class ApiIdentityConfig
    {
        public static void AddApiIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {        
            services.AddEntityFrameworkNpgsql()
            .AddDbContext<BibliotecaContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                  b => b.MigrationsAssembly("BibliotecaCQRS.Infra.CrossCutting.Identity")));

            services.AddIdentityConfiguration();

            services.AddJwtConfiguration(configuration, "AppSettings");
        }
    }
}
