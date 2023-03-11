using System.Net;
using ZaylandShop.ServiceTemplate.Abstractions.HttpClients;
using ZaylandShop.ServiceTemplate.Helpers;
using ZaylandShop.ServiceTemplate.Integration.Test.Invariants;
using ZaylandShop.ServiceTemplate.Integration.Test.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace ZaylandShop.ServiceTemplate.Integration.Test.Extensions;

public static class RegisterTestDependenciesExtension
{
    public static IServiceCollection AddTestHttpApiClient(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        Guard.NotNull(services, nameof(services));
        Guard.NotNull(configuration, nameof(configuration));

        return services
            .RegisterTestApiClient()
            .RegisterTestHttpApiClientOptions(
                configuration,
                RegisterTestHttpApiClientInvariants.OptionsSectionPath);
    }

    private static IServiceCollection RegisterTestHttpApiClientOptions(
        this IServiceCollection services,
        IConfiguration configuration,
        string optionsSectionPath)
    {
        var options = configuration
            .GetSection(optionsSectionPath)
            .Get<TestHttpApiClientOptions>();
        
        if (options is null)
        {
            throw new InvalidOperationException(
                RegisterTestHttpApiClientInvariants.OptionsSectionNotDefined);
        }
        
        options.Validate();

        return services.AddSingleton(options);
    }

    private static IServiceCollection RegisterTestApiClient(
        this IServiceCollection services)
    {
        var retryPolicy = CreateRetryPolicy(services).Result;
        services
            .AddHttpClient<ITestHttpApiClient, TestHttpApiClient>((serviceProvider, httpClient) => 
            {
                var options = serviceProvider.GetService<TestHttpApiClientOptions>();
                httpClient.BaseAddress = new Uri(options.BaseUrl);
            })
            .AddPolicyHandler(retryPolicy);

        return services;
    }
    
    private async static Task<AsyncRetryPolicy<HttpResponseMessage>> CreateRetryPolicy(IServiceCollection services)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(response => response.StatusCode == HttpStatusCode.Forbidden)
            .WaitAndRetryAsync(new[]
            {
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(3),
                TimeSpan.FromSeconds(3)
            }, async (exception, timeSpan, retryCount, context) =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var logger = serviceProvider.GetService<ILogger<TestHttpApiClient>>();
                logger!.LogWarning(
                    $"{nameof(TestHttpApiClient)}: " +
                    $"Retry {retryCount} of {context.PolicyKey} at {timeSpan.TotalSeconds} seconds due to: " +
                    $"{exception?.Exception?.Message ?? await exception?.Result?.Content.ReadAsStringAsync()}");
            });
    }
}