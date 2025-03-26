namespace EventRegistration.Data.ViewModels.Workers
{
    using System.ComponentModel.DataAnnotations;

    using static EventRegistration.Common.ErrorMessages.WorkerMessages;
    using static EventRegistration.Common.Utilities.DTOValidations.WorkerValidationValues;
    public class EditWorkerEmailDTO
    {
        [MaxLength(MaximumEmailAddressLength, ErrorMessage = EmailAddressTooLong)]
        [Required(ErrorMessage = EmailAddressRequired)]
        [EmailAddress(ErrorMessage = EmailAddressInvalid)]
        public string NewEmail { get; set; } = null!;
        public string WorkerId { get; set; } = null!;
    }
}
