using System.Reflection;
using API.Api.Base.Behaviors;
using API.Shared.Caching;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace API;

internal static class ProgramExtensions
{
    public static void AddMediaR(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.RegisterServicesFromAssemblyContaining<ApplicationAssemblyResolver>();
            
            config.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
        });
        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
    }
    
    internal static void MemoryCache(this WebApplicationBuilder builder)
    {
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<ICacheService, CacheService>();
    }
}