namespace Sogetec.Chassis.Utilities;

public static class DateTimeExtensions
{
    extension(DateTimeOffset value)
    {
        public bool IsBetween(DateTimeOffset start, DateTimeOffset end, bool inclusive = true)
        {
            return inclusive ? value >= start && value <= end : value > start && value < end;
        }
    }

    extension(DateOnly value)
    {
        public bool IsBetween(DateOnly start, DateOnly end, bool inclusive = true)
        {
            return inclusive ? value >= start && value <= end : value > start && value < end;
        }

        public bool IsLegalAge(int legalAge = 18)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - value.Year;

            // Adjust age if the user's birthday hasn't occurred yet this year
            if (value > today.AddYears(-age))
            {
                age--;
            }

            return age >= legalAge;
        }
    }
}
