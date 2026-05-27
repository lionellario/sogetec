namespace Sogetec.Chassis.Utilities;

public static class IntegerExtensions
{
    extension(int value)
    {
        public bool IsBetween(int min, int max, bool inclusive = true) => inclusive ? value >= min && value <= max : value > min && value < max;
    }
}
