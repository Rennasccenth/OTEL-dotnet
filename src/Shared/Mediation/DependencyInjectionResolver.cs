using FluentValidation;
using Mediation.Configuration;
using Mediation.PipelineBehaviours;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Mediation;

public static class DependencyInjectionResolver
{
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
                services.AddValidatorsFromAssemblies(mediationConfiguration.Assemblies);
            }

            mediatRConfiguration.RegisterServicesFromAssemblies(mediationConfiguration.Assemblies);
        });

        return services;
    }
}