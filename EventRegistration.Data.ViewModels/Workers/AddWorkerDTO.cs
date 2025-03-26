namespace EventRegistration.Data.ViewModels.Workers
{
    using System.ComponentModel.DataAnnotations;

    using static EventRegistration.Common.ErrorMessages.WorkerMessages;
    using static EventRegistration.Common.Utilities.DTOValidations.WorkerValidationValues;
    using static EventRegistration.Common.Utilities.CustomAttributes;

    public class AddWorkerDTO
    {
        [MaxLength(MaximumEmailAddressLength, ErrorMessage = EmailAddressTooLong)]
        [Required(ErrorMessage = EmailAddressRequired)]
        [EmailAddress(ErrorMessage = EmailAddressInvalid)]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = UsernameRequired)]
        [UsernameContainsOnlyLetters(ErrorMessage = UsernameDoesNotContainOnlyLetters)]
        [MinLength(MinimumUsernameLength, ErrorMessage = UsernameTooShort)]
        [MaxLength(MaximumUsernameLength, ErrorMessage = UsernameTooLong)]
        public string Username { get; set; } = null!;
        [PasswordContainsDigit(ErrorMessage = PasswordDoesNotContainDigit)]
        [Required(ErrorMessage = PasswordRequired)]
        [MinLength(MinimumPasswordLength, ErrorMessage = PasswordTooShort)]
        [MaxLength(MaximumPasswordLength, ErrorMessage = PasswordTooLong)]
        public string Password { get; set; } = null!;
    }
}
