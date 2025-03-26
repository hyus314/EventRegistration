using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EventRegistration.Core.Validations.Worker
{
    using EventRegistration.Data.ViewModels.Workers;
    using static EventRegistration.Common.ErrorMessages.WorkerMessages;
    using static EventRegistration.Common.Utilities.DTOValidations.WorkerValidationValues;
    using static EventRegistration.Common.Utilities.CustomAttributes;
    using System.Text.RegularExpressions;

    internal static class WorkerValidation
    {
        internal static List<string> AddWorkerValidations(AddWorkerDTO model)
        {
            var errors = new List<string>();
            string pattern;
            // Validate Email
            if (string.IsNullOrWhiteSpace(model.Email))
            {
                errors.Add(EmailAddressRequired);
            }
            else
            {
                if (model.Email.Length > MaximumEmailAddressLength)
                {
                    errors.Add(EmailAddressTooLong);
                }
                if (!IsValidEmail(model.Email))
                {
                    errors.Add(EmailAddressInvalid);
                }
            }

            // Validate Username
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                errors.Add(UsernameRequired);
            }
            else
            {
                pattern = @"^[a-zA-Z]+$";
                if (!Regex.IsMatch(model.Username, pattern))
                {
                    errors.Add(UsernameDoesNotContainOnlyLetters);
                }
                if (model.Username.Length < MinimumUsernameLength)
                {
                    errors.Add(UsernameTooShort);
                }
                if (model.Username.Length > MaximumUsernameLength)
                {
                    errors.Add(UsernameTooLong);
                }
            }

            // Validate Password
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                errors.Add(PasswordRequired);
            }
            else
            {
                if (model.Password.Length < MinimumPasswordLength)
                {
                    errors.Add(PasswordTooShort);
                }
                if (model.Password.Length > MaximumPasswordLength)
                {
                    errors.Add(PasswordTooLong);
                }
                if (!model.Password.Any(char.IsDigit))
                {
                    errors.Add(PasswordDoesNotContainDigit);
                }
            }

            return errors;
        }
        internal static List<string> EditWorkerNameValidations(string username)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrWhiteSpace(username))
            {
                errors.Add(UsernameRequired);
            }
            else
            {
                string pattern = @"^[a-zA-Z]+$";
                if (!Regex.IsMatch(username, pattern))
                {
                    errors.Add(UsernameDoesNotContainOnlyLetters);
                }
                if (username.Length < MinimumUsernameLength)
                {
                    errors.Add(UsernameTooShort);
                }
                if (username.Length > MaximumUsernameLength)
                {
                    errors.Add(UsernameTooLong);
                }
            }

            return errors;
        }
        internal static List<string> EditWorkerEmailValidations(string email)
        {
            var errors = new List<string>();
            // Validate Email
            if (string.IsNullOrWhiteSpace(email))
            {
                errors.Add(EmailAddressRequired);
            }
            else
            {
                if (email.Length > MaximumEmailAddressLength)
                {
                    errors.Add(EmailAddressTooLong);
                }
                if (!IsValidEmail(email))
                {
                    errors.Add(EmailAddressInvalid);
                }
            }

            return errors;
        }

        // Helper Method to Validate Email Format
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
