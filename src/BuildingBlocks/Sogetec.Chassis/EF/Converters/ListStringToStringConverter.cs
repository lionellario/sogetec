namespace Sogetec.Chassis.EF.Converters;

public class ListStringToStringConverter : ValueConverter<string[], string>
{
    private const char Delimiter = ',';

    public ListStringToStringConverter()
        : base(
           convertToProviderExpression: list => ConvertToString(list),
           convertFromProviderExpression: value => ConvertFromString(value)
        )
    { }

    public static string ConvertToString(string[] list)
    {
        if (list == null || list.Length == 0)
            return string.Empty;

        return string.Join(Delimiter, list.Select(s => s.Trim()));
    }

    public static string[] ConvertFromString(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return [];

        return [.. value.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim())];
    }
}

public class ListStringToNullableStringConverter : ValueConverter<string[], string?>
{
    public ListStringToNullableStringConverter()
        : base(
           convertToProviderExpression: list => string.IsNullOrWhiteSpace(ListStringToStringConverter.ConvertToString(list))
                                                ? null
                                                : ListStringToStringConverter.ConvertToString(list),
           convertFromProviderExpression: value => ListStringToStringConverter.ConvertFromString(value)
        )
    { }
}
