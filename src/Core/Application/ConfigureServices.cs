using System.Reflection;
using Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly));
        
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
        return services;
    }
}