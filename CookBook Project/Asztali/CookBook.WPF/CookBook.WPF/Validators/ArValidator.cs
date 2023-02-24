﻿using System.Globalization;
using System.Windows.Controls;

namespace CookBook.WPF.Validators
{
    public class ArValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                decimal.TryParse(value.ToString(), out decimal number);
                if (number > 0)
                {
                    return ValidationResult.ValidResult;
                }
            }
            return new ValidationResult(false, "A mező csak 0-nál nagoybb számot tartalmazhat.");
        }
    }
}
