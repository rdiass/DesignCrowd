namespace DesignCrowd.Api.Swagger;

/// <summary>
/// This attribute allows you to specify a default value for parameters in the Swagger TryItOut functionality.
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class SwaggerTryItOutDefaulValueAttribute : Attribute
{
    /// <summary>
    /// The default value to be displayed in the Swagger TryItOut UI.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the `SwaggerTryItOutDefaulValueAttribute` class with the provided default value.
    /// </summary>
    /// <param name="value">The default value to be set.</param>
    public SwaggerTryItOutDefaulValueAttribute(string value)
    {
        Value = value;
    }
}