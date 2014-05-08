using System.Windows.Controls;

namespace ExpenseTrackingSystem.Validation
{
    public class NoSpacesValidationRule :ValidationRule
    {
        public override ValidationResult Validate
            (object value, System.Globalization.CultureInfo cultureInfo)
        {
            string str = value as string;

            if (string.IsNullOrEmpty(str))
                return ValidationResult.ValidResult;

            if (str.Contains(" "))
                return new ValidationResult(false, "Can not contain spaces!");

            return ValidationResult.ValidResult;
        }
    }
}
