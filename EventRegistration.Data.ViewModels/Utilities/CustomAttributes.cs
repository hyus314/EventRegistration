namespace EventRegistration.Common.Utilities
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public static class CustomAttributes
    {
        public class PasswordContainsDigitAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
            {
                if (value is string strValue)
                {
                    string pattern = @"\d";
                    // Check if the string contains at least one digit
                    if (Regex.IsMatch(strValue, pattern))
                    {
                        return ValidationResult.Success!;
                    }
                }

                return new ValidationResult(ErrorMessage ?? "The field must contain at least one digit.");
            }
        }

        public class UsernameContainsOnlyLettersAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
            {
                if (value is string strValue)
                {
                    string pattern = @"^[a-zA-Z]+$";
                    // Check if the string contains only letters
                    if (Regex.IsMatch(strValue, pattern))
                    {
                        return ValidationResult.Success!;
                    }
                }

                return new ValidationResult(ErrorMessage ?? "The field must contain only letters.");
            }
        }
    }
}
