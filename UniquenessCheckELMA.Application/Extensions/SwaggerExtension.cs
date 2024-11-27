using System.Reflection;

using Microsoft.OpenApi.Models;

using UniquenessCheckELMA.Application;

namespace UniquenessCheckELMA.Application;

public static partial class SwaggerExtension
{
    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(
        o =>
        {
            o.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Installment API",
                Description = "ASP.NET Тестовы веб-API для проверки уникальности",
                Contact = new OpenApiContact
                {
                    Name = "Contact",
                    Email = "skhsafarov@gmail.com",
                    Url = new Uri("https://t.me/sardorsafarovv")
                }
            });
            o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            o.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
            o.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        });
    }

    public static void UseSwagger(this WebApplication app)
    {
        app.UseSwagger(u =>
        {
            u.RouteTemplate = "swagger/{documentName}/swagger.json";
        });
    }
    public static void UseSwaggerUI(this WebApplication app)
    {
        app.UseSwaggerUI(c =>
        {
            c.RoutePrefix = "swagger";
            c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "Version 1.0");
        });
    }
}
