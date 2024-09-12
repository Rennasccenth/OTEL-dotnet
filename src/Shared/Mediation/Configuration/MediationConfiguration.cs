using System.Reflection;
using FluentValidation;
using MediatR;

namespace Mediation.Configuration;

/// <summary>
/// Wraps the configuration related to the package.
/// </summary>
public sealed class MediationConfiguration
{
    internal FluentValidationConfiguration FluentValidation { get; } = new();
    internal Assembly[] Assemblies { get; private set; } = [];
    
    /// <summary>
    /// Turn on the <see cref="IBaseRequest"/> Validation by using FluentValidation <see cref="AbstractValidator{T}"/>.
    /// </summary>
    public MediationConfiguration EnableValidation()
    {
        FluentValidation.EnableValidation();
        return this;
    }

    /// <summary>
    /// Turn off the <see cref="IBaseRequest"/> Validation by using FluentValidation <see cref="AbstractValidator{T}"/>.
    /// </summary>
    public MediationConfiguration DisableValidation()
    {
        FluentValidation.DisableValidation();
        return this;
    }

    /// <summary>
    /// Oblige every
    /// <see cref="IBaseRequest"/> to have a matching <see cref="AbstractValidator{T}"/> where T is <see cref="IBaseRequest"/>.  
    /// </summary>
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
        ArgumentNullException.ThrowIfNull(assemblies);
        if (assemblies.Length == 0)
        {
            throw new ArgumentException("At least one assembly must be provided.", nameof(assemblies));
        }

        Assemblies = assemblies;
        return this;
    }
}
