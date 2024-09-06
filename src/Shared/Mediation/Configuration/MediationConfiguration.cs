using System.Reflection;

namespace Mediation.Configuration;

public sealed class MediationConfiguration
{
    internal FluentValidationConfiguration FluentValidation { get; } = new();
    internal Assembly[] Assemblies { get; private set; } = [];
    
    public MediationConfiguration EnableValidation()
    {
        FluentValidation.EnableValidation();
        return this;
    }

    public MediationConfiguration DisableValidation()
    {
        FluentValidation.DisableValidation();
        return this;
    }

    public MediationConfiguration RequireRequestValidator()
    {
        FluentValidation.RequireRequestValidator();
        return this;
    }

    public MediationConfiguration UsingAssembly(Assembly assembly)
    {
        Assemblies = [assembly];
        return this;
    }

    public MediationConfiguration UsingAssemblies(params Assembly[] assemblies)
    {
        Assemblies = assemblies;
        return this;
    }
}
