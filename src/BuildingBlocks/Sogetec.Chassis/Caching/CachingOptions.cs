using System.ComponentModel.DataAnnotations;

namespace Sogetec.Chassis.Caching;

[OptionsValidator]
public sealed partial class CachingOptions : IValidateOptions<CachingOptions>
{
    public const string ConfigurationSection = "Caching";

    [Required]
    [Range(1, int.MaxValue)]
    public int MaximumPayloadBytes { get; set; }

    [Required]
    public TimeSpan Expiration { get; set; }
}
