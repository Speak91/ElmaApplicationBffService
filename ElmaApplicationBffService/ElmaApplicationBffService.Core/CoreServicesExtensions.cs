using AutoMapper;
using ElmaApplicationBffService.Core.Infrastructure;
using ElmaApplicationBffService.Core.Options;
using ElmaApplicationBffService.Core.RestClients;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace ElmaApplicationBffService.Core;

public static class CoreServicesExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        // MediatR requests registration
        services.AddMediatR(typeof(CoreServicesExtensions).Assembly);
        services.AddTransient<AuthHeaderHandler>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        // Automapper Configuration
        services.AddSingleton(new MapperConfiguration(cfg =>
            cfg.AddMaps(typeof(CoreServicesExtensions).Assembly)
        ).CreateMapper());

        return services;
    }

    public static IServiceCollection AddNamedCorsPolicies(
        this IServiceCollection services, IConfiguration configuration)
    {
        var appOptions = configuration.GetSection("App").Get<AppOptions>();
        services.AddCors(options =>
        {
            if (appOptions.CorsPolicies == null)
            {
                return;
            }
            foreach (var (name, policy) in appOptions.CorsPolicies)
            {
                options.AddPolicy(name, policy);
            }
        });
        return services;
    }

    public static IServiceCollection AddRestClients(
    this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureRefitClient<IApplicationRestClient>(services, configuration, "Elma:BaseAddress");
        return services;
    }

    private static void ConfigureRefitClient<T>(
        IServiceCollection services, IConfiguration configuration, string section) where T : class
    {
        services.AddRefitClient<T>(GetRefitSettings())
            .ConfigureHttpClient(client =>
                client.BaseAddress = new Uri(configuration.GetSection(section).Get<string>()))
            .AddHttpMessageHandler<AuthHeaderHandler>().ConfigurePrimaryHttpMessageHandler(_ =>
            new HttpClientHandler { ServerCertificateCustomValidationCallback = (_, _, _, _) => true });
    }

    private static RefitSettings GetRefitSettings()
    {
        return new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() }
            })
        };
    }
}
