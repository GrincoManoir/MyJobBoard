using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyJobBoard.API.Web.Business.SwaggerSchemaFilters
{
    public class DateFieldSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(DateTime))
            {
                schema.Example = new OpenApiString(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            }
            
        }
    }
}
