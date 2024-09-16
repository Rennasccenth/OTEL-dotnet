namespace Nullnes.Mediation.Configuration;

internal sealed class FluentValidationConfiguration
{
    internal bool IsEnabled { get; private set; } = true;
    internal bool EnsureRequestHasValidator { get; private set; } = false;

    internal void EnableValidation()
    {
        IsEnabled = true;
    }

    internal void DisableValidation()
    {
        IsEnabled = false;
    }

    internal void RequireRequestValidator()
    {
        EnsureRequestHasValidator = true;
    }
}