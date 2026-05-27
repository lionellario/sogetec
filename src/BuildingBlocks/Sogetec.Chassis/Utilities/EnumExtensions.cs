namespace Sogetec.Chassis.Utilities;

public static class EnumExtensions
{
    extension(Enum obj)
    {
        public string ToDisplayString()
        {
            var enumType = obj.GetType();
            var memberName = Enum.GetName(enumType, obj) ?? obj.ToString();
            return $"{enumType.Name}.{memberName}";
        }
    }
}
