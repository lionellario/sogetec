namespace Sogetec.Chassis.EF.Converters;

public sealed class NullableObjectToNonNullJsonB<T> : ValueConverter<T?, string>
    where T : class, new()
{
    public NullableObjectToNonNullJsonB() : base(
        v => v == null ? "{}" : JsonSerializer.Serialize(v),
        v => string.IsNullOrEmpty(v) || v == "{}" ? null : JsonSerializer.Deserialize<T>(v))
    {
    }
}
