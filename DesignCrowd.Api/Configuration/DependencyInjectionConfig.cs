using DesignCrowd.Business.Interfaces;
using DesignCrowd.Business.Services;
using DesignCrowd.Data.Factory;

namespace DesignCrowd.Api.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IBusinessDayCounterService, BusinessDayCounterService>();  
        services.AddScoped<IPublicHolidayFactory, PublicHolidayFactory>();
    }
}