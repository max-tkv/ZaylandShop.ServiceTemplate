using AutoMapper;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using ZaylandShop.ServiceTemplate.Controllers;
using ZaylandShop.ServiceTemplate.Controllers.Mappings;
using ZaylandShop.ServiceTemplate.Integration.Test.Extensions;
using ZaylandShop.ServiceTemplate.Storage;
using ZaylandShop.ServiceTemplate.Web.Configuration;
using ZaylandShop.ServiceTemplate.Web.Configuration.Swagger;

namespace ZaylandShop.ServiceTemplate.Web;

public class Startup
{
    private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfiles(new Profile[]
                {
                    new TestControllerMappingProfile()
                });
            }).CreateMapper());
            
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddSqlStorage(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("Db") ?? "");
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            services.AddDomain();
            services.AddHostedServices();
            services.AddConfig(_configuration);
            
            services.AddTestHttpApiClient(_configuration);

            services.AddMvc()
                .AddApi()
                .AddValidators()
                .AddControllersAsServices()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter(new CamelCaseNamingStrategy(),
                        false));
                });
            
            services.AddSwagger(_configuration);
            services.Configure<AppSwaggerOptions>(_configuration);
            
            var serviceProvider = services.BuildServiceProvider();
            ServiceLocator.SetProvider(serviceProvider);
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            IServiceProvider serviceProvider, 
            ILogger<Startup> logger, 
            IHostApplicationLifetime lifetime, 
            IOptions<AppSwaggerOptions> swaggerOptions,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            MigrationsRunner.ApplyMigrations(logger, serviceProvider, "ZaylandShop.ServiceTemplate.Web").Wait();
            RegisterLifetimeLogging(lifetime, logger);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (swaggerOptions.Value.UseSwagger)
            {
                app.UseSwaggerWithVersion(apiVersionDescriptionProvider);
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        
        private static void RegisterLifetimeLogging(IHostApplicationLifetime lifetime, ILogger<Startup> logger)
        {
            lifetime.ApplicationStarted.Register(() => logger.LogInformation("App started"));
            lifetime.ApplicationStopped.Register(() => logger.LogInformation("App stopped"));
        }
}