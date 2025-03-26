namespace EventRegistration.Common.Exceptions
{
    using static ErrorMessages.AdminErrorMessages;
    public static class AdminExceptions
    {
        public class AdminIdDoesNotExistException(string adminId) 
            : Exception(string.Format(AdminIdDoesNotExist, adminId))
        { }

        public class AdminRoleDoesNotExistException() 
            : Exception(AdminRoleDoesNotExist)
        { }

        public class UserNotAdminException(string id) 
            : Exception(string.Format(UserNotAdmin, id))
        { }

        public class AdminPasswordWrongException()
            : Exception(AdminPasswordWrong)
        { }
    }
}
