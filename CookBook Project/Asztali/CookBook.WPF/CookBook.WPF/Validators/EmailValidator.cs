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
    public class EmailValidator : ValidationRule
    {
            public static bool RegexEmailCheck(string input)
            {
                // returns true if the input is a valid email
                return Regex.IsMatch(input, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                try
                {
                     string? valueString=value.ToString();
                    bool validEmail = RegexEmailCheck(valueString);
                    var user=valueString.Split('@');
                    if (user[1].Contains("."))
                    {
                        user = user[1].Split(".");
                        if (user[0].Contains("mail"))
                        {
                            if (user[1] == "com" || user[1] == "hu")     return ValidationResult.ValidResult;
                            else return new ValidationResult(false, "A mező csak .hu vagy .com-ot tartalmazhat ");
                        }
                        else return new ValidationResult(false, "A megadott emailcím nem valid.");
                    }
                }
                catch(Exception)
                {
                 return new ValidationResult(false, "A megadott emailcím nem valid.");
                }
            }
            return new ValidationResult(false, "A mező csak betűket tartalmazhat.");
        }
    }
}
