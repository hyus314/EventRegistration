using EventRegistration.Data.Models;
using EventRegistration.Data.ViewModels.Events;
using System.Globalization;

namespace EventRegistration.Common.Utilities
{
    public static class ChangeFinderForEmail
    {
        public static Dictionary<string, Tuple<string, string>> GetChanges(Event oldEvent, EditEventDTO newEvent)
        {
            CultureInfo bulgarianCulture = new CultureInfo("bg-BG");
            var changes = new Dictionary<string, Tuple<string, string>>();

            if (oldEvent.ClientName != newEvent.ClientName)
                changes.Add("Име на клиент", Tuple.Create(oldEvent.ClientName, newEvent.ClientName));
           
            var startTime = TimeSpan.Parse(newEvent.StartDateValue);
            var endTime = TimeSpan.Parse(newEvent.EndDateValue);

            DateTime newStartDate = newEvent.Date.Date.Add(startTime);
            DateTime newEndDate = newEvent.Date.Date.Add(endTime);

            if (oldEvent.StartDate.Date != newEvent.Date.Date)
                changes.Add("Дата",
                Tuple.Create(oldEvent.StartDate.Date.ToString("D", bulgarianCulture),
                newEvent.Date.Date.ToString("D", bulgarianCulture)));

            if ((oldEvent.StartDate.Hour != newStartDate.Hour) ||
                (oldEvent.StartDate.Minute != newStartDate.Minute))
                changes.Add("Начало",
                Tuple.Create(oldEvent.StartDate.ToString("t", bulgarianCulture),
                newStartDate.ToString("t", bulgarianCulture)));

            if ((oldEvent.EndDate.Hour != newEndDate.Hour) ||
                (oldEvent.EndDate.Minute != newEndDate.Minute))
                changes.Add("Край",
                Tuple.Create(oldEvent.EndDate.ToString("t", bulgarianCulture),
                newEndDate.ToString("t", bulgarianCulture)));

            if (oldEvent.Decorations != newEvent.Decorations)
                changes.Add("Украса", Tuple.Create(oldEvent.Decorations ?? " ", newEvent.Decorations ?? " "));

            if (oldEvent.EventType != newEvent.EventType)
                changes.Add("Тип събитие", Tuple.Create(oldEvent.EventType, newEvent.EventType));

            if (oldEvent.PhoneNumber != newEvent.PhoneNumber)
                changes.Add("Телефонен номер", Tuple.Create(oldEvent.PhoneNumber, newEvent.PhoneNumber));

            if (oldEvent.ChildrenMenu != newEvent.ChildrenMenu)
                changes.Add("Брой деца", Tuple.Create(oldEvent.ChildrenMenu.ToString(), newEvent.ChildrenMenu.ToString()));

            if (oldEvent.AdultsMenu != newEvent.AdultsMenu)
                changes.Add("Брой възрастни", Tuple.Create(oldEvent.AdultsMenu.ToString(), newEvent.AdultsMenu.ToString()));

            if (oldEvent.MoneyInAdvance != newEvent.MoneyInAdvance)
                changes.Add("Капаро", Tuple.Create(oldEvent.MoneyInAdvance.ToString(), newEvent.MoneyInAdvance.ToString()));

            if (oldEvent.Floor != newEvent.Floor)
                changes.Add("Етаж", Tuple.Create(oldEvent.Floor == 1 ? "Долу" : "Горе", newEvent.Floor == 1 ? "Долу" : "Горе"));

            return changes;
        }
    }
}
