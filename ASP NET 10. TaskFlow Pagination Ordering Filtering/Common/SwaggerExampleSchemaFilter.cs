using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace ASP_NET_10._TaskFlow_Pagination_Ordering_Filtering.Common
{
    public class SwaggerExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type is null) return;

            var properties = context.Type.GetProperties();

            foreach (var property in properties)
            {
                var defaultValueAttribute
                    = property.GetCustomAttribute<DefaultValueAttribute>();
                if (defaultValueAttribute is not null)
                {
                    var propertyName = $"{char.ToLowerInvariant(property.Name[0])}{property.Name.Substring(1)}";
                    if(schema.Properties is not null && schema.Properties.ContainsKey(propertyName))
                    {
                        var value = defaultValueAttribute.Value;
                        if (value is string str) 
                                schema.Properties[propertyName].Example = new OpenApiString(str);
                        else if (value is int intValue) 
                            schema.Properties[propertyName].Example = new OpenApiInteger(intValue);
                        else if (value is bool boolValue) 
                            schema.Properties[propertyName].Example = new OpenApiBoolean(boolValue);
                        else if (value is DateTime dateTime) 
                            schema.Properties[propertyName].Example = new OpenApiString(dateTime.ToString("O"));
                    }
                }
            }
        }
    }
}