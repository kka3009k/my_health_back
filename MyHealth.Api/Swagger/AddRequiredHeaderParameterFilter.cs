using Google.Apis.Discovery;
using Microsoft.OpenApi.Models;
using MyHealth.Api.Static;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MyHealth.Api.Swagger
{
    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            operation.Parameters.Add(new OpenApiParameter
            {
                Name = Constants.UserIdHeader,
                In = ParameterLocation.Header,
                Required = false,
                AllowEmptyValue = true,
                Description = "Код пользователя (не обязательный параметр)"
            });
        }
    }
}