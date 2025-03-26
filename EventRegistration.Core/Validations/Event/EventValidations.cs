using EventRegistration.Data.ViewModels.Events;
using static EventRegistration.Common.Exceptions.EventExceptions;

namespace EventRegistration.Core.Validations.Event
{
    internal static class EventValidations
    {
        internal static void ValidateEvent<T>(T model) where T : IEventDTO
        {
            if (string.IsNullOrEmpty(model.ClientName))
                throw new PropertyEmptyException("Client name");

            if (model.ClientName.Length > 100)
                throw new PropertyNameTooLongException("Client name");

            if (model is AddEventDTO addEvent && addEvent.EndDate < addEvent.StartDate)
                throw new EndDateSoonerThanStartDateException();

            if (model is EditEventDTO editEvent)
            {
                if (!TimeSpan.TryParse(editEvent.StartDateValue, out TimeSpan startTime) ||
                    !TimeSpan.TryParse(editEvent.EndDateValue, out TimeSpan endTime))
                {
                    throw new FormatException("Invalid time format. Expected HH:MM.");
                }

                if (endTime <= startTime)
                    throw new EndDateSoonerThanStartDateException();
            }

            if (string.IsNullOrEmpty(model.EventType))
                throw new PropertyEmptyException("Event type");

            if (model.EventType.Length > 100)
                throw new PropertyNameTooLongException("Event type");

            if (model.PhoneNumber.Length < 7 || model.PhoneNumber.Length > 15)
                throw new PhoneNumberOutOfRangeException();

            if (string.IsNullOrEmpty(model.AdultsMenu))
                throw new ValueNegativeException("Adult count");

            if (string.IsNullOrEmpty(model.ChildrenMenu))
                throw new ValueNegativeException("Children count");

            if (model.AdultsMenu.Length > 100)
                throw new PropertyNameTooLongException("Adult count");

            if (model.ChildrenMenu.Length > 100)
                throw new PropertyNameTooLongException("Children count");

            if (model.MoneyInAdvance < 0)
                throw new ValueNegativeException("Money in advance");

            if (model.MoneyInAdvance >= 10000)
                throw new ValueOverLimitException();

            if (model.Floor != 1 && model.Floor != 2)
                throw new WrongFloorException();
        }

    }
}
