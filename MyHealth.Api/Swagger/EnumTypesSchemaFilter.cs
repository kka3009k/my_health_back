using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;
using System.Xml.Linq;

namespace MyHealth.Api.Swagger
{
    public class EnumTypesSchemaFilter : ISchemaFilter
    {
        private readonly XDocument _xmlComments;

        public EnumTypesSchemaFilter(string xmlPath)
        {
            if (File.Exists(xmlPath))
            {
                _xmlComments = XDocument.Load(xmlPath);
            }
        }

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (_xmlComments == null) return;

            if (schema.Enum != null && schema.Enum.Count > 0 &&
                context.Type != null && context.Type.IsEnum)
            {
                schema.Description += "<p>Содержит значения:</p><ul>";

                foreach (var enumMemberName in Enum.GetValues(context.Type))
                {
                    var memInfo = context.Type.GetMember(enumMemberName.ToString());

                    var description = memInfo[0].GetCustomAttribute<DescriptionAttribute>();

                    var enumMemberValue = Convert.ToInt64(enumMemberName);

                    schema.Description += $"<li><i>{enumMemberValue}</i> - {(description== null ? enumMemberName : description.Description.Trim())}</li>";
                }

                schema.Description += "</ul>";
            }
        }
    }
}
