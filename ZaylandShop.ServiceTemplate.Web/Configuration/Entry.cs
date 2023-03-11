using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using ZaylandShop.ServiceTemplate.Controllers.Api.V1;
using ZaylandShop.ServiceTemplate.Web.Configuration.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ZaylandShop.ServiceTemplate.Web.Configuration;

public static class Entry
{
    public static void AddSwagger(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddApiVersioning();
        serviceCollection.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        serviceCollection.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        serviceCollection.AddSwaggerGen(options =>
        {
            {   //Добавляем документации для контроллеров
                var xmlFile = $"{Assembly.GetAssembly(typeof(TestController))?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            }
        });
        serviceCollection.AddSwaggerGenNewtonsoftSupport();
        serviceCollection.Configure<AppSwaggerOptions>(configuration);
    }

    public static void UseSwaggerWithVersion(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                c.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    $"{nameof(ServiceTemplate)} API {description.GroupName}");
            }
                    
            c.RoutePrefix = "swagger";
        });
    }
}