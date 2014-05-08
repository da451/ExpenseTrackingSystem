using System.Windows.Controls;

namespace ExpenseTrackingSystem.Validation
{
    public class NotEmptyValidationRule :ValidationRule
    {
        public override ValidationResult Validate
            (object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value as string))
                return new ValidationResult(false, "value cannot be empty.");

            return ValidationResult.ValidResult;
        }
    }
}
