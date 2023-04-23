using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Vecfleet.Application.Di;
/// <summary>
/// Metodo de extension en la que se injecta los casos de usos y servicios necesarios.
/// </summary>
public static class AddApplicationDi
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(configuration => 
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }


}