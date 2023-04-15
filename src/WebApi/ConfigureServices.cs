using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.OpenApi.Models;
using WebApi.Common.Errors;

namespace WebApi;

public static class ConfigureServices
{
    public static IServiceCollection ConfigureWebApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwagger();
        services.AddSingleton<ProblemDetailsFactory, DrivebackProblemDetailsFactory>();
        
        return services;
    }
    
    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        var info = new OpenApiInfo { Title = "ProtoArq", Version = "v1" };

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(info.Version, info);

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Scheme = "ApiKeyScheme"
            });

            var key = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                In = ParameterLocation.Header
            };
            var requirement = new OpenApiSecurityRequirement
            {
                { key, new List<string>() }
            };
            c.AddSecurityRequirement(requirement);
        });

        return services;
    }
}