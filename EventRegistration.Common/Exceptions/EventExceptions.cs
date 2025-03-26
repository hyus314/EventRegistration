namespace EventRegistration.Common.Exceptions
{
    using static ErrorMessages.EventErrorMessages;
    public static class EventExceptions
    {
        public class EventNotFoundException()
            : Exception(EventNotFound)
        { }
        public class PropertyEmptyException(string property)
            : Exception(string.Format(PropertyEmpty, property))
        { }
        public class PropertyNameTooLongException(string property)
            : Exception(string.Format(PropertyTooLong, property))
        { }
        public class EndDateSoonerThanStartDateException()
            : Exception(EndDateBeforeStartDate)
        { }
        public class InvalidWorkHoursException()
            : Exception(TimeOutsideWorkHours)
        { }
        public class PhoneNumberOutOfRangeException()
            : Exception(PhoneNumberOutOfRange)
        { }
        public class ValueNegativeException(string type)
            : Exception(string.Format(ValueNegative, type))
        { }
        public class ValueOverLimitException()
            : Exception(MoneyInAdvanceExceedsLimit)
        { }
        public class WrongFloorException()
            : Exception(WrongFloor)
        { }
        public class SomethingWentWrongException(string message)
            : Exception(message)
        { }
        public class ChangesHaventBeenMadeEditingException()
            : Exception(ChangesHaventBeenMade)
        { }
    }
}
