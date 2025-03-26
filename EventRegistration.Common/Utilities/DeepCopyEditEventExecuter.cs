using EventRegistration.Data.Models;

namespace EventRegistration.Common.Utilities
{
    public static class DeepCopyEditEventExecuter
    {
        public static Event DeepCopy(Event entity)
        {
            return new Event()
            {
                Id = entity.Id,
                ClientName = entity.ClientName,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Decorations = entity.Decorations,
                EventType = entity.EventType,
                PhoneNumber = entity.PhoneNumber,
                ChildrenMenu = entity.ChildrenMenu,
                AdultsMenu = entity.AdultsMenu,
                MoneyInAdvance = entity.MoneyInAdvance,
                Floor = entity.Floor,
            };
        }
    }
}
