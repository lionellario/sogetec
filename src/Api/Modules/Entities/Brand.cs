using Sogetec.Chassis.Domain;

namespace Api.Modules.Entities;

public sealed class Brand : Entity
{
    public string Name { get; internal set; } = default!;
    public string LogoUrl { get; internal set; } = default!;

    public static Brand Create(
        string name,
        string logoUrl)
        => new()
        {
            Name = name,
            LogoUrl = logoUrl
        };

    public override bool Equals(object? obj) => obj is Brand at && Id == at.Id;

    public override int GetHashCode() => Id.GetHashCode();
}
