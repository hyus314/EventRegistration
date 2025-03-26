namespace EventRegistration.Data.ViewModels.Workers
{
    using static EventRegistration.Common.Utilities.CustomAttributes;
    using System.ComponentModel.DataAnnotations;

    using static EventRegistration.Common.ErrorMessages.WorkerMessages;
    using static EventRegistration.Common.Utilities.DTOValidations.WorkerValidationValues;
    public class EditWorkerNameDTO
    {
        [Required(ErrorMessage = UsernameRequired)]
        [UsernameContainsOnlyLetters(ErrorMessage = UsernameDoesNotContainOnlyLetters)]
        [MinLength(MinimumUsernameLength, ErrorMessage = UsernameTooShort)]
        [MaxLength(MaximumUsernameLength, ErrorMessage = UsernameTooLong)]
        public string NewUsername { get; set; } = null!;
        public string WorkerId { get; set; } = null!;
    }
}
