using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Schellex.Crypto.DustBucket.BusinessLogic.Managers;
using Schellex.Crypto.DustBucket.Interfaces;

namespace Schellex.Crypto.DustBucket.BusinessLogic.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddDustBucketServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDustBucketManager, DustBucketManager>();

        return services;
    }
}
