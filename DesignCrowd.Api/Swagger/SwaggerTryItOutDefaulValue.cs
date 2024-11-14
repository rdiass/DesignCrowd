using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace DesignCrowd.Api.Swagger;

/// <summary>
/// Swagger TryItOut functionality.
/// </summary>
public class SwaggerTryItOutDefaulValue : ISchemaFilter
{
    /// <summary>
    /// This method is called by the Swagger generator for each parameter in the API.
    /// </summary>
    /// <param name="schema">The OpenApiSchema object representing the parameter.</param>
    /// <param name="context">The SchemaFilterContext object providing information about the current filtering context.</param>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.ParameterInfo != null)
        {
            var att = context.ParameterInfo.GetCustomAttribute<SwaggerTryItOutDefaulValueAttribute>();
            if (att != null)
            {
                schema.Example = new Microsoft.OpenApi.Any.OpenApiString(att.Value.ToString());
            }
        }
    }
}
