using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Schellex.Crypto.DustBucket.BusinessLogic.Managers;
using Schellex.Crypto.DustBucket.Interfaces;

namespace Schellex.Crypto.DustBucket.BusinessLogic.Extensions;

/// <summary>
/// Provides extension methods to register DustBucket services with the dependency injection container.
/// </summary>
public static class DependencyInjectionExtensions
{

    /// <summary>
    /// Registers DustBucket-related services and dependencies for use in the application.
    /// </summary>
    /// <param name="services">The service collection to add registrations to.</param>
    /// <param name="configuration">The application configuration (not used currently).</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddDustBucketServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDustBucketManager, DustBucketManager>();
        return services;
    }
}
