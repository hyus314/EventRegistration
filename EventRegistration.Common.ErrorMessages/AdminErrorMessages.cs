namespace EventRegistration.Common.ErrorMessages
{
    public static class AdminErrorMessages
    {
        public const string AdminIdDoesNotExist =
            "Admin with Id {0} does not exist.";

        public const string AdminRoleDoesNotExist =
            "Role with Admin name does not exist.";

        public const string UserNotAdmin =
            "User with Id {0} is not an admin.";

        public const string AdminPasswordWrong =
            "The entered password is wrong for the admin.";

       
    }

}
