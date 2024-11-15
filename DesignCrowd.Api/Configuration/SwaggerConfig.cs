﻿using DesignCrowd.Api.Swagger;
using System.Reflection;

namespace DesignCrowd.Api.Configuration;

/// <summary>
/// Static class to add swagger configuration
/// </summary>
public static class SwaggerConfig
{
    /// <summary>
    /// Adds Swagger configuration services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection with added Swagger services.</returns>
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            // Configure Swagger documentation for API version 1
            c.SwaggerDoc("v1", new()
            {
                Version = "v1",
                Title = "DesignCrowd Api",
                Description = "This API it is used to calculate weekdays between dates",
                Contact = new() { Name = "Rafael Santos", Email = "rafaeldias.a@hotmail.com" }
            });

            // Set the path for including XML comments in Swagger documentation
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);

            // Add default values in the request
            c.SchemaFilter<SwaggerTryItOutDefaulValue>();
        });

        return services;
    }

    /// <summary>
    /// Configures the application pipeline to use Swagger for API documentation.
    /// </summary>
    /// <param name="app">The IApplicationBuilder to configure.</param>
    /// <returns>The configured IApplicationBuilder.</returns>
    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        // Enable Swagger middleware for generating API documentation
        app.UseSwagger();

        // Enable Swagger UI for interactive exploration of the API
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });

        return app;
    }
}
