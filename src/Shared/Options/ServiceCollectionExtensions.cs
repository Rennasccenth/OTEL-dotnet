using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Options;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterOption<T>(
        this IServiceCollection services,
        string sectionName,
        bool validateOnStart = true) where T : class
    {
        OptionsBuilder<T> optionsBuilder = services.AddOptions<T>()
            .BindConfiguration(sectionName);

        if (validateOnStart)
        {
            optionsBuilder.ValidateDataAnnotations()
                .ValidateOnStart();
        }
            
        return services;
    }

    public static IServiceCollection RegisterOption<T>(this IServiceCollection services, bool validateOnStart = true) where T : class 
        => services.RegisterOption<T>(sectionName: typeof(T).Name, validateOnStart);
}
