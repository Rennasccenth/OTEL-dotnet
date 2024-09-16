using System.ComponentModel.DataAnnotations;

namespace Nullnes.Documentation;

public sealed class OpenApiDocumentation
{
    public const string SectionName = nameof(OpenApiDocumentation);

    [Required] public string Title { get; private set; } = string.Empty;

    [Required] public string Description { get; private set; } = string.Empty;

    public OpenApiDocumentation WithTitle(string title)
    {
        Title = title;
        return this;
    }
    
    public OpenApiDocumentation WithDescription(string description)
    {
        Description = description;
        return this;
    }
}