using System.Globalization;
using System.Windows.Controls;
using System.Net.Mail;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

using System.Windows.Media.Animation;


namespace CookBook.WPF.Validators
{
    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
             if (value != null && value.ToString().Length>30) return new ValidationResult(false, "A mező maximum 30 karaktert tartalmazhat!");
             else return ValidationResult.ValidResult;
        }

    }
}
