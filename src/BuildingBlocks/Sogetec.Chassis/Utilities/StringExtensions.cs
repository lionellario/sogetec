using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Sogetec.Chassis.Utilities;

public static class StringExtensions
{
    extension(string? value)
    {
        public bool In(string? str, StringComparison comparisonType = StringComparison.InvariantCultureIgnoreCase)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(value))
            {
                return false;
            }

            return str.Contains(value, comparisonType);
        }

        public bool IsValidEmail()
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            const string pattern = """^(?!\.)("([^"\r\\]|\\["\r\\])*"|""" +
                                   """([-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+(?:\.[-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+)*)|("([^"\r\\]|\\["\r\\])*"\.([-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+(?:\.[-a-zA-Z0-9!#$%&'*+/=?^_`{|}~]+)*)))@""" +
                                   """(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$""";

            if (!Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
            {
                return false;
            }

            try
            {
                var address = new MailAddress(value);
                return address.Address == value;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValidPhoneNumber()
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false; // Phone number cannot be null or empty
            }

            // Remove common formatting characters for consistency
            var number = Regex.Replace(value, @"[\s\-\(\)]+", "");

            // Define a regular expression for validating international and local phone numbers
            const string phonePattern = @"^(\+?[1-9]\d{0,2})?(\d{10,15})$";

            return Regex.IsMatch(number, phonePattern);
        }
    }
}
