using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vecfleet.Application.Interfaces;
using Vecfleet.Infrastructure.Persistence;

namespace Vecfleet.Infrastructure.Di;

/// <summary>
/// Metodo de extension en la que se injecta la persistencia
/// </summary>
public static class AddInfrastructureDi
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SimpleCrudDbContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), builder =>
                builder.MigrationsAssembly(typeof(SimpleCrudDbContext).Assembly.FullName)
                ));
        
        services.AddScoped<ISimpleCrudDbContext>(provider => provider.GetService<SimpleCrudDbContext>());
    }
}