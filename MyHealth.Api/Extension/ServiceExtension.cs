using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyHealth.Api.Service;
using MyHealth.Api.Static;
using MyHealth.Api.Swagger;
using MyHealth.Data;
using System.Text;

namespace MyHealth.Api.Extension;

/// <summary>
/// Настройка сервиса
/// </summary>
public static class ServiceExtension
{
    public static void ConfigureApi(this IServiceCollection services)
    {
        if (!Directory.Exists(Constants.FileStoragePath))
            Directory.CreateDirectory(Constants.FileStoragePath);

        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
                builder.SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
        });

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "MyHealth API",
                Version = "v1",
                Description = "Бэкэнд медицинского приложения",
                Contact = new OpenApiContact
                {
                    Name = "Каргин Константин",
                    Email = "kka3009k@icloud.com"
                }
            });
            var apiXmlPath = Path.Combine(AppContext.BaseDirectory, "MyHealth.Api.xml");
            var dataXmlPath = Path.Combine(AppContext.BaseDirectory, "MyHealth.Data.xml");
            c.IncludeXmlComments(apiXmlPath);
            c.IncludeXmlComments(dataXmlPath);
            c.SchemaFilter<EnumTypesSchemaFilter>(dataXmlPath);
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Введите JWT токен, пример 'Bearer jwt_token'",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            c.OperationFilter<AddRequiredHeaderParameter>();
        });
        services.AddHttpContextAccessor();
        services.AddScoped<UserContextService>();
        services.AddScoped<FileStorageService>();
        services.AddScoped<AuthService>();
        services.AddScoped<RegistrationService>();
        services.AddScoped<MailService>();
    }

    public static void ConfigureDbContext(this IServiceCollection services)
    {
        var connectionString =
            $"Server={Env.GetString("DB_HOST")};Port={Env.GetString("DB_PORT")};Database={Env.GetString("DB_DATABASE")};User ID={Env.GetString("DB_USERNAME")};Password={Env.GetString("DB_PASSWORD")};Include Error Detail=true";

        services.AddDbContext<MyDbContext>((provider, options) =>
        {
            options.UseNpgsql(connectionString);
        });
    }

    public static void ConfigureJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("SECRET_KEY"))),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        services.AddAuthorization();
    }
}