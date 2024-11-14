using DesignCrowd.Business.Interfaces;
using DesignCrowd.Business.Services;
using DesignCrowd.Business.Factory;

namespace DesignCrowd.Api.Configuration;

/// <summary>
/// Static class to register services using dependency injection
/// </summary>
public static class DependencyInjectionConfig
{
    /// <summary>
    /// Registers services required by the API in the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IBusinessDayCounterService, BusinessDayCounterService>();  
        services.AddScoped<IPublicHolidayFactory, PublicHolidayFactory>();
    }
}