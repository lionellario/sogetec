namespace Sogetec.Constants.Core;

public class GlobalConstant
{
    public class Tag
    {
        public const string AUTH = "AUTH";
        public const string TENANT_INFO = "AUTH:TENANTINFO";
        public const string USER_PERMISSIONS = "AUTH:PERMISSIONS";
        public const string USER_INFO = "AUTH:USERINFO";
    }

    public class CookieNameKey
    {
        public const string REFRESH = "Sogetec.session";
        public const string ACCESS = "Sogetec.token";
    }

    public class AntiforgeryNameKey
    {
        public const string AuthAntiForgeryCookieName = "Sogetec.csrf.token";
        public const string AuthAntiForgeryHeaderName = "X-Stud-CSRF-Token";
    }

    public class JWTKey
    {
        public const string ISSUER = "Sogetec.com";
        public static readonly string BEARER = "Identity.Bearer";
        public const string ID = "jti";
        public const string TENANT = "tid";
        public const string USER = "sub";
        public const string TZ = "tmz";
        public const string ROLE = "role";
        public const string LOCALE = "locale";
        public const string PERMISSION = "auth";
        public const string TYPE = "typ";
        public const string USER_NAME = "name";
        public const string EMAIL = "email";
    }

    public class Duration
    {
        public const int ACCESS_TOKEN_MINUTE = 30;
        public const int REFRESH_TOKEN_MINUTE = 60 * 24 * 1;
        public const int PERMISSION_CACHE_MINUTE = 10;
        public const int COOKIE_MINUTE = 60 * 24 * 1;
    }

    public class FieldValueProperty
    {
        public class Text
        {
            public const int L10 = 10;
            public const int L15 = 15;
            public const int SHORT = 100;
            public const int PATH = 200;
            public const int MEDIUM = 500;
            public const int LONG = 2_000;
            public const int PAGE = 5_000;
            public const int MAX = 15_000;
        }

        public class Decimal
        {
            public const decimal MAX_SIX_DIGITS_TWO_DECIMAL = 999_999.99m;
            public const decimal MAX_FOUR_DIGITS_TWO_DECIMAL = 9_999.99m;
            public const decimal MAX_THREE_DIGITS_TWO_DECIMAL = 999.99m;
        }
    }

    public class File
    {
        public IReadOnlyList<string> ALLOWED_FILE_EXTENSIONS =
        [
            ".pdf", ".docx", ".doc", ".csv", ".xls", ".xlsx", ".ppt", ".pptx", ".jpg", ".jpeg", ".gif", ".png"
        ];
    }

    public class HttpHeader
    {
        public const string TENANT_ID = "X-Stud-TenantId";
        public const string USER_ID = "X-Stud-UserId";
        public const string CORRELATION_ID = "X-Correlation-ID";
    }
}
