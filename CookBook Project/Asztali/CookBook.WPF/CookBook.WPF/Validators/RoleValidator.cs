using System.Globalization;
using System.Windows.Controls;

namespace CookBook.WPF.Validators
{
    public class RoleValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString() =="User") return ValidationResult.ValidResult;
            else if (value.ToString() == "Editor")  return ValidationResult.ValidResult;
            else if (value.ToString() == "Admin")  return ValidationResult.ValidResult;
            else return new ValidationResult(false, "A megengedett szerepkörök:1.Admin,2.Editor,3.User");
        }
    }
}
