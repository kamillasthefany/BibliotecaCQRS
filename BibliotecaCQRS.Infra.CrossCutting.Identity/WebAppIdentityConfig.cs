using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetDevPack.Identity;

namespace BibliotecaCQRS.Infra.CrossCutting.Identity
{
    public static class WebAppIdentityConfig
    {
        public static void AddWebAppIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {   
            services.AddIdentityEntityFrameworkContextConfiguration(options =>
              options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("BibliotecaCQRS.Infra.CrossCutting.Identity")));

            services.AddIdentityConfiguration();
        }
    }
}
