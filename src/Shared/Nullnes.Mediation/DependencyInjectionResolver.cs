using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nullnes.Mediation.Configuration;
using Nullnes.Mediation.PipelineBehaviours;

namespace Nullnes.Mediation;

public static class DependencyInjectionResolver
{
    /// <summary>
    /// Configures the Mediation package.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> to be used.</param>
    /// <param name="configurationAction"></param>
    public static IServiceCollection AddMediation(this IServiceCollection services, Action<MediationConfiguration> configurationAction)
    {
        MediationConfiguration configuration = new();

        configurationAction.Invoke(configuration);
        
        return services.AddMediation(configuration);
    }

    private static IServiceCollection AddMediation(this IServiceCollection services, MediationConfiguration mediationConfiguration)
    {
        services.AddSingleton<IOptions<MediationConfiguration>>(new OptionsWrapper<MediationConfiguration>(mediationConfiguration));

        services.AddMediatR(mediatRConfiguration =>
        {
            if (mediationConfiguration.FluentValidation.IsEnabled)
            {
                mediatRConfiguration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
                services.AddValidatorsFromAssemblies(mediationConfiguration.Assemblies, includeInternalTypes: true);
            }

            mediatRConfiguration.RegisterServicesFromAssemblies(mediationConfiguration.Assemblies);
        });

        return services;
    }
}