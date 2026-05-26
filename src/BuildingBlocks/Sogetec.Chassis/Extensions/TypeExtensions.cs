namespace Sogetec.Chassis.Extensions;

public static class TypeExtensions
{
    extension(Type type)
    {
        public bool IChildOf(Type parent) => parent.IsAssignableFrom(type);

        public string GetFriendlyName()
        {
            if (type.IsGenericType)
            {
                // Get generic arguments and recursively call GetFriendlyName
                var genericArgs = type.GetGenericArguments().Select(x => GetFriendlyName(x));

                // Format: TypeName<Arg1, Arg2>
                return
                    $"{type.Name[..type.Name.IndexOf('`')]}" +
                    $"<{string.Join(", ", genericArgs)}>";
            }
            return type.Name;
        }
    }
}
