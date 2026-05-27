namespace Sogetec.Chassis.Helpers;

public static class SerializationHelper
{
    public static string Serialize<T>(this T obj)
    {
        var result = JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return result;
    }
}
