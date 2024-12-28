using System.Reflection;
using API.Api.Base.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace API.Api.Base.Extensions;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var endpointServiceDescriptors = Assembly.GetExecutingAssembly().DefinedTypes
            .Where(type =>
                type is { IsAbstract: false, IsInterface: false }
                && type.IsAssignableTo(typeof(IEndpointBuilder)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpointBuilder), type))
            .ToArray();

        services.TryAddEnumerable(endpointServiceDescriptors);

        return services;
    }

    public static IApplicationBuilder MapEndpoints(this WebApplication application)
    {
        var endpoints = application.Services.GetRequiredService<IEnumerable<IEndpointBuilder>>();
        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoints(application);
        }

        return application;
    }
}