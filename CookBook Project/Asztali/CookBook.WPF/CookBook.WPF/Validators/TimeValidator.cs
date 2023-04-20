using System.Globalization;
using System.Windows.Controls;

namespace CookBook.WPF.Validators
{
    public class TimeValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                string? valueText = value.ToString();
                if (valueText.Contains(':')) return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, "A mező helyes megadása HH:MM:SS .");
        }
    }
}
