namespace Sogetec.Constants.Core;

public static class Authorize
{
    public static class Roles
    {
        public const string Admin = "administrator";
        public const string User = "user";
    }

    public static class Policies
    {
        public const string Admin = Roles.Admin;
        public const string User = Roles.User;
    }

    public static class Actions
    {
        public const string Read = "read";
        public const string Write = "write";
    }
}
