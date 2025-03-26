namespace EventRegistration.Common.ErrorMessages
{
    public static class WorkerMessages
    {
        public const string EmailAddressInvalid =
            "Input is not an email address.";

        public const string EmailAddressRequired =
            "Please input an email address.";

        public const string EmailAddressTooLong =
            "Email address must not exceed 256 characters.";

        public const string UsernameRequired =
            "Please input a username.";

        public const string UsernameTooShort =
            "Username must be at least 3 characters long.";

        public const string UsernameTooLong =
            "Username must not exceed 256 characters long.";

        public const string UsernameDoesNotContainOnlyLetters =
            "Username must contain only letters.";

        public const string WorkerExists =
            "Worker with {0} {1} already exists.";

        public const string PasswordTooShort =
            "Password must be at least 3 characters long.";

        public const string PasswordTooLong =
            "Password must not exceed 256 characters.";

        public const string PasswordRequired =
            "Please input a password.";

        public const string PasswordDoesNotContainDigit =
            "Password must contain at least one digit.";

        public const string WorkerIdNotFound =
            "Worker with Id {0} does not exist.";

        public const string NewUsernameIsSameAsOldOne =
            "The new username cannot be the same as the old one.";

        public const string SomethingWentWrongRemovingWorker =
           "Something went wrong when removing that worker. Please try again.";
    }

}
