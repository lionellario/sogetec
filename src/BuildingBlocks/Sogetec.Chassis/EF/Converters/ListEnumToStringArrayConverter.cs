namespace Sogetec.Chassis.EF.Converters;

public class ListEnumToStringConverter<TEnum> : ValueConverter<TEnum[], string> where TEnum : struct, Enum
{
    private const char Delimiter = ',';

    public ListEnumToStringConverter()
        : base(
           convertToProviderExpression: list => ConvertToString(list),
           convertFromProviderExpression: value => ConvertFromString(value)
        )
    { }

    private static string ConvertToString(TEnum[] list)
    {
        if (list == null || list.Length == 0)
            return string.Empty;

        return string.Join(Delimiter, list.Select(e => e.ToString()));
    }

    private static TEnum[] ConvertFromString(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return [];

        return [.. value
            .Split(Delimiter, StringSplitOptions.RemoveEmptyEntries)
            .Select(s => Enum.Parse<TEnum>(s.Trim(), ignoreCase: true))];
    }
}
