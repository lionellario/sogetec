namespace Sogetec.Chassis.Extensions;

public static class FluentValidationExtensions
{
    /// <summary>
    /// Specifies a custom error code to use if validation fails.
    /// </summary>
    /// <param name="rule">The current rule</param>
    /// <param name="errorCode">The error code to use</param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, TProperty> WithErrorCode<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        Enum errorCode) => rule.WithErrorCode(errorCode.ToDisplayString());
}
