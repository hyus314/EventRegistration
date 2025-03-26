namespace EventRegistration.Common.Exceptions
{
    using static ErrorMessages.WorkerMessages;
    public static class WorkerExceptions
    {
        public class WorkerDoesNotExistException(string id)
            : Exception(string.Format(WorkerIdNotFound, id))
        { }

        public class WorkerExistsException(string type, string username)
            : Exception(string.Format(WorkerExists, type, username))
        { }

        public class ValidationsException(string message) 
            : Exception(message)
        { }

        public class SomethingWentWrongDeletingException()
            : Exception(SomethingWentWrongRemovingWorker)
        { }
    }
}
