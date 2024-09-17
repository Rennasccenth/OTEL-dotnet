using System.ComponentModel.DataAnnotations;

namespace Nullnes.Documentation;

public sealed class OpenApiDocumentation
{
    public const string SectionName = nameof(OpenApiDocumentation);

    [Required] public required string Title { get; init; } = string.Empty;

    [Required] public required string Description { get; init; } = string.Empty;
}