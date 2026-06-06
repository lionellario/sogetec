using Slugify;

namespace Sogetec.Chassis.Extensions;

public static class StringEntensions
{
    private static readonly SlugHelper Slug = new();

    extension(string raw)
    {
        public string Slugify()
        {
            if (string.IsNullOrWhiteSpace(raw))
            {
                return raw;
            }

            return Slug.GenerateSlug(raw);
        }
    }
}