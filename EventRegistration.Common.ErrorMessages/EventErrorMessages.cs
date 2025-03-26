namespace EventRegistration.Common.ErrorMessages
{
    public static class EventErrorMessages
    {
        public const string EventNotFound =
            "Event with such Id does not exist.";

        public const string PropertyEmpty =
            "{0} cannot be empty.";

        public const string PropertyTooLong =
            "{0} length cannot exceed 100 characters.";

        public const string DateNull =
            "{0} cannot be empty.";

        public const string EndDateBeforeStartDate =
            "End date cannot be sooner than start date.";

        public const string TimeOutsideWorkHours =
            "Invalid work hours, acceptable are between 08:00 and 22:59";

        public const string PhoneNumberOutOfRange =
            "Phone number must be between 7 and 15 characters.";

        public const string ValueNegative =
            "{0} must be greater than 0.";

        public const string MoneyInAdvanceExceedsLimit =
            "Money in advance must not exceed 10000.";

        public const string WrongFloor =
            "Floor cannot be different than 1 or 2.";

        public const string SomethingWentWrongAdding =
            "Something went wrong when adding the event. Please try again.";

        public const string SomethingWentWrongEditing =
            "Something went wrong when editing the event. Please try again.";

        public const string SomethingWentWrongDeleting =
            "Something went wrong when deleting the event. Please try again.";

        public const string ChangesHaventBeenMade =
            "Changes have not been made to the event.";
    }
}
